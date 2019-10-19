using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;
using UnityEngine.SceneManagement;

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
    }

    public void GoToLogin()
    {
        login_panel.gameObject.SetActive(true);
        signup_panel.gameObject.SetActive(false);
    }

    private void StartLoader()
    {
        signup_panel.gameObject.SetActive(false);
        login_panel.gameObject.SetActive(false);
        loader_panel.gameObject.SetActive(true);
    }

    public void OnPressSignUp()
    {
        SignUp(signup_email.text, signup_password.text, signup_username.text, signup_phone.text);
    }

    public void OnPressSignIn()
    {
        //StartCoroutine(showPopUpT("Account Created Successfully", "success"));
        SignIn(login_email.text, login_password.text);
    }

    private void SignUp(string email, string password, string username, string phone)
    {
        string[] fields = { email, password, username, password, phone };
        Validate.CheckEmptyFields(
        fields,
        // an empty field found
        (message) => { StartCoroutine(showPopUpT(message, "error")); },

        // no empty fields
        () => {
            string document_path = "users";
            // gets all the users
            FireBase.Get(document_path, "",
                (response) => {
                    List<User> allusers = FormatGetData.AllUsers(response);
                    Validate.CheckIfUserExist(
                        username, phone, email, allusers,
                        // if user details exists
                        (message) => { StartCoroutine(showPopUpT(message[0], "error")); },
                        //if user details dosen't exist
                        () => {
                            SignUpController.SignUp(
                            email, password, username, phone,
                            (user, refresh_token, id_token) => {
                                GlobalState.SetToken(id_token);
                                SetRefreshToken(refresh_token);
                                StartCoroutine(GotoMainMenu());
                                PlayerPrefs.SetString("sign_up_done", "yes");

                                // set the user
                                GlobalState.SetUser(user);
                                StartCoroutine(showPopUpT("Account Created Successfully", "success"));
                            },
                            () => { StartCoroutine(showPopUpT("Error Occurred Making Account", "error")); }
                            );
                        }
                    );
                },
                (error) => { }
                );

            //DatabaseHandler.GetUsers(
            //    (allusers) => {
            //        Validate.CheckIfUserExist(
            //            username, phone, email, allusers,
            //            // if user details exists
            //            (message) => { StartCoroutine(showPopUpT(message[0], "error")); },
            //            //if user details dosen't exist
            //            () => {
            //                SignUpController.SignUp(
            //                email, password, username, phone,
            //                (user, refresh_token, id_token) => {
            //                    GlobalState.SetToken(id_token);
            //                    SetRefreshToken(refresh_token);
            //                    StartCoroutine(GotoMainMenu());
            //                    PlayerPrefs.SetString("sign_up_done", "yes");

            //                    // set the user
            //                    GlobalState.SetUser(user);
            //                    StartCoroutine(showPopUpT("Account Created Successfully", "success")); },
            //                () => { StartCoroutine(showPopUpT("Error Occurred Making Account", "error")); }
            //                );
            //            }
            //        );
            //    }
            //    );
            }
        );
    }

    private void SignIn(string email, string password)
    {     
        string[] fields = { email, password};
        Validate.CheckEmptyFields(
        fields,
        // an empty field found
        (message) => { StartCoroutine(showPopUpT(message, "error")); },
        // no empty fields
        () =>
        {
            SignInController.SignIn(email, password,
              (user, refreshToken, id_token) => {
                  //
                  PlayerPrefs.SetString("sign_up_done", "yes");
                  StartCoroutine(GotoMainMenu());
                  GlobalState.SetToken(id_token);
                  SetRefreshToken(refreshToken);
                  GlobalState.SetUser(user);
                  StartCoroutine(showPopUpT("Welcome " + user.username, "success")); },
              (error) => { StartCoroutine(showPopUpT("Invalid Email or Password", "error")); }
            );
        });
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

    private bool HasToken()
    {
        if (PlayerPrefs.HasKey("refresh_token"))
        {
            return true;
        }
        return false;
    }

    private bool HasSignedUp()
    {
        if (PlayerPrefs.HasKey("sign_up_done"))
        {
            return true;
        }
        return false;
    }

    private string GetRefreshToken()
    {
        return PlayerPrefs.GetString("refresh_token");
    }

    private void SetRefreshToken(string token)
    {
        PlayerPrefs.SetString("refresh_token", token);
    }

    private void OnStartApp()
    {
        loader_panel.gameObject.SetActive(true);
        if (HasSignedUp())
        {
            if (HasToken())
            {
                string token = GetRefreshToken();
                RefreshController.RefreshToken(token,
                    (user, refresh_token, id_token) =>
                    {
                        GlobalState.SetToken(id_token);
                        SetRefreshToken(refresh_token);
                        StartCoroutine(GotoMainMenu());
                        GlobalState.SetUser(user);
                    },
                    (error) =>
                    {
                        loader_panel.gameObject.SetActive(false);
                        Debug.Log(error);
                        login_panel.gameObject.SetActive(true);
                    }
                );
            }
            else
            {
                loader_panel.gameObject.SetActive(false);
                login_panel.gameObject.SetActive(true);
            }
        }
        else
        {
            loader_panel.gameObject.SetActive(false);
            signup_panel.gameObject.SetActive(true);
        }
    }
}
