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
            List<Card> fishes = new List<Card>();
            int answer = 0;
            bool hasCard = false;
            string currentHand = PrintHand(playerHand);
            do{            
                Console.WriteLine("Your hand: " + currentHand + "\nWhat rank do you want to ask for?");
                bool x = Int32.TryParse(Console.ReadLine(),out answer);
                if(x){
                    fishes = FindCards(dealerHand, out hasCard);
                    if(hasCard){
                        Console.WriteLine("Dealer has " + fishes.Count + " " + answer + "s");
                        foreach(Card card in fishes){
                            playerHand.Add(card);
                            dealerHand.Remove(card);
                        }                       
                    } else {     
                        Console.WriteLine("Dealer has no " + answer + "s. Go Fish!");
                        playerHand = GoFish(playerHand, answer, out hasCard);                       
                    }
                }
            } while(hasCard);
            
        }

        public void DealerTurn(){
            Random random = new Random();
            bool hasCard = false;
            do{
                Card dealerPick = dealerHand[random.Next(dealerHand.Count)];
                Console.WriteLine("Dealer wants to know if you have any " + (int)dealerPick.rank + "s.")

            } while(hasCard);
            
        }

        private List<Card> FindCards(List<Card> otherHand, out bool hasCard){
            bool hasCard = false;
            List<Card> fishes = new List<Card>();
            foreach(Card card in otherHand){
                if((int)card.rank == answer){
                    fishes.Add(card);
                    hasCard = true;
                } 
            }
            return fishes;
        }

        
        private List<Card> GoFish(List<Card> hand, int cardValue, out bool hasCard){
            StringBuilder builder = new StringBuilder();
            Card card = deck.DrawCard();
            hand.Add(card); 
            builder.Append("You drew a " + card.ToString() + "\n")
            if((int)card.rank == cardValue){
                builder.Append("You fished your wish! You get another turn.");
                hasCard = true;
            } else {
                hasCard = false;
            }
            Console.WriteLine(builder.ToString());
            return hand;
        }

        public string PrintHand(List<Card> hand){
            StringBuilder builder = new StringBuilder();
            foreach(Card card in hand){
                builder.Append(card.ToString())
                        .Append(" ");
            }
            return builder.ToString();
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