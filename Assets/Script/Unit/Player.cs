using UnityEngine;
using System.Collections;

public class Player : ActionUnit {

    public static Player Create()
    {
        GameObject g = ResourceManager.getInstance().Load(PathManager.getModelPath("230006_daobing"));
        Player p = g.AddComponent<Player>();
        return p;
    }

    protected override void unitFixedUpdate()
    {
        base.unitFixedUpdate();
        if (isMoving)
        {
            playWalk();
        }
        else
        {
            playStand();
        }
    }
}
