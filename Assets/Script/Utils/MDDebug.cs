using UnityEngine;
using System.Collections;

public class MDDebug  {
    public static bool isLog = true;

    public static void Log(object msg)
    {
        if(isLog)
            Debug.Log(msg);
    }

    public static void LogError(object msg)
    {
        if(isLog)
            Debug.LogError(msg);
    }
}
