using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowCommands : MonoBehaviour {

    Vector3 originalPosition;
    public float openWidth;
    private float remainOpen;
    private Text warning;
    private GameObject warningObj;

    void Start()
    {
        originalPosition = this.transform.localPosition;
        remainOpen = openWidth;
        warningObj = GameObject.Find("warningUI");
        warning = warningObj.GetComponentInChildren<Text>();
        warning.enabled = false;
    }

	// Use this for initialization
	void OnSelect () {
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
	}

    void OnOpen()
    {
        gameObject.transform.Translate(0, openWidth, 0);
        remainOpen = 0.0f;
        warning.enabled = false;
    }

    void OnClose()
    {
        /*
        var rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.transform.Translate(0, -openWidth, 0);
        }
        this.transform.localPosition = originalPosition;
        */

        gameObject.transform.Translate(0, -(openWidth - remainOpen), 0);
        remainOpen = openWidth;
        warning.enabled = false;
    }

    void OnQuarter()
    {
        var opening = 0.25f * openWidth;

        if (remainOpen >= opening)
        {
            gameObject.transform.Translate(0, opening, 0);
            remainOpen -= opening;
        }
        else
        {
            warning.text = "not enough space";
            warning.enabled = true;
        }
    }

    void OnHalf()
    {
        var opening = 0.5f * openWidth;

        if (remainOpen >= opening)
        {
            gameObject.transform.Translate(0, opening, 0);
            remainOpen -= opening;
        }
        else
        {
            warning.text = "not enough space";
            warning.enabled = true;
        }
    }

    void OnThreeQuarters()
    {
        var opening = 0.75f * openWidth;

        if (remainOpen >= opening)
        {
            gameObject.transform.Translate(0, 0.75f * openWidth, 0);
            remainOpen -= opening;
        }
        else
        {
            warning.text = "not enough space";
            warning.enabled = true;
        }
    }
}
