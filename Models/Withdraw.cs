using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Withdraw
{
    public string id;
    public string transaction_id;
    public string bank_name;
    public string account_number;
    public float amount;
    public string time;
    public string user_local;
    public string user_name;
    public string user_email;
    public string reference;

    public string status;

    public Withdraw(string _id, string _transaction_id, string _bank_name, string _account_number, float _amount, string _reference, User user)
    {
        id = _id;
        transaction_id = _transaction_id;
        bank_name = _bank_name;
        account_number = _account_number;
        amount = _amount;
        time = DateTime.Now.ToString();
        user_local = user.local_id;
        user_name = user.username;
        user_email = user.email;
        status = "pending";
        reference = _reference;
    }

    public Withdraw(string _id, string _transaction_id, string _bank_name, string _account_number, float _amount, string _reference, string _status, string _local_id, string _username, string _email)
    {
        id = _id;
        transaction_id = _transaction_id;
        bank_name = _bank_name;
        account_number = _account_number;
        amount = _amount;
        time = DateTime.Now.ToString();
        user_local = _local_id;
        user_name = _username;
        user_email = _email;
        status = _status;
        reference = _reference;
    }
}
