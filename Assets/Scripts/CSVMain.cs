using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CSVMain : MonoBehaviour {

    List<Dictionary<string, float>> dataR;
    float Vmax =0;
    //List<KeyValuePair<string, float>> dataR;

    public float offsetX = 6.8f;
    public float offsetY = 14.5f;
    public float offsetZ = 38f;

    static Material lineMaterial;

    void Awake()
    {
        dataR = CSVReader.Read("xz_plane");
    }

    void Start()
    {
        //filter data within room boundary 
        
        for (var i = 0; i < dataR.Count; i++)
        {   /*
            if (dataR[i]["X"] <= 20 || dataR[i]["X"] >= 25)
            {
                dataR.Remove(dataR[i]);
            }
            */

            if (dataR[i]["Vmeg"] > Vmax)
            {
                Vmax = dataR[i]["Vmeg"];
            }
        }
    }

    //rendering hirarchy: after all other renderings are done
    public void OnRenderObject()
    {
        CreateLineMaterial();
        lineMaterial.SetPass(0);

        //room translation
        //var room = GameObject.Find("room");
        //Vector3 offset = room.transform.localPosition;

        GL.PushMatrix();
        //make it follow the camera:
        //GL.MultMatrix(transform.worldToLocalMatrix);      

        //draw lines
        GL.Begin(GL.LINES);
        for(var i=1; i< dataR.Count; ++i)
        {
            //float a = i / (float)dataR.Count;
            //float angle = a * Mathf.PI/2;
            //GL.Color(new Color(a, 1 - a, 0, 0.8f));

            float a = HeatMap.remap(dataR[i]["Vmeg"], 0, Vmax, 0, 1);
            Color Vcolor = new Color(0.85f*a, 1 - a, 0, 0.5f);
            GL.Color(Vcolor);
            //GL.Color(new Color(1,0.5f,1,1));

            //GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            
            GL.Vertex3(dataR[i]["X"]- offsetX, dataR[i]["Z"] - offsetY, dataR[i]["Y"] - offsetZ);
            GL.Vertex3(dataR[i]["X"] - offsetX + dataR[i]["Vx"], dataR[i]["Z"] - offsetY + dataR[i]["Vz"], dataR[i]["Y"]+ dataR[i]["Vy"] - offsetZ);
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
}

