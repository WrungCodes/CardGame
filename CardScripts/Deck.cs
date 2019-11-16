using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Deck
{
    public List<Card> cards = new List<Card>();

    public int CardCount()
    {
        return cards.Count;
    }

    public void AddCards(List<Card> _cards)
    {
        cards.AddRange(_cards);
    }

    public void AddCard(Card _card)
    {
        cards.Add(_card);
    }

    public Card RemoveCard(Card card)
    {
        int index = cards.IndexOf(card);
        cards.RemoveAt(index);
        return card;
    }

    public List<Card> RemoveCards(List<Card> _cards)
    {
        foreach (Card card in _cards)
        {
            int index = cards.IndexOf(card);
            cards.RemoveAt(index);
        }
        return _cards;
    }

    public bool IsCardEnough(int no_card)
    {
        if (cards.Count < no_card)
        {
            return false;
        }
        return true;
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; --i)
        {
            int j = Random.Range(0, i + 1);
            Card card = cards[j];
            cards[j] = cards[i];
            cards[i] = card;
        }
    }

    public List<Card> RandomDrawCards(int number_to_draw)
    {
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < number_to_draw; ++i)
        {
            int randomIndex = Random.Range(0, cards.Count);
            drawnCards.Add(cards[randomIndex]);
            cards.RemoveAt(randomIndex);
        }
        return drawnCards;
    }

    public List<Card> DrawCards(int number_to_draw)
    {
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < number_to_draw; ++i)
        {
            drawnCards.Add(cards[0]);
            cards.RemoveAt(0);
        }
        return drawnCards;
    }

    public Card DrawSingleCard()
    {
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }
}

