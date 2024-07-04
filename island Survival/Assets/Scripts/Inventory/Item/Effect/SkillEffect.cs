using System.Collections;
using System.Collections.Generic;
using Inventory.Item;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Effect", menuName = "Inventory/Effect/New Skill Effect")]
public class SkillEffect : Effect
{
    [SerializeField]
    private SkillEffectType type;
    
    [HideInInspector]
    public int Duration;

    public override void Use(EffectChangeType changeTypetype)
    {
        switch (type)
        {
            case SkillEffectType.HealthRegen:
                
                break;
            
            case SkillEffectType.MoveSpeed:
                
                break;
        }
    }
}
