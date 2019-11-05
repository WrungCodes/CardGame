using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class HistoryController
{
    //public List<Transaction> transactions;
    public delegate void PostSuccessCallback(List<Transaction> transactions);
    public delegate void PostFailedCallback(Exception error);

    public static void GetAllTransactions(User user, string token, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        string quert_data = FormatQuery.GetQueryData("local_id", user.local_id, "transactions");

        FireBase.Post( quert_data, ":runQuery", token,
            (response) => {
                List<Transaction> transactions = FormatGetData.AllTransactions(response);
                callback(transactions);
            },
            (error) => { fallback(error); }
        );
    }
}
