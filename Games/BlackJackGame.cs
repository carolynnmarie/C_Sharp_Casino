using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{

    public class BlackJackGame {

        public Person player {get;set;}
        public Deck houseDeck {get;set;}
        public List<Card> dealerHand {get;set;}
        public List<Card> playerHand {get;set;}
        public int bet {get;set;}

        public BlackJackGame(Person player){
            this.player = player;
            this.houseDeck = new Deck();
            houseDeck.Shuffle();
            this.playerHand = new List<Card>();
            this.dealerHand = new List<Card>();
            this.bet = 0;
        }

        public void Start(){
            this.playerHand = houseDeck.DealCards(2);
            this.dealerHand = houseDeck.DealCards(2);
            Console.WriteLine("Welcome to BlackJack!");
            PlaceBet();
            if(bet > 0){
                Engine();
            } else {
                GoodBye();
            }         
        }

        public void PlaceBet(){  
            do{
                bet = GetBetAmount(); 
                if(bet <= player.chips){
                    this.player.chips -= bet;
                } else if (bet > player.chips && player.chips > 0) {
                    Console.WriteLine("Insufficient funds. Your total chip amount is " + player.chips);
                } else if (player.chips == 0){
                    Console.WriteLine("Out of funds.");
                    break;
                };
            } while(bet == 0);
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
            PlayerTurn();
            DealerTurn();
            DetermineWinner();
            End();
        }

        private void PlayerTurn(){
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
                        .Append(". Dealer's top card is " + dealerHand[0].ToString()+ ". ")
                        .Append("Would you like to hit or stay?");
                Console.WriteLine(builder.ToString());
                string answer = Console.ReadLine();
                if(answer == "hit"){
                    playerHand = Hit(playerHand);
                    value = DetermineHandValue(playerHand);
                    Console.WriteLine("Your new card is " + playerHand[playerHand.Count-1].ToString() 
                        + ". Your new total is " + value + ".");
                } else {
                    break;
                };
            } while(value<21);            
        }

        public void DealerTurn() {
            StringBuilder builder = new StringBuilder();
            int value = DetermineHandValue(dealerHand);
            builder.Append("Dealer's turn. Dealer's cards are ");
            foreach(Card card in dealerHand){
                builder.Append(card.ToString())
                        .Append(" ");
            };
            builder.Append(". For a total of ")
                    .Append(value);
            Console.WriteLine(builder.ToString());
            while(value <= 16){
                dealerHand = Hit(dealerHand);
                value = DetermineHandValue(dealerHand);
                Console.WriteLine("Dealer's new card is: " + dealerHand[2].ToString() + "for a total of " + value);
            };           
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
            hand.Add(houseDeck.DrawCard());
            return hand;
        }

        private void DetermineWinner(){
            int playerTotal = DetermineHandValue(playerHand);
            int dealerTotal = DetermineHandValue(dealerHand);
            StringBuilder builder = new StringBuilder("Your total is: " + playerTotal + "\nDealer's total is " + dealerTotal + "\n");
            if(playerTotal > 21){
                builder.Append("You bust!  You lose your bet of " + bet + " chips.");
            } else if (playerTotal == 21){
                builder.Append("BlackJack! You win " + bet + " chips!");
                player.chips += bet*2;
            } else if (playerTotal < 21) {
                if(dealerTotal>21){
                builder.Append("Dealer busts!  You win " + bet + " chips!");
                player.chips += bet*2;
            } else if (dealerTotal > playerTotal){
                builder.Append("Dealer wins! You lose your bet of " + bet + " chips.");
            } else if (playerTotal > dealerTotal){
                builder.Append("You beat the dealer!  You win " + bet + " chips!");
                player.chips += bet*2;
            }
            }            
            Console.WriteLine(builder.ToString());
        }

        public void End(){
            houseDeck = new Deck();
            playerHand = new List<Card>();
            dealerHand = new List<Card>();
            bet = 0;
            string answer = "";
            do{
                Console.WriteLine("Would you like to play again? y/n");
                answer = Console.ReadLine();
                if(answer.Equals("y")){
                    Start();
              } else if (answer.Equals("n")) {
                  GoodBye();
              } else {
                  Console.WriteLine("Invalid input")
              }
            } while(!answer.Equals("y") && !answer.Equals("n"));
            
        }

        public void GoodBye(){
            Console.WriteLine("Returning to Main Menu.");
        }
    }
    
}