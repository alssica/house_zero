using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPop : MonoBehaviour
{

    public static GameObject TriggerObj;

    void Awake()
    {
        TriggerObj = GameObject.Find("UIpop");
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

    void OnHide()
    {
        TriggerObj.GetComponentInChildren<Canvas>().enabled = false;
    }
}
