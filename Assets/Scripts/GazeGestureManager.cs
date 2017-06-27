using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance { get; private set; }
    public GameObject FocusedObject { get; private set; }

    //public Text dynamicUIText;
    public delegate void FocusedAction();
    public static event FocusedAction OnFocused;


    GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {
       
        Instance = this;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };

        recognizer.StartCapturingGestures();

	}
	
	// Update is called once per frame
	void Update () {
        GameObject oldFocusObject = FocusedObject;

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
            // print(FocusedObject.name);

            /*
                if (dynamicUIText)
                {
                    dynamicUIText.text = FocusedObject.name;
                }

                else
                {
                    UIText.text = "...";
                }
            */
        }
        else
        {
            FocusedObject = null;
        }

        if(FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();

            if (OnFocused != null)
                OnFocused();
        }
	}
}
