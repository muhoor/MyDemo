using UnityEngine;
using System.Collections;

public class MoveUnit : MonoBehaviour {

    protected bool isMoving = false;
    protected float speed = 0.1f;
    private float moveX = 0;
    private float moveZ = 0;

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
        if(moveX == 0 && moveZ == 0)
        {
            isMoving = false;
            return;
        }
        float x = transform.localPosition.x + moveX;
        float z = transform.localPosition.z + moveZ;
        
        float y = MapInfoManager.getInstance().getMapY(x, z);
        if(y != MapInfoManager.NotExist)
        {
            MDDebug.Log(y.ToString());
            isMoving = true;
            transform.localPosition = new Vector3(x, y, z);
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(x,y,x), 1);
        }
        else
        {
            isMoving = false;
        }
    }


    public void move(float x,float z)
    {
        moveX = x * speed;
        moveZ = z * speed;
        
    }
}
