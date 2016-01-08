using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILogin : UILayer {
    public Button enterBtn;

    // Use this for initialization
    void Start () {
        enterBtn.onClick.AddListener(delegate () { this.onEnter(enterBtn.gameObject);});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void onEnter(GameObject g)
    {
        MDDebug.Log("enter game");
        ResourceManager.getInstance().loadScene("SC002zhucheng", loadComplete);
        
    }

    private void loadComplete()
    {
        UIManager.getInstance().pop();
    }

    public static UILogin Open()
    {
        UIManager.getInstance().push("Login/UILogin");
        return null;
        //UILogin login = (UILogin)UIManager.getInstance().push("Login/UILogin");
        //return login;
    }
}
