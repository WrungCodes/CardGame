using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;
using UnityEngine.SceneManagement;
using System.Linq;

public class AuthHandler : MonoBehaviour
{
    //private static int refresh_token;

    public GameObject login_panel;
    public GameObject signup_panel;
    //public GameObject setpin_panel;
    //public GameObject pin_panel;
    public GameObject loader_panel;
    public GameObject info_panel;
    //public GameObject otp_panel;

    // INFO PANEL TEXT
    public Text info_panel_text;

    // LOGIN UP
    public InputField login_email;
    public InputField login_password;

    // SIGN UP INPUTFIELD
    public InputField signup_username;
    public InputField signup_email;
    public InputField signup_password;
    public InputField signup_phone;

    // Use this for initialization
    void Start()
    {
        OnStartApp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createAccount()
    {
        login_panel.gameObject.SetActive(false);
        signup_panel.gameObject.SetActive(true);
        loader_panel.gameObject.SetActive(false);
    }

    public void GoToLogin()
    {
        login_panel.gameObject.SetActive(true);
        signup_panel.gameObject.SetActive(false);
        loader_panel.gameObject.SetActive(false);
    }

    private void StartLoader()
    {
        signup_panel.gameObject.SetActive(false);
        login_panel.gameObject.SetActive(false);
        loader_panel.gameObject.SetActive(true);
    }

    public void OnPressSignUp()
    {
        StartLoader();
        SignUpUser(signup_email.text, signup_password.text, signup_username.text);
    }

    public void OnPressSignIn()
    {
        StartLoader();
        LoginUser(login_email.text, login_password.text);
    }

    private void SignUpUser(string email, string password, string username)
    {
        string[] fields = { email, password, username };

        Validate.CheckEmptyFields(
            fields,
            (message) => {
                loader_panel.gameObject.SetActive(false);
                StartCoroutine(showPopUpT(message, "error"));
            },
            () =>
            {
                SignUp.SignUpUser(
                    new SignUpPayload( username, email, password),
                    (response) => {
                        SignUpResponse signUpResponse = (SignUpResponse)response;

                        AuthStatus.SetSignedUp("true");
                        Token.SetToken(signUpResponse.token);

                        StartCoroutine(showPopUpT(signUpResponse.message, "success"));

                        loader_panel.gameObject.SetActive(false);
                        login_panel.gameObject.SetActive(true);
                    },
                    (statusCode, error) => {

                        createAccount();

                        if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                        {
                            ValidationError validationError = (ValidationError)error;
                            StartCoroutine(showPopUpT(validationError.errors.First().Value[0], "error"));                           
                        }
                        else
                        {
                            GenericError genericError = (GenericError)error;
                            StartCoroutine(showPopUpT(genericError.message, "error"));
                        }
                    }
                );
            }
        );
    }

    private void LoginUser(string email, string password)
    {
        Debug.Log(email);
        string[] fields = { email, password };

            Validate.CheckEmptyFields(
            fields,
            (message) => {
                loader_panel.gameObject.SetActive(false);
                StartCoroutine(showPopUpT(message, "error"));
            },
            () =>
            {
                Login.LoginUser(
                    new LoginPayload(email, password),
                    (response) => {
                        LoginResponse loginResponse = (LoginResponse)response;

                        AuthStatus.SetSignedUp("true");
                        Token.SetToken(loginResponse.token);

                        GetProfileOfUser();

                        StartCoroutine(showPopUpT(loginResponse.message, "success"));
                    },
                    (statusCode, error) => {

                        GoToLogin();

                        if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                        {
                            ValidationError validationError = (ValidationError)error;
                            StartCoroutine(showPopUpT(validationError.errors.First().Value[0], "error"));
                        }
                        else
                        {
                            GenericError genericError = (GenericError)error;
                            StartCoroutine(showPopUpT(genericError.message, "error"));
                        }
                    }
                );
            }
        );
    }

    public void GetProfileOfUser()
    {
        GetProfile.GetUserProfile(
            (response) => {
                ProfileResponse profileResponse = (ProfileResponse)response;

                State.UserProfile = profileResponse.profile;

                StartCoroutine(showPopUpT("successfully signed in", "success"));

                StartCoroutine(GotoMainMenu());
            },
            (statusCode, error) => {

                GoToLogin();

                if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                {
                    ValidationError validationError = (ValidationError)error;
                    StartCoroutine(showPopUpT(validationError.errors.First().Value[0], "error"));
                }
                else
                {
                    GenericError genericError = (GenericError)error;
                    StartCoroutine(showPopUpT(genericError.message, "error"));
                }
            }
        );
           
        
    }

    private void showPopUp(string message, string type)
    {
        info_panel.gameObject.SetActive(true);
        switch (type)
        {
            case "error":
                info_panel_text.color = Color.red;
                break;

            case "success":
                info_panel_text.color = Color.black;
                break;
        }
        info_panel_text.text = message;
    }

    IEnumerator showPopUpT(string message, string type)
    {
        showPopUp(message, type);
        yield return new WaitForSeconds(5f);
        info_panel.gameObject.SetActive(false);
    }

	IEnumerator GotoMainMenu()
	{
		StartLoader();
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(sceneBuildIndex:1);
	}

    private void OnStartApp()
    {
        loader_panel.gameObject.SetActive(true);
        if (!AuthStatus.HasSignedUp())
        {
            // SignUp Screen
            createAccount();
        }
        else if(!Token.HasToken())
        {
            // Login Screen
            GoToLogin();
        }
        else
        {
            //Login Automatically
            GetProfileOfUser();
        }
    }
}
