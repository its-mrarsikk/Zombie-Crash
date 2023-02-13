using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LocalizationSystem
{
    public enum Language
    {
        Russian, English
    }

    public static Language language = Language.Russian;

    private static Dictionary<string, string> localizedRU;
    private static Dictionary<string, string> localizedEN;

    private static bool initialized;

    private static void Init()
    {
        Localizer localizer = new();
        localizer.LoadLocalizationFile();
        localizedRU = localizer.GetLocalizationDict("ru");
        localizedEN = localizer.GetLocalizationDict("en");

#if UNITY_EDITOR
        LogDicts(localizedEN, localizedRU);
#endif

        initialized = true;
    }

    public static LocalizedValue Localize(string key)
    {
        if (!initialized) Init();

        string value = null;
        switch (language)
        {
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
            case Language.Russian:
                localizedRU.TryGetValue(key, out value);
                break;
        }
        return new(key, value, language);
    }

    public static LocalizedValue Localize(string key, Language language)
    {
        if (!initialized) Init();

        string value = null;
        switch (language)
        {
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
            case Language.Russian:
                localizedRU.TryGetValue(key, out value);
                break;
        }
        return new(key, value, language);
    }

#if UNITY_EDITOR
    static void LogDicts(params Dictionary<string, string>[] dicts)
    {
        foreach (var dict in dicts)
        {
            StringBuilder result = new("{");

            foreach (KeyValuePair<string, string> kvp in dict)
            {
                result.Append($"\"{kvp.Key}\": \"{kvp.Value}\", ");
            }

            result.Length -= 2;
            result.Append("}");
            Debug.Log(result.ToString());
        }
    }
#endif
}
