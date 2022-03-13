using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGlobalData:MonoBehaviour
{
    [System.Serializable]
    public struct Float2
    {
        public float x;
        public float y;

        public Float2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    }
    
    public  static  StaticGlobalData Inst;
    public Vector3[] allSolarPos;

    private void Awake()
    {
        Inst = this;
        var tmp = LocalDataUtil.Load<Float2[]>("solarPos", "data");
        if (tmp != null)
        {
            allSolarPos = new Vector3[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
            {
                allSolarPos[i] = new Vector3(tmp[i].x,tmp[i].y,0);
            }
        }
    }

    public Vector3[] GetSolarPos()
    {
        return allSolarPos;
    }

    public void UpdateSolarPos(List<Vector3> list)
    {
        allSolarPos = new Vector3[list.Count];
        list.CopyTo(allSolarPos);
        Float2[] tmp = new Float2[allSolarPos.Length];
        for (int i = 0; i < allSolarPos.Length; i++)
        {
            tmp[i] = new Float2(allSolarPos[i].x,allSolarPos[i].y);
        }
        LocalDataUtil.Save<Float2[]>("solarPos",tmp,"data");
    }
}