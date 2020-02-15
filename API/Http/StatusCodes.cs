
public static class StatusCodes
{
    public const long CODE_SUCCESS = 200;

    public const long CODE_CREATED = 201;

    public const long CODE_REDIRECT = 302;

    public const long CODE_SERVER_ERROR = 500;

    public const long CODE_BLACKLISTED_USER = 419; // TOKEN EXPIRED

    public const long HTTP_PAYMENT_REQUIRED = 402; //INSUFFICIENT FUNDS

    public const long HTTP_FORBIDDEN = 403; //FORBINDIN

    public const long HTTP_NOT_FOUND = 404; //NOT FOUND

    public const long HTTP_METHOD_NOT_ALLOWED = 405;

    public const long HTTP_BAD_REQUEST = 400; //SERVER ERROR

    public const long CODE_VALIDATION_ERROR = 422; //VALIDATION ERROR

    public const long HTTP_UNAUTHORIZED = 401; //TOKEN EXPIRED

    public const long HTTP_NOT_ACCEPTABLE = 406; //UNREGISTERD ACCOUNT

    public const long HTTP_EXPECTATION_FAILED = 417; //NO TOKEN PROVIDED

    public const long HTTP_PRECONDITION_FAILED = 412; //INVALID DETAILS

    public const long HTTP_REQUEST_TIMEOUT = 408;

    public const long HTTP_TOO_MANY_REQUESTS = 429;
}
