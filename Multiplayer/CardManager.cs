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

    public CardAnimator CardAnimator;

    public Text text;

    PhotonView photonView;

    DataManager dataManager;

    void Start()
    {
        dataManager = new DataManager();

        photonView = GetComponent<PhotonView>();

        cardFunctions = new CardFunctions();

        dataManager.AllPlayerList = PlayerFunctions.CreatePlayerCardForAllPlayers(dataManager.GetPlayerFromPlayerCards());

        StartGame();
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetAllPlayersAllCards();
            InitilizeAllCards();
            DealCardsToAllPlayers();
        }
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

        dataManager.AddCardsToAllCards(new Card(datas));
    }

    public void InitilizeAllCards()
    {
        CardSerializer datas = cardFunctions.PickSingleCard(dataManager).ConvertCardToCardSerializer();
        photonView.RPC("RPC_SetPlayinCard", RpcTarget.All, datas.Rank, datas.Suit);
    }

    [PunRPC]
    private void RPC_SetPlayinCard(string rank, string suit)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        dataManager.AddCardsToPlayingDeck(card);

        card = PlayerFunctions.GetSameCard(card, GetAllCards());

        dataManager.RemoveCardsFromAllCards(card);
    }

    public void DealCardsToAllPlayers()
    {
        for (int x = 1; x <= Constants.PLAYER_INITIAL_CARDS; x++)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                DealCardToPlayer(cardFunctions.PickSingleCard(dataManager), player.NickName);
            }
        }
    }

    public void DealCardToPlayer(Card card,  string playerId)
    {
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_DealCardToPlayer", RpcTarget.All, datas.Rank, datas.Suit, playerId);
    }

    [PunRPC]
    private void RPC_DealCardToPlayer(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        PlayerCards playerCards = PlayerFunctions.GetPlayer(playerId, GetAllPlayerCards());

        Card card = new Card(datas);

        playerCards.AddCards(card);

        card = PlayerFunctions.GetSameCard(card, playerCards.GetAllCards());

        dataManager.RemoveCardsFromAllCards(card);
    }

    public List<Card> GetAllCards()
    {
        return dataManager.GetAllCards();
    }

    public List<PlayerCards> GetAllPlayerCards()
    {
        return dataManager.GetPlayerFromPlayerCards();
    }

    public List<Card> GetPlayingCard()
    {
        return dataManager.GetPlayingDeck();
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
    private void RPC_MasterDealCardToPlayer(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        PlayerCards playerCards = PlayerFunctions.GetPlayer(playerId, dataManager.GetPlayerFromPlayerCards()); //GetPlayer(playerId);

        playerCards.AddCards(card);

        card = PlayerFunctions.GetSameCard(card, GetAllCards());

        dataManager.RemoveCardsFromAllCards(card);

        CardAnimator.GetComponent<CardAnimator>().AddCardToPlayerDeck(card, playerId);
    }

    [PunRPC]
    private void RPC_AskMasterForMarketCard(string playerId)
    {
        Card card = cardFunctions.PickSingleCard(dataManager);
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_MasterDealCardToPlayer", RpcTarget.All, datas.Rank, datas.Suit, playerId);
    }

    [PunRPC]
    private void RPC_PlayCard(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        PlayerCards playerCards = PlayerFunctions.GetPlayer(playerId, dataManager.GetPlayerFromPlayerCards());

        card = PlayerFunctions.GetSameCard(card, playerCards.GetAllCards());

        dataManager.AddCardsToPlayingDeck(card);

        playerCards.RemoveCards(card);

        CardAnimator.GetComponent<CardAnimator>().AddCardtoPlayingDeck(card, playerId);
    }
}
