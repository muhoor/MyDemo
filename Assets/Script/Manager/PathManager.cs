using UnityEngine;
using System.Collections;

public class PathManager  {

    public static string getUIPath(string fileName)
    {
        return "UI/Prefab/" + fileName;
    }
    
    public static string getModelPath(string fileName)
    {
        return "Model/ModelMesh/Prefab/" + fileName;
    }

    public static string getMapInfoPath(string fileName)
    {
        return "ExpandFile/MapInfo/" + fileName;
    }
}
