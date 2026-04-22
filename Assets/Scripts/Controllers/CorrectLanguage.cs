using UnityEngine;
using YG;

public static class CorrectLang // This class sets the correct language for the game.
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {

        YG2.onCorrectLang += On—hangeLang;
    }

    /// <summary>
    /// Sets english as initial language.
    /// </summary>
    /// <param name="lang">The language code to check and potentially correct.</param>
    public static void On—hangeLang(string lang)
    {
        if (lang != "ru" && lang != "en")
        {
            YG2.lang = "en";
        }
    }
}