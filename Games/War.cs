using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{
    
    public class War{
        
        Person player {get;set;}
        Deck playerDeck {get;set;}
        Deck dealerDeck {get;set;}
        List<Card> playerTableCards {get;set;}
        List<Card> dealerTableCards {get;set;}

        public War(Person player){
            player = player;
            Deck houseDeck = new Deck();
            houseDeck.Shuffle();
            playerDeck = new Deck (houseDeck.DealCards(26));
            dealerDeck = new Deck(houseDeck.cards);
            playerTableCards = new List<Card>();
            dealerTableCards = new List<Card>();
        }

        
        public void Start(){
            DealHands();
            Console.WriteLine("Welcome to War! Press any key to start");
            Engine();
        }

        //not finished
        public void Engine(){
            while(!String.IsNullOrEmpty(Console.ReadLine())){
                do{
                    Card pTopCard = playerDeck.DrawCard();
                    Card dTopCard = dealerDeck.DrawCard();
                    Console.WriteLine("Your card is " + pTopCard.ToString() + ".  Dealer's card is " + dTopCard.ToString());
                    int winner = CompareCards(pTopCard,dTopCard);
                    if(winner == 2){
                        playerDeck.AddCard(pTopCard);
                        playerDeck.AddCard(dTopCard);
                        Console.WriteLine("You won the round!")
                    } else if (winner == 1){
                        dealerDeck.AddCard(pTopCard);
                        dealerDeck.AddCard(dTopCard);
                        Console.WriteLine("Dealer won the round!")
                    } else {
                        IDeclareWar();
                    }
            }while(CheckHandSizes());
            }
            
        }

        public int CompareCards(Card playerCard, Card dealerCard){
            return ((int)playerCard.rank > (int)dealerCard.rank)?2:((int)dealerCard.rank > (int)playerCard.rank)?1:0;            
        }

        //not finished
        public void IDeclareWar(){
            int pileSize = FindWarPileSize();


        }

        public int FindWarPileSize(){
            int pCount = playerDeck.DeckCount();
            int dCount = dealerDeck.DeckCount();
            int pileCount = 0;
            if(pCount < 3 || dCount < 3){
                if(pCount < dCount){
                    pileCount = pCount;
                } else {
                    pileCount = dCount;
                }
            } else {
                pileCount = 3;
            }
            return pileCount;
        }

        public bool CheckHandSizes(){
            return (playerDeck.DeckCount() == 0 || dealerDeck.DeckCount() == 0)?false : true;
        }

        public void End(){

        }
    }
}