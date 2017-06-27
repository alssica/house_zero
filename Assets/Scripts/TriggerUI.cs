using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour {

    public static GameObject TriggerObj;

    void Awake()
    {
        TriggerObj = GameObject.Find("UI");
        TriggerObj.SetActive(true);
        TriggerObj.GetComponentInChildren<Canvas>().enabled = false;
    }

    /*
    void ShowKeyword()
    {
        Instance.SetActive (true);
    }

    void HideKeyword()
    {
        Instance.SetActive(false);
    }
    */

    void OnShow()
    {
        TriggerObj.GetComponentInChildren<Canvas>().enabled = true;
    }

    void OnHide()
    {
        TriggerObj.GetComponentInChildren<Canvas>().enabled = false;
    }
}
