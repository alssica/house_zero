using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

    private GameObject helper;
    private Canvas helperCanvas;

    void Start()
    {
        helper = GameObject.Find("HelperUI");
        //helper.SetActive(true);
        helperCanvas = helper.GetComponentInChildren<Canvas>();
        helperCanvas.enabled = false;
    }

    void OnEnable()
    {
        GazeGestureManager.OnFocused += PanelShow;
    }

    void OnDisable()
    {
        //GazeGestureManager.OnFocused -= PanelShow;
        GazeGestureManager.OnFocused += PanelHide;
    }

    void PanelShow()
    {
        helperCanvas.enabled = true;
        //var text = helperCanvas.GetComponentInChildren<text>();
    }

    void PanelHide()
    {
        helperCanvas.enabled = false;
    }
}
