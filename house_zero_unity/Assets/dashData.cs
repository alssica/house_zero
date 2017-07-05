using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashData : MonoBehaviour {
    private Text data;
    public float t = 1, h = 1, s = 1, x=1;

	// Use this for initialization
	void Start () {
        data = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        var windowStatus = WindowCommands.windowOpen;
        //var windowStatus = true;

        float newTemp = 70+ x*Mathf.Cos(Time.time/10+t);
        float newHum = 20 + x*Mathf.Cos(Time.time/10+h);

        if (windowStatus == true)
        {
            data.text = "Temperature : " + newTemp.ToString("F2") + "F" + "\n"
                + "Humidity : " + newHum.ToString("F2") + "%" + "\n"
                + "Lighting: 500lux";
        }
        else
        {
            data.text = "Temperature : 70F" + "\n"
                + "Humidity : 20%" + "\n"
                + "Lighting: 500lux";
        }
	}
}
