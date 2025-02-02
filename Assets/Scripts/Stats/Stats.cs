using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    [SerializeField] private int baseValue;

    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;

    }

    public void SetDefaultValue(int value)
    {
        baseValue = value;
    }

    public void AddModifiers(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifiers(int modifier)
    {
        modifiers.RemoveAt(modifier);
    }
}
