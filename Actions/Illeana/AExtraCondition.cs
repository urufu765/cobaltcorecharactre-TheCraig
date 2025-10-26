using System.Collections.Generic;
using Nickel;

namespace Illeana.Actions;

public class AExtraConditionMaxShield : CardAction
{
    public required int minimum;
    public override List<Tooltip> GetTooltips(State s)
    {
        return [
            new GlossaryTooltip("howMuchShield")
            {
                Description = ModEntry.Instance.Localizations.Localize(["action", "ExtraCondition", "MaxShield"], new{
                    requirement = minimum
                }),
            }
        ];
    }
    public override Icon? GetIcon(State s)
    {
        bool fulfilled = s.ship.shieldMaxBase >= minimum;
        return new Icon(StableSpr.icons_questionMark, null, fulfilled?Colors.healthBarShield:Colors.redd);
    }
}

public class AExtraConditionMaxTotalShield : CardAction
{
    public required int minimum;
    public override List<Tooltip> GetTooltips(State s)
    {
        return [
            new GlossaryTooltip("howMuchShield")
            {
                Description = ModEntry.Instance.Localizations.Localize(["action", "ExtraCondition", "MaxTotalShield"], new{
                    requirement = minimum
                }),
            }
        ];
    }
    public override Icon? GetIcon(State s)
    {
        bool fulfilled = s.ship.GetMaxShield() >= minimum;
        return new Icon(StableSpr.icons_questionMark, null, fulfilled?Colors.healthBarShield:Colors.redd);
    }
}