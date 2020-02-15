using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FullSerializer;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using static StatusCodes;

public class HttpClient
{
    public enum Method {
        POST,
        GET,
        DELETE,
        PATCH,
        PUT,
        HEAD
    };

    private static string baseUri = "http://naijawhot.herokuapp.com/api/v1";

    private static int timeout = 10;

    private static string JWT_SECRET = "qCKunjSkRcDa7cJML5Hdpd2YKvypOmZiUDspDE7jD27vmUyQqItgH8EQ5AprHgCP";

    private MonoBehaviour monoBehaviourContext;

    public HttpClient()
    {
        this.monoBehaviourContext = State.monoBehaviour;
    }

    public delegate void Callback(long statusCode, object response);

    public delegate void Fallback(long statusCode, object error);

    public void Request(Request request, Callback callback, Fallback fallback)
    {
        monoBehaviourContext.StartCoroutine(Interceptors(request.method, request.uri, request.payload, request.pathParams, callback, fallback));
    }

    public void Resend(Request request, Callback callback, Fallback fallback)
    {
        monoBehaviourContext.StartCoroutine(Interceptors(request.method, request.uri, request.payload, request.pathParams, callback, fallback));
    }


    private IEnumerator Interceptors(Method method, string uri, Payloads payload = null, Dictionary<string, string> pathParams = null, Callback callback = null, Fallback fallback = null)
    {
        UnityWebRequest webRequest = new UnityWebRequest(baseUri + uri, method.ToString());

        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.SetRequestHeader("Authorization", "Bearer " + Token.GetToken());


        if (payload != null)
        {
           byte[] bodyRaw = Encoding.UTF8.GetBytes(PostSerializer.SerializePostData(payload));

            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        webRequest.downloadHandler = new DownloadHandlerBuffer();


        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            if (webRequest.responseCode == 0)
            {

                webRequest.Abort();

                Request request = new Request(method, uri, payload, pathParams);

                Resend(request, callback, fallback);
            }
        }

        else if (webRequest.isHttpError)
        {
            if (webRequest.responseCode == CODE_BLACKLISTED_USER)
            {
                // Save Request, Callback and Fallback
                State.PendingFullRequestWhileRefreshingToken = new FullRequest(
                    new Request(method, uri, payload, pathParams),
                    callback, fallback
                 );

                RefreshToken.Refresh();
            }

            else
            {
                var responseJson = webRequest.downloadHandler.text;

                fallback(webRequest.responseCode, responseJson);
            }

        }

        else
        {
            var responseJson = webRequest.downloadHandler.text;

            callback(webRequest.responseCode, responseJson);
        }
    }
}
