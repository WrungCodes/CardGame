using FullSerializer;
using UnityEngine;
using static Delegates;

public class PayStake
{
    public static void StakeGame(StakePayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.PAY_STAKE_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                PayStakeResponse payStakeResponse = Deserialize(response);
                callback(payStakeResponse);
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

    private static PayStakeResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(PayStakeResponse), ref deserialized);

        PayStakeResponse serializedData = deserialized as PayStakeResponse;

        return serializedData;
    }
}
