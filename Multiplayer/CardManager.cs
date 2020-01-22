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

    RPC_Manager RPC_Manager;

    void Start()
    {
        dataManager = new DataManager();

        photonView = GetComponent<PhotonView>();

        cardFunctions = new CardFunctions();

        RPC_Manager = new RPC_Manager(dataManager, photonView, CardAnimator, cardFunctions);

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

            RPC_Manager.SetAllPlayerAllCards(datas);

            //photonView.RPC("RPC_SetAllPlayerAllCards", RpcTarget.AllBuffered, datas.Rank, datas.Suit);
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

        RPC_Manager.SetPlayinCard(datas);

        //photonView.RPC("RPC_SetPlayinCard", RpcTarget.AllBuffered, datas.Rank, datas.Suit);
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

        RPC_Manager.DealCardToPlayer(datas, playerId);

        //photonView.RPC("RPC_DealCardToPlayer", RpcTarget.AllBuffered, datas.Rank, datas.Suit, playerId);
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

    public void DealCardFromMarket()
    {
        PlayerCards playerCards = PlayerFunctions.GetPlayer(PhotonNetwork.NickName, dataManager.GetPlayerFromPlayerCards()); //GetPlayer(playerId);

        if (!playerCards.isTurn)
        {
            return;
        }

        playerCards.UnSetTurn();

        RPC_Manager.AskMasterForMarketCard();
        //photonView.RPC("RPC_AskMasterForMarketCard", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.NickName);
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

        Debug.Log("Using this RPC");

        // Set next turn
        playerCards.UnSetTurn();

        Player nextPlayer = playerCards.player.GetNext();

        PlayerCards nextPlayerPlayerCards = PlayerFunctions.GetPlayer(nextPlayer.NickName, dataManager.GetPlayerFromPlayerCards());

        nextPlayerPlayerCards.SetToTurn();
        // End set turn

        card = PlayerFunctions.GetSameCard(card, GetAllCards());

        dataManager.RemoveCardsFromAllCards(card);

        CardAnimator.GetComponent<CardAnimator>().AddCardToPlayerDeck(card, playerId);
    }




    public void PlayCard(Card card)
    {
        PlayerCards playerCards = PlayerFunctions.GetPlayer(PhotonNetwork.NickName, dataManager.GetPlayerFromPlayerCards()); //GetPlayer(playerId);

        if (!playerCards.isTurn)
        {
            return;
        }

        playerCards.UnSetTurn();

        CardSerializer datas = card.ConvertCardToCardSerializer();

        RPC_Manager.PlayCard(datas);

        //photonView.RPC("RPC_PlayCard", RpcTarget.AllBuffered, datas.Rank, datas.Suit, PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    private void RPC_PlayCard(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        PlayerCards playerCards = PlayerFunctions.GetPlayer(playerId, dataManager.GetPlayerFromPlayerCards());

        List<Card> allCards = playerCards.GetAllCards();

        // Set next turn
        playerCards.UnSetTurn();

        Player nextPlayer = playerCards.player.GetNext();

        PlayerCards nextPlayerPlayerCards = PlayerFunctions.GetPlayer(nextPlayer.NickName, dataManager.GetPlayerFromPlayerCards());

        //nextPlayerPlayerCards.SetToTurn();
        // End set turn


        //Card currentCard = allCards[allCards.Count - 1];

        card = PlayerFunctions.GetSameCard(card, allCards);

        dataManager.AddCardsToPlayingDeck(card);

        playerCards.RemoveCards(card);

        //CardAnimator.GetComponent<CardAnimator>().DisableCardInPlayingDeck();

        CardAnimator.GetComponent<CardAnimator>().AddCardtoPlayingDeck(card, playerId);

        CarryCardAction(card, playerCards.player);

        // Set Turn 
        //nextPlayerPlayerCards.SetToTurn();
        // End set turn
    }


    public void DealACard(string playerId)
    {
        Card card = cardFunctions.PickSingleCard(dataManager);
        CardSerializer datas = card.ConvertCardToCardSerializer();

        photonView.RPC("RPC_MasterDealCard", RpcTarget.AllBuffered, datas.Rank, datas.Suit, playerId);
    }

    [PunRPC]
    private void RPC_MasterDealCard(string rank, string suit, string playerId)
    {
        CardSerializer datas = new CardSerializer(rank, suit);

        Card card = new Card(datas);

        PlayerCards playerCards = PlayerFunctions.GetPlayer(playerId, dataManager.GetPlayerFromPlayerCards()); //GetPlayer(playerId);

        playerCards.AddCards(card);

        card = PlayerFunctions.GetSameCard(card, GetAllCards());

        dataManager.RemoveCardsFromAllCards(card);

        CardAnimator.GetComponent<CardAnimator>().AddCardToPlayerDeck(card, playerId);
    }

    public IEnumerator WaitForTime(float timeToMove, string playerName, int numberOfCard)
    {
        for (int i = 0; i < numberOfCard; i++)
        {
            Debug.Log("Got 4");
            DealACard(playerName);
            yield return new WaitForSeconds(timeToMove);
        }
    }

    public void CarryCardAction(Card card, Player player)
    {
        switch (card.GetRank())
        {
            case Ranks.two:
                PickSomeCards(2, player.GetNext());
                GetPlayerN(player.GetNext().GetNext()).SetToTurn();
                break;

            case Ranks.one:
                GetPlayerN(player).SetToTurn();
                break;

            case Ranks.fourteen:
                foreach(Player p in PhotonNetwork.PlayerList)
                {
                    if(p != player)
                    {
                        PickSomeCards(1, p);
                    }
                }
                GetPlayerN(player).SetToTurn();
                break;

            case Ranks.five:
                PickSomeCards(3, player.GetNext());
                GetPlayerN(player.GetNext().GetNext()).SetToTurn();
                break;

            case Ranks.eight:
                GetPlayerN(player.GetNext().GetNext()).SetToTurn();
                break;

            case Ranks.Whot:
                break;

            default:
                GetPlayerN(player.GetNext()).SetToTurn();
                break;
        }
    }

    public PlayerCards GetPlayerN(Player player)
    {
        return PlayerFunctions.GetPlayer(player.NickName, dataManager.GetPlayerFromPlayerCards());
    }

    public void PickSomeCards(int numberOfCard, Player playerToPick)
    {
        StartCoroutine(WaitForTime(4f, playerToPick.NickName, numberOfCard));
    }

}
