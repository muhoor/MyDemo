using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager  {

    private static UnitManager instance = null;

    public static UnitManager getInstance()
    {
        if (instance == null)
        {
            instance = new UnitManager();
        }
        return instance;
    }

    private GameObject unitNode = null;

    private Dictionary<int, ActionUnit> monsterDic = new Dictionary<int, ActionUnit>();
    private Dictionary<int, Player> playerDic = new Dictionary<int, Player>();
    private Player myPlayer = null;

    public void setUnitNode(GameObject node)
    {
        unitNode = node;
    }


    public Player getMyPlayer()
    {
        return myPlayer;
    }

    //创建自己的角色
    public Player createMyPlayer()
    {
        myPlayer = Player.Create(unitNode.transform, getRandomPosition(1));
        CameraManager.getInstance().setPlayer(myPlayer);
        return myPlayer;
    }

    public void controlPlayer(int leftOrRight, int downOrUp, KeyCode keyCode = KeyCode.None)
    {
        if (myPlayer != null)
        {
            myPlayer.move(leftOrRight, downOrUp);
            if (keyCode == KeyCode.A)
            {
                myPlayer.playAttack();
            }
        }
    }


    //创建随机怪物
    public void createRandomMonster(int id)
    {
        Monster monster = Monster.Create(unitNode.transform,getRandomPosition((int)Random.Range(100,1000)));
        monsterDic.Add(id, monster);
    }

    //获取随机坐标
    private Vector3 getRandomPosition(int random)
    {
        int r = 0;
        foreach (var v in MapInfoManager.getInstance().getMapInfo())
        {
            if(r == random)
            {
                string k = v.Key;
                float y = v.Value;
                string[] s = k.Split('_');
                return new Vector3(float.Parse(s[0]), y, float.Parse(s[1]));
            }
            r++;
        }
        return Vector3.zero;
    }

    public void myPlayerAttack()
    {
        foreach(var v in monsterDic)
        {
            if (isAttackable(myPlayer, v.Value))
            {
                MDDebug.Log("攻击到了");
                
                v.Value.showInjured(myPlayer,myPlayer.attackDelay);
            }
        }
    }

    //是否在攻击范围
    public bool isAttackable(ActionUnit player,ActionUnit monster)
    {
        Vector3 v1 = player.transform.localPosition;
        Vector3 v2 = monster.transform.localPosition;
        if(Vector3.Distance(v1, v2) <= player.attackRange)
        {
            if (Vector3.Dot(player.transform.forward,monster.transform.position - player.transform.position) > 0)
            {
                float t = Vector3.Cross(player.transform.forward, monster.transform.position - player.transform.position).y;
                if(Mathf.Abs(t) < 0.5)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
