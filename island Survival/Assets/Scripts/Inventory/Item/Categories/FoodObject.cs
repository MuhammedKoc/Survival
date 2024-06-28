using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodObject", menuName = "Inventory/New Food Object")]
public class FoodObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Food;
    }

    public EffectSlot[] Effects;
}


[System.Serializable]
public class EffectSlot
{
    public Effect effect;

    [SerializeField]
    public int value;

    [Header("If Effect is SkillEffect")]
    public int Duration;

    public void Use()
    {
        effect.value = this.value;

        if(effect is SkillEffect skillEffect)
        {
            skillEffect.Duration = this.Duration;
        }

        effect.Use();
    }
}
