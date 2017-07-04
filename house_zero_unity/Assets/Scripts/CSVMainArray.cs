﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CSVMainArray : MonoBehaviour {
    
    [SerializeField]

    private List<csvData> dataCFD;
    private List<float> Vmag = new List<float>();
    static Material lineMaterial;
    public float offsetX = 6.8f;
    public float offsetY = 14.5f;
    public float offsetZ = 38f;
    private float vLenMin = 0.25f;
    public float vLen = 0.65f;
    private float vmagMin, vmagMax;
    private bool showCFD = false;

    void Awake()
    {
        dataCFD = CSVReaderClass.Read("xz_plane");                      //put csv file in the resources folder to read
    }

    // Use this for initialization
    void Start () {
        /*
        for (var i=0; i<dataCFD.Count; i++)
        {
            //var c = dataCFD[i];
            //Debug.Log(i + "  " + "v0" + c.v0 + "  " + "vt" + c.vt + "  " + "mag" + c.vmag + "  ");

            if(dataCFD[i].vmag<vmagMin)
            {
                vmagMin = dataCFD[i].vmag;
            }
            else if (dataCFD[i].vmag > vmagMax)
            {
                vmagMax = dataCFD[i].vmag;
            }
        }
        */
        
        foreach(csvData c in dataCFD)
        {
            Vmag.Add(GetVmag(c));
        }

        vmagMin = Vmag.Min();
        vmagMax = Vmag.Max();
    }

    void OnCFDToggle()
    {
        if (showCFD == false)
        {
            showCFD = true;
        }
        else
        {
            showCFD = false;
        }
    }

    public void OnRenderObject()                                        //rendering hirarchy: after all other renderings are done
    {
        CreateLineMaterial();
        lineMaterial.SetPass(0);

        //room translation
        //var room = GameObject.Find("room");
        //Vector3 offset = room.transform.localPosition;

        GL.PushMatrix();
        //GL.MultMatrix(transform.worldToLocalMatrix);                  //make it follow the camera:

        //draw lines
        GL.Begin(GL.LINES);
        foreach (csvData CFD in dataCFD)
        {
            //float a = i / (float)dataR.Count;
            //float angle = a * Mathf.PI/2;
            //GL.Color(new Color(a, 1 - a, 0, 0.8f));

            float a = HeatMap.remap(CFD.vmag, vmagMin, vmagMax, 0, 1);
            Color Vcolor = new Color(0.85f * a, 1 - a, 0, 0.5f);
            GL.Color(Vcolor);

            //GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            var vLenMax = vLenMin = vLen;
            float r = HeatMap.remap(CFD.vmag, vmagMin, vmagMax, vLenMin, vLenMax);
            Vector3 newVT = CFD.v0 + r * CFD.vvec;
            GL.Vertex3(CFD.v0.x - offsetX, CFD.v0.y - offsetY, CFD.v0.z - offsetZ);
            GL.Vertex3(newVT.x - offsetX, newVT.y - offsetY, newVT.z - offsetZ);
        }

        GL.End();
        GL.PopMatrix();
    }

    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            Shader lineShader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(lineShader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;

            //shader config: add alpha blending, turn off back face cull and depth writes
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    private static float GetVmag(csvData c)
    {
        return c.vmag;
    }
    
}