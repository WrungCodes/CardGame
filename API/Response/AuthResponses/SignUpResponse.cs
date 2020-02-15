using System;

[Serializable]
public class SignUpResponse : IDeserilizable, IResponse
{
    public string message { get; set; }
    public string token { get; set; }
}