using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
	//[SerializeField]
	//private GameObject delayStartButton; //button used for creating and joining a game.
	[SerializeField]
	private GameObject delayCancelButton; //button used to stop searing for a game to join.
	[SerializeField]
	private int roomSize; //Manual set the number of player in the room at one time.

	public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
	{
		//PhotonNetwork.AutomaticallySyncScene = true; //Makes it so whatever scene the master client has loaded is the scene all other clients will load
		//delayStartButton.SetActive(true);
        DelayStart();
        //PhotonNetwork.PlayerList;

    }

	public void DelayStart() //Paired to the Delay Start button
	{
		//delayStartButton.SetActive(false);
		delayCancelButton.SetActive(true);
		PhotonNetwork.JoinRandomRoom(); //First tries to join an existing room
		Debug.Log("Delay Start");
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		CreateRoom(); // if it fails to join a room then it will try to create its own
	}

	void CreateRoom()
	{
		Debug.Log("Creating room now");
		int randomRoomNumber = Random.Range(0, 10000); //creating a random name for the room
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)State.CurrentStake.number_of_players };
		PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //attempting to create a new room
		Debug.Log(randomRoomNumber);
    }

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log("Failed to create room... trying again");
		CreateRoom(); //Retrying to create a new room with a different name.
	}

	public void DelayCancel() //Paired to the cancel button. Used to stop looking for a room to join.
	{
		delayCancelButton.SetActive(false);
		//delayStartButton.SetActive(true);
		PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(1);
    }
}