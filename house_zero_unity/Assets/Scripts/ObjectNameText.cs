using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectNameText : MonoBehaviour {

    private Text textInstance;

	// Use this for initialization
	void Start () {

        textInstance = this.GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        if (GazeGestureManager.Instance.FocusedObject!= null)
        {
            textInstance.text = GazeGestureManager.Instance.FocusedObject.name;
        }
	}
}
