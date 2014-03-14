using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IDataCollection
{
    void Add(System.Object data);
}

public class DataList<T> :  ScriptableObject, IDataCollection
{
    public List<T> elements = new List<T>();
 
    public void Add(System.Object data)
    {
        elements.Add((T)data);
    }

    public T this[int idx]
    {
        get { return elements[idx]; }
    }

}

