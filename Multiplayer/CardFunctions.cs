using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunctions
{

    public List<Card> InitializeCards()
    {
        List<Card> cards = new List<Card>();

        for (Suits s = Suits.Circle; s <= Suits.Triangle; ++s)
        {
            for (Ranks r = Ranks.one; r <= Ranks.fourteen; ++r)
            {
                cards.Add( new Card(r, s) );
            }
        }

        cards = RemoveInvalidCards(cards);

        cards = AddWhotCards(cards);

        cards = ShuffleCards(cards);

        return cards;
    }

    public List<Card> ShuffleCards(List<Card> cards)
    {
        for (int i = cards.Count - 1; i > 0; --i)
        {
            int j = Random.Range(0, i + 1);
            Card card = cards[j];
            cards[j] = cards[i];
            cards[i] = card;
        }

        return cards;
    }

    private List<Card> RemoveInvalidCards(List<Card> cards)
    {
        // Removing the invalid star cards
        cards.RemoveRange(55, 5);

        // Removing the invalid crosses cards
        cards.RemoveAt(27);
        cards.RemoveAt(30);
        cards.RemoveAt(33);

        // Removing the invalid Square cards
        cards.RemoveAt(15);
        cards.RemoveAt(18);
        cards.RemoveAt(21);

        return cards;
    }

    private List<Card> AddWhotCards(List<Card> cards)
    {
        cards.Add(new Card(Ranks.Whot, Suits.Whot));
        cards.Add(new Card(Ranks.Whot, Suits.Whot));
        cards.Add(new Card(Ranks.Whot, Suits.Whot));
        cards.Add(new Card(Ranks.Whot, Suits.Whot));

        return cards;
    }
}
