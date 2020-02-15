using System;

[Serializable]
public class BalanceResponse : IDeserilizable, IResponse
{
	public string balance { get; set; }
}
