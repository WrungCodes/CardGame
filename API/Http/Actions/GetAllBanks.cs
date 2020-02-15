using FullSerializer;
using UnityEngine;
using static Delegates;

public class GetAllBanks
{
    public static void AllBanks(ResponseCallback callback, ResponseFallback fallback)
    {
        HttpClient httpClient = new HttpClient();

        Request request = new Request(HttpClient.Method.GET, Route.GET_BANKS_ROUTE);

        httpClient.Request(
            request,
            (statusCode, response) => {
                BanksResponse banksResponse = Deserialize(response);

                callback(banksResponse);
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

    private static BanksResponse Deserialize(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(BanksResponse), ref deserialized);

        BanksResponse serializedData = deserialized as BanksResponse;

        return serializedData;
    }
}
