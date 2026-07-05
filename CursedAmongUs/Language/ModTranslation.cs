using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CursedAmongUs.Modules;

/// <summary>
/// 偷的TORGM的翻译系统，也就不用csv了，性能差别不大。
/// 按注释来看，年华和梦初应该参与了开发，因此在这里留名
/// </summary>
public static class ModTranslation
{
    public static int defaultLanguage = (int)SupportedLangs.SChinese;
    public static Dictionary<string, Dictionary<int, string>> stringData;

    private const string blankText = "[BLANK]";

    public static void Load()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Stream stream = assembly.GetManifestResourceStream("CursedAmongUs.Resources.stringData.json");

        if (stream == null)
        {
            System.Diagnostics.Debug.WriteLine("[ModTranslation] Failed to load stringData.json");
            stringData = new Dictionary<string, Dictionary<int, string>>();
            return;
        }

        var byteArray = new byte[stream.Length];
        stream.Read(byteArray, 0, (int)stream.Length);
        string json = Encoding.UTF8.GetString(byteArray);
        stream.Close();

        stringData = new Dictionary<string, Dictionary<int, string>>();

        using JsonDocument document = JsonDocument.Parse(json);
        JsonElement root = document.RootElement;

        foreach (JsonProperty property in root.EnumerateObject())
        {
            string stringName = property.Name;
            JsonElement langObj = property.Value;

            if (langObj.ValueKind != JsonValueKind.Object) continue;

            var strings = new Dictionary<int, string>();

            foreach (JsonProperty langEntry in langObj.EnumerateObject())
            {
                if (int.TryParse(langEntry.Name, out int langId))
                {
                    string text = langEntry.Value.GetString();

                    if (!string.IsNullOrEmpty(text))
                    {
                        if (text == blankText)
                            strings[langId] = "";
                        else
                            strings[langId] = text;
                    }
                }
            }

            if (strings.Count > 0)
                stringData[stringName] = strings;
        }

        System.Diagnostics.Debug.WriteLine($"[ModTranslation] loaded {stringData.Count} strings");
    }

    public static string GetString(string key, string def = null)
    {
        string keyClean = Regex.Replace(key, "<.*?>", "");
        keyClean = Regex.Replace(keyClean, "^-\\s*", "");
        keyClean = keyClean.Trim();

        def ??= key;

        if (stringData == null || !stringData.TryGetValue(keyClean, out var data))
            return def;

        int lang = (int)AmongUs.Data.DataManager.Settings.language.CurrentLanguage;

        if (data.TryGetValue(lang, out var result) && !string.IsNullOrEmpty(result))
            return key.Replace(keyClean, result);

        if (data.TryGetValue(defaultLanguage, out var defaultResult) && !string.IsNullOrEmpty(defaultResult))
            return key.Replace(keyClean, defaultResult);

        if (data.TryGetValue(0, out var englishResult) && !string.IsNullOrEmpty(englishResult))
            return key.Replace(keyClean, englishResult);

        return def;
    }

    public static string GetString(string key, string def, params object[] args)
    {
        string text = GetString(key, def);
        try
        {
            return string.Format(text, args);
        }
        catch
        {
            return text;
        }
    }
}

public static class LanguageExtension
{
    public static string Translate(this string key)
    {
        return ModTranslation.GetString(key);
    }

    public static string Translate(this string key, params object[] args)
    {
        return ModTranslation.GetString(key, key, args);
    }
}
