using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace NS_Crystal.NS_CrystalCode.Cards;

public class Rebuild() : CrystalCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
  protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    HoverTipFactory.FromPower<PlatingPower>(),
    HoverTipFactory.Static(StaticHoverTip.Block)
  ];

  protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<PlatingPower>(8),
    new CardsVar(2)
  ];

  protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
  {
    if (Owner.Creature.HasPower<PlatingPower>())
    {
      await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
    }
    else
    {
      await PowerCmd.Apply<PlatingPower>(choiceContext, Owner.Creature, DynamicVars["PlatingPower"].IntValue, Owner.Creature, this);
    }
  }

  protected override void OnUpgrade()
  {
    DynamicVars["PlatingPower"].UpgradeValueBy(4);
    DynamicVars.Cards.UpgradeValueBy(1);
  }
}