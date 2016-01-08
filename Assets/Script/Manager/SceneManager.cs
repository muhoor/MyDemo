using UnityEngine;
using System.Collections;

public class SceneManager  {

    private static SceneManager instance = null;
    private GameObject unitNode = null;
    private Player player = null;

    public static SceneManager getInstance()
    {
        if(instance == null)
        {
            instance = new SceneManager();
        }
        return instance;
    }

    public void setUnitNode(GameObject node)
    {
        unitNode = node;
    }

    public void sceneLoadComplete()
    {
        player = Player.Create();
        player.transform.SetParent(unitNode.transform);

        foreach(var v in MapInfoManager.getInstance().getMapInfo())
        {
            string k = v.Key;
            float y = v.Value;
            string[] s = k.Split('_');
            player.transform.localPosition = new Vector3(float.Parse(s[0]),y, float.Parse(s[1]));
            break;
        }

        CameraManager.getInstance().setPlayer(player);
    }

    public void controlPlayer(int leftOrRight,int downOrUp)
    {
        if (player != null)
        {
            player.move(leftOrRight, downOrUp);
        }
    }
}
