using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReaderArray : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    // public static Vector3 [ , ] Read(string file)
    public static ArrayList [ , ] Read(string file)
    {
        //Vector3[,] vector;

        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        //if (lines.Length <= 1) return vector;

        var vector = new ArrayList[lines.Length,3];

        var header = Regex.Split(lines[0], SPLIT_RE);
        float[] headerValue = new float [header.Length];

        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            //var entry = new float();

            for (var j = 0; j < header.Length && j < values.Length; j++)
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

                headerValue[j] = (float) finalvalue;
            }

            vector[i, 0].Add(new Vector3(headerValue[0], headerValue[1], headerValue[2]));
            vector[i, 1].Add(new Vector3(headerValue[4], headerValue[5], headerValue[6]));
            vector[i, 2].Add(headerValue[3]);
        }
        return vector;
    }

    //fixing the quotation marks
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
