using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatable
{
    public void Increase(int value);
    public void Decrease(int value);
    public void UpdateUI();
}
