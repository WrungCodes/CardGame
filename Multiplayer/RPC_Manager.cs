using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPC_Manager
{
    DataManager dataManager;

    PhotonView photonView;

    CardAnimator cardAnimator;

    CardFunctions cardFunctions;

    public RPC_Manager(DataManager _dataManager, PhotonView _photonView, CardAnimator _cardAnimator, CardFunctions _cardFunctions)
    {
        dataManager = _dataManager;
        photonView = _photonView;
        cardAnimator = _cardAnimator;
        cardFunctions = _cardFunctions;
    }

    public void SetAllPlayerAllCards(CardSerializer datas)
    {
        photonView.RPC("RPC_SetAllPlayerAllCards", RpcTarget.AllBuffered, datas.Rank, datas.Suit);
    }

    [PunRPC]
    private void RPC_SetAllPlayerAllCards(string rank, string suit)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        dataManager.AddCardsToAllCards(new Card(datas));
    }

    public void SetPlayinCard(CardSerializer datas)
    {
        photonView.RPC("RPC_SetPlayinCard", RpcTarget.AllBuffered, datas.Rank, datas.Suit);
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

    public void DealCardToPlayer(CardSerializer datas, string playerId)
    {
        photonView.RPC("RPC_DealCardToPlayer", RpcTarget.AllBuffered, datas.Rank, datas.Suit, playerId);
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

    public void PlayCard(CardSerializer datas)
    { 
        photonView.RPC("RPC_PlayCard", RpcTarget.AllBuffered, datas.Rank, datas.Suit, PhotonNetwork.LocalPlayer.NickName);
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

        cardAnimator.AddCardtoPlayingDeck(card, playerId);
    }

    public void AskMasterForMarketCard()
    {
        photonView.RPC("RPC_AskMasterForMarketCard", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    private void RPC_AskMasterForMarketCard(string playerId)
    {
        Card card = cardFunctions.PickSingleCard(dataManager);
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_MasterDealCardToPlayer", RpcTarget.AllBuffered, datas.Rank, datas.Suit, playerId);
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

        cardAnimator.AddCardToPlayerDeck(card, playerId);
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

}
