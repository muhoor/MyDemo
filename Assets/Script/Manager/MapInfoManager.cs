using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapInfoManager  {
    public const float NotExist = -9999;
    private static MapInfoManager instance = null;

    public static MapInfoManager getInstance()
    {
        if(instance == null)
        {
            instance = new MapInfoManager();
        }
        return instance;
    }

    private string sceneName;
    private Dictionary<string, float> dic;

    public Dictionary<string, float> enterScene(string sceneName)
    {
        this.sceneName = sceneName;
        loadMapInfo();
        return dic;
    }

    public Dictionary<string, float> load(string sceneName)
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();

        StreamReader read = new StreamReader(PathManager.getMapInfoPath(sceneName));

        string r;
        while ((r = read.ReadLine()) != null)
        {
            string[] split = r.Split('_');
            dic.Add(split[0] + "_" + split[1], float.Parse(split[2]));
        }
        read.Close();
        MDDebug.Log("map:"+sceneName +" load complete");
        return dic;
    }

    private void loadMapInfo()
    {
        dic = load(sceneName);
    }

    public Dictionary<string, float> getMapInfo()
    {
        return dic;
    }

    public float getMapY(float x,float z)
    {
        int maxX = Mathf.CeilToInt(x);
        int maxZ = Mathf.CeilToInt(z);
        int minX = Mathf.FloorToInt(x);
        int minZ = Mathf.FloorToInt(z);

        return getValue(maxX, maxZ);

        /*float offsetX = x % 1 - 0.5f;
        float offsetZ = z % 1 - 0.5f;
        float angle = Mathf.Atan2(offsetZ, offsetX)/Mathf.PI;
        if(angle > -0.25f && angle < 0.25f)
        {
            //取(maxX,minZ)和(maxX,maxZ)计算
            float minY = getValue(maxX, minZ);
            float maxY = getValue(maxX, maxZ);
            if (minY == NotExist || maxY == NotExist)
            {
                return NotExist;
            }
            return (maxY - minY) * offsetZ + minY;
        }
        else if (angle > 0.25f && angle < 0.75f)
        {
            //取(minX,maxZ)和(maxX,maxZ)计算
            float minY = getValue(minX, maxZ);
            float maxY = getValue(maxX, maxZ);
            if (minY == NotExist || maxY == NotExist)
            {
                return NotExist;
            }
            return (maxY - minY) * offsetX + minY;
        }
        else if(angle > 0.75f || angle < -0.75f)
        {
            //取(minX,minZ)和(minX,maxZ)计算
            float minY = getValue(minX, minZ);
            float maxY = getValue(minX, maxZ);
            if (minY == NotExist || maxY == NotExist)
            {
                return NotExist;
            }
            return (maxY - minY) * offsetZ + minY;
        }
        else
        {
            //取(minX,minZ)和(maxX,minZ)计算
            float minY = getValue(minX, minZ);
            float maxY = getValue(maxX, minZ);
            if (minY == NotExist || maxY == NotExist)
            {
                return NotExist;
            }
            return (maxY - minY) * offsetX + minY;
        }*/
    }

    private float getValue(int x,int z)
    {
        string key = x + "_" + z;
        if (dic.ContainsKey(key))
        {
            return dic[key];
        }
        return NotExist;
    }
}
