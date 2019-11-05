using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Transaction
{
    public string id;
    public float initial_amount;
    public float final_amount;
    public string user_local;
    public string type;
    public string time;

    public Transaction(string _id, string _type, float _initial_amount, float _final_amount, string _user_local)
    {
        id = _id;
        initial_amount = _initial_amount;
        final_amount = _final_amount;
        user_local = _user_local;
        type = _type;
        time = DateTime.Now.ToString();
    }

    public Transaction(string _id, string _type, float _initial_amount, float _final_amount, string _user_local, string _time)
    {
        id = _id;
        initial_amount = _initial_amount;
        final_amount = _final_amount;
        user_local = _user_local;
        type = _type;
        time = _time;
    }
}
