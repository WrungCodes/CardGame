using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DepositController
{
    public static void CreateDeposit(string bank_name, string account_number, float amount, User user, string id_token)
    {
        string id = "ds";
        string transaction_id = "hhhww";
        string time = DateTime.Now.ToString();
        string reference = "ddcdcdcd";
        Deposit deposit = new Deposit(id, transaction_id, bank_name, account_number, amount, reference, user);
        DatabaseHandler.PostDeposits(deposit,
            () => {
                Debug.Log("SuccessFul Deposit");
            },
            id_token
        );
    }

    public static float CreditUserWallet(User user, float amount)
    {
        float new_balance = user.wallet_balance + amount;
        return new_balance;
    }
}
