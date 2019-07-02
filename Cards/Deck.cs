using System;
using System.Collections.Generic;

namespace BlackJack.Cards{

    public class Deck {

        public List<Card> cards {get;set;}

        public Deck(){
            this.cards = new List<Card>();
            int[] ranks = {1,2,3,4,5,6,7,8,9,10,11,12,13};
            int[] suits = {1,2,3,4};
            foreach(int rankValue in ranks){
                Rank rank = (Rank) rankValue;
                foreach(int suitValue in suits){
                    Suit suit = (Suit) suitValue;
                    cards.Add(new Card((Rank)rank, (Suit)suit));                                      
                }
            }
        }

        public Deck(List<Card> cards){
            this.cards = cards;
        }

        public void Shuffle(){
            int size = cards.Count;
            Random random = new Random();
            for(int i = 0; i<size; i++){
                Card card1 = cards[i];
                int x = random.Next(i,size);
                Card card2 = cards[x];
                cards[i] = card2;
                cards[x] = card1;
            }
        }

        public Card DrawCard(){
            Card card = cards[cards.Count-1];
            cards.Remove(card);
            return card;
        }

        public List<Card> DealCards(int numDealt){
            List<Card> list = new List<Card>();
            for(int i = 0; i<numDealt; i++){
                list.Add(cards[cards.Count-1-i]);
            }
            foreach(Card card in list){
                cards.Remove(card);
            }
            return list;
        }

        public bool RemoveCard(Card card){
            bool remCard = false;
            if(cards.Contains(card)){
                cards.Remove(card);
                remCard = true;
            }
            return remCard;
        }

        public void ClearDeck(){
            cards.Clear();
        }

        public void AddCard(Card card){
            if(!cards.Contains(card)){
                cards.Add(card);
            }
        }

        public bool ContainsCard(Card card){
            if(cards.Contains(card)){
                return true;
            }
            return false;
        }

        public string PrintDeck(){
            List<string> deckString = new List<string>();
            foreach(Card card in cards){
                string x = card.ToString();
                deckString.Add(x);
            }
            return string.Join(", ",deckString);
        }

        // public int DeckSize(){
        //     return this.cards.Size();
        // }

        
        
    }
}