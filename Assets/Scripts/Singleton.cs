using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{

    //meters
    int actionRadius;
    public bool fakeGpsOn;



    private static Singleton instance;


    public static Singleton GetInstance()
    {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
    }

    public Singleton()
    {
        actionRadius = 0;
        fakeGpsOn = true;
    }

    public void SetActionRadius(int r)
    {
        if (r < 500)
        {
            actionRadius = r;
        }
        else
        {
            actionRadius = 500;
        }

    }
    public int GetActionRadius()
    {
        return actionRadius;
    }

    public void IncrementActionRadius(int i)
    {
        actionRadius = actionRadius + i;
        if (actionRadius > 500)
            actionRadius = 500;
    }

    public void DecrementActionRadius(int i)
    {
        actionRadius = actionRadius - i;
        if (actionRadius < 0)
            actionRadius = 0;
    }

    public void SetFakegpsOn()
    {
        fakeGpsOn = true;
    }

    public void SetFakeGpsOff()
    {
        fakeGpsOn = false;
    }
}
            