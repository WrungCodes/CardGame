using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

public class RefreshToken
{
    public static void Refresh()
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.GET, Route.REFRESH_TOKEN);

        httpClient.Request(
            request,

            (statusCode, response) => {

                //SetToken
                RefreshTokenResponse refreshToken = Deserialize(response);

                Token.SetToken(refreshToken.token);  

                //Send Delayed Request
                FullRequest fullRequest = State.PendingFullRequestWhileRefreshingToken;

                httpClient.Request(
                   fullRequest.request,
                   fullRequest.callback,
                   fullRequest.fallback
                );
            },

            (statusCode, error) => {

            }
         );
    }

    private static fsSerializer serializer = new fsSerializer();

    private static RefreshTokenResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(RefreshTokenResponse), ref deserialized);

        RefreshTokenResponse serializedData = deserialized as RefreshTokenResponse;

        return serializedData;
    }
}
