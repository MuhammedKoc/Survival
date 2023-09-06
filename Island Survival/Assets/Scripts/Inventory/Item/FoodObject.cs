using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodObject", menuName = "Inventory/New Food Object")]
public class FoodObject : ItemObject
{
    public EffectType FoodType;
    public Valence FoodValence;
    [SerializeField] int FoodValue;

    private void Awake()
    {
        type = ItemType.Food;
    }

    public override void Use()
    {
        switch (FoodValence)
        {
            case Valence.Positive:
                switch (FoodType)
                {
                    case EffectType.Health:
                        HealthManager.Instance.Increase(FoodValue);
                        break;

                    case EffectType.Hunger:
                        SurivalStatus.Instance.Hungry.Increase(FoodValue);
                        break;

                    case EffectType.Thirst:
                        SurivalStatus.Instance.Thirst.Increase(FoodValue);
                        break;
                }
                break;

            case Valence.Negative:
                switch (FoodType)
                {
                    case EffectType.Health:
                        HealthManager.Instance.Decrease(FoodValue);
                        break;

                    case EffectType.Hunger:
                        SurivalStatus.Instance.Hungry.Decrease(FoodValue);
                        break;

                    case EffectType.Thirst:
                        SurivalStatus.Instance.Thirst.Decrease(FoodValue);
                        break;
                }
                break;
        }

       
    }

    
}

public enum EffectType
{
    Health,
    Hunger,
    Thirst
}

public enum Valence
{
    Positive,
    Negative
}

