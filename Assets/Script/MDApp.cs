using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MDApp : MonoBehaviour {

    public GameObject Main = null;
    public GameObject UI = null;
    public GameObject EventSystem = null;
    public GameObject CamObj = null;
    public RectTransform UIRoot = null; 
    public GameObject UnitNode = null;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(Main);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(EventSystem);
        DontDestroyOnLoad(CamObj);
        DontDestroyOnLoad(UnitNode);
        UIManager.getInstance().setUIRoot(UIRoot);
        UnitManager.getInstance().setUnitNode(UnitNode);
        UILogin.Open();
    }
    // Update is called once per frame
    void Update ()
    {
    }
}
