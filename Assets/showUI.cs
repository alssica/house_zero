using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUI : MonoBehaviour {

    private bool Showing;

    // Use this for initialization
    private void OnEnable()
    {
        GazeGestureManager.OnFocused += Show;
        Showing = false;
    }

    private void OnDisable()
    {
        GazeGestureManager.OnFocused -= Show;
    }

    void Show()
    {   /*
        GameObject target = this.gameObject;
        if (!Showing)
        {
            target.GetComponentInChildren<Canvas>().enabled = true;
            Showing = true;
        }
        else
        {
            target.GetComponentInChildren<Canvas>().enabled = false;
            Showing = false;
        }
        */
    }
}
