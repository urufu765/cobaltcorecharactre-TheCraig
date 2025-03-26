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
//using System.Reflection;

namespace Illeana;

internal class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal static IPlayableCharacterEntryV2 IlleanaTheSnek { get; private set; } = null!;
    internal Harmony Harmony;
    internal IKokoroApi KokoroApi;
    internal IDeckEntry IlleanaDeck;
    internal ICharacterAnimationEntryV2 IlleanaAnim_Neutral;
    internal ICharacterAnimationEntryV2 IlleanaAnim_Squint;
    internal ICharacterAnimationEntryV2 IlleanaAnim_Panic;
    internal IStatusEntry TarnishStatus { get; private set; } = null!;

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
        typeof(FindCure)
    ];
    private static List<Type> IlleanaUncommonCardTypes = [
        typeof(GoneJiffy),
        typeof(MakeshiftHull),
        typeof(Amputation),
        typeof(DeadlyAdrenaline),
        typeof(PartSwap),
        typeof(Disinfect),
        typeof(AcidicPackage)
    ];
    private static List<Type> IlleanaRareCardTypes = [
        typeof(GreatHealing),
        typeof(ImmunityShot),
        typeof(Distracted),
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
        typeof(SnekTunezGroovy)
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
        typeof(ExternalFuelSource)
    ];
    private static List<Type> IlleanaBossArtifacts = [
        // typeof(ConstantInnovation),
        // typeof(Limbless),
        typeof(PersonalStereo),
        typeof(Tempoboosters)
    ];
    private static List<Type> IlleanaEventArtifacts = [
        typeof(LightenedLoad)
    ];
    private static IEnumerable<Type> IlleanaArtifactTypes =
        IlleanaCommonArtifacts
            .Concat(IlleanaBossArtifacts)
            .Concat(IlleanaEventArtifacts);

    private static IEnumerable<Type> AllRegisterableTypes =
        IlleanaCardTypes
            .Concat(IlleanaArtifactTypes);

    public Spr SprTunezOn {get; private set;}
    public Spr SprTunezChill {get; private set;}
    public Spr SprTunezHype {get; private set;}
    public Spr SprTunezSad {get; private set;}
    public Spr SprTunezGroovy {get; private set;}
    public Spr SprBoostrE {get; private set;}
    public Spr SprBoostrB {get; private set;}
    public Spr SprBoostrO {get; private set;}


    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
        Harmony = new Harmony("urufudoggo.Craig");
        
        /*
         * Some mods provide an API, which can be requested from the ModRegistry.
         * The following is an example of a required dependency - the code would have unexpected errors if Kokoro was not present.
         * Dependencies can (and should) be defined within the nickel.json file, to ensure proper load mod load order.
         */
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        MoreDifficultiesApi = helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties");

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
                color = new Color("45e2b0"),

                titleColor = new Color("000000")
            },

            DefaultCardArt = StableSpr.cards_colorless,
            BorderSprite = RegisterSprite(package, "assets/frame_illeana.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "name"]).Localize
        });

        /*
         * All the IRegisterable types placed into the static lists at the start of the class are initialized here.
         * This snippet invokes all of them, allowing them to register themselves with the package and helper.
         */
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);


        KokoroApi.V2.ActionCosts.RegisterStatusResourceCostIcon(Status.corrode, RegisterSprite(package, "assets/corrode_pay.png").Sprite, RegisterSprite(package, "assets/corrode_cost.png").Sprite);
        
        /*
         * Characters have required animations, recommended animations, and you have the option to add more.
         * In addition, they must be registered before the character themselves is registered.
         * The game requires you to have a neutral animation and mini animation, used for normal gameplay and the map and run start screen, respectively.
         * The game uses the squint animation for the Extra-Planar Being and High-Pitched Static events, and the gameover animation while you are dying.
         * You may define any other animations, and they will only be used when explicitly referenced (such as dialogue).
         */
        IlleanaAnim_Neutral = RegisterAnimation(package, "neutral", "assets/Animation/illeana_neutral", 4);
        IlleanaAnim_Squint = RegisterAnimation(package, "squint", "assets/Animation/illeana_squint", 4);
        IlleanaAnim_Panic = RegisterAnimation(package, "panic", "assets/Animation/illeana_panic", 4);
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = IlleanaDeck.Deck.Key(),
            LoopTag = "gameover",
            Frames = [
                RegisterSprite(package, "assets/Animation/illeana_gameover0.png").Sprite,
            ]
        });
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = IlleanaDeck.Deck.Key(),
            LoopTag = "mini",
            Frames = [
                RegisterSprite(package, "assets/Animation/illeana_mini0.png").Sprite,
            ]
        });

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
            Description = AnyLocalizations.Bind(["character", "desc"]).Localize
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

        SprTunezOn = RegisterSprite(package, "assets/Artifact/Personal_Stereo.png").Sprite;
        SprTunezChill = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Chill.png").Sprite;
        SprTunezHype = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Hype.png").Sprite;
        SprTunezSad = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Sad.png").Sprite;
        SprTunezGroovy = RegisterSprite(package, "assets/Artifact/Personal_Stereo_Groovy.png").Sprite;
        SprBoostrE = RegisterSprite(package, "assets/Artifact/TempoboostersE.png").Sprite;
        SprBoostrB = RegisterSprite(package, "assets/Artifact/TempoboostersB.png").Sprite;
        SprBoostrO = RegisterSprite(package, "assets/Artifact/TempoboostersO.png").Sprite;


        DrawLoadingScreenFixer.Apply(Harmony);
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

