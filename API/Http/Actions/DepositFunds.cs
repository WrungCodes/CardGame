using FullSerializer;
using UnityEngine;
using static Delegates;

public class DepositFunds
{
    public static void DepositUserFunds(DepositPayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.DEPOSIT_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                DepositResponse depositResponse = Deserialize(response);
                callback(depositResponse);
            },
            (statusCode, error) => {
                if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                {
                    ValidationError validationError = ErrorDeserilizer.DeserializeValidationErrorData(error);
                    fallback(statusCode, validationError);
                }
                else
                {
                    GenericError genericError = ErrorDeserilizer.DeserializeGenericErrorData(error);
                    fallback(statusCode, genericError);
                }
            }
        );
    }

    private static DepositResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(DepositResponse), ref deserialized);

        DepositResponse serializedData = deserialized as DepositResponse;

        return serializedData;
    }
}
