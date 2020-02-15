using FullSerializer;
using UnityEngine;
using static Delegates;

public class ForgotPassword
{
    public static void ForgotUserPassword(ForgotPasswordPayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.FORGOT_PASSWORD_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                ForgotPasswordResponse forgotPasswordResponse = Deserialize(response);
                callback(forgotPasswordResponse);
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

    private static ForgotPasswordResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(ForgotPasswordResponse), ref deserialized);

        ForgotPasswordResponse serializedData = deserialized as ForgotPasswordResponse;

        return serializedData;
    }
}
