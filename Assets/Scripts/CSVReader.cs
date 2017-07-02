using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, float>> Read(string file)
    {
        var list = new List<Dictionary<string, float>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, float>();
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
                entry[header[j]] = (float) finalvalue;
            }
            list.Add(entry);
        }
        return list;
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