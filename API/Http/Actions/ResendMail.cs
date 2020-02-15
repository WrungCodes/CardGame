using FullSerializer;
using UnityEngine;
using static Delegates;

public class ResendMail
{
    public static void ResendUserMail(ResendEmailPayload payload, ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.POST, Route.RESEND_MAIL_ROUTE, payload);

        httpClient.Request(
            request,
            (statusCode, response) => {
                ResendEmailResponse resendEmailResponse = Deserialize(response);
                callback(resendEmailResponse);
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

    private static ResendEmailResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(ResendEmailResponse), ref deserialized);

        ResendEmailResponse serializedData = deserialized as ResendEmailResponse;

        return serializedData;
    }
}
