using UnityEngine;
using YG;

public static class CorrectLanguage
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {

        YG2.onCorrectLang += On—hangeLang;
    }

    public static void On—hangeLang(string lang)
    {
        if (lang != "ru" && lang != "en")
        {
            YG2.lang = "en";
        }
    }
}