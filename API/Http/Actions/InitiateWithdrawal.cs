using FullSerializer;
using UnityEngine;
using static Delegates;

public class InitiateWithdrawal
{
	public static void Withdraw(WithdrawalPayload payload, ResponseCallback callback, ResponseFallback fallback)
	{
		HttpClient httpClient = new HttpClient();

		Request request = new Request(HttpClient.Method.POST, Route.CREATE_WITHDRAWAL_ROUTE, payload);

		httpClient.Request(
			request,
			(statusCode, response) => {
                BalanceResponse depositResponse = Deserialize(response);
				callback(depositResponse);
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

	private static BalanceResponse Deserialize(object response)
	{
		var responseJson = (string)response;

		var data = fsJsonParser.Parse(responseJson);

		object deserialized = null;

		serializer.TryDeserialize(data, typeof(BalanceResponse), ref deserialized);

        BalanceResponse serializedData = deserialized as BalanceResponse;

		return serializedData;
	}
}
