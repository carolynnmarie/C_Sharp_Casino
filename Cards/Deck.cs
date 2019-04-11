using System;
using System.Collections.Generic;

namespace BlackJack.Cards{

    public class Deck {

        List<Card> deck {get;set;}

        public Deck(){
            this.deck = new List<Card>();
            int[] ranks = {1,2,3,4,5,6,7,8,9,10,11,12,13};
            int[] suits = {0,1,2,3};
            foreach(int rankValue in ranks){
                Rank rank = (Rank) rankValue;
                foreach(int suitValue in suits){
                    Suit suit = (Suit) suitValue;
                    deck.Add(new Card((Rank)rank, (Suit)suit));                                      
                }
            }
        }

        public void shuffle(){
            int size = deck.Count;
            Random random = new Random();
            for(int i = 0; i<size; i++){
                Card card1 = deck[i];
                int x = random.Next(i,size);
                Card card2 = deck[x];
                deck[i] = card2;
                deck[x] = card1;
            }
        }

        public string printDeck(){
            List<string> deckString = new List<string>();
            foreach(Card card in deck){
                int r = (int)card.rank;
                //string s = ;
                string x = r + card.suit.ToString();
                deckString.Add(x);
            }
            return string.Join(", ",deckString);
        }
    }
}