using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour {

    public static GameObject TriggerObj;

    void Awake()
    {
        TriggerObj = GameObject.Find("DashboardUI");
        TriggerObj.SetActive(true);
        TriggerObj.GetComponentInChildren<Canvas>().enabled = false;
    }

    void OnDashboard()
    {
        TriggerObj.GetComponentInChildren<Canvas>().enabled = true;
    }

    void OnHide()
    {
        TriggerObj.GetComponentInChildren<Canvas>().enabled = false;
    }
}
