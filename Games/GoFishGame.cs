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
        public int playerBooks {get;set;}
        public int dealerBooks {get;set;}

        public GoFishGame(Person player){
            this.player = player;
            this.playerHand = new List<Card>();
            this.dealerHand = new List<Card>();
            this.deck = new Deck();
            this.playerBooks = 0;
            this.dealerBooks = 0;
        }

        public void Start(){
            deck.Shuffle();
            playerHand = deck.DealCards(7);
            dealerHand = deck.DealCards(7);
            Engine();
            End();
        }

        public void Engine(){
            bool keepPlaying = true;
            do{
                PlayerTurn();
                keepPlaying = CheckSizes();
                DealerTurn();
                keepPlaying = CheckSizes();
            } while (keepPlaying);            
        }

        public void PlayerTurn(){
            string currentHand = "";
            int answer = 0;
            do{
                foreach (Card card in playerHand){
                    currentHand += card.ToString() + " ";
                }
                Console.WriteLine("Your hand: " + currentHand + "\nWhat rank do you want to ask for?");
                bool x = Int32.TryParse(Console.ReadLine(),out answer);
                if(x){
                //*Enter game logic here*
                } else {
                    Console.WriteLine("Invalid input.")
                }
            } while(true);
            
        }

        public void DealerTurn(){

        }

        public List<Card> GoFish(List<Card> hand){
            return hand;
        }

        private bool CheckSizes(){
            if(playerHand.Count == 0 || dealerHand.Count == 0 || deck.cards.Count == 0){
                return false;
            }
            return true;
        }

        public void End(){

        }

    }
}