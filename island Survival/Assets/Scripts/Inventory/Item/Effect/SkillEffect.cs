using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Effect", menuName = "Inventory/Effect/New Skill Effect")]
public class SkillEffect : Effect
{
    [HideInInspector]
    public int Duration;

    public override void Use()
    {
        base.Use();


    }
}
