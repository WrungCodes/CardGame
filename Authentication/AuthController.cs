using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;

public class AuthController : MonoBehaviour
{
    public static fsSerializer serializer = new fsSerializer(); 

    private string firebase_url = "https://cardgame-944de.firebaseio.com/users";
    private string firebase_signup_url = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
    private string firebase_api_key = "AIzaSyBJjoympIt56cFec3EV5yYdD92SYw7daP0";

    public GameObject login_panel;
    public GameObject signup_panel;
    public GameObject setpin_panel;
    public GameObject pin_panel;
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

    // SET PIN
    public InputField pin_input_1;
    public InputField pin_input_2;
    public InputField pin_input_3;
    public InputField pin_input_4;

    // ENTER PIN
    public InputField enter_pin_input_1;
    public InputField enter_pin_input_2;
    public InputField enter_pin_input_3;
    public InputField enter_pin_input_4;

	private Dictionary<string, User> all_users;
	// Start is called before the first frame update
	void Start()
    {
		//signup_panel.gameObject.SetActive(true);
		GetAllUsers();

	}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnSubmitEnterPin()
    {

    }

    public void OnSubmitSetPin()
    {

    }

    public void OnSubmitLogin()
    {

    }

    public void OnSubmitSignUp()
    {
        SignUpUser(signup_password.text, signup_email.text);
        //CreateUser(signup_username.text, signup_phone.text, signup_email.text);
    }

    private void StartLoader()
    {
        signup_panel.gameObject.SetActive(false);
        loader_panel.gameObject.SetActive(true);
    }

    private void StopLoader()
    {
        loader_panel.gameObject.SetActive(false);
    }

    private void CreateUser(string _username, string _phone_no, string _email, string local_id, string idToken)
    {
        float wallet_balance = 0.00f;
        User user = new User(_username, _phone_no, _email, wallet_balance, local_id);
        PostToDatabase(user, idToken);
    }

    private void PostToDatabase(User user, string idToken)
    {
        //StartLoader();
        Debug.Log("started");
        RestClient.Put(firebase_url +"/" + user.local_id + ".json?auth=" + idToken, user);
        Debug.Log("sent");
        //StopLoader();
    }

    private void SignUpUser(string password, string email)
    {
		//if (Validate.CheckEmptyFields(signup_username.text)
	 //       || Validate.CheckEmptyFields(signup_phone.text)
	 //       || Validate.CheckEmptyFields(signup_email.text)
	 //       || Validate.CheckEmptyFields(signup_password.text)

	 //   ){

		//	StartCoroutine(showPopUpT("Incomplete fields, Fill in all fields", "error"));
		//	return;
		//};

		//GetAllUsers();

		//if (Validate.CheckIfUserNameExist(signup_username.text, all_users))
		//{
		//	StartCoroutine(showPopUpT("Username already Taken", "error"));
		//	return;
		//}

		//if (Validate.CheckIfUserPhoneExist(signup_phone.text, all_users))
		//{
		//	StartCoroutine(showPopUpT("Phone Number already Used", "error"));
		//	return;
		//};

		//if (Validate.CheckIfUserEmailExist(signup_email.text, all_users))
		//{
		//	StartCoroutine(showPopUpT("Email already Used", "error"));
		//	return;
		//};

		var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post<SignResponse>($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key={firebase_api_key}",
            payLoad).Then(response => {
                //Debug.Log(response);
                string local_id = response.localId;
                string idToken = response.idToken;
                CreateUser(signup_username.text, signup_phone.text, signup_email.text, local_id, idToken);
				StartCoroutine(showPopUpT("Successfully Created Account", "success"));
				return;
			}).Catch(error => {
                Debug.Log(error);
            });
    }

    

    private void GetAllUsers()
	{
		//string status;
		RestClient.Get(firebase_url + ".json").
			Then( response => {	
				fsData usersData = fsJsonParser.Parse(response.Text);
				Dictionary<string, User> users = null;
				serializer.TryDeserialize(usersData, ref users);
				all_users = users;
			}).
			Catch( error => {
				Debug.Log("An error Occurred" + error);
				return;
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
}
