using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public bool loop;
    public float time = 5;

    public bool position;
    public bool orientation;
    public bool scale;
    float timeElapse;

    void Update()
    {
        if (!loop)
        {
            if (timeElapse + Time.deltaTime >= time)
            {
                EffectManager.Instance.DestroyFx(this.gameObject);
                return;
            }
        }

        timeElapse += Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}