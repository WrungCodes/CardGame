using UnityEngine;
using System.Collections;
using System;
using FullSerializer;
using Proyecto26;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Networking;

public static class FormatGetData
{
    private static fsSerializer serializer = new fsSerializer();

    public static List<User> AllUsers(string responseJson)
    {
        var data = fsJsonParser.Parse(responseJson);
        object deserialized = null;
        serializer.TryDeserialize(data, typeof(AllUserRootObject), ref deserialized);
        var authResponse = deserialized as AllUserRootObject;

        List<User> all_users = new List<User>();
        foreach (var user in authResponse.documents)
        {
            all_users.Add(user.returnUser());
            //Debug.Log(user.returnUser().username);
        }
        return all_users;
    }

    public static List<Transaction> AllTransactions(string responseJson)
    {
        var data = fsJsonParser.Parse(responseJson);
        object deserialized = null;
        serializer.TryDeserialize(data, typeof(AllTransactionRootObject), ref deserialized);
        var authResponse = deserialized as AllTransactionRootObject;

        List<Transaction> all_transactions = new List<Transaction>();
        foreach (var user in authResponse.documents)
        {
            all_transactions.Add(user.returnTransaction());
            //Debug.Log(user.returnUser().username);
        }
        return all_transactions;
    }

    public static User GetUser(string responseJson)
    {
        var data = fsJsonParser.Parse(responseJson);
        object deserialized = null;
        serializer.TryDeserialize(data, typeof(UserRootObject), ref deserialized);
        var authResponse = deserialized as UserRootObject;
        return authResponse.fields.returnUser();
    }

    public static Deposit GetDeposit(string responseJson)
    {
        var data = fsJsonParser.Parse(responseJson);
        object deserialized = null;
        serializer.TryDeserialize(data, typeof(DepositRootObject), ref deserialized);
        var authResponse = deserialized as DepositRootObject;
        return authResponse.fields.returnDeposit();
    }

    public static Withdraw GetWithdrawal(string responseJson)
    {
        var data = fsJsonParser.Parse(responseJson);
        object deserialized = null;
        serializer.TryDeserialize(data, typeof(WithdrawalRootObject), ref deserialized);
        var authResponse = deserialized as WithdrawalRootObject;
        return authResponse.fields.returnWithdrawal();
    }
}
