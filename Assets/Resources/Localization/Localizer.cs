using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Localizer
{
    TextAsset locFile;
    const char lineSep = '\n';
    const char entrySurround = '"';
    readonly string[] fieldSep = { "\",\"" };

    public bool LoadLocalizationFile()
    {
        locFile = Resources.Load<TextAsset>("Localization/localization");
        return locFile != null;
    }

    public Dictionary<string, string> GetLocalizationDict(string attrId)
    {
        string[] lines = locFile.text.Split(lineSep);
        int attrIndex = -1;
        string[] headers = lines[0].Split(fieldSep, StringSplitOptions.None);
        Dictionary<string, string> result = new();

        foreach (string h in headers)
        {
            if (h.Contains(attrId))
            {
                attrIndex = Array.IndexOf(headers, h);
                break;
            }
        }

        Regex parser = new(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        foreach (string line in lines)
        {
            string[] fields = parser.Split(line);

            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i];
                fields[i] = field.TrimStart(' ', entrySurround);
                fields[i] = field.TrimEnd(entrySurround);
            }

            if (fields.Length > attrIndex)
            {
                var key = fields[0];

                if (result.ContainsKey(key)) continue;

                var value = fields[attrIndex];

                result.Add(key, value);
            }
        }

        return result;
    }
}