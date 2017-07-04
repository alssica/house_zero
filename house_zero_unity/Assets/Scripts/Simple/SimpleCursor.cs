﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCursor : MonoBehaviour {

    public static SimpleCursor Instance;
    private GameObject cursorObj;
    private float focusDist;
    public GameObject FocusedObj;

	void Awake () {
		if (Instance == null)
        {
            Instance = this;
        }
	}

    void Start()
    {
        focusDist = Camera.main.farClipPlane / 3;
        Vector3 focusTranslate = focusDist * Camera.main.transform.forward;
        Instance.gameObject.transform.position = Camera.main.transform.position+ focusTranslate;
        
    }
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObj = hitInfo.collider.gameObject;
            transform.position = hitInfo.point;
        }
        else
        {
            FocusedObj = null;
            transform.position = headPosition + gazeDirection * 3;
        }
	}
}