using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class FxInfo
{
    public string fx_file;
    public bool loop;
    public float time;

    public bool position;
    public bool orientation;
    public bool scale;
}

[System.Serializable]
public class FxInfoTable : ScriptableObject, IDataCollection
{
    public Dictionary<string, FxInfo> elements = new Dictionary<string, FxInfo>();

    #region IDataCollection 成员

    public void Add(object data)
    {
        FxInfo fxInfo = data as FxInfo;
        elements.Add(fxInfo.fx_file, fxInfo);
    }

    #endregion

    public FxInfo GetById(string id)
    {
        return elements[id];
    }

}