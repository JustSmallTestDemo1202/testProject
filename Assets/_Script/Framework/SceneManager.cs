using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

    List<GameObject> objList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject SpawnObject(string fileName)
    {
        Object objRes = Resources.Load(name);
        GameObject obj = GameObject.Instantiate(objRes, Vector3.zero, Quaternion.identity) as GameObject;
        objList.Add(obj);
        return obj;
    }

    public GameObject SpawnPlayer(string fileName, int id)
    {
        GameObject obj = SpawnObject(fileName);
        return obj;
    }

    public GameObject SpawnMonster(string fileName, int id)
    {
        GameObject obj = SpawnObject(fileName);
        return obj;
    }
}
