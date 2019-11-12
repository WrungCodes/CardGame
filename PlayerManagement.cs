using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{

    public static PlayerManagement Instance;

    private PhotonView PhotonView;

    private List<CardPlayer> CardPlayers = new List<CardPlayer>();

    public Deck allCards;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        //if (PhotonView.IsMine)
        //{
        //    GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.up, Quaternion.identity, 0);
        //    AddCardPlayer(PhotonNetwork.LocalPlayer);
        //    Debug.Log(PhotonNetwork.NickName);
        //}

        //if (PhotonNetwork.IsMasterClient)
        // {
        //    StartCardShuffle();

        //    foreach(CardPlayer cardPlayer in CardPlayers)
        //    {
        //        // allCards.RandomDrawCards(5);
        //        AddCardToPlayer(cardPlayer.PhotonPlayer, allCards.RandomDrawCards(5));
        //    }
        //}
    }

    public void AddCardPlayer( Player photonPlayer) {
        int index = CardPlayers.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if (index == -1)
        {
            CardPlayers.Add(new CardPlayer(photonPlayer));
        }

        foreach (CardPlayer CardPlayer in CardPlayers)
        {
            Debug.Log(CardPlayer.PhotonPlayer.NickName);
        }
    }

    public void AddCardToPlayer(Player photonPlayer, List<Card> cards)
    {
        int index = CardPlayers.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if (index != -1)
        {
            //CardPlayers.Add(new CardPlayer(photonPlayer));
            CardPlayer cardPlayer = CardPlayers[index];
            cardPlayer.AddCards(cards);
            PlayerNetwork.Instance.AddCardsToPlayer(photonPlayer, cardPlayer.cards);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RemoveInvalidCards(List<Card> cards)
    {
        // Removing the star cards
        cards.RemoveRange(55, 5);

        // Removing the crosses cards
        cards.RemoveAt(27);
        cards.RemoveAt(30);
        cards.RemoveAt(33);

        // Removing the Square cards
        cards.RemoveAt(15);
        cards.RemoveAt(18);
        cards.RemoveAt(21);
    }

    void AddWhotCards(List<Card> cards)
    {
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
    }

    public void StartCardShuffle()
    {
        List<Card> cards_list = new List<Card>();

        for (Card.Shape s = Card.Shape.circle; s <= Card.Shape.star; ++s)
        {
            for (Card.Rank r = Card.Rank.one; r <= Card.Rank.fourteen; ++r)
            {
                cards_list.Add(new Card(s, r, Card.Type.normal));
            }
        }

        RemoveInvalidCards(cards_list);
        AddWhotCards(cards_list);

        allCards = new Deck();
        allCards.AddCards(cards_list);
        allCards.Shuffle();
    }

    //private void RPC_CreatePlayer(Player cardPlayer)
    //{
    //    Debug.Log(cardPlayer.NickName);
    //    GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.up, Quaternion.identity, 0);
    //    PlayerManagement.Instance.AddCardPlayer(cardPlayer);
    //    player = obj.GetComponent<GamePlayer>();
    //    player.username = cardPlayer.NickName;

    //    //Debug.Log("Player Created " + cardPlayer.NickName);
    //}
}
