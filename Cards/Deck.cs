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

        public Deck(List<Card> deck){
            this.deck = deck;
        }

        public void Shuffle(){
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

        public Card DrawCard(){
            Card card = deck[deck.Count-1];
            deck.Remove(card);
            return card;
        }

        public List<Card> DealCards(int numDealt){
            List<Card> list = new List<Card>();
            for(int i = 0; i<numDealt; i++){
                list.Add(deck[deck.Count-1-i]);
            }
            foreach(Card card in list){
                deck.Remove(card);
            }
            return list;
        }

        public bool RemoveCard(Card card){
            bool remCard = false;
            if(deck.Contains(card)){
                deck.Remove(card);
                remCard = true;
            }
            return remCard;
        }

        public void ClearDeck(){
            deck.Clear();
        }

        public void AddCard(Card card){
            if(!deck.Contains(card)){
                deck.Add(card);
            }
        }

        public bool ContainsCard(Card card){
            if(deck.Contains(card)){
                return true;
            }
            return false;
        }

        public string PrintDeck(){
            List<string> deckString = new List<string>();
            foreach(Card card in deck){
                string x = card.ToString();
                deckString.Add(x);
            }
            return string.Join(", ",deckString);
        }

        // public int DeckSize(){
        //     return this.deck.Size();
        // }

        
        
    }
}