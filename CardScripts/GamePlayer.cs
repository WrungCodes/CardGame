using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class GamePlayer: MonoBehaviourPun
{
    public string username;

    public PhotonView PhotonView;

    //public string status;

    //public bool wasTurn = false;
    //public bool isTurn = false;
    //public bool isTurnNext = false;

    public CardPlayer player;

    public GameObject playerDeck;

    public GameObject GameSetup;
    //public GameObject board;

    //public int player_no;
    public Deck cards;

    private void Start()
    {
        //SendRPC();
        //GameObject objBoard = GameObject.FindGameObjectWithTag("Board");
        //GameObject obj = PhotonNetwork.Instantiate(
        //    Path.Combine("Prefabs", "playerDeck"),
        //    //objBoard.transform.position * UnityEngine.Random.Range(-20, 20)
        //    Vector2.up
        //    ,
        //    Quaternion.identity, 0);
        //obj.transform.SetParent(objBoard.transform);
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        GameSetup.GetComponent<GameSetup>().MyObjects.Add(this.gameObject);
    }

    public void SendRPC()
    {
        PhotonView.RPC("RPC_UpdatePlayerList", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_UpdatePlayerList(Player photon_player)
    {
        //PlayerManagement.Instance.AddPlayerGameObject(obj);
        PlayerManagement.Instance.AddCardPlayer(photon_player);
    }


    //private void Awake()
    //{
    //    //PhotonView = GetComponent<PhotonView>();
    //}

    //public GamePlayer(string _username, int _player_no)
    //{
    //    username = _username;
    //    player_no = _player_no;
    //    isTurn = false;

    //    cards = new List<Card>();
    //}

    //public void SetTurn()
    //{
    //    isTurn = true;
    //}

    //public void EndTurn()
    //{
    //    isTurn = false;
    //}

    //public void AddCards(List<Card> _cards)
    //{
    //    cards.AddRange(_cards);
    //}

    //public void AddCard(Card _card)
    //{
    //    cards.Add(_card);
    //}

    //public Card RemoveCard(Card card)
    //{
    //    int index = cards.IndexOf(card);
    //    cards.RemoveAt(index);
    //    return card;
    //}

    //public int GetCard(Card card)
    //{
    //    return cards.IndexOf(card);
    //}

    //public int GetCardNumber()
    //{
    //    return cards.Count;
    //}

    //public List<Card> GetCards()
    //{
    //    return cards;
    //}
}
