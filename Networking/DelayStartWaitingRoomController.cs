using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    /*This object must be attached to an object
    / in the waiting room scene of your project.*/
    // photon view for sending rpc that updates the timer

    private PhotonView myPhotonView;
    // scene navigation indexes

    [SerializeField]
    private int multiplayerSceneIndex;

    [SerializeField]
    private int menuSceneIndex;
    // number of players in the room out of the total room size

    private int playerCount;
    private int roomSize;

    // text variables for holding the displays for the countdown timer and player count
    [SerializeField]
    private Text playerCountDisplay;

    private bool readyToStart;
    private bool startingGame;

    private void Start()
    {
        //initialize variables
        myPhotonView = GetComponent<PhotonView>();
        PlayerCountUpdate();
    }
    void PlayerCountUpdate()
    {
        // updates player count when players join the room
        // displays player count
        // triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + "/" + roomSize;
        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //called whenever a new player joins the room
        PlayerCountUpdate();
        //send master clients countdown timer to all other players in order to sync time.
        //if (PhotonNetwork.IsMasterClient)
        //    myPhotonView.RPC("RPC_SyncTimer", RpcTarget.Others, timerToStartGame);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //called whenever a player leaves the room
        PlayerCountUpdate();
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        if (readyToStart)
        {
            if (startingGame)
                return;
            StartGame();
        }
    }

    void StartGame()
    {
        //Multiplayer scene is loaded to start the game
        startingGame = true;
        //if (!PhotonNetwork.IsMasterClient)
        //    return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        //PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        //public function paired to cancel button in waiting room scene
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(menuSceneIndex);
    }
}