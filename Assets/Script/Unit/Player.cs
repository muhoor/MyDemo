using UnityEngine;
using System.Collections;

public class Player : ActionUnit {

    public static Player Create(Transform parent,Vector3 position)
    {
        GameObject g = ResourceManager.getInstance().Load(PathManager.getModelPath("230006_daobing"));
        Player p = g.AddComponent<Player>();
        p.transform.SetParent(parent);
        p.attackRange = 2;
        p.attackDelay = 0.6f;
        p.transform.localPosition = position;
        return p;
    }

    protected override void unitFixedUpdate()
    {
        base.unitFixedUpdate();
        if (status == UnitStatus.move)
        {
            playWalk();
        }
        else if(status == UnitStatus.stand)
        {
            playStand();
        }
    }

    protected override void attack()
    {
        base.attack();
        UnitManager.getInstance().myPlayerAttack();
    }
}
