using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace NS_Crystal.NS_CrystalCode.Cards;

public class LastStand() : CrystalCard(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
  protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    HoverTipFactory.FromPower<PlatingPower>(),
    HoverTipFactory.Static(StaticHoverTip.Block)
  ];

  protected override IEnumerable<DynamicVar> CanonicalVars => [
    new DamageVar(20, ValueProp.Move)
  ];

  protected override bool IsPlayable => !Owner.Creature.HasPower<PlatingPower>();

  protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
  {
    ArgumentNullException.ThrowIfNull(play.Target, "play.Target");
    await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3").Execute(choiceContext);
  }

  protected override void OnUpgrade()
  {
    DynamicVars.Damage.UpgradeValueBy(10);
  }
}