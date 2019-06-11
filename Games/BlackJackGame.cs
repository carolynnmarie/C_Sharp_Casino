using System;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{

    public class BlackJackGame{

        public Person player {get;set;}
        public Person dealer {get;set;}
        public Deck houseDeck {get;set;}
        public List<Card> dealerHand {get;set;}
        public List<Card> playerHand {get;set;}

        public BlackJackGame(Person player){
            this.player = player;
            this.dealer = new Person("dealer");
            this.houseDeck = new Deck();
            houseDeck.Shuffle();
            this.playerHand = new List<Card>();
            this.dealerHand = new List<Card>();
        }

        public void Start(){
            this.playerHand = houseDeck.DealCards(2);
            this.dealerHand = houseDeck.DealCards(2);
            PlaceBet();
            Engine();
        }

        public void PlaceBet(){            
            this.player.chips -= GetBetAmount();
        }

        private int GetBetAmount(){
            int answer = 0;
            Console.WriteLine("Please enter the amount you would like to bet.  Minimum bet is 5 chips.");
            do{
                try{
                answer = Convert.ToInt32(Console.ReadLine());
                } catch {
                Console.WriteLine("Incorrect input type.  Please enter amount in numeric digits");
                }
            } while(answer == 0);
            return answer;
        }

        public void Engine(){

        }

        public int DetermineHandValue(List<Card> hand){
            int handValue = 0;
            foreach(Card card in hand){
                handValue += (int)card.rank;
            }
            int aces = countAces(hand);
            if(aces == 1 && handValue <= 11){
                handValue += 10;
            }
            return handValue;
        }

        private int countAces(List<Card> hand){
            int count = 0;
            foreach (var card in hand){
                if ((int)card.rank == 1){
                    count++;
                }
            }
            return count;
        }

        private List<Card> Hit(List<Card> hand){
            hand.Add(houseDeck.DealCards(1));
            return hand;
        }

        public void End(){
            this.houseDeck = new Deck();
            Console.WriteLine("Would you like to play again? y/n");

        }
    }
    
}