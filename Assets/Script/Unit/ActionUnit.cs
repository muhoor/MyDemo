using UnityEngine;
using System.Collections;

public class ActionUnit : MoveUnit {

    private Transform animTransform = null;
    private Animation anim = null;
    private bool isAttacking = false;

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

    private void playAction(string action,WrapMode wrapMode = WrapMode.Loop)
    {
        anim.wrapMode = wrapMode;
        anim.CrossFade(action);
    }

    public void playStand()
    {
        if (isAttacking)
        {
            return;
        }
        playAction(Action.Stand);
    }

    public void playWalk()
    {
        if (isAttacking)
        {
            return;
        }
        playAction(Action.Walk);
    }

    public void playAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            movable = false;
            Invoke("attackComplete", anim[Action.Attack].length);
            playAction(Action.Attack, WrapMode.Once);
        }
    }

    private void attackComplete()
    {
        isAttacking = false;
        movable = true;
        playAction(Action.Stand);
        MDDebug.Log("攻击完毕");
    }
}
