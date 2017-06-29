using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeatMap : MonoBehaviour {

    private Shader heatmapShader;
    private GameObject floor;
    private Renderer floorRend;
    public float[] sensors = new float[2];

	// Use this for initialization
	void Start () {
        heatmapShader = Shader.Find("HeatMap_Linked");
        floor = GameObject.Find("Quad");
        floorRend = floor.GetComponent<Renderer>();
        floorRend.material.SetColor ("_Color1", Color.white);
        floorRend.material.SetColor("_Color2", Color.white);

    }
	
	// Update is called once per frame
	void Update () {

        
        if (GazeGestureManager.Instance.FocusedObject == floor)
        {
            //floorRend.material.shader = Shader.Find("HeatMap_Linked");
            
            float smin = remap(sensors.Min(), sensors.Min(), sensors.Max(), 0.25f, 0.8f);
            float smax = remap(sensors.Max(), sensors.Min(), sensors.Max(), 0.25f, 0.8f);

            Color colMin = new Color ((1-smax), 0.5f, 0.5f, 1);
            Color colMax = new Color (0.9f, 0.6f, (1-smin), 1);

            floorRend.material.SetColor("_Color1", colMin);

            floorRend.material.SetColor("_Color2", colMax);

            //Debug.Log(floorRend.material.GetFloat)
        }
        
      
	}

    private float remap(float x, float dataMin, float dataMax, float targetMin, float targetMax)
    {
        return targetMin + (targetMax - targetMin) * (x - dataMin) / (dataMax - dataMin);
    }

    void OnHeatMap()
    {
        if (GazeGestureManager.Instance.FocusedObject == floor)
        {
            //floorRend.material.shader = Shader.Find("HeatMap_Linked");
            float smin = remap(sensors.Min(), sensors.Min(), sensors.Max(), 0.25f, 0.8f);
            float smax = remap(sensors.Max(), sensors.Min(), sensors.Max(), 0.25f, 0.8f);

            Color colMin = new Color((1 - smax), 0.5f, 0.5f, 1);
            Color colMax = new Color(0.9f, 0.6f, (1 - smin), 1);

            floorRend.material.SetColor("_Color1", colMin);

            floorRend.material.SetColor("_Color2", colMax);
        }
    }
}
