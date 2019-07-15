using System;
using System.Text;
using System.Collections.Generic;
using BlackJack.Cards;
using BlackJack;

namespace BlackJack.Games{

    public class GoFishGame : Game {

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

        public override void Start(){
            deck.Shuffle();
            playerHand = deck.DealCards(7);
            dealerHand = deck.DealCards(7);
            Engine();
            End();
        }

        public override void Engine(){
            bool keepPlaying = true;
            Console.WriteLine("Welcome to Go Fish! Enter 1 for Ace, 11 for Jack, 12 for Queen, and 13 for King");
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
            string currentHand = "";
            int book = 0;    
            do{            
                currentHand = PrintHand(playerHand);
                Console.WriteLine("Your hand: " + currentHand + "\nWhat rank do you want to ask for?");
                bool x = Int32.TryParse(Console.ReadLine(),out answer);
                if(x){
                    fishes = FindCards(dealerHand, answer, out hasCard);
                    if(hasCard){
                        Console.WriteLine("Dealer has " + fishes.Count + " " + answer + "s");
                        foreach(Card card in fishes){
                            playerHand.Add(card);
                            dealerHand.Remove(card);
                        }                       
                    } else {     
                        Console.WriteLine("Dealer has no " + answer + "s. Go Fish!");
                        playerHand = GoFishPlayer(playerHand, answer, out hasCard);                       
                    }
                }
                playerHand = CheckBooks(playerHand, out book);
                playerBooks += book;
                if(book == 1){
                    Console.WriteLine("You scored a book! Your book count is now " + playerBooks);
                }
            } while(hasCard);
            
        }

        //not finished
        public void DealerTurn(){
            Random random = new Random();
            bool hasCard = false;
            List<Card> fishCards = new List<Card>();
            int books = 0;
            do{
                Card dealerPick = dealerHand[random.Next(dealerHand.Count)];
                int dPickRank = (int)dealerPick.rank;
                Console.WriteLine("Dealer wants to know if you have any " + dPickRank + "s.");
                fishCards = FindCards(playerHand, dPickRank, out hasCard);
                if(hasCard){
                    Console.WriteLine("You have " + fishCards.Count + " " + dPickRank + "s");
                    foreach(Card card in fishCards){
                        dealerHand.Add(card);
                        playerHand.Remove(card);
                    }
                } else {
                    Console.WriteLine("You have no " + dPickRank + "s.  Dealer must Go Fish!");
                    dealerHand = GoFishDealer(dealerHand, dPickRank, out hasCard);
                }
                dealerHand = CheckBooks(dealerHand, out books);
                dealerBooks += books;
                if(books == 1){
                    Console.WriteLine("Dealer scored a book! Dealer's book count is now " + dealerBooks);
                }
            } while(hasCard);
            
        }

        private List<Card> FindCards(List<Card> otherHand, int answer, out bool hasCard){
            hasCard = false;
            List<Card> fishes = new List<Card>();
            foreach(Card card in otherHand){
                if((int)card.rank == answer){
                    fishes.Add(card);
                    hasCard = true;
                } 
            }
            return fishes;
        }

        
        private List<Card> GoFishPlayer(List<Card> hand, int cardValue, out bool hasCard){
            StringBuilder builder = new StringBuilder();
            Card card = deck.DrawCard();
            hand.Add(card); 
            builder.Append("You drew a " + card.ToString() + "\n");
            if((int)card.rank == cardValue){
                builder.Append("You fished your wish! You get another turn.\n--------");
                hasCard = true;
            } else {
                builder.Append("You did not fish your wish.\n----------");
                hasCard = false;
            }
            Console.WriteLine(builder.ToString());
            return hand;
        }

        private List<Card> GoFishDealer(List<Card> hand, int cardValue, out bool hasCard){
            StringBuilder builder = new StringBuilder();
            Card card = deck.DrawCard();
            hand.Add(card); 
            if((int)card.rank == cardValue){
                builder.Append("Dealer fished his wish! Dealer gets another turn.");
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

        private List<Card> CheckBooks(List<Card> hand, out int books){
            int book = 0;
            int bookCardRank = 0;
            bool has = false;
            List<Card> cards = new List<Card>();
            foreach(Card card in hand){
                cards = FindCards(hand, (int)card.rank, out has);
                if(cards.Count == 4){
                    book = 1;  
                    bookCardRank = (int)card.rank;
                } 
            }
            if(book > 0){
                foreach(Card card in cards){
                    hand.Remove(card);
                }
            }            
            books = book;
            return hand;
        }

        private bool CheckSizes(){
            if(playerHand.Count == 0 || dealerHand.Count == 0 || deck.cards.Count == 0){
                return false;
            }
            return true;
        }

        public override void End(){
            Console.WriteLine("Thank you for playing Go Fish!");
        }

    }
}