using System;
using BlackJack.Games;
using BlackJack;


namespace BlackJack {

    public class MainMenu {

        public Person player {get;set;}

        public MainMenu(){
            this.player = new Person();
        }

        public void Welcome(){
            Console.WriteLine("Welcome to the Casino!");
            Person player = CreatePerson();
            do{
                Game game = ChooseGame(player);
                game.Start();
            } while (PlayAgain());

        }

        public Person CreatePerson(){
            Console.WriteLine("What is your name?");
            player.name = Console.ReadLine();
            Console.WriteLine("How many chips would you like to start with?");
            int chips = 0;
            Int32.TryParse(Console.ReadLine(),out chips);
            player.chips = chips;
            return player;
        }

        public Game ChooseGame(Person player){
            Game game;
            string choice = "";
            do{
                Console.WriteLine("What game would you like to play? BlackJack, Slots, Go Fish, or War?");
                choice = Console.ReadLine().ToLower();
                if(choice == "blackjack" || choice == "black jack"){
                    game = new BlackJackGame(player);
                } else if (choice == "go fish" || choice == "gofish") {
                    game = new GoFishGame(player);
                } else if (choice == "war"){
                    game = new War(player);
                } else {
                    game = null;
                }
            } while(choice!="blackjack" && choice!="black jack" && choice!="go fish" && choice!="gofish" && choice !="war" && choice!="slots");
            Console.WriteLine("Going to " + choice + "!");
            return game;
        }

        public bool PlayAgain(){
            string playAgain = "";   
            bool choice = false;         
            do{
                Console.WriteLine("Would you like to play a different game? y or n");
                playAgain = Console.ReadLine().ToLower();
                if(playAgain == "y" || playAgain == "yes"){
                    choice = true;
                } else if (playAgain == "n" || playAgain == "no"){
                    choice = false;
                }
            } while (playAgain != "y" && playAgain != "yes" && playAgain !="n" && playAgain!="no");
            return choice;
        }
    }
}