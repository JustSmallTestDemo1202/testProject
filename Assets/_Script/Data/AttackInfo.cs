using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[System.Serializable]
public class AttackInfo
{
    public int id;
    public int hitType;
    public float force;
    public string hitEffect;

}

[System.Serializable]
public class AttackInfoTable : DataList<AttackInfo>
{
    public AttackInfo GetById(int id)
    {
        foreach (AttackInfo e in elements)
        {
            if (e.id == id)
            {
                return e;
            }
        }

        return null;
    }


}
