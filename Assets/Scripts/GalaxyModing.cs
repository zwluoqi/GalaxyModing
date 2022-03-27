using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DelaunatorSharp;
using DelaunatorSharp.Unity.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GalaxyModing : MonoBehaviour
{
	
	public class Solar
	{
		public string desc;
	}

	public GameObject descBg;
	public Image Bg;
	public TextMeshProUGUI descText;
	public TextMeshProUGUI text;
	public Material line;
	public Material star;
    private bool isNearestWithOther(Vector3 pos, List<Vector3> others)
    {
	    float num = 400f;
	    for (int i = 0; i < others.Count; i++)
	    {
		    if (Vector3.SqrMagnitude(pos - others[i]) < num)
		    {
			    return true;
		    }
	    }
	    return false;
    }

    private Vector2 minYear = new Vector2(10,100);//亿
    
    private Vector2 HPercent = new Vector2(0.6f,0.7f)*100;
    private Vector2 HePercent = new Vector2(0.2f,0.3f)*100;
    private Vector2 TiePercent = new Vector2(0.01f,0.02f)*100;
    private Vector2 LuoPercent = new Vector2(0.01f,0.02f)*100;
    private Vector2 XituPercent = new Vector2(0.01f,0.02f)*100;
    private Vector2 CPercent = new Vector2(0.01f,0.02f)*100;
    
    //半径
    private Vector2 Radius = new Vector2(100,1000);//Solar,太阳单位
    
    private Vector2 HSpeed = new Vector2(100,1000);//径向速度,公里/秒
    private Vector2 SSpeed = new Vector2(100, 1000);//自行速度,mas/年
    private Vector2 CSpeed = new Vector2(1, 300);//自传速度，公里/秒
    
    //磁场
    private  Vector2 magneticfield = new Vector2(1610,2000);
    
    //质量
    private  Vector2 mass = new Vector2(0.07f,100);//太阳单位
    
    //表面温度
    private  Vector2 surfaceTem = new Vector2(1000,50000);//表面温度,50,000K(开尔文)
    
    //辐射
    private  Vector2 radiate = new Vector2(1000,50000);//自由质子、α粒子、和β粒子
    
    //视星等
    private  Vector2 light = new Vector2(0,8);//自由质子、α粒子、和β粒子
    
    
    private string GetSolarDesc()
    {
	    System.Text.StringBuilder sb = new StringBuilder();
	    sb.AppendLine($"年龄:{(int)Random.Range(minYear.x,minYear.y)}亿年");
	    sb.AppendLine($"质量:{Random.Range(mass.x,mass.y).ToString("F02")}个太阳");
	    sb.AppendLine($"半径:{(int)Random.Range(Radius.x,Radius.y)}个太阳");
	    sb.AppendLine($"辐射强度:{(int)Random.Range(radiate.x,radiate.y)}");
	    sb.AppendLine($"表面温度:{(int)Random.Range(surfaceTem.x,surfaceTem.y)}开尔文");
	    sb.AppendLine($"磁场强度:{(int)Random.Range(magneticfield.x,magneticfield.y)}");
	    sb.AppendLine($"径向速度:{(int)Random.Range(HSpeed.x,HSpeed.y)}公里/秒");
	    sb.AppendLine($"自转速度:{(int)Random.Range(CSpeed.x,CSpeed.y)}公里/秒");
	    sb.AppendLine($"自行速度:{(int)Random.Range(SSpeed.x,SSpeed.y)}mas/年");
	    sb.AppendLine($"化学成分:H{Random.Range(HPercent.x,HPercent.y).ToString("F02")}%," +
	                  $"He{Random.Range(HePercent.x,HePercent.y).ToString("F02")}%," +
	                  $"Fe{Random.Range(TiePercent.x,TiePercent.y).ToString("F02")}%," +
	                  $"Cr{Random.Range(LuoPercent.x,LuoPercent.y).ToString("F02")}%," +
	                  $"稀土元素{Random.Range(XituPercent.x,XituPercent.y).ToString("F02")}%," +
	                  $"C{Random.Range(CPercent.x,CPercent.y).ToString("F02")}%,");
	    
	    return sb.ToString();
    }

    
    private void Start()
    {
	    descBg.gameObject.SetActive(false);
	    StartCoroutine(Create());
    }

    
    private List<Solar> _solars = new List<Solar>();
    IEnumerator Create()
    {
	    int numberOfSolars = 10000;
	    
			ShowText("生成银河系背影");
    		yield return null;
    		int solarCount = numberOfSolars;
    		List<Vector3> allOrgPos = new List<Vector3>();
    		List<GameObject> allOrgGo = new List<GameObject>();
    		var list = GlobalSolarData.Inst.GetSolarPos();
    
    		if (list == null)
            {
	            float min = float.MaxValue;
	            float max = -float.MaxValue;
    			for (int i = 0; i < numberOfSolars; i++)
                {
	                
                    var (num6,num7) = GetSpiralPos();
                    if (num6 < min)
                    {
	                    min = num6;
                    }
                    if (num7 < min)
                    {
	                    min = num7;
                    }
                    if (num6 > max)
                    {
	                    max = num6;
                    }
                    if (num7 > max)
                    {
	                    max = num7;
                    }
                    
                    // var (num6,num7) = GetHealthPos();
                    
    				float x = Mathf.Clamp(240f * num6, -240f, 240f);
    				float y = Mathf.Clamp(240f * num7, -240f, 240f);
    				allOrgPos.Add(new Vector3(x, y, 0f));

                    var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    int number = i;
                    
                    go.transform.localPosition = new Vector3(x, y, 0);
    				allOrgGo.Add(go);
                    Debug.LogWarning($"min:{min},max:{max}");
                    yield return null;
    			}
    		}
    		else
            {
	            int c = 0;
    			for (int i = 0; i < list.Length; i++)
    			{
    				var pos = list[i];
    				allOrgPos.Add(new Vector3(pos.x, pos.y, 0f));
    				var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    int number = i;
    				go.transform.localPosition = new Vector3(pos.x, pos.y, 0);
    				allOrgGo.Add(go);
                    if (c++ > 20)
                    {
	                    c = 0;
	                    yield return null;
                    }
                    
                }
    		}
    		ShowText("生成银河系背影结束");
    
    		bool loop = true;
    		loop = true;
    		while (loop)
    		{
    			yield return null;
    			if (Input.GetMouseButtonUp(0))
    			{
    				loop = false;
    			}
    		}
    
            GlobalSolarData.Inst.UpdateSolarPos(allOrgPos);
    		yield return null;
            ShowText("生成恒星数据");
    		yield return null;
    		List<Vector3> gameSolarPos = new List<Vector3>();
    		List<GameObject> gameSolarGo =  new List<GameObject>();
    		var solarNum = allOrgPos.Count;
    		int maxDestroyNum = 0;
    		for (int j = 0; j < solarCount; j++)
    		{
    			int destroyNum = 0;
    			while (allOrgPos.Count > 0)
    			{
    				int index = UnityEngine.Random.Range(0, allOrgPos.Count);
    				Vector3 vector = allOrgPos[index];
    				var go = allOrgGo[index];
    				allOrgPos.RemoveAt(index);
    				allOrgGo.RemoveAt(index);
    				if (!isNearestWithOther(vector, gameSolarPos))
    				{
    					gameSolarPos.Add(vector);
    					gameSolarGo.Add(go);
    					break;
    				}
    				else
    				{
    					GameObject.Destroy(go);
    					destroyNum++;
    					if (maxDestroyNum < destroyNum)
    					{
    						maxDestroyNum = destroyNum;
    					}
    				}
    			}

                if (destroyNum > 0)
                {
	                yield return null;
                }
            }
    
    		int onceDestroyNum = 0;
    		for (int i = 0; i < allOrgGo.Count; i++)
    		{
    			GameObject.Destroy(allOrgGo[i]);
    			// yield return null;
    			onceDestroyNum++;
    			if (onceDestroyNum > maxDestroyNum)
    			{
    				onceDestroyNum = 0;
    				yield return null;
    			}
    		}

            
            for (int i = 0; i < gameSolarGo.Count; i++)
            {
	            var solar = gameSolarGo[i];
	            int number = i;
	            string desc = GetSolarDesc();
	            solar.AddComponent<TestCollider2D>().onClick = delegate { ShowText($"恒星{number}号"); };
	            solar.AddComponent<TestCollider2D>().onPress = delegate(bool isPress,Vector2 mousePos) { ShowTips(mousePos,isPress,number,desc); };
            }
    
            ShowText("生成恒星数量" + solarNum + ", 最终可玩数量" + gameSolarPos.Count);
    		yield return null;
    		loop = true;
    		while (loop)
    		{
    			yield return null;
    			if (Input.GetMouseButtonUp(0))
    			{
    				loop = false;
    			}
    		}
    		
            ShowText("恒星网格化");
    		yield return null;
    		List<IPoint> list2 = gameSolarPos.Select((Vector3 point) => new Vector2(point.x, point.y)).ToPoints().ToList();
    		Delaunator delaunator = new Delaunator(list2.ToArray());
    		Vector3[] source = delaunator.Points.ToVectors3();

            List<Vector3> list3 = source.ToList();
    		Dictionary<int, List<int>> dictionary = MathUtil.GenPathConnectionsByMesh(delaunator.Triangles);
    		List<GameObject> lines  = new List<GameObject>();
    		foreach (var kv in dictionary)
    		{
    			foreach (var end in kv.Value)
    			{
    				var go = new GameObject();
    				var lineRenderer = go.AddComponent<LineRenderer>();
    				lineRenderer.SetPositions(new Vector3[]{source[kv.Key],source[end]});
    				lineRenderer.startColor = Color.red;
    				lineRenderer.endColor = Color.red;
    				lineRenderer.widthMultiplier = 0.1f;
                    lineRenderer.sharedMaterial = line;
    				lines.Add(lineRenderer.gameObject);
    			}
    		}
            ShowText("恒星网格化结束");
    		loop = true;
    		while (loop)
    		{
    			yield return null;
    			if (Input.GetMouseButtonUp(0))
    			{
    				loop = false;
    			}
    		}
            ShowText("删除冗余恒星连接");
    		yield return null;
    		MathUtil.RemoveRedundantConnections(list3, dictionary);
    		for (int i = 0; i < lines.Count; i++)
    		{
    			GameObject.Destroy(lines[i]);
    		}
    		foreach (var kv in dictionary)
    		{
    			foreach (var end in kv.Value)
    			{
    				var go = new GameObject();
    				var lineRenderer = go.AddComponent<LineRenderer>();
    				lineRenderer.SetPositions(new Vector3[]{source[kv.Key],source[end]});
    				lineRenderer.startColor = Color.red;
    				lineRenderer.endColor = Color.red;
    				lineRenderer.widthMultiplier = 0.1f;
                    lineRenderer.sharedMaterial = line;
    				lines.Add(lineRenderer.gameObject);
    			}
    		}
    		
            ShowText("删除冗余恒星连接结束");
    		loop = true;
    		while (loop)
    		{
    			yield return null;
    			if (Input.GetMouseButtonUp(0))
    			{
    				loop = false;
    			}
    		}
    		
            ShowText("构建银河星区");
    		GalaxyStar galaxyStar = new GameObject("galaxyStar").AddComponent<GalaxyStar>();
            galaxyStar.GetComponent<Renderer>().sharedMaterial = star;
            galaxyStar.color = Color.white;
            galaxyStar.starSize = 0.2f;
            galaxyStar.startSizeRange = 0.5f;
    		galaxyStar.Init(GlobalSolarData.Inst.allSolarPos);
    		loop = true;
    		while (Camera.main.orthographicSize > 20)
    		{
    			yield return null;
    			Camera.main.orthographicSize -= 1;
    		}
            ShowText("构建银河星区结束");
    		loop = true;
    		while (loop)
    		{
    			yield return null;
    			// if (Input.GetMouseButtonUp(0))
    			// {
    			// 	loop = false;
    			// }
    		}
    }

    private void ShowTips(Vector2 mousePos,bool isPress,int number, string desc)
    {
	    descBg.GetComponent<RectTransform>().anchoredPosition = mousePos;
	    descBg.SetActive(isPress);
	    descText.text = desc;
	    Bg.rectTransform.sizeDelta = new Vector2(Bg.rectTransform.sizeDelta.x, descText.preferredHeight + 20);
    }

    private (float,float) GetSpiralPos()
    {
	    
	    float numberOfArms = 5;
	    float armOffsetMax = 0.5f;
	    float rotationFactor = 5;
	    float randomOffsetXY = 0.02f;
	    float num = (float)Math.PI * 2f / (float)numberOfArms;
	    
	    //
	    // float t1 = MathUtil.GaussianRandom/4;
	    // float t2 = MathUtil.GaussianRandom/4;
	    // return (t1, t2);
	    
	    var t = UnityEngine.Random.value;//[0-1]

	    // var r = (Mathf.Pow((float) Math.E, (1.0f * t - Mathf.Floor(1.0f * t)) * 1.0f) - 1) /
	    //         (float) (Math.E-1);
	    
	    
	    var t0 = (numberOfArms * t - Mathf.Floor(numberOfArms * t)) * 2.0f;//[0-2]
	    
	    //根据对数曲线,50%恒星分布在中心点20%区域
        var r = (Mathf.Pow((float) Math.E, 
	                 t0)- 1) /
                (float) (Math.E*Math.E-1);//[0,1]
	    
        float offset = MathUtil.GaussianRandom * armOffsetMax;//[-2,2]
        offset /= 2f;//[-1,1]
        offset *= 1/r;//半斤越小 偏振越大
        offset = Mathf.Pow(offset, 2f);//收敛
        // if (offset < 0f)
        // {
	       //  offset *= -1f;
        // }
        
        // offset = num4;
		
        
        float value = 2f * (float) Mathf.PI * (t)+offset;//[0,2PI]
        float num6 = r*Mathf.Cos(value*4);
	    float num7 = r*Mathf.Sin(value*4);
	    float num8 = UnityEngine.Random.value * randomOffsetXY;
	    float num9 = UnityEngine.Random.value * randomOffsetXY;
	    num6 += num8;
	    num7 += num9;
	    
	    return (num6, num7);
    }

    private (float,float) GetHealthPos()
    {
	    
	    float value = 2f * (float) Mathf.PI * UnityEngine.Random.value - Mathf.PI;
	    float num6 = (16 * Mathf.Pow( (Mathf.Sin(value)) , 3.0f));
	    float num7 = 13 * ((Mathf.Cos(value))) - 5 * Mathf.Cos(2 * value) - 2 * Mathf.Cos(3 * value) -
	                 Mathf.Cos(4 * value);
	    num7 /= 16;
	    return (num6, num7);
    }

    private void ShowText(string msg)
    {
	    Debug.LogWarning(msg);
	    text.text = msg;
    }
}
