using System;

namespace BlackJack.Cards{

    

    

    public class Card{

        public Rank rank {get;set;}
        public Suit suit {get;set;}

        public Card(Rank rank, Suit suit){
            this.rank = rank;
            this.suit = suit;
        }

    }
    
}