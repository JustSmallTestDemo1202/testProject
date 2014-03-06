using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class MFollowState : State
{
    public MFollowState()
        : base()
    {
		properties.Add(new SerializedStringValuePair("Speed",0.0f));
		properties.Add(new SerializedStringValuePair("Rotation",0.0f));	
	}

    public float Speed
    {
        get
        {
            return GetProperty("Speed").floatValue;
        }
    }

    public float Rotation
    {
        get
        {
            return GetProperty("Rotation").floatValue;
        }
    }

    public override void HandleState(AIRuntimeController controller)
    {
        controller.GetCharacter().SetSpeed(Speed);
        controller.GetCharacter().SetDestination(controller.target.position);
    }

    public override void Reset(AIRuntimeController controller)
    {
        base.Reset(controller);

        controller.GetCharacter().Path.Clear();
    }
}
