using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SignResponse
{
    public string localId;
    public string idToken;
    public string refreshToken;
    public string email;
    public bool registered;
    public string expiresIn;
}
