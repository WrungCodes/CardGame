using FullSerializer;
using UnityEngine;
using static Delegates;

public class GetAllWithdrawals
{
	public static void GetUserWithdrawals(ResponseCallback callback, ResponseFallback fallback)
	{
		HttpClient httpClient = new HttpClient();

		Request request = new Request(HttpClient.Method.GET, Route.GET_WITHDRAWAL_ROUTE);

		httpClient.Request(
			request,
			(statusCode, response) => {
				WithdrawalResponse withdrawalResponse = Deserialize(response);

				callback(withdrawalResponse);
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

	private static WithdrawalResponse Deserialize(object response)
	{
		var responseJson = (string)response;

		var data = fsJsonParser.Parse(responseJson);

		object deserialized = null;

		serializer.TryDeserialize(data, typeof(WithdrawalResponse), ref deserialized);

		WithdrawalResponse serializedData = deserialized as WithdrawalResponse;

		return serializedData;
	}
}
