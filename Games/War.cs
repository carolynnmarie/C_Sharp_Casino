using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{
    
    public class War : CardGame {
        
        Person player {get;set;}
        Deck playerDeck {get;set;}
        Deck dealerDeck {get;set;}
        List<Card> playerTableCards {get;set;}
        List<Card> dealerTableCards {get;set;}

        public War(Person player){
            this.player = player;
            Deck houseDeck = new Deck();
            houseDeck.Shuffle();
            this.playerDeck = new Deck (houseDeck.DealCards(26));
            this.dealerDeck = new Deck(houseDeck.cards);
            this.playerTableCards = new List<Card>();
            this.dealerTableCards = new List<Card>();
        }

        
        public void Start(){
            Console.WriteLine("Welcome to War! Press any key to start.  Type exit to exit the game at any time.");
            Engine();
            End();
        }

        //not finished
        public void Engine(){
            string next = Console.ReadLine();
            while(next != "exit"){
                do{
                    Card pTopCard = playerDeck.DrawCard();
                    Card dTopCard = dealerDeck.DrawCard();
                    Console.WriteLine("Your card is " + pTopCard.ToString() + ".  Dealer's card is " + dTopCard.ToString());
                    int winner = CompareCards(pTopCard,dTopCard);
                    if(winner == 2){
                        playerDeck.AddCard(pTopCard);
                        playerDeck.AddCard(dTopCard);
                        Console.WriteLine("You won the round!");
                    } else if (winner == 1){
                        dealerDeck.AddCard(pTopCard);
                        dealerDeck.AddCard(dTopCard);
                        Console.WriteLine("Dealer won the round!");
                    } else if (winner == 0) {                       
                        IDeclareWar(pTopCard, dTopCard);
                    }
                    Console.WriteLine("You now have " + playerDeck.DeckCount() + " cards");
                    next = Console.ReadLine();
                }while(CheckDeckSizes() && next != "exit");
            }
            
        }

        public int CompareCards(Card playerCard, Card dealerCard){
            return ((int)playerCard.rank > (int)dealerCard.rank)?2:((int)dealerCard.rank > (int)playerCard.rank)?1:0;            
        }

        public bool CheckDeckSizes(){
            return (playerDeck.DeckCount() == 0 || dealerDeck.DeckCount() == 0)?false:true;
        }

        public void IDeclareWar(Card pCard, Card dCard){
            int winner = 0;
            List<Card> tablePile = new List<Card>();
            tablePile.Add(pCard);
            tablePile.Add(dCard);            
            while(CheckDeckSizes() && winner == 0){
                Console.WriteLine("I DECLARE WAR!");
                int pileSize = FindWarPileSize();
                List<Card> pWarPile = playerDeck.DealCards(pileSize);
                List<Card> dWarPile = dealerDeck.DealCards(pileSize);
                for(int i = 0; i< pWarPile.Count; i++){
                    tablePile.Add(pWarPile[i]);
                    tablePile.Add(dWarPile[i]);
                }                               
                winner = CompareCards(pWarPile[pWarPile.Count-1],dWarPile[dWarPile.Count-1]);
                Console.WriteLine("Your top card is " + pWarPile[pWarPile.Count-1] + ". Dealer's top card is " 
                    + dWarPile[dWarPile.Count-1]);
            }
            if(winner == 2){
                playerDeck.AddCards(tablePile);
                Console.WriteLine("You win the round!");
            } else if (winner == 1) {
                Console.WriteLine("Dealer wins the round!");
                dealerDeck.AddCards(tablePile);
            } else if(!CheckDeckSizes()){
                if(playerDeck.DeckCount() == 0){
                    Console.WriteLine("You are out of cards!");
                } else {
                    Console.WriteLine("Dealer is out of cards!");
                }
            }
        }

        public int FindWarPileSize(){
            int pCount = playerDeck.DeckCount();
            int dCount = dealerDeck.DeckCount();
            int pileCount = 0;
            if(pCount < 4 || dCount < 4){
                pileCount = (pCount<dCount)? pCount: dCount;
            } else {
                pileCount = 4;
            }
            return pileCount;
        }

        public void End(){
            Console.WriteLine("Thank you for playing War!");
        }
    }
}