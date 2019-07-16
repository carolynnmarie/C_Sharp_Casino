using System;
using System.Text;
using System.Collections.Generic;
using BlackJack;
using BlackJack.Dice;

namespace BlackJack.Games{

    public class Craps : Game{

        public Person player {get;set;}
        public Die[] dice {get;set;}
        public int throwTotal {get;set;}
        public string[] betTypes {get;set;}
        public Dictionary<string, int> bets {get;set;}  
        public Dictionary<int, double> fieldPayout {get;set;}
        public Dictionary<string, Dictionary<int, double>> oddPayouts {get;set;}         

        public Craps(Person player){
            this.player = player;
            this.dice = new Die[2];
            this.throwTotal = 0;
            this.betTypes = new string[] {"Pass Line", "Don't Pass Line", "Come", "Don't Come", "Pass Line Odds", "Don't Pass Line Odds", 
                            "Come Odds", "Don't Come Odds", "Field","Place"};
            this.bets = new Dictionary<string, int>();
            this.fieldPayout = new Dictionary<int, double>();
            fieldPayout.Add(3, 1.0);
            fieldPayout.Add(4, 1.0);
            fieldPayout.Add(9, 1.0);
            fieldPayout.Add(10, 1.0);
            fieldPayout.Add(11, 1.0);
            fieldPayout.Add(2, 2.0);
            fieldPayout.Add(12, 2.0);

            Dictionary<int, double> placeWin = new Dictionary<int, double>();
            placeWin.Add(6, 1.16);
            placeWin.Add(8, 1.16);
            placeWin.Add(5, 1.4);
            placeWin.Add(9, 1.4);
            placeWin.Add(4, 1.8);
            placeWin.Add(10, 1.8);
            Dictionary<int, double> placeLose = new Dictionary<int, double>();
            placeLose.Add(6, 0.8);
            placeLose.Add(8, 0.8);
            placeLose.Add(5, 0.62);
            placeLose.Add(9, 0.62);
            placeLose.Add(4, 0.45);
            placeLose.Add(10, 0.45);
            Dictionary<int, double> passLineComeOdds = new Dictionary<int, double>();
            passLineComeOdds.Add(6, 1.2);
            passLineComeOdds.Add(8, 1.2);  
            passLineComeOdds.Add(5, 1.5);
            passLineComeOdds.Add(9, 1.5); 
            passLineComeOdds.Add(4, 2.0);
            passLineComeOdds.Add(10, 2.0);
            Dictionary<int, double> dontPassLineDontComeOdds = new Dictionary<int, double>();
            dontPassLineDontComeOdds.Add(6, .83);
            dontPassLineDontComeOdds.Add(8, .83);
            dontPassLineDontComeOdds.Add(5, .66);
            dontPassLineDontComeOdds.Add(9, .66);
            dontPassLineDontComeOdds.Add(4, .5);
            dontPassLineDontComeOdds.Add(10, .5);

            this.oddPayouts = new Dictionary<string, Dictionary<int,double>>();
            oddPayouts.Add("place win", placeWin);
            oddPayouts.Add("place lose", placeLose);
            oddPayouts.Add("pass line odds", passLineComeOdds);
            oddPayouts.Add("don't pass line odds", dontPassLineDontComeOdds);
            oddPayouts.Add("come odds", passLineComeOdds);
            oddPayouts.Add("don't come odds", dontPassLineDontComeOdds);                                                
        }

        public override void Start(){
            Console.WriteLine("Welcome to Craps!  You currently have " + player.chips + " chips.");
            Engine();
            End();
        }

        public override void Engine(){
            PlaceFirstBet();
            ComeOutRoll();
        }

        public int MakeBet(){
            int answer = 0;            
            do{
                Console.WriteLine("How much would you like to bet?");
                Int32.TryParse(Console.ReadLine(),out answer);
                if(answer > player.chips){
                    Console.WriteLine("Insufficient funds. You currently have " + player.chips + " chips.")
                }
                if(answer == 0){
                    Console.WriteLine("Please enter numeric value.");
                }
                if(player.chips == 0){
                    Console.WriteLine("Out of funds");
                    break;
                }
            } while (answer == 0 || answer > player.chips)
            player.chips -= answer;
            return answer;
        }

        public void PlaceFirstBet(){
            Console.WriteLine("First, you must place your initial bet on the Pass Line or the Don't Pass Line\n
            To place a Pass Line bet, type 'pass'. To place a Don't Pass Line bet, type 'don't pass'.");
            if(Console.ReadLine().ToLower()== "pass"){
                bets.Add("pass", MakeBet());
            } else {
                bets.Add("don't pass", MakeBet());
            }
        }


        public void ComeOutRoll(){
            RollDice();
            int throwTotal = TotalDiceValue();
            StringBuilder builder = new StringBuilder("Time to make your first roll! You rolled a " + throwTotal);
            if(bets.ContainsKey("pass")){
                if(throwTotal == 2 || throwTotal == 3){
                    builder.Append(". You crapped out. ")
                            .Append(PassLineResult(false));
                } else if (throwTotal == 7 || throwTotal == 11){
                    builder.Append(". You rolled a natural! ")
                            .Append(PassLineResult(true));                   
                } else if (throwTotal == 12){
                    builder.Append(PassLineResult(false));
                } else {
                    builder.Append(". The point is now ")
                            .Append(throwTotal + ".");
                }
            } else if(bets.ContainsKey("don't pass")){
                if(throwTotal == 2 || throwTotal == 3){
                    builder.Append(". You crapped out. ")
                            .Append(DontPassLineResult(true));
                } else if (throwTotal == 7 || throwTotal == 11){
                    builder.Append(". You rolled a natural. ")
                            .Append(DontPassLineResult(false));
                } else if (throwTotal == 12){
                    builder.Append(". Don't Pass bets are pushed to next round.");
                } else {
                    builder.Append(". The point is now ")
                            .Append(throwTotal + ".");                    
                }
            }
        }

        public void RollDice(){
            for(int i = 0; i<2; i++){
                Die die = new Die();
                dice[i] = die.RollDie();
            }
        }

        public int TotalDiceValue(){
            int total = 0;
            for(int i = 0; i<2; i++){
                total += (int)dice[i];
            }
            return total;
        }

        private string PassLineResult(bool winLose){
            string result = "";
            if(winLose){
                winLose = "Pass Line won!  You won " + bet["pass"] + " chips!";
                player.chips += bet["pass"]*2;
            } else {
                winLose = "Pass Line lost. You lost your bet of " + bet["pass"] + " chips.";
            }
            bet.Remove("pass");
            return win;
        }

        private string DontPassLineResult(bool winLose){
            string result = "";
            if(winLose){
                result = "Don't Pass Line won!  You won " + bet["don't pass"] + " chips.";
                player.chips += bet["don't pass"]*2;
            } else {
                result = "Don't Pass Line lost. You lost your bet of " + bet["don't pass"] + " chips."
            }
            bet.Remove("don't pass");
            return result;
        }


        public override void End(){

        }

    }
}

//second dictionary of <string, int> for the bet, assigning same string name as key as above with corresponding bet as
//value.  
// for odds payouts on second roll:
//if (throwTotal == 4,5,6,8,9, or 10)
//foreach loop of keys
//if (dictionaryPayoutKey == dictionaryBetKey && dictionaryBet[key] !=0){
// "your bet on " + dictionaryBetKey + " of " + dictionaryBet[key] + "won! You win " + (insert appropriate math here)
// other appropriate method logic here