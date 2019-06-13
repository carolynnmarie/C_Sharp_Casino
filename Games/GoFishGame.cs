using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{

    public class GoFishGame{

        public Person player {get;set;}
        public List<Card> playerHand {get;set;}
        public List<Card> dealerHand {get;set;}
        public Deck deck {get;set;}

        public GoFishGame(Person player){
            this.player = player;
            this.playerHand = new List<Card>();
            this.dealerHand = new List<Card>();
            this.deck = new Deck();
        }

        public void Start(){

        }

        public void Engine(){

        }

        public void PlayerTurn(){

        }

        public void DealerTurn(){

        }

        public List<Card> GoFish(List<Card> hand){
            return hand;
        }

        public void End(){

        }
    }
}