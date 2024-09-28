using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DemoMod;

internal class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal IDeckEntry DemoDeck;
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    /*
     * The following lists contain references to all types that will be registered to the game.
     * All cards and artifacts must be registered before they may be used in the game.
     * In theory only one collection could be used, containing all registerable types, but it is seperated this way for ease of organization.
     */
    private static List<Type> DemoCommonCardTypes = [
    ];
    private static List<Type> DemoUncommonCardTypes = [
    ];
    private static List<Type> DemoRareCardTypes = [
    ];
    private static IEnumerable<Type> DemoCardTypes =
        DemoCommonCardTypes
            .Concat(DemoUncommonCardTypes)
            .Concat(DemoRareCardTypes);

    private static List<Type> DemoCommonArtifacts = [
    ];
    private static List<Type> DemoBossArtifacts = [
    ];
    private static IEnumerable<Type> DemoArtifactTypes =
        DemoCommonArtifacts
            .Concat(DemoBossArtifacts);

    private static IEnumerable<Type> AllRegisterableTypes =
        DemoCardTypes
            .Concat(DemoArtifactTypes);

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;

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
        var border = RegisterSprite(package, "assets/frame_dave.png").Sprite;
        DemoDeck = helper.Content.Decks.RegisterDeck("Demo", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                /*
                 * This color is used in a few places:
                 * TODO On cards, it dictates the sheen on higher rarities, as well as influences the color of the energy cost.
                 * If this deck is given to a playable character, their name will be this color, and their mini will have this color as their border.
                 */
                color = new Color("999999"),

                titleColor = new Color("000000")
            },

            DefaultCardArt = StableSpr.cards_colorless,
            BorderSprite = border,
            Name = AnyLocalizations.Bind(["character", "name"]).Localize
        });

        /*
         * All the IRegisterable types placed into the static lists at the start of the class are initialized here.
         * This snippet invokes all of them, allowing them to register themselves with the package and helper.
         */
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);
        
        /*
         * Characters have required animations, recommended animations, and you have the option to add more.
         * In addition, they must be registered before the character themselves is registered.
         * The game requires you to have a neutral animation and mini animation, used for normal gameplay and the map and run start screen, respectively.
         * The game uses the squint animation for the Extra-Planar Being and High-Pitched Static events, and the gameover animation while you are dying.
         * You may define any other animations, and they will only be used when explicitly referenced (such as dialogue).
         */
        RegisterAnimation(package, "neutral", "assets/Animation/DaveNeutral", 4);
        RegisterAnimation(package, "squint", "assets/Animation/DaveSquint", 4);
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = DemoDeck.Deck.Key(),
            LoopTag = "gameover",
            Frames = [
                RegisterSprite(package, "assets/Animation/DaveGameOver.png").Sprite,
            ]
        });
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = DemoDeck.Deck.Key(),
            LoopTag = "mini",
            Frames = [
                RegisterSprite(package, "assets/Animation/DaveMini.png").Sprite,
            ]
        });

        helper.Content.Characters.V2.RegisterPlayableCharacter("Demo", new PlayableCharacterConfigurationV2
        {
            Deck = DemoDeck.Deck,
            BorderSprite = border,
            Starters = new StarterDeck
            {
                cards = [
                ],
                /*
                 * Some characters have starting artifacts, in addition to starting cards.
                 * This is where they would be added, much like their starter cards.
                 * This can be safely removed if you have no starting artifacts.
                 */
                artifacts = [
                ]
            }
        });
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
    public static void RegisterAnimation(IPluginPackage<IModManifest> package, string tag, string dir, int frames)
    {
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Instance.DemoDeck.Deck.Key(),
            LoopTag = tag,
            Frames = Enumerable.Range(0, frames)
                .Select(i => RegisterSprite(package, dir + i + ".png").Sprite)
                .ToImmutableList()
        });
    }
}

