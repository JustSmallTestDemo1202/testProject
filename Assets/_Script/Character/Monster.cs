using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aegis;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.CharacterController))]
public class Monster : Character
{
    CharacterAI ai;
    protected override void Awake()
    {
        base.Awake();

        ai = gameObject.AddComponent<CharacterAI>();
    }

}
