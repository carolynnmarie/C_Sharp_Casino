using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{
    
    public class War{
        
        Person player {get; set; }
        Deck deck {get; set; }
        List<Card> playerCards {get; set; }
        List<Card> dealerCards {get; set; }

        public War(Person player){
            player = player;
            deck = new Deck();
            playerCards = new 
        }

        public void Start(){}

        public void Engine(){}

        public 

        public void End(){}
    }
}