using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck<Card>
{
    List<Card> cards;

    public Deck(List<Card> cards)
    {
        this.cards = cards;
    }

    // Shuffle the Current Deck of cards
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
    public Card DrawSingleCard()
    {
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    // Return a list of drawn Cards from deck
    public List<Card> Draw(int numberToDraw = 1)
    {
        if (numberToDraw > cards.Count)
            numberToDraw = cards.Count;
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < numberToDraw; ++i)
        {
            drawnCards.Add(cards[0]);
            cards.RemoveAt(0);
        }
        return drawnCards;
    }

    public void addCards()
    {

    }
}
