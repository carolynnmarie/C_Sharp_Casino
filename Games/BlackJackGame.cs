using System;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{

    public class BlackJackGame{

        public Person player {get;set;}
        public Person dealer {get;set;}
        public Deck houseDeck {get;set;}
        public Deck dealerHand {get;set;}
        public Deck playerHand {get;set;}

        public BlackJackGame(Person player){
            this.player = player;
            this.dealer = new Person("dealer");
            this.houseDeck = new Deck();
            houseDeck.Shuffle();
            this.playerHand = new Deck(houseDeck.DealCards(2));
            this.dealerHand = new Deck(houseDeck.DealCards(2));
        }

        public void Start(){

        }

        public void Engine(){

        }

        public int GetBetAmount(){
            
        }

        public void PlaceBet(int bet){

        }

        public int DetermineHandValue(Deck hand){

            return 0;
        }

        public void End(){

        }
    }
    
}