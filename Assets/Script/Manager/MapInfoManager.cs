using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapInfoManager  {
    public const float NotExist = -9999;
    private static MapInfoManager instance = null;
    private bool showGrid = false;

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

        List<float> list = new List<float>();
        FileStream fs = new FileStream(PathManager.getMapInfoPath(sceneName), FileMode.Open);

        byte[] b = new byte[4];
        for (int index = 0; index < fs.Length / 4; index++)
        {
            fs.Read(b, 0, 4);
            list.Add(System.BitConverter.ToSingle(b, 0));
            if(list.Count == 3)
            {
                if (showGrid)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.9f, 0f, 0.9f);
                    cube.transform.localPosition = new Vector3(list[0], list[2], list[1]);
                }
                dic.Add(list[0] + "_" + list[1], list[2]);
                list.Clear();
            }
        }
        fs.Close();

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

        x = Mathf.RoundToInt(x);
        z = Mathf.RoundToInt(z);
        float result = getValue((int)x, (int)z);

        float offsetX = x % 1 - 0.5f;
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
            
        }
        return result;
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
