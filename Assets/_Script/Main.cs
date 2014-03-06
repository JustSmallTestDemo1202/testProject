using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


public class Main : MonoBehaviour
{

    public bool enableNet;
    static GameObject uiRoot;

    void Awake()
    {
        //create game instance
        if (!Framework.HasInstance)
        {
            Framework instance = Framework.Instance;

            Framework.AddComponent<DataCenter>();
            Framework.AddComponent<SceneManager>();
            Framework.AddComponent<EffectManager>();

            Framework.ScreenManager.AddScreen<GamePlay>();
       /*
            uiRoot = GameObject.Find("UI Root (3D)");

            if (uiRoot != null)
            {
                GameObject.DontDestroyOnLoad(uiRoot);
            }
            else
            {
                Debug.LogError("cannot find ui root object");
            }
          */

            Framework.ScreenManager.ChangeScreen(typeof(GamePlay));

        }
    }

    IEnumerator Start()
    {
        yield return null;
    }



}


