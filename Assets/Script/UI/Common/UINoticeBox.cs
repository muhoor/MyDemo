using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINoticeBox : UILayer {

    public delegate void OnNBCallBack();

    public Button btn;
    public Text content;

    private string cStr = "123";
    private OnNBCallBack callBack = null;

	// Use this for initialization
	void Start () {
        btn.onClick.AddListener(delegate () { this.onClick(btn.gameObject); });
        content.text = this.cStr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static UINoticeBox Open(string contentStr,OnNBCallBack cb = null)
    {
        UINoticeBox notice = (UINoticeBox)UIManager.getInstance().push("Common/UINoticeBox");
        notice.setInfo(contentStr, cb);
        return notice;
    }

    private void setInfo(string str, OnNBCallBack cb)
    {
        cStr = str;
        callBack = cb;
    }

    private void onClick(GameObject g)
    {
        UIManager.getInstance().pop(this);
        if (callBack != null) callBack();
    }
}
