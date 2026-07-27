using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace NS_Crystal.NS_CrystalCode.Cards;

public class Reinforce() : CrystalCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
  public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

  protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    HoverTipFactory.FromPower<PlatingPower>(),
    HoverTipFactory.Static(StaticHoverTip.Block)
  ];

  protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
  {
    await PowerCmd.Apply<PlatingPower>(choiceContext, Owner.Creature, Owner.Creature.GetPowerAmount<PlatingPower>(), Owner.Creature, this);
  }

  protected override void OnUpgrade()
  {
    EnergyCost.UpgradeBy(-1);
  }
}