using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthStatus
{
    public static void SetSignedUp(string status)
    {
        PlayerPrefs.SetString("has_signed_up", status);
    }

    public static bool HasSignedUp()
    {
        return PlayerPrefs.HasKey("has_signed_up");
    }
}
