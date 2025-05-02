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
using System.Diagnostics;
//using System.Reflection;

namespace Illeana;

internal class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal static IPlayableCharacterEntryV2 IlleanaTheSnek { get; private set; } = null!;
    internal string UniqueName { get; private set; }
    internal Harmony Harmony;
    internal IKokoroApi KokoroApi;
    internal IDeckEntry IlleanaDeck;
    internal IDeckEntry DecrepitCraigDeck;
    public bool modDialogueInited;

    internal IStatusEntry TarnishStatus { get; private set; } = null!;
    internal IStatusEntry SnekTunezStatus { get; private set; } = null!;

    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
    internal IMoreDifficultiesApi? MoreDifficultiesApi {get; private set; } = null!;

    /*
     * The following lists contain references to all types that will be registered to the game.
     * All cards and artifacts must be registered before they may be used in the game.
     * In theory only one collection could be used, containing all registerable types, but it is seperated this way for ease of organization.
     */
    private static List<Type> IlleanaCommonCardTypes = [
        typeof(BuildCure),
        typeof(Cleanse),
        typeof(Exposure),
        typeof(UntestedSubstance),
        typeof(Autotomy),
        typeof(ScrapPatchkit),
        typeof(FalseVaccine),
        typeof(IncompatibleFuel),
        typeof(FindCure),
        typeof(IlleanaExe)
    ];
    private static List<Type> IlleanaUncommonCardTypes = [
        typeof(GoneJiffy),
        typeof(Amputation),
        typeof(DeadlyAdrenaline),
        typeof(PartSwap),
        typeof(Distracted),
        typeof(Disinfect),
        typeof(AcidicPackage)
    ];
    private static List<Type> IlleanaRareCardTypes = [
        typeof(MakeshiftHull),
        typeof(GreatHealing),
        typeof(ImmunityShot),
        typeof(LacedYoFood),
        typeof(WeaponisedPatchkit)
    ];
    private static List<Type> IlleanaSpecialCardTypes = [
        typeof(TheCure),
        typeof(TheFailure),
        typeof(TheAccident),
        typeof(SnekTunezChill),
        typeof(SnekTunezHype),
        typeof(SnekTunezSad),
        typeof(SnekTunezGroovy),
        typeof(SnekTunezPlaceholder),
        typeof(Reminiscent),
        typeof(Coalescent),
        typeof(Obmutescent),
        typeof(PerfectShieldColourless)
    ];
    private static IEnumerable<Type> IlleanaCardTypes =
        IlleanaCommonCardTypes
            .Concat(IlleanaUncommonCardTypes)
            .Concat(IlleanaRareCardTypes)
            .Concat(IlleanaSpecialCardTypes);

    private static List<Type> IlleanaCommonArtifacts = [
        typeof(ForgedCertificate),
        typeof(ByproductProcessor),
        // typeof(TarnishedSyringe),
        typeof(CausticArmor),
        typeof(ExperimentalLubricant),
        typeof(ExternalFuelSource),
        typeof(SportsStereo),
        typeof(DigitalizedStereo)
    ];
    private static List<Type> IlleanaBossArtifacts = [
        // typeof(ConstantInnovation),
        // typeof(Limbless),
        typeof(PersonalStereo),
        typeof(Tempoboosters),
        typeof(WarpPrototype)
    ];
    private static List<Type> IlleanaEventArtifacts = [
        typeof(LightenedLoad),
        //typeof(ToxicSports)
    ];
    private static List<Type> IlleanaDialogueTypes = [
        typeof(NewCombatDialogue),
        typeof(NewArtifactDialogue),
        typeof(NewEventDialogue),
        typeof(NewCardDialogue),
        typeof(NewStoryDialogue)
    ];
    private static IEnumerable<Type> IlleanaArtifactTypes =
        IlleanaCommonArtifacts
            .Concat(IlleanaBossArtifacts)
            .Concat(IlleanaEventArtifacts);

    private static IEnumerable<Type> AllRegisterableTypes =
        IlleanaCardTypes
            .Concat(IlleanaArtifactTypes)
            .Concat(IlleanaDialogueTypes)
            ;

    private static List<string> Illeana1Anims = [
        "gameover",
        "knife",
        "mini",
        "readytoeat",
        "stareatcamera",
        "placeholder"
    ];
    private static List<string> Illeana3Anims = [
        "giggle",
        "nap",
        //"screamA",
        "screamB",
        //"screamC",
    ];
    private static List<string> Illeana4Anims = [
        "curious",
        "explain",
        "holdcable",
        "intense",
        "mad",
        "neutral",
        "panic",
        "shocked",
        "sly",
        "solemn",
        "squint",
        "tired",
    ];
    private static List<string> Illeana5Anims = [
        "possessed",
        "possessedmad",
        "sad",
        "silly",
    ];
    private static List<string> Illeana6Anims = [
        "salavating",
        "blinkrapid",
        //"headbang"
    ];
    public readonly static IEnumerable<string> IlleanaAnims =
        Illeana1Anims
            .Concat(Illeana3Anims)
            .Concat(Illeana4Anims)
            .Concat(Illeana5Anims)
            .Concat(Illeana6Anims);

    public Spr SprStolenOn {get; private set;}
    public Spr SprStolenChill {get; private set;}
    public Spr SprStolenHype {get; private set;}
    public Spr SprStolenSad {get; private set;}
    public Spr SprStolenGroovy {get; private set;}
    public Spr SprSportsOn {get; private set;}
    public Spr SprSportsChill {get; private set;}
    public Spr SprSportsHype {get; private set;}
    public Spr SprSportsSad {get; private set;}
    public Spr SprSportsGroovy {get; private set;}
    public Spr SprDigitalOn {get; private set;}
    public Spr SprDigitalChill {get; private set;}
    public Spr SprDigitalHype {get; private set;}
    public Spr SprDigitalSad {get; private set;}
    public Spr SprDigitalGroovy {get; private set;}
    public Spr SprBoostrE {get; private set;}
    public Spr SprBoostrB {get; private set;}
    public Spr SprBoostrO {get; private set;}
    public Spr SprExLubeO {get; private set;}
    public Spr SprExLubeX {get; private set;}
    public Spr SprEFLavailable {get; private set;}
    public Spr SprEFLdepleted {get; private set;}

    public LocalDB localDB { get; set; } = null!;


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
        helper.Events.OnModLoadPhaseFinished += (_, phase) =>
        {
            if (phase == ModLoadPhase.AfterDbInit)
            {
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



        KokoroApi.V2.ActionCosts.RegisterStatusResourceCostIcon(Status.corrode, RegisterSprite(package, "assets/corrode_pay.png").Sprite, RegisterSprite(package, "assets/corrode_cost.png").Sprite);
        
        /*
         * Characters have required animations, recommended animations, and you have the option to add more.
         * In addition, they must be registered before the character themselves is registered.
         * The game requires you to have a neutral animation and mini animation, used for normal gameplay and the map and run start screen, respectively.
         * The game uses the squint animation for the Extra-Planar Being and High-Pitched Static events, and the gameover animation while you are dying.
         * You may define any other animations, and they will only be used when explicitly referenced (such as dialogue).
         */
        foreach (string s1 in Illeana1Anims)
        {
            RegisterAnimation(package, s1, $"assets/Animation/illeana_{s1}", 1);
        }
        foreach (string s3 in Illeana3Anims)
        {
            RegisterAnimation(package, s3, $"assets/Animation/illeana_{s3}", 3);
        }
        foreach (string s4 in Illeana4Anims)
        {
            RegisterAnimation(package, s4, $"assets/Animation/illeana_{s4}", 4);
        }
        foreach (string s5 in Illeana5Anims)
        {
            RegisterAnimation(package, s5, $"assets/Animation/illeana_{s5}", 5);
        }
        foreach (string s6 in Illeana6Anims)
        {
            RegisterAnimation(package, s6, $"assets/Animation/illeana_{s6}", 6);
        }

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
            helper.Content.Artifacts.RegisterArtifact(ta.Name, UhDuhHundo.ArtifactRegistrationHelper(ta, RegisterSprite(package, "assets/Artifact/" + ta.Name + ".png").Sprite));
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
        /*
         * All the IRegisterable types placed into the static lists at the start of the class are initialized here.
         * This snippet invokes all of them, allowing them to register themselves with the package and helper.
         */
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);

        //DrawLoadingScreenFixer.Apply(Harmony);
        //SashaSportingSession.Apply(Harmony);
        WarpPrototypeHelper.Apply(Harmony);
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
}

