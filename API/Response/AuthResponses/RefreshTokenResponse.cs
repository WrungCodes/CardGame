using System;

[Serializable]
public class RefreshTokenResponse : IDeserilizable, IResponse
{
    public string token { get; set; }
}