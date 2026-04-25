using System;
using System.Collections.Generic;
using System.Linq;
using Illeana.Cards;
using Microsoft.Extensions.Logging;

namespace Illeana.Actions;

public class ASelectAPotentialFix : CardAction
{
    public override Route? BeginWithRoute(G g, State s, Combat c)
    {
        timer = 0.0;
        BuildCure buildCure = new BuildCure
        {
            temporaryOverride = true,
            flipAnim = 1
        };
        Upgrade upgrade = CardReward.GetUpgrade(s, s.rngCardOfferingsMidcombat, s.map, buildCure, (s.GetDifficulty() >= 1) ? .5 : 1, false);
        buildCure.upgrade = upgrade;
        FindCure findCure = new FindCure
        {
            temporaryOverride = true,
            upgrade = upgrade,
            flipAnim = 1
        };
        return new CardReward
        {
            cards = [buildCure, findCure],
            canSkip = false
        };
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        return [
            new TTCard{
                card = new BuildCure()
            },
            new TTCard{
                card = new FindCure()
            },
        ];
    }
}