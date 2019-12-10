using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public List<Card> AllCards;

    public List<Card> PlayingDeck;

    public List<PlayerCards> AllPlayerList;

    public DataManager()
    {
        AllCards = new List<Card>();
        PlayingDeck = new List<Card>();
        AllPlayerList = new List<PlayerCards>();
    }


    public Card GetCurrentPlayingCard()
    {
        return PlayingDeck[PlayingDeck.Count - 1];
    }

    public Card GetCardFromAllCards(int index)
    {
        return AllCards[1];
    }

    public List<Card> GetAllCards()
    {
        return AllCards;
    }

    public void AddCardsToAllCards(Card card)
    {
        AllCards.Add(card);
    }

    public void RemoveCardsFromAllCards(Card card)
    {
        AllCards.Remove(card);
    }

    public List<Card> GetPlayingDeck()
    {
        return PlayingDeck;
    }

    public void AddCardsToPlayingDeck(Card card)
    {
        PlayingDeck.Add(card);
    }

    public void RemoveCardsFromPlayingDeck(Card card)
    {
        PlayingDeck.Remove(card);
    }

    public List<PlayerCards> GetPlayerFromPlayerCards()
    {
        return AllPlayerList;
    }

    public void AddPlayerToPlayerCards(PlayerCards playerCards)
    {
        AllPlayerList.Add(playerCards);
    }

    public void RemovePlayerFromPlayerCards(PlayerCards playerCards)
    {
        AllPlayerList.Remove(playerCards);
    }


}
