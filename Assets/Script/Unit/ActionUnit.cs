using UnityEngine;
using System.Collections;

public class ActionUnit : MoveUnit {

    private Transform animTransform = null;
    private Animation anim = null;

    protected override void unitStart()
    {
        base.unitStart();
        animTransform = transform.FindChild("bodyctrl");
        if(animTransform == null)
        {
            MDDebug.LogError("bodyctrl is null");
        }
        anim = animTransform.GetComponent<Animation>();
        playStand();
    }

    public void playStand()
    {
        anim.wrapMode = WrapMode.Loop;
        anim.Play("stand");
    }

    public void playWalk()
    {
        anim.wrapMode = WrapMode.Loop;
        anim.Play("walk");
    }
}
