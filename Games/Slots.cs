using System;

namespace BlackJack.Games{

    public class Slots: Game {
        
        string[] fruits {get;set;}
        Person player {get;set;}

        public Slots(Person player){
            this.player = player;
            this.fruits = new string[]{"\uD83C\uDF52", "\uD83C\uDF4E", "\uD83C\uDF47", "\uD83C\uDF4B", "\uD83C\uDF4C", "\uD83C\uDF49", 
                                    "\uD83C\uDF4A", "\u3374", "7"};
        }

        public override void Start(){
            Console.WriteLine("This is a 3 reel single line slot machine.  Each spin is 1 chip.\n  The payout for 3 of " +
                "the same fruit in a line is 21 chips.\n  The payout for three bars in a line is 30 chips.\n  " +
                "The payout for three 7's in a line is 45 chips");
            Engine();
            End();
        }

        public override void Engine(){
            Console.WriteLine("Press any key to pull the lever.  To exit the game at any time type the word exit.");
            string exit = Console.ReadLine();
            while(exit != "exit"){
                player.chips -= 1;
                PullLever();
                Console.WriteLine("Your current chip balance is " + player.chips + ". Would you like to spin again?");
                exit = Console.ReadLine();
            }
        }

        public void PullLever(){
            string[] pulled = Spin();
            Console.WriteLine("Your spin was " + pulled[0] + ", " + pulled[1] + ", " + pulled[2]);
            if(pulled[0]==pulled[1] && pulled[0]==pulled[2]){
                if(pulled[0]=="7"){
                    Console.WriteLine("You won 45 chips!");
                    player.chips += 46;
                } else if (pulled[0]=="\u3374"){
                    Console.WriteLine("You won 30 chips!");
                    player.chips += 31;
                } else {
                    Console.WriteLine("You won 21 chips!");
                    player.chips += 22;
                }          
            } else {
                Console.WriteLine("You did not get 3 in a row.");
            }
        }

        public string[] Spin(){
            Random random = new Random();
            string[] result = new string[3];
            for(int i = 0; i < 3; i++){
                result[i] = fruits[random.Next(9)];
            }
            return result;
        }

        public override void End(){
            Console.WriteLine("Thank you for playing the slots!  Returning to Main Menu.\n");
        }
    }
}