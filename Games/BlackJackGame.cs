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
            Console.WriteLine("Welcome to BlackJack!")
            int bet = PlaceBet();
            if(bet > 0){
                Engine();
            }            
        }

        public int PlaceBet(){  
            int bet = 0;  
            do{
            bet = GetBetAmount(); 
            if(bet <= player.chips.count){
                this.player.chips -= bet
            } else if (bet > player.chips.count && player.chips.count > 0) {
                Console.WriteLine("Insufficient funds. Your total chip amount is " + player.chips.count)
            } else if (player.chips.count == 0){
                Console.WriteLine("Out of funds.")
                GoodBye();
                break;
            }
            } while(bet == 0);
            return bet;
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
            int pTotal = PlayerTurn();
        }

        private int PlayerTurn(){
            int value = DetermineHandValue(playerHand);
            StringBuilder builder = new StringBuilder();
            do{
                builder.Append("Your cards are: ");
                foreach(Card card in playerHand){
                    builder.Append(card.ToString())
                            .Append(" ");
                }
                builder.Append("for a total of ")
                        .Append(value)
                        .Append(". Would you like to hit or stay?");
                Console.WriteLine(builder.ToString());
                string answer = Console.ReadLine();
                if(answer == "hit"){
                    playerHand = Hit(playerHand);
                    value = DetermineHandValue(playerHand)
                    Console.WriteLine("Your new card is " + playerHand[playerHand.count-1].ToString() 
                        + ". Your new total is " + value + ".");
                } else {
                    break;
                };
            } while(value<21);
            return value;
        }

        public int dealerTurn(){
            int value = DetermineHandValue(dealerHand);

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

        public void GoodBye(){
            Console.WriteLine("Returning to Main Menu.")
        }
    }
    
}