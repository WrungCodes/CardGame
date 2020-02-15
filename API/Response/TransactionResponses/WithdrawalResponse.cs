using System;
using System.Collections.Generic;

[Serializable]
public class WithdrawalResponse : IDeserilizable, IResponse
{
	public List<WithdrawalModel> withdrawals { get; set; }
}
