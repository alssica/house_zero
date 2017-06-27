using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public World Instance;
    private Vector3 prevPos;
    private float prevY;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartManipulation(Vector3 position)
    //public void StartManipulation(float y)
    {
        prevPos = position;
        prevY = position.y;
    }

    public void UpdateManipulation(Vector3 position)
    {
        //Vector3 delta = (position - prevPos) * 2;
        //gameObject.transform.position += delta;
        //prevPos = position;

        float deltaY = (position.y - prevY) * 2;
        Vector3 delta = new Vector3(0, deltaY, 0);
        gameObject.transform.position += delta;
        prevPos = position;
        prevY = position.y;
    }
}
