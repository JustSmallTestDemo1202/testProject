﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace IceFire
{


    public class Main : MonoBehaviour
    {

        public bool enableNet;
     //   static GameObject uiRoot;

        void Awake()
        {
            //create game instance
            if (!Framework.HasInstance)
            {
                Framework instance = Framework.Instance;

                Framework.AddComponent<DataCenter>();
                Framework.AddComponent<SceneManager>();
                Framework.AddComponent<EffectManager>();

                Framework.ScreenManager.AddScreen<MainMenu>();
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

                Framework.ScreenManager.ChangeScreen(typeof(MainMenu));

            }
        }

        IEnumerator Start()
        {
            yield return null;
        }



    }
}


