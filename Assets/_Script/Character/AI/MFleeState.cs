using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class MFleeState : FollowState
{
    public MFleeState()
        : base()
    {
    }

    public override void HandleState(AIRuntimeController controller)
    {
        if (controller.navMeshAgent != null)
        {
            Vector3 directionToTarget = controller.target.position - controller.transform.position;
            float angel = Vector3.Angle(controller.target.forward, directionToTarget);
            Vector3 fleePosition = controller.transform.position + controller.target.forward * 5;

            if (Mathf.Abs(angel) < 90 || Mathf.Abs(angel) > 270)
            {
                fleePosition = controller.transform.position - controller.target.forward * 5;
            }

            controller.GetCharacter().SetSpeed(Speed);
            controller.GetCharacter().SetDestination(fleePosition);
        }
    }
}
