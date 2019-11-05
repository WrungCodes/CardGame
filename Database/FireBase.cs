using UnityEngine;
using System.Collections;
using System;
using FullSerializer;
using Proyecto26;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Networking;

public static class FireBase
{
    private static readonly string projectID = Env.PROJECT_ID;
    private static readonly string key = Env.FIREBASE_API_KEY;
	private static readonly string firebase_baseurl = "https://firestore.googleapis.com/v1beta1/";
    private static fsSerializer serializer = new fsSerializer();

    public static void CreateUser( string token )
    {
        string document = "users";
        string url = firebase_baseurl + $"projects/{projectID}/databases/(default)/documents/{document}";

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = url,
            Headers = new Dictionary<string, string> {
                { "Content-Type", "application/json" },
                {"Authorization", "Bearer " + token }
            },
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Get(currentRequest)
        .Then(response => {

            var responseJson = response.Text;
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

            //Debug.Log(authResponse.documents[1].returnUser().username);
        })
        .Catch(error => {  });

        //Email email = new Email { stringValue = "danii@gmail.com" };
        //LocalId localId = new LocalId { stringValue = "hbdjbhdcdcd" };
        //PhoneNo phone = new PhoneNo { stringValue = "0703483728" };
        //Username username = new Username { stringValue = "danny" };
        //WalletBalance walletBalance = new WalletBalance { doubleValue = 20.12f };

        //UserRootObject rootObject = new UserRootObject
        //{
        //    fields = new UserFields
        //    {
        //        email = new Email { stringValue = "daniiyhbsw@gmail.com" },
        //        local_id = new LocalId { stringValue = "hbddededejbhdcdcd" },
        //        phone_no = new PhoneNo { stringValue = "0713483728" },
        //        username = new Username { stringValue = "dannystone" },
        //        wallet_balance = new WalletBalance { doubleValue = 340.12f }
        //    }
        //};

        //fsData data;
        //serializer.TrySerialize(typeof(UserRootObject), rootObject, out data).AssertSuccessWithoutWarnings();
        //Debug.Log(data);
        //Debug.Log(fsJsonPrinter.CompressedJson(data));

        //RequestHelper currentRequest = new RequestHelper
        //{
        //    Uri = url,

        //    BodyString = data.ToString(),
        //    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" },
        //        {"Authorization", "Bearer " + token }
        //    },
        //    ContentType = "application/json",
        //    EnableDebug = true
        //};

        //RestClient.Post(currentRequest)
        //.Then(res => { Debug.Log(res.Text);})
        //.Catch(err => { });
    }

    public static string UserFieldModel(User user)
    {
        UserFields fields = new UserFields
        {
            email = new Email { stringValue = user.email },
            local_id = new LocalId { stringValue = user.local_id },
            phone_no = new PhoneNo { stringValue = user.local_id },
            username = new Username { stringValue = user.username },
            wallet_balance = new WalletBalance { doubleValue = user.wallet_balance }
        };

        UserRootObject rootObject = new UserRootObject
        {
            fields = fields
        };

        fsData data;
        serializer.TrySerialize(typeof(UserRootObject), rootObject, out data).AssertSuccessWithoutWarnings();

        string post_data = data.ToString();
        return post_data;
    }

    public delegate void PostSuccessCallback(string response);
    public delegate void PostFailedCallback(Exception error);
    public static void Post(string post_data, string document, string token, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        string url = firebase_baseurl + $"projects/{projectID}/databases/(default)/documents{document}";
        Debug.Log(url);
        Debug.Log(post_data);
        RequestHelper currentRequest = new RequestHelper
        {
            Uri = url,
            BodyString = post_data,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                {"Authorization", "Bearer " + token }
            },
            ContentType = "application/json",
            EnableDebug = true,
            Timeout = 200
        };

        RestClient.Post(currentRequest)
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }

    public static void Put(string post_data, string document, string token, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        string url = firebase_baseurl + $"projects/{projectID}/databases/(default)/documents/{document}";
        Debug.Log(url);
        Debug.Log(post_data);
        RequestHelper currentRequest = new RequestHelper
        {
            Uri = url,
            BodyString = post_data,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                {"Authorization", "Bearer " + token }
            },
            ContentType = "application/json",
            EnableDebug = true,
            Timeout = 200
        };

        RestClient.Put(currentRequest)
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }

    public static void Patch(string post_data, string document, string token, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        string url = firebase_baseurl + $"projects/{projectID}/databases/(default)/documents/{document}";
        RestClient.Request(new RequestHelper
        {
            Uri = url,
            Method = "PATCH",
            Timeout = 200,
            Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    {"Authorization", "Bearer " + token }
                },
            BodyString = post_data,
        })
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }

    public delegate void GetSuccessCallback(string  response);
    public delegate void GetFailedCallback(Exception error);
    public static void Get(string document, string token, GetSuccessCallback callback, GetFailedCallback fallback)
    {
        string url = firebase_baseurl + $"projects/{projectID}/databases/(default)/documents/{document}";
        
        RequestHelper currentRequest = new RequestHelper
        {
            Uri = url,
            Headers = new Dictionary<string, string> {
                { "Content-Type", "application/json" },
                //{"Authorization", "Bearer " + token }
            },
            ContentType = "application/json",
            EnableDebug = true
        };
        RestClient.Get(currentRequest)
        .Then(response => { var responseJson = response.Text; callback(responseJson);})
        .Catch(error => { fallback(error); });
    }

    public delegate void PostFormSuccessCallback(string response);
    public delegate void PostFormFailedCallback(Exception error);
    public static void PostForm(WWWForm form_data, string url, string token, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        //Debug.Log(url);
        //Debug.Log(post_data);
        RequestHelper currentRequest = new RequestHelper
        {
            FormData = form_data,
            Uri = url,
            //BodyString = post_data,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                {"Authorization", "Bearer " + token }
            },
            ContentType = "application/json",
            EnableDebug = true
        };

        RestClient.Post(currentRequest)
        .Then(response => { Debug.Log(response.StatusCode); callback(response.Text); })
        .Catch(error => { fallback(error); });
    }



    public static void Put()
    {

    }
}
