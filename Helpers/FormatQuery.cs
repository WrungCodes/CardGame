using UnityEngine;
using System.Collections;
using FullSerializer;
using System.Collections.Generic;
using System;

public static class FormatQuery
{
	private static fsSerializer serializer = new fsSerializer();

	public static string GetQueryData(string key, string value, string document )
	{

		RootQueryObject fields = new RootQueryObject
		{
			structuredQuery = new StructuredQuery
			{
				from = new List<From>
				{
                    new From { collectionId = document }
				},
				where = new Where
				{
                    fieldFilter = new FieldFilter
					{
                        field = new Field { fieldPath = key  },
                        op = "EQUAL",
                        value = new Value { stringValue = value }
					}
				},

                orderBy = new List<OrderBy>
                {
                    new OrderBy
                    {
                        field = new Field { fieldPath = "time"  },
                        direction = "DESCENDING"
                    }
                }
			}
		};

		fsData data;
		serializer.TrySerialize(typeof(RootQueryObject), fields, out data).AssertSuccessWithoutWarnings();

		string query_data = data.ToString();
		return query_data;
	}
}

//{
//    "structuredQuery": {
//        "where" : {
//            "fieldFilter" : { 
//                "field": {"fieldPath": "local_id"}, 
//                "op":"EQUAL", 
//                "value": {"stringValue": "kEVxKtlClcUlzWW4uPRZpAjYdzQ2"}
//            }
//        },
//        "from": [{"collectionId": "transactions"}]
//    }
//}

public class Field
{
	public string fieldPath { get; set; }
}

public class Value
{
	public string stringValue { get; set; }
}

public class FieldFilter
{
	public Field field { get; set; }
	public string op { get; set; }
	public Value value { get; set; }
}

public class Where
{
	public FieldFilter fieldFilter { get; set; }
}

public class From
{
	public string collectionId { get; set; }
}

public class StructuredQuery
{
	public Where where { get; set; }
	public List<From> from { get; set; }
    public List<OrderBy> orderBy { get; set; }
}

public class RootQueryObject
{
	public StructuredQuery structuredQuery { get; set; }
}

[Serializable]
public class OrderBy
{
    public Field field { get; set; }
    public string direction { get; set; }
}


