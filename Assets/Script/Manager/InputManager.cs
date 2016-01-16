using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private int lefrOrRight = 0;//-1:left,1:right
    private int downOrUp = 0;//-1:down,1:up
                     // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            downOrUp++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downOrUp--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lefrOrRight++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lefrOrRight--;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            downOrUp--;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            downOrUp++;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            lefrOrRight--;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            lefrOrRight++;
        }

        KeyCode keyCode = KeyCode.None;
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyCode = KeyCode.A;
        }

        UnitManager.getInstance().controlPlayer(lefrOrRight, downOrUp, keyCode);
    }
}
