using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    private GameObject helper;
    private Canvas helperCanvas;
    private Text helperText;
    private bool showing;
    public float timer =0;
    int waitingTime = 7;

    void Start()
    {
        helper = this.gameObject;

        helperCanvas = helper.GetComponentInChildren<Canvas>();
        helperCanvas.enabled = false;
        helperText = helperCanvas.GetComponentInChildren<Text>();

        showing = true;
    }

    /*
    void OnEnable()
    {
        GazeGestureManager.OnFocused += ToggleHelper;
        showing = false;
    }

    void OnDisable()
    {
        GazeGestureManager.OnFocused -= ToggleHelper;
    }

    void ToggleHelper()
    {
        if (GazeGestureManager.Instance.FocusedObject != null)
        {
            
            if (!showing)
            {
                helperCanvas.enabled = true;
                UpdateText();
                showing = true;
            }
            else
            {
                helperCanvas.enabled = false;
                showing = false;
            }
        }
        else
        {
            helperCanvas.enabled = false;
            showing = false;
        }
    }
    */

    void Update()
    {
        /*
        timer += Time.deltaTime;

        if (timer< waitingTime)
        {
            UpdateText();
            helperCanvas.enabled = true;
            timer += Time.deltaTime; 
        }
        else 
        {
            helperCanvas.enabled = false;
        }
        */
        if (GazeGestureManager.Instance.FocusedObject != null)
        {
            UpdateText();
            helperCanvas.enabled = true;
        }
    }

    void UpdateText()
    {
        
        if (GazeGestureManager.Instance.FocusedObject.name.Contains("window"))
        {
            helperText.text = "Have your gaze focused on window panel, say 'Open' or 'Close'" + 
                "\n" +"You can also say 'A Quarter Open', 'Half Open', or 'Three Quarters Open'";
            //helperCanvas.GetComponent<Text>().CrossFadeAlpha(0.1f, 7, false);
        }

        if (GazeGestureManager.Instance.FocusedObject.name.Contains("Quad"))
        {
            var showHM = GazeGestureManager.Instance.FocusedObject.GetComponent<HeatMap>().showHM;
            //helperCanvas.GetComponent<Text>().CrossFadeAlpha(0.1f, 7, false);
            if (showHM == true)
            {
                helperText.text = "To turn off heat map, say'Heat Map Off' ";
            }
            else
            {
                helperText.text = "Say'Heat Map' ";
            }
        }

        if (GazeGestureManager.Instance.FocusedObject.name.Contains("room"))
        {
            var showCFD = Camera.main.GetComponent<CSV3DMainArray>().showCFD;
            //helperCanvas.GetComponent<Text>().CrossFadeAlpha(0.1f, 7, false);
            if (showCFD == true)
            {
                helperText.text = "To turn off air flow vector field, say'Air Flow Off' ";
            }
            else
            {
                helperText.text = "Say'Air Flow'";
            }
        }

        if (GazeGestureManager.Instance.FocusedObject.name.Contains("Sensor"))
        {
            helperText.text = "You can also say 'Dashboard' to view all sensor data. Say 'Hide' to hide it.";
            //helperCanvas.GetComponent<Text>().CrossFadeAlpha(0.1f, 7, false);
        }
    }
}

