using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Collections.Generic;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }

    private PhotonView PhotonView;

    //public GamePlayer player;

    // Use this for initialization
    private void Awake()
    {
        //Instance = this;
        //PhotonView = GetComponent<PhotonView>();
        //PlayerName = GlobalState.GetUser().username;
        //SceneManager.sceneLoaded += OnSceneFinishedLoading;
        //Debug.Log(PhotonNetwork.NickName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //if (scene.name == "SampleScene")
        //{
        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        PhotonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
        //        //PhotonView.RPC("RPC_CreatePlayer", RpcTarget.Others, PhotonNetwork.LocalPlayer);
        //    }
        //    //else
        //        PhotonView.RPC("RPC_CreatePlayer", RpcTarget.All, PhotonNetwork.LocalPlayer);
        //}
    }

    //PhotonView.RPC("RPC_CreatePlayer", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);

    public void SendRPC(GameObject obj, Player player)
    {
        PhotonView.RPC("RPC_UpdatePlayerList", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_UpdatePlayerList(Player player)
    {
        //PlayerManagement.Instance.AddPlayerGameObject(obj);
        PlayerManagement.Instance.AddCardPlayer(player);
    }

    public void AddCardsToPlayer(Player photonPlayer , List<Card> cards)
    {
        PhotonView.RPC("RPC_AddCardsToPlayer", photonPlayer, cards);
    }


    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(2);
    }

    [PunRPC]
    private void RPC_AddCardsToPlayer(List<Card> cards) {
        //player.cards = cards;
        //foreach (Card card in cards)
        //{
        //    Debug.Log(card.GetCardRank() +" "+ card.GetCardShape());
        //}
    }

    [PunRPC]
    private void RPC_CreatePlayer(Player cardPlayer)
    {

        Debug.Log(cardPlayer.NickName);
        GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.up, Quaternion.identity, 0);
        PlayerManagement.Instance.AddCardPlayer(cardPlayer);
        //player = obj.GetComponent<GamePlayer>();
        //player.username = cardPlayer.NickName;

        //Debug.Log("Player Created " + cardPlayer.NickName);
    }
}
