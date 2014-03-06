using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.CharacterController))]
public class Player : Character
{
    protected override void Awake()
    {
        base.Awake();
    }
}
