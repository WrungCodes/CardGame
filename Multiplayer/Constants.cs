using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const int PLAYER_INITIAL_CARDS = 4;
    public const int CARDS_ACTIVE_OFFSET = 5;
    public const int CARDS_SPACE_OFFSET = 1;
    public const int PLAYER_CARDS_SPACE_OFFSET = 11;
    public const int NUMBER_OF_CARDS = 49;
}

public enum Suits
{
    None = -1,
    Whot = 0,

    Circle = 1,
    Square = 2,
    Cross = 3,
    Star = 4,
    Triangle = 5,
}

public enum Ranks
{
    None = -1,
    Whot = 0,

    one = 1,
    two = 2,
    three = 3,
    four = 4,
    five = 5,
    seven = 6,
    eight = 7,
    ten = 8,
    eleven = 9,
    twelve = 10,
    thirteen = 11,
    fourteen = 12,
}
