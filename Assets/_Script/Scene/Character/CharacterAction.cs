using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MAction : UnityEngine.Object
{
    public float startTime;
    public float duration;
    
    public virtual void OnActive()
    {
    }

    public virtual void OnDeactive()
    {
    }
}
