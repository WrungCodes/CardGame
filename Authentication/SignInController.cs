using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;
using Proyecto26;

public static class SignInController
{
    private static readonly string firebase_signin_url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=";
    private const string apiKey = Env.FIREBASE_API_KEY;

    private static fsSerializer serializer = new fsSerializer();

    public delegate void SignInSuccessCallback(User user, string refreshToken, string id_token);
    public delegate void SignInFailedCallback(object error);

    public static void SignIn(string email, string password, SignInSuccessCallback callback, SignInFailedCallback fallback)
    {
         // 'grant_tyrefresh_token&refresh_token=[REFRESH_TOKEN]'
         // '{"email":"[user@example.com]","password":"[PASSWORD]","returnSecureToken":true}'
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":\"{true}\"}}";
        RestClient.Post<SignResponse>(firebase_signin_url + apiKey,payLoad)
        .Then(
        response =>
        {
            DatabaseHandler.GetUser(response.localId, response.idToken,
              (user) =>
              {
                  callback(user, response.refreshToken, response.idToken);
              }
           );
        }).Catch(error => {
           Debug.Log(error);
           fallback(error);
       });
    }
}
