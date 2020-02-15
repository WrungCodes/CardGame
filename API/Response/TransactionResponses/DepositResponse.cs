using System;

[Serializable]
public class DepositResponse : IDeserilizable, IResponse
{
    public string url { get; set; }
    public string reference { get; set; }
    public string access_code { get; set; }
}