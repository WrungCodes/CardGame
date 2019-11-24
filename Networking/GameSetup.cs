using Photon.Pun;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public Sprite circle;
    public Sprite triangle;
    public Sprite square;
    public Sprite star;
    public Sprite cross;
    public Sprite whot;

    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite seven;
    public Sprite eight;
    public Sprite ten;
    public Sprite eleven;
    public Sprite twelve;
    public Sprite thirteen;
    public Sprite fourteen;

    public GameObject cardPrefab;
    public GameObject cardPool;

    private PhotonView PhotonView;

    public Text usernameText;

    public Deck allCards;

    //public List<CardPlayer> CardPlayers = new List<CardPlayer>();

    //public int noPeople = 0;

    //public List<GameObject> playersObj = new List<GameObject>();

    public List<GameObject> MyObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();

        SpawnPlayer();

        if (PhotonNetwork.IsMasterClient)
        {
            StartCardShuffle();
            foreach (Card cardd in allCards.cards)
            {
                SpawnCards(cardd);
            //PhotonView.RPC("RPC_SpawnCardsForAllUsers", RpcTarget.OthersBuffered);
            }
        }

        usernameText.text = PhotonNetwork.NickName +" "+ PhotonNetwork.LocalPlayer.ActorNumber;
    }

    //void StartSpawning()
    //{
    //    SpawnPlayer();

    //    if (noPeople != 2)
    //        return;

    //    if ( PhotonNetwork.IsMasterClient )
    //    {
    //        StartCardShuffle();

    //        foreach (Card cardd in allCards.cards)
    //        {
    //            SpawnCards(cardd);
    //        }

    //        //foreach (GameObject obj in playersObj)
    //        //{
    //        //    Card singleCard = allCards.DrawSingleCard();
    //        //    GamePlayer player = obj.GetComponent<GamePlayer>();
    //        //    player.cards.AddCard(singleCard);

    //        //    foreach (Transform cardObj in cardPool.transform)
    //        //    {
    //        //        //child is your child transform
    //        //        if (cardObj.GetComponent<card>()._card == singleCard)
    //        //        {
    //        //            cardObj.transform.SetParent(obj.GetComponent<Transform>());
    //        //        }
    //        //    }
    //        //}
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void SpawnPlayer()
    {
        GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.up, Quaternion.identity, 0);

        GamePlayer player = obj.GetComponent<GamePlayer>();

        player.username = PhotonNetwork.NickName;

        //player.cards = new Deck();
    }


    [PunRPC]  
    void RPC_SpawnCardsForAllUsers(Card cardd)
    {
        SpawnCards(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
    }

    //void SpawnDecksForPlayer()
    //{

    //}

    void SpawnCards(Card cardd)
    {
        object[] instantiationData = { cardd.GetCardShape(), cardd.GetCardRank(), cardd.GetCardType() };

        GameObject card = PhotonNetwork.InstantiateSceneObject(
            Path.Combine("Prefabs", "card"),
            cardPool.transform.position,
            Quaternion.identity,
            0,
            instantiationData
            );
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

    public Sprite GetImage(Card card1)
    {
        switch (card1.GetCardShape())
        {
            case "circle":
                return circle;
            case "triangle":
                return triangle;
            case "square":
                return square;
            case "cross":
                return star;
            case "star":
                return star;
            case "whot":
                return null;
            default:
                return star;
        }
    }

    public Sprite GetRankImage(Card card1)
    {
        switch (card1.GetCardRank())
        {
            case "one":
                return one;
            case "two":
                return two;
            case "three":
                return three;
            case "four":
                return four;
            case "five":
                return five;
            case "seven":
                return seven;
            case "eight":
                return eight;
            case "ten":
                return ten;
            case "eleven":
                return eleven;
            case "twelve":
                return twelve;
            case "thirteen":
                return thirteen;
            case "fourteen":
                return fourteen;
            case "whot":
                return null;
            default:
                return star;
        }
    }
}
