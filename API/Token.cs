using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token
{
    public static void SetToken(string token)
    {
        PlayerPrefs.SetString("token", token);
    }

    public static string GetToken()
    {
        return PlayerPrefs.GetString("token");
    }

    public static bool HasToken()
    {
        return PlayerPrefs.HasKey("token");
    }
}
