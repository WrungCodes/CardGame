using System;

[Serializable]
public class ResendEmailResponse : IDeserilizable, IResponse
{
    public string message { get; set; }
    public UserModel user { get; set; }
}