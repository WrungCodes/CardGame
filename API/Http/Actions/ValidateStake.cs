using FullSerializer;
using UnityEngine;
using static Delegates;

public class ValidateStake
{
    public static void ValidateStakeAmount(StakePayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.VALIDATE_STAKE_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                ValidateStakeResponse validateResponse = Deserialize(response);
                callback(validateResponse);
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

    private static ValidateStakeResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(ValidateStakeResponse), ref deserialized);

        ValidateStakeResponse serializedData = deserialized as ValidateStakeResponse;

        return serializedData;
    }
}
