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
using Illeana.Conversation;
using Shockah.Dyna;

namespace Illeana;

internal partial class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal static IPlayableCharacterEntryV2 IlleanaTheSnek { get; private set; } = null!;
    internal static INonPlayableCharacterEntryV2 CraigTheSnek { get; private set; } = null!;
    internal static INonPlayableCharacterEntryV2 LisardEXE { get; private set; } = null!;
    internal string UniqueName { get; private set; }
    internal Harmony Harmony;
    internal IKokoroApi KokoroApi;
    internal IDeckEntry IlleanaDeck;
    internal IDeckEntry DecrepitCraigDeck;
    internal Settings settings;
    private IWritableFileInfo SettingsFile => Helper.Storage.GetMainStorageFile("json");
    public bool modDialogueInited;

    internal IStatusEntry TarnishStatus { get; private set; } = null!;
    internal IStatusEntry SnekTunezStatus { get; private set; } = null!;
    internal IStatusEntry ExcessShardStatus { get; private set; } = null!;

    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
    internal IMoreDifficultiesApi? MoreDifficultiesApi { get; private set; } = null!;
    internal IDuoArtifactsApi? DuoArtifactsApi { get; private set; } = null!;
    internal IDynaApi? DynaApi { get; private set; } = null!;

    internal ReactiveFormulae reactiveFormulae;
    internal DemolitionSetupProfessional demoSetupPro;

    #region Card/artifact type declaration
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
        typeof(AcidicPackage),
        typeof(AcidBackflow),
        typeof(BakedHull),
        typeof(BakedHullOld)
    ];
    private static List<Type> IlleanaRareCardTypes = [
        typeof(MakeshiftHull),
        typeof(GreatHealing),
        typeof(ImmunityShot),
        typeof(LacedYoFood),
        typeof(WeaponisedPatchkit),
        typeof(ImprovisedTiming)
    ];
    private static List<Type> IlleanaSpecialCardTypes = [
        typeof(TheCure),
        typeof(TheSolution),
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
        typeof(PerfectShieldColourless),
        typeof(CreditCard),
        typeof(CheapFuel)
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
        typeof(LightenedLoad),      // Alt starter
        typeof(ReusableScrap),      // (REPLACED) Dizzy duo
        typeof(SuperInjection),     // (REPLACED) Isaac duo
        typeof(AirlockSnek),        // (REPLACED) Peri duo
        // typeof(TarnishedSyringe),   // (UNIMPLEMENTED) Old Illeana common
        // typeof(ConstantInnovation), // (UNIMPLEMENTED) Old Illeana boss
        // typeof(Limbless),           // (UNUSED) Old Illeana boss
        // typeof(ToxicSports),        // (UNUSED) Sasha event-only temp artifact
        // typeof(GooseH8r),           // (UNIMPLEMENTED) Goose event-only temp artifact
        // typeof(AutododgeExpress),   // (UNIMPLEMENTED)
        // typeof(PseudoPacifist),     // (UNIMPLEMENTED) Solo starter idea 1
        // typeof(CobraVoodoo),        // (UNIMPLEMENTED) Solo starter idea 2
        // typeof(JitterStick),        // (UNIMPLEMENTED) Peri V2 duo idea 1
        // typeof(ResidualHeatSolder), // (UNIMPLEMENTED) Ilya duo idea 2
    ];
    private static List<Type> IlleanaDuoArtifacts = [
        typeof(ResourceOverflowCatcher),// Dizzy V2
        typeof(LooseStick),             // (Slated for replacement) Peri V2
        // typeof(MadeFromAcid),           // Peri V3
        typeof(ThrustThursters),        // Riggs
        typeof(ExtraSlip),              // Max
        typeof(PerfectedProtection),    // CAT
        typeof(FuelEqualizer),          // Isaac V2
        typeof(LubricatedHeatpump),     // Drake
        typeof(UnprotectedStorage),     // Books
        typeof(HullHarvester),          // Weth
        typeof(Competition),            // Eddie
        typeof(BountifulBloodBank),     // Dracula
        typeof(ChanceOfAcid),           // Dave
        typeof(ReactiveFormula),        // Marielle
        typeof(DemolitionSetup),        // Dyna
        typeof(RegenerativeBacktrack),  // Nibbs
        typeof(SlippyShuffle),          // Nola
        typeof(SwaySwheel),             // Isabelle
        typeof(ResidualHeatWelder),     // Ilya
        typeof(TakeABreak),             // Jost
        //typeof(SpecialSalt),            // Gauss
        // typeof(),                       // Sorrel*
        // typeof(Oxyboom),                // Jack*
        // typeof(WallSnek),               // Scarlet*
        // typeof(),                       // TH34*
        // typeof(),                       // Nichole*
        // typeof(),                       // Equilynx*
        // typeof(),                       // Bloch*
        // typeof(),                       // Destiny*
        // typeof(),                       // Natasha*
        // typeof(),                       // Soggins*
        // typeof(),                       // Bucket*
        // typeof(),                       // Louis*
        // typeof(),                       // Ty&Sasha*
        // typeof(),                       // Tucker*
        // typeof(),                       // Rosa*
        // typeof(),                       // Wade*
        // typeof(),                       // Install*
        // typeof(),                       // Ruhig*
        // typeof(),                       // Cleo*
        // typeof(),                       // Sierra*
        // typeof(),                       // Bjorn*
        // typeof(),                       // Angder*
        // typeof(),                       // D26*
        // typeof(),                       // Grunan*
        // typeof(),                       // Kobrette*
        // typeof(),                       // Destiny*
        // typeof(),                       // Mina*
        // typeof(),                       // Pilot*
        // typeof(),                       // Philip*
        // typeof(),                       // Randall*
        // typeof(),                       // Andromeda*
        // typeof(),                       // Johnson*
        // typeof(),                       // Garrus*

    ];
    #endregion

    private static List<Type> IlleanaDialogueTypes = [
        typeof(NewNewCombatDialogue),
        typeof(NewNewArtifactDialogue),
        typeof(NewNewEventDialogue),
        typeof(NewCardDialogue),
        typeof(NewStoryDialogue),
        typeof(NewMemoryDialogue),
        typeof(KO_CombatDialogue),
    ];
    private static IEnumerable<Type> IlleanaArtifactTypes =
        IlleanaCommonArtifacts
            .Concat(IlleanaBossArtifacts)
            .Concat(IlleanaEventArtifacts)
            .Concat(IlleanaDuoArtifacts);

    private static IEnumerable<Type> AllRegisterableTypes =
        IlleanaCardTypes
            .Concat(IlleanaArtifactTypes)
            .Concat(IlleanaDialogueTypes)
            ;

    #region Dialogue Animations
    private static List<string> Illeana1Anims = [
        "gameover",
        "knife",
        "mini",
        "readytoeat",
        "stareatcamera",
        "placeholder",
        "shoeana",
        "shoeanamini",
    ];
    private static List<string> Illeana3Anims = [
        "giggle",
        "nap",
        "screamA",
        "screamB",
        "screamC",
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
        "unamused"
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
    public readonly static Dictionary<int, List<string>> IlleanaSnekAnims = new()
    {
        {1, Illeana1Anims},
        {3, Illeana3Anims},
        {4, Illeana4Anims},
        {5, Illeana5Anims},
        {6, Illeana6Anims}
    };
    public readonly static Dictionary<int, List<string>> CraigAnims = new()
    {
        {1, [
            "mini",
            "flabbergasted",
            "flabberclosed",
            "penthink",
            "cracs"
        ]},
        {4, [
            "panic"
        ]},
        {5, [
            "neutral",
            "explain",  // make loading circle disappear on default frame
            "eyeroll",  // make decal clearer
            "squint",
            "confident",
            "glare",
            "sly",
            "write",
            "writepissed",
            "tired",
            "therefore",
            "teehee",
            "recall"
        ] }
    };
    public readonly static Dictionary<int, List<string>> LisardAnims = new()
    {
        {1, [
            "static",
            "mini"
        ]},
        {5, [
            "neutral"
        ]}
    };
    #endregion


    #region Artifacts
    public Spr SprStolenOn { get; private set; }
    public Spr SprStolenChill { get; private set; }
    public Spr SprStolenHype { get; private set; }
    public Spr SprStolenSad { get; private set; }
    public Spr SprStolenGroovy { get; private set; }
    public Spr SprSportsOn { get; private set; }
    public Spr SprSportsChill { get; private set; }
    public Spr SprSportsHype { get; private set; }
    public Spr SprSportsSad { get; private set; }
    public Spr SprSportsGroovy { get; private set; }
    public Spr SprDigitalOn { get; private set; }
    public Spr SprDigitalChill { get; private set; }
    public Spr SprDigitalHype { get; private set; }
    public Spr SprDigitalSad { get; private set; }
    public Spr SprDigitalGroovy { get; private set; }
    public Spr SprBoostrE { get; private set; }
    public Spr SprBoostrB { get; private set; }
    public Spr SprBoostrO { get; private set; }
    public Spr SprExLubeO { get; private set; }
    public Spr SprExLubeX { get; private set; }
    public Spr SprEFLavailable { get; private set; }
    public Spr SprEFLdepleted { get; private set; }
    public Spr SprThurstDepleted { get; private set; }
    public Spr SprAirlockShoe { get; private set; }
    public Spr SprHullHarvestDepleted { get; private set; }
    public Spr SprCompetitionDepleted { get; private set; }
    public Spr SprCompetitionShoeDepleted { get; private set; }
    public Spr SprCompetitionShoe { get; private set; }
    public Spr SprCompetitionIlleana { get; private set; }
    public Spr SprCompetitionShoeana { get; private set; }
    public Spr SprCompetitionEddie { get; private set; }
    public Spr SprDemolitionSetupDepleted { get; private set; }
    public Spr SprLooseStickDepleted { get; private set; }
    public Spr SprResidualHeatWelderInactive { get; private set; }
    public Spr SprSwaySwheelDepleted { get; private set; }
    public Spr SprTakeABreakActive { get; private set; }
    public Spr SprTakeABreakDepleted { get; private set; }
    #endregion

    #region Story Sprites
    public Spr IlleanaEnd { get; private set; }
    public Spr IlloodleEnd { get; private set; }
    public Spr ShoeanaEnd { get; private set; }
    public Spr BGShip_0_Platform { get; private set; }
    public Spr BGShip_1_Craig { get; private set; }
    public Spr BGShip_1_Varrigradona { get; private set; }
    public Spr BGShip_1_CraigProps { get; private set; }
    public Spr BGShip_2_Persona { get; private set; }
    public Spr BGShip_3_Backing { get; private set; }
    public Spr BGShip_4_Props { get; private set; }
    public Spr BGShip_A_Craig { get; private set; }
    public Spr BGShip_B_Persona { get; private set; }
    public Spr BGShip_C_Props { get; private set; }
    public Spr BGShip_D_Glass { get; private set; }
    #endregion

    #region Story Sounds
    public IModSoundEntry BGShip_0_Down { get; private set; }
    public IModSoundEntry BGShip_1_Ding { get; private set; }
    public IModSoundEntry BGShip_2_Beep { get; private set; }
    public IModSoundEntry BGShambles_0_Tinnitus { get; private set; }
    #endregion

    public LocalDB localDB { get; set; } = null!;

    public bool shoeanaMode = false;

}