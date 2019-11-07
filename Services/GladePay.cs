using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Proyecto26;

public class GladePay
{
    private string merchant_key = "123456789"; //Test Keys
    private string merchant_id = "GP0000001"; //Test Keys

    private string glade_pay_url = "https://demo.api.gladepay.com/payment";

    private string glade_pay_withdrawal_url = "https://demo.api.gladepay.com/disburse";


    public string SerializePost(string email, string amount, string card_no, string cvv, string expiry_month, string expiry_year, string pin)
    {
        string currency = "NGN";
        string country = "NG";
        string data = $"{{\"action\":\"initiate\",\"paymentType\":\"card\",\"user\":{{\"email\":\"{email}\"}},\"card\":{{\"card_no\":\"{card_no}\",\"expiry_month\":\"{expiry_month}\",\"expiry_year\":\"{expiry_year}\",\"ccv\":\"{cvv}\",\"pin\":\"{pin}\"}},\"amount\":\"{amount}\",\"country\":\"{country}\",\"currency\":\"{currency}\"}}";
        return data;
    }

    public delegate void PostSuccessCallback(string response);
    public delegate void PostFailedCallback(Exception error);

    public void PostDeposit(
        string email, string amount, string card_no, string cvv, string expiry_month, string expiry_year, string pin,
        PostSuccessCallback callback, PostFailedCallback fallback)
    {
        string post_string = SerializePost( email, amount, card_no, cvv, expiry_month, expiry_year, pin);

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = glade_pay_url,
            BodyString = post_string,
            Headers = new Dictionary<string, string>
            {
                {"Access-Control-Allow-Origin", "*"},
                { "Content-Type", "application/json" },
                { "key", merchant_key },
                { "mid", merchant_id },
            },
            ContentType = "application/json",
            EnableDebug = true,
            
        };

        RestClient.Put(currentRequest)
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }

    public void PostDepositValidate(
        string txnRef, string otp,
        PostSuccessCallback callback, PostFailedCallback fallback)
    {

        string post_string = $"{{\"action\":\"validate\",\"txnRef\":\"{txnRef}\",\"otp\":\"{otp}\"}}";

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = glade_pay_url,
            BodyString = post_string,
            Headers = new Dictionary<string, string>
            {
                {"Access-Control-Allow-Origin", "*"},
                { "Content-Type", "application/json" },
                { "key", merchant_key },
                { "mid", merchant_id },
            },
            ContentType = "application/json",
            EnableDebug = true
        };

        RestClient.Put(currentRequest)
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }

    public void PostWithdrawal(
        string txnRef, string bankCode, string accountNumber, string userName, float amount,
        PostSuccessCallback callback, PostFailedCallback fallback)
    {
        string narration = "Niaja whot withdrawal";
        string post_string = $"{{\"action\":\"transfer\",\"amount\":\"{amount}\",\"bankcode\":\"{bankCode}\",\"accountnumber\":\"{accountNumber}\",\"sender_name\":\"{userName}\",\"narration\":\"{narration}\",\"orderRef\":\"{txnRef}\"}}";

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = glade_pay_withdrawal_url,
            BodyString = post_string,
            Headers = new Dictionary<string, string>
            {
                {"Access-Control-Allow-Origin", "*"},
                { "Content-Type", "application/json" },
                { "key", merchant_key },
                { "mid", merchant_id },
            },
            ContentType = "application/json",
            EnableDebug = true,
            Timeout = 500
        };

        RestClient.Put(currentRequest)
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }

    }
