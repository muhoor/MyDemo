using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager  {

    private static SceneManager instance = null;

    public static SceneManager getInstance()
    {
        if(instance == null)
        {
            instance = new SceneManager();
        }
        return instance;
    }

    public void sceneLoadComplete()
    {
        UnitManager.getInstance().createMyPlayer();
        for(int i = 0; i < 10; i++)
        {
            UnitManager.getInstance().createRandomMonster(i);
        }
    }

    
}
