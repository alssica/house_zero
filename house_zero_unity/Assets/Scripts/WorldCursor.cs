using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour {

    private MeshRenderer meshRenderer;
    private GameObject cursorObj;

	// Use this for initialization
	void Start () {
        //meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        cursorObj = GameObject.Find("cursor");
        meshRenderer = cursorObj.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            meshRenderer.enabled = true;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            meshRenderer.enabled = false;
        }
	}
}
