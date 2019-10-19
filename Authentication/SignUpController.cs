using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;
using Proyecto26;

public static class SignUpController
{
    private static readonly string firebase_signup_url = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
    private static readonly string firebase_sms_url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key=";
    private const string apiKey = Env.FIREBASE_API_KEY;

    private static fsSerializer serializer = new fsSerializer();

    public delegate void SignUpSuccessCallback(User user, string refresh_token, string id_token);
    public delegate void SignUpFailedCallback();

    public static void SignUp(string email, string password,string username, string phone_no, SignUpSuccessCallback callback, SignUpFailedCallback fallback) {
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post(firebase_signup_url + apiKey, payLoad)
            .Then(response => {            
                var responseJson = response.Text;
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof( Dictionary<string, string> ), ref deserialized);
                var authResponse = deserialized as Dictionary<string, string>;
                User user = CreateNewUser(username, phone_no, email, authResponse["localId"]);

                string document_path = "users/?documentId=" + user.local_id; //?documentId = 10
                string post_data = FormatPostData.UserFieldModel(user);

                string token = authResponse["idToken"];
                string refresh_token = authResponse["refreshToken"];

                FireBase.Post(post_data, document_path, token,
                (firebase_response) => {
                    callback(user, authResponse["refreshToken"], authResponse["idToken"]);
                    SendEmailVerification(authResponse["localId"]);
                },
                (error) => { fallback(); }
                );
                //DatabaseHandler.PostUser(user, authResponse["localId"],
                //    () => { callback(user, authResponse["refreshToken"], authResponse["idToken"]);
                //      SendEmailVerification(authResponse["localId"]); }, authResponse["idToken"]);
            })
            .Catch(response => {

                fallback();
            });
    }

    private static void SendEmailVerification(string newIdToken)
    {
        var payLoad = $"{{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"{newIdToken}\"}}";
        RestClient.Post(firebase_sms_url + apiKey, payLoad);
    }

    private static User CreateNewUser(string username, string phone_no, string email, string local_id)
    {
        float wallet_balance = 0.00f;
        User user = new User(username, phone_no, email, wallet_balance, local_id);
        return user;
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
