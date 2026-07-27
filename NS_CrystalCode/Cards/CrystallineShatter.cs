using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace NS_Crystal.NS_CrystalCode.Cards;

public class CrystallineShatter() : CrystalCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
  protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    HoverTipFactory.FromPower<PlatingPower>(),
    HoverTipFactory.Static(StaticHoverTip.Block)
  ];

  protected override IEnumerable<DynamicVar> CanonicalVars => [
    new CalculationBaseVar(6),
    new ExtraDamageVar(2),
    new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => card.Owner?.Creature.GetPowerAmount<PlatingPower>() ?? 0)
  ];

  protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
  {
    ArgumentNullException.ThrowIfNull(play.Target, "play.Target");
    await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3").Execute(choiceContext);
    await PowerCmd.Remove<PlatingPower>(Owner.Creature);
  }

  protected override void OnUpgrade()
  {
    DynamicVars.ExtraDamage.UpgradeValueBy(1);
  }
}