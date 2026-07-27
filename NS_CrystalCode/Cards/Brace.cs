using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace NS_Crystal.NS_CrystalCode.Cards;

public class Brace() : CrystalCard(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
  protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    HoverTipFactory.FromPower<PlatingPower>(),
    HoverTipFactory.Static(StaticHoverTip.Block)
  ];

  protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<PlatingPower>(20)
  ];

  protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
  {
    await PowerCmd.Apply<PlatingPower>(choiceContext, Owner.Creature, DynamicVars["PlatingPower"].IntValue, Owner.Creature, this);
    PlayerCmd.EndTurn(Owner, false);
  }

  protected override void OnUpgrade()
  {
    EnergyCost.UpgradeBy(-1);
  }
}