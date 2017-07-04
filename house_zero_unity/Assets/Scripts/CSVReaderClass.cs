using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReaderClass {
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    
    public static List<csvData> Read(string file)
    {
        TextAsset data = Resources.Load(file) as TextAsset;
        //List<csvData> CFDtable = new List<csvData>();
        var CFDtable = new List<csvData>();

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return CFDtable;

        var header = Regex.Split(lines[0], SPLIT_RE);
        //float[] headerValue = new float[header.Length];
        var headerValue = new float[header.Length];

        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var CFD = new csvData();

            for (var j = 0; j < header.Length; j++)
            {
                //string value = values[j];     //without quotation fix
                //value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                string value = values[j].Replace("\"\"", "\"");
                value = UnquoteString(value);
                value = value.Replace("\\", "");

                object finalvalue = value;
                int n;
                float f;

                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                headerValue[j]=((float)finalvalue);              
            }

            CFD.v0 = new Vector3(headerValue[0], headerValue[2], headerValue[1]);               //remember to change (x,y,z) to (x,z,y) for unity coord
            CFD.vvec = new Vector3(headerValue[4], headerValue[6], headerValue[5]);             //remember to change (x,y,z) to (x,z,y) for unity coord
            CFD.vt = CFD.v0 + CFD.vvec;
            CFD.vmag = headerValue[3];

            CFDtable.Add(CFD);
        }
        return CFDtable;
    }

    public static string UnquoteString(string str)
    {
        if (String.IsNullOrEmpty(str))
            return str;

        int length = str.Length;
        if (length > 1 && str[0] == '\"' && str[length - 1] == '\"')
            str = str.Substring(1, length - 2);

        return str;
    }

}

public class csvData
{
    public Vector3 v0;
    public Vector3 vt;
    public Vector3 vvec;
    public float vmag;

    /*
    public csvData(Vector3 v0, Vector3 vt, Vector3 vvec, float vmag)
    {
        this.v0 = v0;
        this.vt = vt;
        this.vvec = vvec;
        this.vmag = vmag;
    }
    */
}

