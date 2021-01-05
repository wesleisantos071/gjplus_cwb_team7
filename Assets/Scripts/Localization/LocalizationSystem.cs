using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour {
    public enum Language {
        English = 0,
        Portugues
    }

    public static Action onChangeLanguage;

    public static Language language;

    private static Dictionary<string, string> localizedEn;
    private static Dictionary<string, string> localizedPt;

    public static bool isInit;

    public static void Init() {
        CsvLoader csvLoader = new CsvLoader();
        csvLoader.LoadCsv("localization");
        localizedEn = csvLoader.GetDictionaryValues("en");
        localizedPt = csvLoader.GetDictionaryValues("pt");
        language = DataHandler.instance.selectedLanguage;
        isInit = true;
    }

    public static void AlternateLanguage(int newLanguage) {
        switch ((Language)newLanguage) {
            case Language.English:
                language = Language.English;
                break;
            case Language.Portugues:
                language = Language.Portugues;
                break;
        }
        DataHandler.instance.SetSelectedLanguage(language);
        onChangeLanguage();
    }

    public static string GetLocalizedValue(string key) {
        if (!isInit) { Init(); }
        string value = "";
        switch (language) {
            case Language.English:
                localizedEn.TryGetValue(key, out value);
                break;
            case Language.Portugues:
                localizedPt.TryGetValue(key, out value);
                break;
        }
        return value;
    }

}