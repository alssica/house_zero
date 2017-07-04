using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CSV3DMainArray : MonoBehaviour {

    //[SerializeField]

    private List<csvData> data;
    //csvData[] dataCFD;
    //private List<float> Vmag = new List<float>();
    //float[] Vmag;
    static Material lineMaterial;
    public float offsetX = 8f;
    public float offsetY = 14.5f;
    public float offsetZ = 32f, obsDist = 8f;
    private float vLenMin = 0.25f;
    public float vLen = 0.65f;
    private float vmagMin, vmagMax;
    public bool showCFD;

    void Awake()
    {
        data = CSVReaderClass.Read("3d_field");                      //put csv file in the resources folder to read
    }

    // Use this for initialization
    void Start () {
        /*
        foreach(csvData c in dataCFD)
        {
            Vmag.Add(GetVmag(c));
        }

        vmagMin = Vmag.Min();
        vmagMax = Vmag.Max();
        */
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

    void Update()
    {
        /*
         if (showCFD == true)
         {
             var cameraPos = Camera.main.transform.position;
             print(cameraPos);

             for (var i = 0; i < data.Count(); i++)
             {
                 var c = data[i];
                 if (c.v0.z - offsetZ - 0.1f < Camera.main.transform.position.z || Camera.main.transform.position.z < c.v0.z - offsetZ + 0.1f)
                 {
                     dataCFD[i] = c;
                     Vmag[i] = (GetVmag(c));
                 }
             }
             print(dataCFD.Length);
             print(Camera.main.transform.position.z);

             vmagMin = Vmag.Min();
             vmagMax = Vmag.Max();      
           }
        */
    }

    public List<csvData> updateCFD()
    {
        var cameraPos = Camera.main.transform.position;

        List<csvData> newCFD = new List<csvData>();

        foreach (csvData c in data)
        {
            if (c.v0.z - offsetZ - 0.1f < Camera.main.transform.position.z && Camera.main.transform.position.z < c.v0.z - offsetZ + 0.1f)
            //if (c.v0.z - offsetZ == Camera.main.transform.position.z)
            {
                newCFD.Add(c);  
            }
        }
        //print(newCFD.Count);
        return (newCFD);
    }

    void updateMag()
    {
        List<float> Vmag = new List<float>();
        foreach (csvData CFD in updateCFD())
        {
            Vmag.Add(GetVmag(CFD));
        }

        vmagMin = Vmag.Min();
        vmagMax = Vmag.Max();
    }
    

    public void OnRenderObject()                                        //rendering hirarchy: after all other renderings are done
    {
        if (showCFD == true)
        {
            updateCFD();
            updateMag();

            CreateLineMaterial();
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            //GL.MultMatrix(transform.worldToLocalMatrix);                  //make it follow the camera:

            //draw lines
            GL.Begin(GL.LINES);

            foreach (csvData CFD in updateCFD())
            {

                float a = HeatMap.remap(CFD.vmag, vmagMin, vmagMax, 0, 1);
                Color Vcolor = new Color(0.85f * a, 1 - a, 0, 0.5f);
                GL.Color(Vcolor);

                //GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                var vLenMax = vLenMin = vLen;
                float r = HeatMap.remap(CFD.vmag, vmagMin, vmagMax, vLenMin, vLenMax);
                Vector3 newVT = CFD.v0 + r * CFD.vvec;
                GL.Vertex3(CFD.v0.x - offsetX, CFD.v0.y - offsetY, CFD.v0.z - offsetZ-obsDist);
                GL.Vertex3(newVT.x - offsetX, newVT.y - offsetY, newVT.z - offsetZ-obsDist);
            }

            GL.End();
            GL.PopMatrix();
        }
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
