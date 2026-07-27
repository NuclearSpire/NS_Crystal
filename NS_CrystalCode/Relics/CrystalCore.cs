using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace NS_Crystal.NS_CrystalCode.Relics;

public class CrystalCore : CrystalRelic
{
  public override RelicRarity Rarity => RelicRarity.Starter;

  private CardModel? _triggeringCard;
  private bool _platingGainedThisCombat;

  private CardModel? TriggeringCard
  {
    get { return _triggeringCard; }
    set { AssertMutable(); _triggeringCard = value; }
  }

  private bool PlatingGainedThisCombat
  {
    get { return _platingGainedThisCombat; }
    set { AssertMutable(); _platingGainedThisCombat = value; }
  }

  public override Task BeforeCombatStart()
  {
    TriggeringCard = null;
    PlatingGainedThisCombat = false;
    Status = RelicStatus.Active;
    return Task.CompletedTask;
  }

  public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
  {
    if (power is not PlatingPower)
    {
      return 0;
    }
    if (cardSource == null)
    {
      return 0;
    }
    if (TriggeringCard != null && TriggeringCard != cardSource)
    {
      return 0;
    }
    if (target != Owner.Creature)
    {
      return 0;
    }
    if (PlatingGainedThisCombat)
    {
      return 0;
    }
    return 2;
  }

  public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
  {
    if (power is not PlatingPower)
    {
      return Task.CompletedTask;
    }
    if (cardSource == null)
    {
      return Task.CompletedTask;
    }
    Flash();
    Status = RelicStatus.Normal;
    TriggeringCard = cardSource;
    return Task.CompletedTask;
  }

  public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
  {
    if (cardPlay.Card.Owner != Owner)
    {
      return Task.CompletedTask;
    }
    if (cardPlay.Card != TriggeringCard)
    {
      return Task.CompletedTask;
    }
    if (PlatingGainedThisCombat)
    {
      return Task.CompletedTask;
    }
    PlatingGainedThisCombat = true;
    return Task.CompletedTask;
  }

  public override Task AfterCombatEnd(CombatRoom room)
  {
    TriggeringCard = null;
    PlatingGainedThisCombat = false;
    Status = RelicStatus.Normal;
    return Task.CompletedTask;
  }

  public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
  {
    if (target == Owner.Creature && props.IsPoweredAttack() && result.UnblockedDamage > 0)
    {
      await PowerCmd.Apply<PlatingPower>(choiceContext, Owner.Creature, -result.UnblockedDamage, dealer, cardSource);
    }
  }
}