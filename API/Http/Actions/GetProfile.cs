using FullSerializer;
using UnityEngine;
using static Delegates;

public class GetProfile
{
    public static void GetUserProfile(ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.GET, Route.GET_PROFILE);

        httpClient.Request(
            request,
            (statusCode, response) => {
                ProfileResponse profileResponse = Deserialize(response);
                callback(profileResponse);
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

    private static ProfileResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(ProfileResponse), ref deserialized);

        ProfileResponse serializedData = deserialized as ProfileResponse;

        return serializedData;
    }
}
