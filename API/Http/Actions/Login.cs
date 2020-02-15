using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;
using static Delegates;

public class Login
{
    public static void LoginUser(LoginPayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.LOGIN_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                LoginResponse loginResponse = Deserialize(response);
                callback(loginResponse);
            },
            (statusCode, error) => {
                if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                {
                    ValidationError validationError = ErrorDeserilizer.DeserializeValidationErrorData(error);
                    fallback(statusCode, validationError);
                }
                else
                {
                    GenericError genericError  = ErrorDeserilizer.DeserializeGenericErrorData(error);
                    fallback(statusCode, genericError);
                }
            }
        );
    }

    private static LoginResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(LoginResponse), ref deserialized);

        LoginResponse serializedData = deserialized as LoginResponse;

        return serializedData;
    }
}
