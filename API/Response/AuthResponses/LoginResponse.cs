using System;

[Serializable]
public class LoginResponse : IDeserilizable, IResponse
{
    public string message { get; set; }
    public string token { get; set; }
}