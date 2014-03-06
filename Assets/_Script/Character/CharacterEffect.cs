using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterEffect : MonoBehaviour, FxListener
{
    List<GameObject> fxList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddEffect(string name)
    {
        GameObject fx = Framework.Get<EffectManager>().SpawnFx(name);
        fxList.Add(fx);
    }

    void EffectEvent(string fx)
    {
        string[] parms = fx.Split(':');
        if (parms.Length > 0)
        {
            AddEffect(parms[0]);

        }
    }

    #region FxListener 成员

    public void OnDestroyFx(GameObject fx)
    {
        fxList.Remove(fx);
    }

    #endregion
}
