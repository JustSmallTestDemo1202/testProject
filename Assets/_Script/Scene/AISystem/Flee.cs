using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISystem;
using UnityEngine;

namespace AISystem.States
{
    [Category("Character")]
    [System.Serializable]
    public class Flee : Follow
    {
        public override void OnUpdate()
        {
            if (mTarget != null)
            {
                Vector3 dirToTarget = mTarget.transform.position - owner.transform.position;
                float angle = Vector3.Angle(mTarget.transform.forward, dirToTarget);
                Vector3 fleePosition = owner.transform.position + mTarget.transform.forward * 5;

                if (Mathf.Abs(angle) < 90 || Mathf.Abs(angle) > 270)
                {
                    fleePosition = owner.transform.position - mTarget.transform.position * 5;
                }
                agent.SetDestination(fleePosition);
            }
        }
    }
}
