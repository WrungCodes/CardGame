using System.Collections.Generic;
using UnityEngine.UI;

public class Card
{
    public enum Type
    {
        normal,
        jackpot
    }

    //public Image shapeImage;
    //public Image rankImage;

    public enum Shape
	{
        whot,
		circle,
		square,
		cross,
		triangle,
		star
	}

	public enum Rank
    {
        whot,
        one,
        two,
        three,
        four,
        five,
        seven,
        eight,
        ten,
        eleven,
        twelve,
        thirteen,
        fourteen
    }

	Shape shape;
	Rank rank;
    Type type;

	public Card(Shape s, Rank r, Type t)
	{
       shape = s;
       rank = r;  
	}

    public Card(string shape_string, string rank_string, string type_string)
    {
        for (Shape s = Shape.circle; s <= Shape.star; ++s)
        {
            if (s.ToString() == shape_string)
            {
                shape = s;
            }
        }

        for (Rank r = Rank.one; r <= Rank.fourteen; ++r)
        {
            if (r.ToString() == rank_string)
            {
                rank = r;
            }
        }

        for (Type t = Type.normal; t <= Type.jackpot; ++t)
        {
            if (t.ToString() == type_string)
            {
                type = t;
            }
        }
    }


    public string GetCardRank()
    {
        return rank.ToString();
    }

    public Rank GetRank()
    {
        return rank;
    }

    public string GetCardShape()
    {
        return shape.ToString();
    }

    public string GetCardType()
    {
        return type.ToString();
    }

    public Shape GetCardTypeShape()
    {
        return shape;
    }

    //public Image SetShapeImage(Shape s)
    //{

    //}
}
