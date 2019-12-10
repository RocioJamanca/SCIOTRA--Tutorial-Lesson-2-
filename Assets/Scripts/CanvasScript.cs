using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    bool onOf;
    private void Start()
    {
        onOf = true;
    }
    public void IncrementRadiusButton(int i)
    {
        Singleton.GetInstance().IncrementActionRadius(i);
        GameObject o = GameObject.Find("DebugTextRadius");
        o.GetComponent<Text>().text = "R: " + Singleton.GetInstance().GetActionRadius();
    }
    public void DecrementRadiusButton(int i)
    {
        Singleton.GetInstance().DecrementActionRadius(i);
        GameObject o = GameObject.Find("DebugTextRadius");
        o.GetComponent<Text>().text = "R: " + Singleton.GetInstance().GetActionRadius();
    }

    public void OnOffFakeGps()
    {
        if (onOf) {
            Singleton.GetInstance().SetFakegpsOn();
            onOf = false;
        }
        else
        {
            Singleton.GetInstance().SetFakeGpsOff();
            onOf = true;
        }
    }
}
