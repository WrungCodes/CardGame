using FullSerializer;
using UnityEngine;
using static Delegates;

public class GetStakeType
{
    public static void GetAllStakeType(ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.GET, Route.GET_STAKE_TYPE_ROUTE);

        httpClient.Request(
            request,
            (statusCode, response) => {
                StakeTypesResponse stakeTypeResponse = Deserialize(response);
                callback(stakeTypeResponse);
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

    private static StakeTypesResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(StakeTypesResponse), ref deserialized);

        StakeTypesResponse serializedData = deserialized as StakeTypesResponse;

        return serializedData;
    }
}
