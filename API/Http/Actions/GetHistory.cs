using FullSerializer;
using UnityEngine;
using static Delegates;

public class GetHistory
{
    public static void GetUserHistory(ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.GET, Route.GET_HISTORY_ROUTE);

        httpClient.Request(
            request,
            (statusCode, response) => {
                HistoryResponse historyResponse = Deserialize(response);

                callback(historyResponse);
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

    private static HistoryResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(HistoryResponse), ref deserialized);

        HistoryResponse serializedData = deserialized as HistoryResponse;

        return serializedData;
    }
}
