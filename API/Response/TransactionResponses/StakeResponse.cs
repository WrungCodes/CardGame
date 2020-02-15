using System;

[Serializable]
public class StakeResponse : IDeserilizable, IResponse
{
    public string balance { get; set; }
}