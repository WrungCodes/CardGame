using System;

[Serializable]
public class ForgotPasswordResponse : IDeserilizable, IResponse
{
    public string message { get; set; }
    public UserModel user { get; set; }
}