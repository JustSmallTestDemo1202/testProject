using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GamePlay : GameScreen
{
    public GamePlay()
    {
    }

    public override void OnEnter(object param)
    {
        base.OnEnter(param);
    }


    public override void OnGUI()
    {
        base.OnGUI();
        /*
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 80, 400, 100));

        if (GUILayout.Button("Score"))
        {
            Game.ScreenManager.ChangeScreen(typeof(ScoreScreen));
        }

        GUILayout.EndArea();*/
    }
}

