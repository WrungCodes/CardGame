using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
	//scene navigation index
	[SerializeField]
	private int waitingRoomSceneIndex;

	public override void OnEnable()
	{
		//register to photon callback functions
		PhotonNetwork.AddCallbackTarget(this);
	}
	public override void OnDisable()
	{
		//unregister to photon callback functions
		PhotonNetwork.RemoveCallbackTarget(this);
	}
	public override void OnJoinedRoom() //Callback function for when we successfully create or join a room.
	{
        // called when our player joins the room
        // load into waiting room scene
        //StartGame();

        SceneManager.LoadScene(waitingRoomSceneIndex);

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    GameModel game = new GameModel();

        //    foreach (Player player in PhotonNetwork.PlayerList)
        //    {
        //        game.playersList.Add(player.NickName);
        //    }

        //    State.GameModel = game;
        //}
    }

    //private void StartGame() //Function for loading into the multiplayer scene.
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        Debug.Log("Starting Game");
    //        PhotonNetwork.LoadLevel(multiplayerSceneIndex); //because of AutoSyncScene all players who join the room will also be loaded into the multiplayer scene.
    //    }
    //}
}