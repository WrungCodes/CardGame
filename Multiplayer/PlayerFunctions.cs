using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class PlayerFunctions
{

    public static List<PlayerCards> CreatePlayerCardForAllPlayers(List<PlayerCards> AllPlayerList)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            bool isTurn = false;

            //if (player.IsMasterClient)
            //    isTurn = true;

            AllPlayerList.Add(new PlayerCards(player.NickName, new List<Card>(), player, isTurn));
        }

        return AllPlayerList;
    }

    public static void DealCardsToAllPlayers(DataManager dataManager)
    {
        CardFunctions cd = new CardFunctions();

        for (int x = 1; x <= Constants.PLAYER_INITIAL_CARDS; x++)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                cd.PickSingleCard(dataManager);
            }
        }
    }

    public static PlayerCards GetPlayer(string playerId, List<PlayerCards> playerCards)
    {
        return playerCards.Where(ap => ap.playerName == playerId).First();
    }

    public static Card GetSameCard(Card card, List<Card> Cards)
    {
        return Cards.Where(c => c.Rank == card.Rank && c.Suit == card.Suit).First();
    }
}
