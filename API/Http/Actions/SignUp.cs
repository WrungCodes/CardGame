using FullSerializer;
using UnityEngine;
using static Delegates;

public class SignUp
{
    public static void SignUpUser(SignUpPayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.SIGNUP_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                SignUpResponse signUpResponse = Deserialize(response);
                callback(signUpResponse);
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

    private static SignUpResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(SignUpResponse), ref deserialized);

        SignUpResponse serializedData = deserialized as SignUpResponse;

        return serializedData;
    }
}
