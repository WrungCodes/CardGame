using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{

    public Sprite circle;
    public Sprite triangle;
    public Sprite square;
    public Sprite star;
    public Sprite cross;
    public Sprite whot;

    //public Image shape_center;
    //public Image number_top;
    //public Image number_bottom;
    //public Image shape_top;
    //public Image shape_bottom;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetSuitImage(Card card)
    {
        switch (card.GetSuit().ToString())
        {
            case "Circle":
                return circle;
            case "Triangle":
                return triangle;
            case "Square":
                return square;
            case "Cross":
                return cross;
            case "Star":
                return star;
            case "Whot":
                return whot;
            default:
                return null;
        }
    }

    public Sprite GetRankImage(Card card)
    {
        switch (card.GetRank().ToString())
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
                return null;
        }
    }
}
