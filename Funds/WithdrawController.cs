using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class WithdrawController
{
    //public delegate void PostCallback();
    //public static void CreateWithdrawal(string bank_name, string account_number, float amount, User user, string id_token, PostCallback callback )
    //{
    //    string transaction_id = "hhhww";
    //    string id = "ds";
    //    debitUserWallet(user, amount);
    //    //string time = DateTime.Now.ToString();
    //    string reference = "ddcdcdcd";
    //    Withdraw withdraw = new Withdraw(id, transaction_id, bank_name, account_number,amount, reference, user);
    //    DatabaseHandler.PostWithdrawal(withdraw,
    //        () => {
    //            DatabaseHandler.UpdateUserWallet(user, amount,() => { callback(); }, id_token);             
    //            Debug.Log("SuccessFul Withdrawal");
    //        },
    //        id_token
    //    );
    //}

    public delegate void PostSuccessCallback(string response, float newAmount);
    public delegate void PostFailedCallback(Exception error);

    public static void WithdrawToBank(User user, string token, float amount, string bankCode, string bankName, string accountNumber, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        float current_amount = CurrentUserWallet(user);
        float new_amount = DebitUserWallet(user, amount + Env.WITHDRAWAL_FEE);

        GladePay gladePay = new GladePay();
        string tranRef = Generate.UniqueUuid();

        string tranx_id = Generate.UniqueUuid();
        Transaction transaction = new Transaction(tranx_id, "withdrawal", current_amount, new_amount, user.local_id);
        string trans_data = FormatPostData.TransactionFieldModel(transaction);

        gladePay.PostWithdrawal(tranRef, bankCode, accountNumber, user.username, amount,
            (response) =>
            {
                GladePayWithdrawalResponse pay = FormatGetData.GladePayWithdrawal(response);
                user.wallet_balance = new_amount;
                string new_user_data = FormatPostData.UserFieldModel(user);
                //FireBase.Patch(
                //    new_user_data, "users/" + user.local_id, token,
                //    (response3) =>
                //    {
                //        FireBase.Post(
                //            trans_data, "/transactions/?documentId=" + tranx_id, token,
                //            (response1) =>
                //            {
                //                string id = Generate.UniqueUuid();
                //                Withdraw withdraw = new Withdraw(id, tranx_id, bankCode,bankName, accountNumber, amount, pay.txnRef, user);
                //                string withdraw_data = FormatPostData.WithdrawalFieldModel(withdraw);
                //                FireBase.Post(
                //                    withdraw_data, "/withdrawals/?documentId=" + tranx_id, token,
                //                    (response2) => { callback(response, new_amount); },
                //                    (error) => { fallback(error); }
                //                );
                //            },
                //                (error) =>
                //                {
                //                    fallback(error);
                //                }
                //            );
                //    },
                //    (error) => { fallback(error); }
                //);
            },
            (error) => { fallback(error); }
          );
    }

    public static float debitUserWallet(User user, float amount)
    {
        float new_balance = user.wallet_balance - amount;
        return new_balance;
    }

    public static bool CheckIfUserBalanceIsEnough(User user, float amount)
    {
        if (user.wallet_balance > amount)
        {
            return true;
        }
        return false;
    }

    public static float DebitUserWallet(User user, float amount)
    {
        float new_balance = user.wallet_balance - amount;
        return new_balance;
    }

    public static float CurrentUserWallet(User user)
    {
        return user.wallet_balance;
    }
}
