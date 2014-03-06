using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class MAttackState : State
{
    public MAttackState()
        : base()
    {
        properties.Add(new SerializedStringValuePair("Damage", 0));
        properties.Add(new SerializedStringValuePair("Hit Chance", 70.0f));
    }

    public int Damage
    {
        get
        {
            return (int)GetProperty("Damage").intValue;
        }
    }

    public float HitChance
    {
        get
        {
            return (float)GetProperty("Hit Chance").floatValue;
        }
    }
}