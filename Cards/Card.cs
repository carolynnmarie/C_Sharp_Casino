using System;

namespace BlackJack.Cards{

    public class Card{

        public Rank rank {get;set;}
        public Suit suit {get;set;}

        public Card(Rank rank, Suit suit){
            this.rank = rank;
            this.suit = suit;
        }

        public override string ToString(){
            int r = (int)rank;
            string rStr = r.ToString();
            if(rStr == "1"){
                rStr = "A";
            } else if (rStr == "11") {
                rStr = "J";
            } else if (rStr == "12") {
                rStr = "Q";
            } else if (rStr == "13"){
                rStr = "K";
            }
            string s = suit.ToString();
            if(s == "Hearts"){
                s = "\u2665";
            } else if (s == "Diamonds"){
                s = "\u2666";
            } else if (s == "Clubs"){
                s = "\u2663";
            } else if (s == "Spades"){
                s = "\u2660";
            }
            return rStr + s;
        }

    }
    
}

/*
 CLUBS("clubs", "\u2663"),
    DIAMONDS("diamonds", "\u2666"),
    HEARTS("hearts", "\u2665"),
    SPADES("spades", "\u2660");
 */