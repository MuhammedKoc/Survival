using System.Collections;
using System.Collections.Generic;
using Inventory.Item;
using UnityEngine;

[CreateAssetMenu(fileName ="New  Effect", menuName ="Inventory/Effect/New Stat Effect")]
public class StatEffect : Effect
{
    public StatEffectType type;
    
    public override void Use(EffectChangeType changeType)
    {
        switch (changeType)
        {
            case EffectChangeType.Increase:
                switch (type)
                {
                    case StatEffectType.Health:
                        PlayerHealth.Instance.Increase(value);
                        break;
                    case StatEffectType.Hunger:
                        SurvivalStatus.Instance.IncreaseHunger(value);
                        break;
                    case StatEffectType.Thirst:
                        SurvivalStatus.Instance.IncreaseThirst(value);
                        break;
                }
                break;
            
            case EffectChangeType.Decrease:
                switch (type)
                {
                    case StatEffectType.Health:
                        PlayerHealth.Instance.Decrease(value);
                        break;
                    case StatEffectType.Hunger:
                        SurvivalStatus.Instance.DecreaseHunger(value);
                        break;
                    case StatEffectType.Thirst:
                        SurvivalStatus.Instance.DecreaseThirst(value);
                        break;
                }
                break;
        }
        
       
    }
}
