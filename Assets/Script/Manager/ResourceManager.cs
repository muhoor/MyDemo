using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager :MonoBehaviour{

    public delegate void SceneLoadedComplete();
    private SceneLoadedComplete sceneLoadedComplete;

    public static ResourceManager instance = null;

    void Awake()
    {
        instance = this;
    }

    public static ResourceManager getInstance()
    {
        return instance;
    }

    public GameObject Load(string path)
    {
        Object g = Resources.Load(path);
        if(g == null)
        {
            MDDebug.LogError(path+"不存在");
        }
        return GameObject.Instantiate(g) as GameObject;
    }

    public void loadScene(string name,SceneLoadedComplete cb)
    {
        sceneLoadedComplete = cb;
        StartCoroutine(loadScene(name));
    }

    private IEnumerator loadScene(string name)
    {
        yield return Application.LoadLevelAsync(name);
        if (sceneLoadedComplete != null)
        {
            MapInfoManager.getInstance().enterScene(name);
            SceneManager.getInstance().sceneLoadComplete();
            sceneLoadedComplete();
        }
    }
}
