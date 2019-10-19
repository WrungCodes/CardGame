using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;
using Proyecto26;

public static class RefreshController
{
    private static readonly string firebase_refresh_url = "https://securetoken.googleapis.com/v1/token?key=";
    private const string apiKey = Env.FIREBASE_API_KEY;

    private static fsSerializer serializer = new fsSerializer();

    public delegate void RefreshSuccessCallback(User user, string refresh_token, string id_token);
    public delegate void RefreshFailedCallback(object error);

    public static void RefreshToken(string refresh_token, RefreshSuccessCallback callback, RefreshFailedCallback fallback)
    {
        // 'grant_type=refresh_token&refresh_token=[REFRESH_TOKEN]'
        // '{"email":"[user@example.com]","password":"[PASSWORD]","returnSecureToken":true}'
        var payLoad = $"{{\"grant_type\":\"refresh_token\",\"refresh_token\":\"{refresh_token}\"}}";
        //var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post(firebase_refresh_url + apiKey, payLoad)
        .Then(response => {
            
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, string>), ref deserialized);

            var authResponse = deserialized as Dictionary<string, string>;

            string document_path = "users/" + authResponse["user_id"];
            string token = authResponse["id_token"];
            FireBase.Get(document_path, token,
                (firebase_response) => {
                    User user = FormatGetData.GetUser(firebase_response);
                    callback(user, authResponse["refresh_token"], authResponse["id_token"]);
                },
                (error) => {
                    fallback(error);
                }
            );

            //DatabaseHandler.GetUser(authResponse["user_id"], authResponse["id_token"],
            //(user) =>
            //{
            //    callback(user, authResponse["refresh_token"], authResponse["id_token"]);
            //});

        })
        .Catch(error => { fallback(error); });
    }

    //private static void PostUserToDB(User user, string token)
    //{
    //    string document_path = "users/" + user.local_id;
    //    string post_data = FormatPostData.UserFieldModel(user);
    //    FireBase.Post(post_data, document_path, token,
    //        (response) => { },
    //        (error) => { }
    //        );
    //}
}
