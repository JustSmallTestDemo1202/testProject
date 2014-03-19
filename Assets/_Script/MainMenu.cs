using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MainMenu : GameScreen
{
    public MainMenu()
    {
    }

    public override void OnEnter(object param)
    {
        base.OnEnter(param);
    }


    public override void OnGUI()
    {
        base.OnGUI();
  
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 200, 400, 100));

        if (GUILayout.Button("Start"))
        {
            Framework.ScreenManager.ChangeScreen(typeof(GamePlay));
        }

        GUILayout.EndArea();
    }
}

