using UnityEngine;
using System.Collections;

public class UILayer: MonoBehaviour{

    public RectTransform rectTransform = null;

    public void destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
