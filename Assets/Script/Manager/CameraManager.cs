using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public Camera sceneCamera = null;

    private static CameraManager instance = null;

    private Player player = null;

	// Use this for initialization
	void Awake () {
        instance = this;
	}

    void Start()
    {
        setCamera();
    }
	
	// Update is called once per frame
	void Update () {
        setCamera();
    }

    public static CameraManager getInstance()
    {
        return instance;
    }

    public void setPlayer(Player player)
    {
        this.player = player;
    }

    private void setCamera()
    {
        if(player != null)
        {
            Vector3 p = player.transform.localPosition;
            sceneCamera.transform.localPosition = new Vector3(p.x,p.y+3,p.z - 5);
        }
    }
}
