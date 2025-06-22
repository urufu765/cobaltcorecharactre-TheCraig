using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Illeana.Artifacts;
using Illeana.Cards;
using Illeana.External;
using Illeana.Features;
using System.Reflection;
using Illeana.Dialogue;

namespace Illeana;

internal partial class ModEntry : SimpleMod
{

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
        Harmony = new Harmony("urufudoggo.Illeana");
        UniqueName = package.Manifest.UniqueName;
        modDialogueInited = false;
        /*
         * Some mods provide an API, which can be requested from the ModRegistry.
         * The following is an example of a required dependency - the code would have unexpected errors if Kokoro was not present.
         * Dependencies can (and should) be defined within the nickel.json file, to ensure proper load mod load order.
         */
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        MoreDifficultiesApi = helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties");
        DuoArtifactsApi = helper.ModRegistry.GetApi<IDuoArtifactsApi>("Shockah.DuoArtifacts");
        settings = helper.Storage.LoadJson<Settings>(SettingsFile);


        helper.Events.OnModLoadPhaseFinished += (_, phase) =>
        {
            if (phase == ModLoadPhase.AfterDbInit)
            {
                if (DuoArtifactsApi is not null)
                {
                    foreach (Type type in IlleanaDuoArtifacts)
                    {
                        DuoArtifactMeta? dam = type.GetCustomAttribute<DuoArtifactMeta>();
                        if (dam is not null)
                        {
                            if (dam.duoModDeck is null)
                            {
                                DuoArtifactsApi.RegisterDuoArtifact(type, [IlleanaDeck!.Deck, dam.duoDeck]);
                            }
                            else
                            {
                                try
                                {
                                    if (helper.Content.Decks.LookupByUniqueName(dam.duoModDeck) is IDeckEntry ide)
                                    {
                                        DuoArtifactsApi.RegisterDuoArtifact(type, [IlleanaDeck!.Deck, ide.Deck]);
                                    }
                                }
                                catch (Exception err)
                                {
                                    Logger.LogError(err, "FUCK couldn't register {DuoModDeck}", dam.duoModDeck);
                                }
                            }
                        }
                    }
                }

                localDB = new(helper, package);
            }
        };
        helper.Events.OnLoadStringsForLocale += (_, thing) =>
        {
            foreach (KeyValuePair<string, string> entry in localDB.GetLocalizationResults())
            {
                thing.Localizations[entry.Key] = entry.Value;
            }
        };

        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        /*
         * A deck only defines how cards should be grouped, for things such as codex sorting and Second Opinions.
         * A character must be defined with a deck to allow the cards to be obtainable as a character's cards.
         */
        IlleanaDeck = helper.Content.Decks.RegisterDeck("illeana", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                /*
                 * This color is used in a few places:
                 * TODO On cards, it dictates the sheen on higher rarities, as well as influences the color of the energy cost.
                 * If this deck is given to a playable character, their name will be this color, and their mini will have this color as their border.
                 */
                color = new Color("45e260"),

                titleColor = new Color("000000")
            },

            DefaultCardArt = StableSpr.cards_colorless,
            BorderSprite = RegisterSprite(package, "assets/frame_illeana.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "Illeana", "name"]).Localize,
            ShineColorOverride = _ => new Color("3045c0").addClarityBright(),
        });
        DecrepitCraigDeck = helper.Content.Decks.RegisterDeck("deadcraig", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                color = new Color("41b5ae"),

                titleColor = new Color("000000")
            },

            DefaultCardArt = StableSpr.cards_colorless,
            BorderSprite = RegisterSprite(package, "assets/frame_dead.png").Sprite,
            OverBordersSprite = RegisterSprite(package, "assets/frame_dead_overlay.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "DeadCraig", "name"]).Localize
        });
        IlleanaEnd = RegisterSprite(package, "assets/Memry/illeana_end.png").Sprite;
        IlloodleEnd = RegisterSprite(package, "assets/Memry/illoodle_end.png").Sprite;
        ShoeanaEnd = RegisterSprite(package, "assets/Memry/shoeana_end.png").Sprite;
        Vault.charsWithLore.Add(IlleanaDeck.Deck);
        BGRunWin.charFullBodySprites.Add(IlleanaDeck.Deck, IlleanaEnd);

        /*
         * Statuses are used to achieve many mechanics.
         * However, statuses themselves do not contain any code - they just keep track of how much you have.
         */
        TarnishStatus = helper.Content.Statuses.RegisterStatus("Tarnish", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = false,
                affectedByTimestop = true,
                color = new Color("a43fff"),
                icon = ModEntry.RegisterSprite(package, "assets/tarnish.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "Tarnish", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Tarnish", "desc"]).Localize
        });
        SnekTunezStatus = helper.Content.Statuses.RegisterStatus("SnekTunezStat", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = true,
                color = new Color("ffffff"),
                icon = ModEntry.RegisterSprite(package, "assets/snektunezstat.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "SnekTunezStat", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "SnekTunezStat", "desc"]).Localize
        });
        ExcessShardStatus = helper.Content.Statuses.RegisterStatus("ExessShards", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = false,
                color = DB.statuses[Status.shard].color,
                icon = StableSpr.icons_shard
            },
            Name = AnyLocalizations.Bind(["status", "ExcessShard", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "ExcessShard", "desc"]).Localize
        });


        KokoroApi.V2.ActionCosts.RegisterStatusResourceCostIcon(TarnishStatus.Status, RegisterSprite(package, "assets/tarnish_pay.png").Sprite, RegisterSprite(package, "assets/tarnish_cost.png").Sprite);
        KokoroApi.V2.ActionCosts.RegisterStatusResourceCostIcon(Status.corrode, RegisterSprite(package, "assets/corrode_pay.png").Sprite, RegisterSprite(package, "assets/corrode_cost.png").Sprite);

        /*
         * Characters have required animations, recommended animations, and you have the option to add more.
         * In addition, they must be registered before the character themselves is registered.
         * The game requires you to have a neutral animation and mini animation, used for normal gameplay and the map and run start screen, respectively.
         * The game uses the squint animation for the Extra-Planar Being and High-Pitched Static events, and the gameover animation while you are dying.
         * You may define any other animations, and they will only be used when explicitly referenced (such as dialogue).
         */
        foreach (KeyValuePair<int, List<string>> anims in IlleanaSnekAnims)
        {
            foreach (string anim in anims.Value)
            {
                RegisterAnimation(IlleanaDeck.Deck.Key(), package, anim, $"assets/Animation/illeana_{anim}", anims.Key);
            }
        }
        // foreach (string s1 in Illeana1Anims)
        // {
        //     RegisterAnimation(package, s1, $"assets/Animation/illeana_{s1}", 1);
        // }
        // foreach (string s3 in Illeana3Anims)
        // {
        //     RegisterAnimation(package, s3, $"assets/Animation/illeana_{s3}", 3);
        // }
        // foreach (string s4 in Illeana4Anims)
        // {
        //     RegisterAnimation(package, s4, $"assets/Animation/illeana_{s4}", 4);
        // }
        // foreach (string s5 in Illeana5Anims)
        // {
        //     RegisterAnimation(package, s5, $"assets/Animation/illeana_{s5}", 5);
        // }
        // foreach (string s6 in Illeana6Anims)
        // {
        //     RegisterAnimation(package, s6, $"assets/Animation/illeana_{s6}", 6);
        // }

        IlleanaTheSnek = helper.Content.Characters.V2.RegisterPlayableCharacter("illeana", new PlayableCharacterConfigurationV2
        {
            Deck = IlleanaDeck.Deck,
            BorderSprite = RegisterSprite(package, "assets/char_frame_illeana.png").Sprite,
            Starters = new StarterDeck
            {
                cards = [
                    new ScrapPatchkit(),
                    new Cleanse()
                ],
                /*
                 * Some characters have starting artifacts, in addition to starting cards.
                 * This is where they would be added, much like their starter cards.
                 * This can be safely removed if you have no starting artifacts.
                 */
                artifacts = [
                ]
            },
            Description = AnyLocalizations.Bind(["character", "Illeana", "desc"]).Localize,
            ExeCardType = typeof(IlleanaExe)
        });

        CraigTheSnek = helper.Content.Characters.V2.RegisterNonPlayableCharacter("craigsnek", new NonPlayableCharacterConfigurationV2
        {
            CharacterType = "craigsnek",
            Name = AnyLocalizations.Bind(["character", "Craig", "name"]).Localize,
            BorderSprite = RegisterSprite(package, "assets/char_frame_craig.png").Sprite,
        });

        LisardEXE = helper.Content.Characters.V2.RegisterNonPlayableCharacter("lisardexe", new NonPlayableCharacterConfigurationV2
        {
            CharacterType = "lisardexe",
            Name = AnyLocalizations.Bind(["character", "lisard", "name"]).Localize,
            BorderSprite = RegisterSprite(package, "assets/char_frame_lisard.png").Sprite,
        });
        foreach (KeyValuePair<int, List<string>> anims in CraigAnims)
        {
            foreach (string anim in anims.Value)
            {
                RegisterAnimation(CraigTheSnek.CharacterType, package, anim, $"assets/Animation/craig_{anim}", anims.Key);
            }
        }
        foreach (KeyValuePair<int, List<string>> anims in LisardAnims)
        {
            foreach (string anim in anims.Value)
            {
                RegisterAnimation(LisardEXE.CharacterType, package, anim, $"assets/Animation/lisard_{anim}", anims.Key);
            }
        }


        MoreDifficultiesApi?.RegisterAltStarters(IlleanaDeck.Deck, new StarterDeck
        {
            cards = [
                new Exposure(),
                new FalseVaccine()
            ],
            artifacts =
            [
                new LightenedLoad()
            ]
        });


        /*
         * Managers are typically made to register themselves when constructed.
         * _ = makes the compiler not complain about the fact that you are constructing something for seemingly no reason.
         */
        //_ = new KnowledgeManager();
        _ = new Tarnishing();

        /*
         * Some classes require so little management that a manager may not be worth writing.
         * In AGainPonder's case, it is simply a need for two sprites and evaluation of an artifact's effect.
         */
        //AGainPonder.DrawSpr = RegisterSprite(package, "assets/ponder_draw.png").Sprite;
        //AGainPonder.DiscardSpr = RegisterSprite(package, "assets/ponder_discard.png").Sprite;
        //AOverthink.Spr = RegisterSprite(package, "assets/overthink.png").Sprite;

        // Artifact Section
        foreach (Type ta in IlleanaArtifactTypes)
        {
            Deck deck = IlleanaDeck.Deck;
            if (IlleanaDuoArtifacts.Contains(ta))
            {
                if (DuoArtifactsApi is null)
                {
                    continue;
                }
                else
                {
                    deck = DuoArtifactsApi.DuoArtifactVanillaDeck;
                }
            }

            helper.Content.Artifacts.RegisterArtifact(ta.Name, UhDuhHundo.ArtifactRegistrationHelper(ta, RegisterSprite(package, "assets/Artifact/" + ta.Name + ".png").Sprite, deck));
        }

        SprStolenOn = RegisterSprite(package, "assets/Artifact/Personal_Stereo.png").Sprite;
        SprStolenChill = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Chill.png").Sprite;
        SprStolenHype = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Hype.png").Sprite;
        SprStolenSad = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Sad.png").Sprite;
        SprStolenGroovy = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Groovy.png").Sprite;
        SprSportsOn = RegisterSprite(package, "assets/Artifact/Sports_Stereo.png").Sprite;
        SprSportsChill = RegisterSprite(package, "assets/Artifact/Sports_Stereo_Chill.png").Sprite;
        SprSportsHype = RegisterSprite(package, "assets/Artifact/Sports_Stereo_Hype.png").Sprite;
        SprSportsSad = RegisterSprite(package, "assets/Artifact/Sports_Stereo_Sad.png").Sprite;
        SprSportsGroovy = RegisterSprite(package, "assets/Artifact/Sports_Stereo_Groovy.png").Sprite;
        SprDigitalOn = RegisterSprite(package, "assets/Artifact/Digitalized_Stereo.png").Sprite;
        SprDigitalChill = RegisterSprite(package, "assets/Artifact/Digitalized_Stereo_Chill.png").Sprite;
        SprDigitalHype = RegisterSprite(package, "assets/Artifact/Digitalized_Stereo_Hype.png").Sprite;
        SprDigitalSad = RegisterSprite(package, "assets/Artifact/Digitalized_Stereo_Sad.png").Sprite;
        SprDigitalGroovy = RegisterSprite(package, "assets/Artifact/Digitalized_Stereo_Groovy.png").Sprite;
        SprBoostrE = RegisterSprite(package, "assets/Artifact/TempoboostersE.png").Sprite;
        SprBoostrB = RegisterSprite(package, "assets/Artifact/TempoboostersB.png").Sprite;
        SprBoostrO = RegisterSprite(package, "assets/Artifact/TempoboostersO.png").Sprite;
        SprExLubeO = RegisterSprite(package, "assets/Artifact/ExperimentalLubricantO.png").Sprite;
        SprExLubeX = RegisterSprite(package, "assets/Artifact/ExperimentalLubricantX.png").Sprite;
        SprEFLavailable = RegisterSprite(package, "assets/Artifact/ExternalFuelSourceAvailable.png").Sprite;
        SprEFLdepleted = RegisterSprite(package, "assets/Artifact/ExternalFuelSourceDepleted.png").Sprite;
        SprThurstDepleted = RegisterSprite(package, "assets/Artifact/ThrustThurstersDepleted.png").Sprite;
        SprHullHarvestDepleted = RegisterSprite(package, "assets/Artifact/HullHarvesterDepleted.png").Sprite;
        SprAirlockShoe = RegisterSprite(package, "assets/Artifact/AirlockShoe.png").Sprite;
        SprCompetitionShoeana = RegisterSprite(package, "assets/Artifact/CompetitionShoeana.png").Sprite;
        SprCompetitionShoe = RegisterSprite(package, "assets/Artifact/CompetitionShoe.png").Sprite;
        SprCompetitionShoeDepleted = RegisterSprite(package, "assets/Artifact/CompetitionShoeDepeleted.png").Sprite;
        SprCompetitionIlleana = RegisterSprite(package, "assets/Artifact/CompetitionIlleana.png").Sprite;
        SprCompetitionEddie = RegisterSprite(package, "assets/Artifact/CompetitionEddie.png").Sprite;
        SprCompetitionDepleted = RegisterSprite(package, "assets/Artifact/CompetitionDepleted.png").Sprite;

        /*
         * All the IRegisterable types placed into the static lists at the start of the class are initialized here.
         * This snippet invokes all of them, allowing them to register themselves with the package and helper.
         */
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);

        //DrawLoadingScreenFixer.Apply(Harmony);
        //SashaSportingSession.Apply(Harmony);
        WarpPrototypeHelper.Apply(Harmony);
        ThrustMaster.Apply(Harmony);
        HeatpumpLubricator.Apply(Harmony);
        ShardStorageUnlimiter.Apply(Harmony);
        IlleanaClock.Apply(Harmony);
        SwapTheAnimation.Apply(Harmony);
        ReplaceSnakeBodyArt.Apply(Harmony);
        // SetXRenderer.Apply(Harmony);


        helper.ModRegistry.AwaitApi<IModSettingsApi>(
            "Nickel.ModSettings",
            api => api.RegisterModSettings(api.MakeList([
                api.MakeProfileSelector(
                    () => package.Manifest.DisplayName ?? package.Manifest.UniqueName,
                    settings.ProfileBased
                ),
                api.MakeCheckbox(
                    () => Localizations.Localize(["settings", "AntiSnakeMode", "name"]),
                    () => settings.ProfileBased.Current.AntiSnakeMode,
                    (_, _, value) => settings.ProfileBased.Current.AntiSnakeMode = value
                ).SetTooltips(() => [
                    new GlossaryTooltip("illeanasettings.antisnakemode")
                    {
                        Description = Localizations.Localize(["settings", "AntiSnakeMode", "desc"])
                    }
                ])
            ]).SubscribeToOnMenuClose(_ =>
            {
                helper.Storage.SaveJson(SettingsFile, settings);
            }))
        );
    }

    /*
     * assets must also be registered before they may be used.
     * Unlike cards and artifacts, however, they are very simple to register, and often do not need to be referenced in more than one place.
     * This utility method exists to easily register a sprite, but nothing prevents you from calling the method used yourself.
     */
    public static ISpriteEntry RegisterSprite(IPluginPackage<IModManifest> package, string dir)
    {
        return Instance.Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile(dir));
    }


    /*
     * Animation frames are typically named very similarly, only differing by the number of the frame itself.
     * This utility method exists to easily register an animation.
     * It expects the animation to start at frame 0, up to frames - 1.
     * TODO It is advised to avoid animations consisting of 2 or 3 frames.
     */
    public static ICharacterAnimationEntryV2 RegisterAnimation(IPluginPackage<IModManifest> package, string tag, string dir, int frames)
    {
        return Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Instance.IlleanaDeck.Deck.Key(),
            LoopTag = tag,
            Frames = Enumerable.Range(0, frames)
                .Select(i => RegisterSprite(package, dir + i + ".png").Sprite)
                .ToImmutableList()
        });
    }

    public static ICharacterAnimationEntryV2 RegisterAnimation(string type, IPluginPackage<IModManifest> package, string tag, string dir, int frames)
    {
        return Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = type,
            LoopTag = tag,
            Frames = Enumerable.Range(0, frames)
                .Select(i => RegisterSprite(package, dir + i + ".png").Sprite)
                .ToImmutableList()
        });
    }
}

