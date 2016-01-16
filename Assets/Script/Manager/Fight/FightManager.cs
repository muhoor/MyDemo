using UnityEngine;
using System.Collections;

public class FightManager  {

    private static FightManager instance = null;

    public static FightManager getInstance()
    {
        if(instance == null)
        {
            instance = new FightManager();
        }
        return instance;
    }
}
