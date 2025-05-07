using System.Collections.Generic;
using Nickel;

namespace Illeana.Actions;

/// <summary>
/// Only used so that the CreditCard can have a custom tooltip.
/// </summary>
public class ADoesLiterallyNothingButItsForACreditCard : CardAction
{
    public override List<Tooltip> GetTooltips(State s)
    {
        return [
            new GlossaryTooltip("CreditCardDontKill")
            {
                Description = ModEntry.Instance.Localizations.Localize(["card", "Token", "BloodCard", "tooltip"]),
            }
        ];
    }
}