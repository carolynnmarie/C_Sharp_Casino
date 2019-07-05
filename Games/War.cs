using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{
    
    public class War{
        
        Person player {get;set;}
        Deck deck {get;set;}
        List<Card> playerCards {get;set;}
        List<Card> dealerCards {get;set;}
        List<Card> playerTableCards {get;set;}
        List<Card> dealerTableCards {get;set;}

        public War(Person player){
            player = player;
            deck = new Deck();
            playerCards = new 
        }

        public void Start(){}

        public void Engine(){}

        public int CompareCards(){
            return 0;
        }

        public void IDeclareWar(){}

        public int FindPileSize(){
            return 0;
        }

        public void End(){}
    }
}