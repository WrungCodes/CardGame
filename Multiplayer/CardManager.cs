using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    CardFunctions cardFunctions;

    public List<Card> AllCards;

    public List<Card> PlayingDeck;

    public List<PlayerCards> AllPlayerList;

    public CardAnimator CardAnimator;

    public Text text;

    //public List<Card> OwnCards;

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        cardFunctions = new CardFunctions();

        CreatePlayerCardForAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            SetAllPlayersAllCards();
            InitilizeAllCards();
            DealCardsToAllPlayers();
        }

        //DebugCards();
    }

    private void SetAllPlayersAllCards()
    {
        List<Card> InstalizedCards = cardFunctions.InitializeCards();
        foreach (Card card in InstalizedCards)
        {
            CardSerializer datas = card.ConvertCardToCardSerializer();
            photonView.RPC("RPC_SetAllPlayerAllCards", RpcTarget.All, datas.Rank, datas.Suit);
        }
    }

    [PunRPC]
    private void RPC_SetAllPlayerAllCards(string rank, string suit)
    {
        CardSerializer datas = new CardSerializer(rank, suit);
        AllCards.Add(new Card(datas));
    }

    public void DebugCards()
    {
        foreach(PlayerCards pc in AllPlayerList)
        {
            Debug.Log(pc.playerName);
            foreach(Card card in pc.cards)
            {
                Debug.Log(card.GetRank() +"  "+ card.GetSuit());
            }
        }
    }

    public void InitilizeAllCards()
    {
        //AllCards = cardFunctions.InitializeCards();
        PlayingDeck = new List<Card>();

        CardSerializer datas = PickSingleCard().ConvertCardToCardSerializer();
        photonView.RPC("RPC_SetPlayinCard", RpcTarget.All, datas.Rank, datas.Suit);
    }

    public void CreatePlayerCardForAllPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AllPlayerList.Add(new PlayerCards( player.NickName, new List<Card>()));
        }
    }

    public void DealCardsToAllPlayers()
    {
        for (int x = 1; x <= Constants.PLAYER_INITIAL_CARDS; x++)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                DealCardToPlayer(PickSingleCard(), player.NickName);
            }
        }
    }

    public void DealCardToPlayer(Card card,  string playerId)
    {
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_CustomCardSerialization", RpcTarget.All, datas.Rank, datas.Suit, playerId);
    }

    [PunRPC]
    private void RPC_CustomCardSerialization(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        PlayerCards playerCard = AllPlayerList.Where(ap => ap.playerName == playerId).First();

        Card card = new Card(datas);

        playerCard.cards.Add(card);
        AllCards.Remove(GetSameCard(card, AllCards));
    }

    [PunRPC]
    private void RPC_SetPlayinCard(string rank, string suit)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);
        PlayingDeck.Add(card);
        AllCards.Remove(GetSameCard(card, AllCards));
    }

    public Card DrawSingleCard()
    {
        Card drawnCard = AllCards[0];
        AllCards.RemoveAt(0);
        return drawnCard;
    }

    public Card PickSingleCard()
    {
        Card drawnCard = AllCards[0];
        return drawnCard;
    }

    public Card GetCurrentPlayingCard()
    {
        return PlayingDeck[PlayingDeck.Count - 1];
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCard(Card card)
    {
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_PlayCard", RpcTarget.All, datas.Rank, datas.Suit, PhotonNetwork.LocalPlayer.NickName);
    }

    public void DealCardFromMarket()
    {
        photonView.RPC("RPC_AskMasterForMarketCard", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    private void RPC_AskMasterForMarketCard(string playerId)
    {
        Card card = PickSingleCard();
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_MasterDealCardToPlayer", RpcTarget.All, datas.Rank, datas.Suit, playerId);
    }


    [PunRPC]
    private void RPC_MasterDealCardToPlayer(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        PlayerCards playerCards = GetPlayer(playerId);

        playerCards.cards.Add(card);

        //if (PhotonNetwork.IsMasterClient)
        //{
            card = GetSameCard(card, AllCards);

            AllCards.Remove(card);
        //}

        CardAnimator.GetComponent<CardAnimator>().AddCardToPlayerDeck(card, playerId);
    }

    [PunRPC]
    private void RPC_PlayCard(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        PlayerCards playerCards = GetPlayer(playerId);

        card = GetSameCard(card, playerCards.cards);

        PlayingDeck.Add(card);

        playerCards.cards.Remove(card);

        CardAnimator.GetComponent<CardAnimator>().AddCardtoPlayingDeck(card, playerId);

    }

    public PlayerCards GetPlayer(string playerId)
    {
        return AllPlayerList.Where(ap => ap.playerName == playerId).First();
    }

    public Card GetSameCard(Card card, List<Card> playerCards)
    {
       return playerCards.Where(c => c.Rank == card.Rank && c.Suit == card.Suit).First();
    }
}
