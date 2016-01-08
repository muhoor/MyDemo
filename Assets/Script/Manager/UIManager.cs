using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager  {

    private static UIManager instance = null;
    private RectTransform UIRoot = null;
    private List<UILayer> layerList = new List<UILayer>();


    public static UIManager getInstance()
    {
        if(instance == null)
        {
            instance = new UIManager();
        }
        return instance;
    }

    public void setUIRoot(RectTransform rt)
    {
        UIRoot = rt;
    }

    private RectTransform getRectTransform(string path)
    {
        GameObject g = ResourceManager.getInstance().Load(path);
        if (g == null)
        {
            MDDebug.LogError(path + " not exist");
        }
        return g.GetComponent<RectTransform>();
    }

    public UILayer push(string path)
    {
        RectTransform rt = UIManager.getInstance().getRectTransform(PathManager.getUIPath(path));
        addChild(rt, UIRoot);
        UILayer layer = rt.GetComponent<UILayer>();
        layer.rectTransform = rt;
        layerList.Add(layer);
        return layer;
    }

    public void pop(UILayer layer)
    {
        if(layer == null)
        {
            MDDebug.LogError("layer is null");
            return;
        }
        layerList.Remove(layer);
        layer.destroy();
    }

    public void pop()
    {
        UILayer layer = layerList[layerList.Count - 1];
        layerList.Remove(layer);
        layer.destroy();
    }

    public void addChild(RectTransform rt ,RectTransform parent = null)
    {
        if(parent == null)
        {
            parent = UIRoot;
        }
        rt.SetParent(parent);
        rt.localPosition = Vector3.zero;
        rt.localScale = Vector3.one;
    }
}
