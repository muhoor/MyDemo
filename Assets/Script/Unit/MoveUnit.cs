using UnityEngine;
using System.Collections;

public class MoveUnit : MonoBehaviour {

    public enum UnitStatus
    {
        attack,
        stand,
        injured,
        move,
    }


    protected bool movable = true;                  //控制是否可以移动，比如攻击中不能移动
    protected UnitStatus status = UnitStatus.stand; //当前状态
    protected float speed = 0.1f;                   //移动速度

    private float moveX = 0;
    private float moveZ = 0;
    private float lerpY = 0;

	// Use this for initialization
	void Start () {
        unitStart();
    }
	
	// Update is called once per frame
	void Update () {
        unitUpdate();
    }

    void FixedUpdate()
    {
        unitFixedUpdate();
    }

    protected virtual void unitStart()
    {

    }

    protected virtual void unitUpdate()
    {

    }

    protected virtual void unitFixedUpdate()
    {
        updatePosition();
    }

    protected void updatePosition()
    {
        if (!movable)
        {
            return;
        }
        if(moveX == 0 && moveZ == 0)
        {
            status = UnitStatus.stand;
            return;
        }
        setDirection();
        float x = transform.localPosition.x + moveX;
        float z = transform.localPosition.z + moveZ;
        
        float y = MapInfoManager.getInstance().getMapY(x, z);
        if(y != MapInfoManager.NotExist)
        {
            status = UnitStatus.move;
            if (lerpY == 0) lerpY = y;
            lerpY = Mathf.Lerp(lerpY, y, 0.1f);
            transform.localPosition = new Vector3(x, lerpY, z);
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(x,y,x), 1);
        }
        else
        {
            status = UnitStatus.stand;
        }
        
    }


    public void move(float x,float z)
    {
        moveX = x * speed;
        moveZ = z * speed;
        
        
    }

    public void setDirection()
    {
        float angle = Mathf.Atan2(moveZ, -moveX) / Mathf.PI *180 - 90;
        transform.localRotation = Quaternion.Euler(0,angle,0);
    }
}
