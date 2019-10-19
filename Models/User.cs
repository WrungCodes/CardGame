using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
    public string username;
    public string phone_no;
    public string email;
    public float wallet_balance;
    public string local_id;

    public User(string _username, string _phone_no, string _email, float _wallet_balance, string _local_id)
	{
        username = _username;
        phone_no = _phone_no;
        email = _email;
        wallet_balance = _wallet_balance;
        local_id = _local_id;
    }

	private string getUserName()
	{
	    return username;
	}

	//private float getUserBalance()
	//{
	//        return 3.0f;
	//    // API call to firebase to get balance
	//}

	//// credits user wallet and returns the final value
	//public float creditUser(float credit_amount)
	//{
	//        return 3.0f;
	//        // API Post to firebase to update user wallet
	//}

	//// debits user wallet and returns the final value
	//public float debitUser(float debit_amount)
	//{
	//        return 3.0f;
	//        // API Post to firebase to update user wallet
	//}

	//// sends user sms
	//public float sendUserMessage(string message)
	//{
	//        return 3.0f;
	//}

	//// send user mail
	//public float sendUserEmail(string title, string message)
	//{
	//        return 3.0f;
	//}
}