using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUI : MonoBehaviour {

    private GameObject sensorObj;
    private bool showing;


    void Start()
    {
        sensorObj = this.gameObject;
        sensorObj.GetComponentInChildren<Canvas>().enabled = false;
    }

    // Use this for initialization
    void OnEnable()
    {
        GazeGestureManager.OnFocused += ToggleUI;
        showing = false;
    }

    void OnDisable()
    {
        GazeGestureManager.OnFocused -= ToggleUI;
    }

    void ToggleUI()
    {
        if (GazeGestureManager.Instance.FocusedObject == sensorObj)
        {
            if (!showing)
            {
                sensorObj.GetComponentInChildren<Canvas>().enabled = true;
                showing = true;
            }
            else
            {
                sensorObj.GetComponentInChildren<Canvas>().enabled = false;
                showing = false;
            }
        }
        else
        {
            sensorObj.GetComponentInChildren<Canvas>().enabled = false;
            showing = false;
        }
    }

}
