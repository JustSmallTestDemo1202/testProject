using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public interface FxListener
{
    void OnDestroyFx(GameObject fx);
}

public class EffectManager : SingletonBehaviour<EffectManager>
{
    List<GameObject> fxList = new List<GameObject>();
	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
    void Update()
    {      
    }

    public GameObject SpawnFx(string name)
    {
        return SpawnFx(name, Vector3.zero, Quaternion.identity);
    }

    public GameObject SpawnFx(string name, Vector3 position, Quaternion rotation)
    {
        Object fxRes = Resources.Load(name);
        GameObject fxObj = GameObject.Instantiate(fxRes, position, rotation) as GameObject;
        fxList.Add(fxObj);
        return fxObj;
    }

    public GameObject SpawnFx(GameObject fxRes)
    {
        return SpawnFx(fxRes, Vector3.zero, Quaternion.identity);
    }

    public GameObject SpawnFx(GameObject fxRes, Vector3 position, Quaternion rotation)
    {
        GameObject fxObj = GameObject.Instantiate(fxRes, position, rotation) as GameObject;
        fxList.Add(fxObj);
        return fxObj;
    }

    public void DestroyFx(GameObject fxObj)
    {
        fxList.Remove(fxObj);
        GameObject.Destroy(fxObj);
    }
    
    public void Clear()
    {
        foreach (GameObject obj in fxList)
        {
            GameObject.Destroy(obj);
        }

        fxList.Clear();
    }
}
