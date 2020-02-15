using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;
using static Delegates;

public class ErrorDeserilizer
{
    public static ValidationError DeserializeValidationErrorData(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(ValidationError), ref deserialized);

        ValidationError serializedData = deserialized as ValidationError;

        return serializedData;
    }

    public static GenericError DeserializeGenericErrorData(object response)
    {
        var responseJson = (string)response;

        var data = fsJsonParser.Parse(responseJson);

        object deserialized = null;

        serializer.TryDeserialize(data, typeof(GenericError), ref deserialized);

        GenericError serializedData = deserialized as GenericError;

        return serializedData;
    }
}
