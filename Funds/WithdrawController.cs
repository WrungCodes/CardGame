using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class WithdrawController
{
    public delegate void PostCallback();
    public static void CreateWithdrawal(string bank_name, string account_number, float amount, User user, string id_token, PostCallback callback )
    {
        string transaction_id = "hhhww";
        string id = "ds";
        debitUserWallet(user, amount);
        //string time = DateTime.Now.ToString();
        string reference = "ddcdcdcd";
        Withdraw withdraw = new Withdraw(id, transaction_id, bank_name, account_number,amount, reference, user);
        DatabaseHandler.PostWithdrawal(withdraw,
            () => {
                DatabaseHandler.UpdateUserWallet(user, amount,() => { callback(); }, id_token);             
                Debug.Log("SuccessFul Withdrawal");
            },
            id_token
        );
    }

    public static float debitUserWallet(User user, float amount)
    {
        float new_balance = user.wallet_balance - amount;
        return new_balance;
    }

    public static bool CheckIfUserBalanceIsEnough(User user, float amount)
    {
        //decimal new_balance = user.wallet_balance - amount;
        if (user.wallet_balance > amount)
        {
            return true;
        }
        return false;
    }
}
