using System.Collections;
using System.Collections.Generic;
using Inventory.Item;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodObject", menuName = "Inventory/New Food Object")]
public class FoodObject : UseableItem
{
    public EffectSlot[] Effects;
    
    private void Awake()
    {
        type = ItemType.Food;
    }

    public override void Use()
    {
        foreach (var effectSlot in Effects)
        {
            effectSlot.Use();
        }
    }
}


[System.Serializable]
public class EffectSlot
{
    public Effect effect;

    [SerializeField]
    public int value;

    public EffectChangeType changeType;
    
    [Header("If Effect is SkillEffect")]
    public int Duration;

    public void Use()
    {
        effect.value = this.value;

        if(effect is SkillEffect skillEffect)
        {
            skillEffect.Duration = this.Duration;
        }

        effect.Use(changeType);
    }
}
