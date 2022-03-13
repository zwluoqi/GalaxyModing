using System.Collections.Generic;
using UnityEngine;

public class MathUtil
{
    /// <summary>
    /// [-1,1]
    /// </summary>
    public static float GaussianRandom
    {
        get
        {
            float x1;
            float w;
            do
            {
                x1 = 2f * UnityEngine.Random.value - 1f;
                float x2 = 2f * UnityEngine.Random.value - 1f;
                w = x1 * x1 + x2 * x2;
            }
            while ((double)w >= 1.0 || w == 0.0);
            float num4 = Mathf.Sqrt(-2f * Mathf.Log(w) / w);
            return x1 * num4;
        }
    }


    /// <summary>
    /// 根据三角形信息得到每个点与其他点的连接信息
    /// </summary>
    /// <param name="triangles"></param>
    /// <returns></returns>
    public static Dictionary<int, List<int>> GenPathConnectionsByMesh(int[] triangles)
    {
        Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
        if (triangles.Length % 3 != 0)
        {
            Debug.LogError("该网格索引数量不为3的倍数");
            return null;
        }
        int num = triangles.Length / 3;
        for (int i = 0; i < num; i++)
        {
            int num2 = triangles[i * 3];
            int num3 = triangles[i * 3 + 1];
            int num4 = triangles[i * 3 + 2];
            if (!dictionary.ContainsKey(num2))
            {
                dictionary.Add(num2, new List<int>());
            }
            if (!dictionary.ContainsKey(num3))
            {
                dictionary.Add(num3, new List<int>());
            }
            if (!dictionary.ContainsKey(num4))
            {
                dictionary.Add(num4, new List<int>());
            }
            if (!dictionary[num2].Contains(num3))
            {
                dictionary[num2].Add(num3);
            }
            if (!dictionary[num2].Contains(num4))
            {
                dictionary[num2].Add(num4);
            }
            if (!dictionary[num3].Contains(num2))
            {
                dictionary[num3].Add(num2);
            }
            if (!dictionary[num3].Contains(num4))
            {
                dictionary[num3].Add(num4);
            }
            if (!dictionary[num4].Contains(num2))
            {
                dictionary[num4].Add(num2);
            }
            if (!dictionary[num4].Contains(num3))
            {
                dictionary[num4].Add(num3);
            }
        }
        return dictionary;
    }

    /// <summary>
    /// 删除冗余连接,一个点只能最多个minCount个其他点连接
    /// </summary>
    /// <param name="posDatas"></param>
    /// <param name="posConnects"></param>
    /// <param name="minCount"></param>
    public static void RemoveRedundantConnections(List<Vector3> posDatas, Dictionary<int, List<int>> posConnects,int minCount =3)
    {
        foreach (int key in posConnects.Keys)
        {
            List<int> list = posConnects[key];
            Vector3 pos = posDatas[key];
            if (list.Count <= minCount)
            {
                continue;
            }
            list.Sort(delegate(int left, int right)
            {
                Vector3 vector = posDatas[left];
                Vector3 vector2 = posDatas[right];
                float sqrMagnitude = (pos - vector).sqrMagnitude;
                float sqrMagnitude2 = (pos - vector2).sqrMagnitude;
                return sqrMagnitude.CompareTo(sqrMagnitude2);
            });
            while (list.Count > minCount)
            {
                int key2 = list[list.Count - 1];
                List<int> list2 = posConnects[key2];
                if (list2.Count <= minCount)
                {
                    break;
                }
                list.Remove(key2);
                list2.Remove(key);
            }
        }
    }
}