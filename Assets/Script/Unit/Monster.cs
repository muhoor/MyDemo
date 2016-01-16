using UnityEngine;
using System.Collections;

public class Monster : ActionUnit {

    public static Monster Create(Transform parent,Vector3 position)
    {
        GameObject g = ResourceManager.getInstance().Load(PathManager.getModelPath("230006_daobing"));
        Monster p = g.AddComponent<Monster>();
        p.transform.SetParent(parent);
        p.transform.localPosition = position;
        return p;
    }

    protected override void unitFixedUpdate()
    {
        base.unitFixedUpdate();
        if(status == UnitStatus.stand)
        {
            playStand();
        }
    }

    protected override void injured(ActionUnit attacker)
    {
        base.injured(attacker);
        transform.LookAt(attacker.transform);
        Vector3 v = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(0, v.y, v.z);
    }
}
