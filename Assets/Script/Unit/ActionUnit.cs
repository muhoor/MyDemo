using UnityEngine;
using System.Collections;

public class ActionUnit : MoveUnit {
    
    private Transform animTransform = null;
    private Animation anim = null;
    public float attackRange = 1;
    public float attackDelay = 1f;

    private ActionUnit attacker = null;

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
        if (status == UnitStatus.attack)
        {
            return;
        }
        playAction(Action.Stand);
    }

    public void playWalk()
    {
        if (status == UnitStatus.attack)
        {
            return;
        }
        playAction(Action.Walk);
    }

    public void playAttack()
    {
        if (status != UnitStatus.attack)
        {
            status = UnitStatus.attack;
            movable = false;
            Invoke("attackComplete", anim[Action.Attack].length);
            playAction(Action.Attack, WrapMode.Once);
            attack();
        }
    }

    protected virtual void attack()
    {

    }

    private void attackComplete()
    {
        status = UnitStatus.stand;
        movable = true;
        playStand();
    }

    public void showInjured(ActionUnit attacker,float delay)
    {
        this.attacker = attacker;
        Invoke("playInjured", delay);
    }

    private void playInjured()
    {
        status = UnitStatus.injured;
        movable = false;
        playAction(Action.Injured,WrapMode.Once);
        Invoke("injuredComplete", anim[Action.Injured].length);
        injured(attacker);
    }

    protected virtual void injured(ActionUnit attacker)
    {

    }

    private void injuredComplete()
    {
        status = UnitStatus.stand;
        movable = true;
        playStand();
    }

}
