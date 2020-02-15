using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DepositController
{
    public static void CreateDeposit(float amount, User user, string id_token)
    {
        string id = "ds";
        string transaction_id = "hhhww";
        string time = DateTime.Now.ToString();
        string reference = "ddcdcdcd";
        Deposit deposit = new Deposit(id, transaction_id, amount, reference, user);
    }

    public static float CreditUserWallet(User user, float amount)
    {
        float new_balance = user.wallet_balance + amount;
        return new_balance;
    }

    public static float CurrentUserWallet(User user)
    {
        return user.wallet_balance; 
    }

    public delegate void PostSuccessCallback(string response);
    public delegate void PostFailedCallback(Exception error);

    public static void DepositWithCard(
        string amount, string card_no, string cvv, string expiry_month, string expiry_year, string pin,
        User user, string id_token, PostSuccessCallback callback, PostFailedCallback fallback 
        )
    {
        GladePay gladePay = new GladePay();
        gladePay.PostDeposit(
            user.email, amount, card_no, cvv, expiry_month, expiry_year, pin,
            (response) => { callback(response); },
            (error) => { fallback(error); }
            );
    }

    public delegate void PostSuccessWalCallback(string response, float amount);

    public static void DepositValidate(
        float amount, string otp, string tranx_ref,
        User user, string id_token, PostSuccessWalCallback callback, PostFailedCallback fallback
    )
    {
        GladePay gladePay = new GladePay();
        gladePay.PostDepositValidate(
            tranx_ref, otp,
            response => {
                float current_amount = CurrentUserWallet(user);
                float new_amount = CreditUserWallet(user, amount);
                string tranx_id = Generate.UniqueUuid();
                Transaction transaction = new Transaction(tranx_id, "deposit", current_amount, new_amount, user.local_id);
                string id = Generate.UniqueUuid();
                Deposit deposit = new Deposit(id, tranx_id, amount, tranx_ref, user);
                string trans_data = FormatPostData.TransactionFieldModel(transaction);
                string deposit_data = FormatPostData.DepositFieldModel(deposit);
                //FireBase.Post(
                //    trans_data, "/transactions/?documentId="+ tranx_id, id_token,
                //    (response1) => {
                //        FireBase.Post(
                //            deposit_data, "/deposits/?documentId="+ tranx_id, id_token,
                //            (response2) => {
                //                user.wallet_balance = new_amount;
                //                string new_user_data = FormatPostData.UserFieldModel(user);
                //                FireBase.Patch(
                //                    new_user_data, "users/"+user.local_id, id_token,
                //                    (response3) => { callback(response2, new_amount); },
                //                    (error) => { fallback(error); }
                //                ); 
                //            },
                //            (error) => { fallback(error); }
                //        );
                //    },
                //    (error) => {
                //        fallback(error);
                //    }
                //);
            },
            error => {
                Debug.Log(error);
                fallback(error);
            }
        );
    }
}
