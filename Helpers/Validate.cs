using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Validate
{
    public delegate void ValidateSuccessCallback();
    public delegate void ValidateFailedCallback(List<string> message);

    public delegate void IsEmptySuccessCallback();
    public delegate void IsEmptyFailedCallback(string message);

    //delegate ValidateModel Validations( string value, Dictionary<string, User> all_users);

    public static void CheckIfUserExist(string username, string phone, string email, List<User> all_users, ValidateFailedCallback fallback, ValidateSuccessCallback callback)
    {
         var message = new List<string>();

        if (CheckIfUserNameExist(username, all_users).status)
        {
            message.Add(CheckIfUserNameExist(username, all_users).message);
            //fallback(CheckIfUserNameExist(username, all_users).message);
        }

        if (CheckIfUserPhoneExist(phone, all_users).status)
        {
            message.Add(CheckIfUserPhoneExist(phone, all_users).message);
        }

        if (CheckIfUserEmailExist(phone, all_users).status)
        {
            message.Add(CheckIfUserEmailExist(phone, all_users).message);
        }

        if (message.Count <= 0)
        {
            callback();
        }
        else
        {
            fallback(message);
        }
    }

    public static void CheckEmptyFields(string[] fields, IsEmptyFailedCallback fallback, IsEmptySuccessCallback callback)
    {
        string message = "You Have Empty Fields";
        bool isEmpty = false;

        foreach (var field in fields)
        {
            if (field == "")
            {
                isEmpty = true;
                break;
            }
        }

        if (isEmpty)
        {
            fallback(message);
        }
        else
        {
            callback();
        }
    }

    public static ValidateModel CheckIfUserNameExist(string username, List<User> all_users)
    {
        string message;
        ValidateModel response;
        foreach (var user in all_users)
        {
            if (user.username == username)
            {
                message = "";
                response = new ValidateModel(true, message);
                return response;
            }
        }
        message = "";
        response = new ValidateModel(false, message);
        return response;
    }

    public static ValidateModel CheckIfUserPhoneExist(string phone, List<User> all_users)
    {
        string message;
        ValidateModel response;
        foreach (var user in all_users)
        {
            if (user.phone_no == phone)
            {
                message = "";
                response = new ValidateModel(true, message);
                return response;
            }
        }
        message = "";
        response = new ValidateModel(false, message);
        return response;
    }

    public static ValidateModel CheckIfUserEmailExist(string email, List<User> all_users)
    {
        string message;
        ValidateModel response;
        foreach (var user in all_users)
        {
            if (user.email == email)
            {
                message = "";
                response = new ValidateModel(true, message);
                return response;
            }
        }
        message = "";
        response = new ValidateModel(false, message);
        return response;
    }
}
