using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{

    private Text textInstance;

    // Use this for initialization
    void Start()
    {

        textInstance = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var focusedObj = GazeGestureManager.Instance.FocusedObject;

        if (focusedObj != null)
        {
            if (focusedObj.name.Contains("window"))
            {
                textInstance.text = "Say'Open', 'Close', 'Open A Quarter', 'Half', or 'Open Three Quarters'";
            }

            else
            {
                textInstance.text = "nothing is going on here";
            }
        }

    }
}
