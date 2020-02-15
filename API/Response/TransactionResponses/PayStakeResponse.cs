using System;

[Serializable]
public class PayStakeResponse : IDeserilizable, IResponse
{
    public string balance { get; set; }
}