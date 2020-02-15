using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithdrawalPayload : Payloads
{
	public float amount;

	public string bank_uid;

    public string account_number;

	public WithdrawalPayload(float _amount, string _account_number, string _bank_uid)
	{
		this.amount = _amount;
		this.bank_uid = _bank_uid;
        this.account_number = _account_number;
	}
}
