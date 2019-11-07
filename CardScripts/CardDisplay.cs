using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDisplay : MonoBehaviour
{
    public Sprite circle;
    public Sprite triangle;
    public Sprite square;
    public Sprite star;
    public Sprite cross;
    public Sprite whot;

    public Image shape_center;
    public Image number_top;
    public Image number_bottom;
    public Image shape_top;
    public Image shape_bottom;

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
    //public Sprite whot;



    public Card thisCard;


    private Sprite GetImage(Card card1)
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

    private Sprite GetRankImage(Card card1)
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

    public void ShowCard(Card card)
    {
        shape_center.sprite = GetImage(card);
        shape_top.sprite = GetImage(card);
        shape_bottom.sprite = GetImage(card);
        number_top.sprite = GetRankImage(card);
        number_bottom.sprite = GetRankImage(card);

        thisCard = card;
    }

    public Card getCard()
    {
        return thisCard;
    }
    //public void 
}
