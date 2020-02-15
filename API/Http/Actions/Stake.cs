using FullSerializer;
using UnityEngine;
using static Delegates;

public class Stake
{
    public static void StakeGame(StakePayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.STAKE_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                StakeResponse stakeResponse = Deserialize(response);
                callback(stakeResponse);
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

    private static StakeResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(StakeResponse), ref deserialized);

        StakeResponse serializedData = deserialized as StakeResponse;

        return serializedData;
    }
}
