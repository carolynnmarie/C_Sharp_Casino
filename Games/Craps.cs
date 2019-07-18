using System;
using System.Text;
using System.Collections.Generic;
using BlackJack;
using BlackJack.Dice;

namespace BlackJack.Games{

    public class Craps : Game{

        public Person player {get;set;}
        Die[] dice {get;set;}
        int throwTotal {get;set;}
        Dictionary<string, int> bets {get;set;}  
        int fieldNumb {get;set;}
        int placeNumb {get;set;}
        Dictionary<int, double> fieldPayout {get;set;}
        Dictionary<int, double> placeWin {get;set;}
        Dictionary<int, double> placeLose {get;set;}
        Dictionary<int, double> passLineComePointOdds {get;set;}
        Dictionary<int, double> dontPassLineDontComePointOdds {get;set;}
        Dictionary<string, List<int>> points {get;set;}
        List<int> pointList {get;set;}
        List<int> comePointsList {get;set;}
        List<int> dontComePointsList {get;set;}

        public Craps(Person player){
            this.player = player;
            this.dice = new Die[2];
            this.throwTotal = 0;
            this.bets = new Dictionary<string, int>();
            this.points = new Dictionary<string, List<int>>();
            this.pointList = new List<int>();
            this.comePointsList = new List<int>();
            this.dontComePointsList = new List<int>
            this.fieldNumb = 0;
            this.placeNumb = 0;
            this.fieldPayout = new Dictionary<int, double>();
            fieldPayout.Add(3, 1.0);
            fieldPayout.Add(4, 1.0);
            fieldPayout.Add(9, 1.0);
            fieldPayout.Add(10, 1.0);
            fieldPayout.Add(11, 1.0);
            fieldPayout.Add(2, 2.0);
            fieldPayout.Add(12, 2.0);
            this.placeWin = new Dictionary<int, double>();
            placeWin.Add(6, 1.16);
            placeWin.Add(8, 1.16);
            placeWin.Add(5, 1.4);
            placeWin.Add(9, 1.4);
            placeWin.Add(4, 1.8);
            placeWin.Add(10, 1.8);
            this.placeLose = new Dictionary<int, double>();
            placeLose.Add(6, 0.8);
            placeLose.Add(8, 0.8);
            placeLose.Add(5, 0.62);
            placeLose.Add(9, 0.62);
            placeLose.Add(4, 0.45);
            placeLose.Add(10, 0.45);
            this.passLineComePointOdds = new Dictionary<int, double>();
            passLineComePointOdds.Add(6, 1.2);
            passLineComePointOdds.Add(8, 1.2);  
            passLineComePointOdds.Add(5, 1.5);
            passLineComePointOdds.Add(9, 1.5); 
            passLineComePointOdds.Add(4, 2.0);
            passLineComePointOdds.Add(10, 2.0);
            this.dontPassLineDontComePointOdds = new Dictionary<int, double>();
            dontPassLineDontComePointOdds.Add(6, .83);
            dontPassLineDontComePointOdds.Add(8, .83);
            dontPassLineDontComePointOdds.Add(5, .66);
            dontPassLineDontComePointOdds.Add(9, .66);
            dontPassLineDontComePointOdds.Add(4, .5);
            dontPassLineDontComePointOdds.Add(10, .5);                                              
        }

        public override void Start(){
            Console.WriteLine("Welcome to Craps!  You currently have " + player.chips + " chips.");
            Engine();
            End();
        }

        public override void Engine(){
            PlaceFirstBet();
            ComeOutRoll();
            do{
                PhaseTwoRoll();
                Console.WriteLine("Would you like to roll again? yes or no");               
            } while (Console.ReadLine() != "no");
        }

        public void PlaceFirstBet(){
            Console.WriteLine("First, you must place your initial bet on the Pass Line or the Don't Pass Line. " + 
            "To place a Pass Line bet, type 'pass'. To place a Don't Pass Line bet, type 'don't pass'.");
            if(Console.ReadLine().ToLower()== "pass"){
                bets.Add("pass", MakeBet());
            } else {
                bets.Add("don't pass", MakeBet());
            }
        }

        public int MakeBet(){
            int answer = 0;            
            do{
                Console.WriteLine("How much would you like to bet?");
                Int32.TryParse(Console.ReadLine(),out answer);
                if(answer > player.chips){
                    Console.WriteLine("Insufficient funds. You currently have " + player.chips + " chips.");
                }
                if(answer == 0){
                    Console.WriteLine("Please enter numeric value.");
                }
                if(player.chips == 0){
                    Console.WriteLine("Out of funds");
                    break;
                }
            } while (answer == 0 || answer > player.chips);
            player.chips -= answer;
            return answer;
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
                    List<int> pointList = new List<int>();
                    pointList.Add(throwTotal);
                    points.Add("all",pointList);
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
                    List<int> pList = new List<int>();
                    pointList.Add(throwTotal);
                    points.Add("all",pointList);
                    
                }
            }
            Console.WriteLine(builder.ToString());
        }

        //incomplete
        public void PhaseTwoRoll(){
            MakePhaseTwoBet();
            RollDice()
            int throwTotal = TotalDiceValue();
            Console.WriteLine("You rolled a " + diceTotal);
            PhaseTwoResultsHandler();
        }

        //incomplete -need to add field number and/or place number to bet on
        public void MakePhaseTwoBet(){
            string answer = "";
            do{
                Console.WriteLine("What type of bet would you like to place?\nPass, don't pass, come, don't come, field, "+
                "place, pass odds, don't pass odds, come point odds, or don't come point odds?");
                answer = Console.ReadLine().ToLower();
            } while (answer != "pass" && answer != "don't pass" && answer != "come" && answer != "don't come" && answer != "field" 
            && answer != "place" && answer != "pass odds" && answer != "don't pass odds" 
            && answer != "come point odds"&& answer != "don't come point odds" );
            if(answer == "field"){
                Console.WriteLine("What number would you like to put your field bet on? 2, 3, 4, 9, 10, 11, or 12?");
                Int32.TryParse(Console.ReadLine(), out fieldNumb);
            }
            if(answer == "place"){
                Console.WriteLine("What number would you like to put your place bet on? 4, 5, 6, 8, 9, or 10?");
                Int32.TryParse(Console.ReadLine, out placeNumb);
            }
            Console.WriteLine("How much would you like to bet?");
            int betAmnt = 0;
            Int32.TryParse(Console.ReadLine(),out betAmnt);
            bets.Add(answer,betAmnt);
        }

        public void PhaseTwoResultsHandler(){
            StringBuilder builder = new StringBuilder();            
            if(bets.ContainsKey("come")){
                builder.Append(ComeBetResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("don't come")){
                builder.Append(DontComeBetResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("pass")){
                builder.Append(PassLineResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("don't pass")){
                builder.Append(DontPassLineResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("field")){
                builder.Append(FieldResult())
                        .Append("\n");
            }            
            if(bets.ContainsKey("pass odds")){
                builder.Append(PassLineOddsResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("don't pass odds")){
                builder.Append(DontPassLineOddsResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("come point odds")){
                builder.Append(ComePointOddsResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("don't come point odds")){
                builder.Append(DontComePointOddsResult())
                        .Append("\n");
            }
            if(bets.ContainsKey("place")){
                builder.Append(PlaceResult())
                        .Append("\n");
            }                
            Console.WriteLine(builder.ToString());
        }

        private string PassLineResult(bool winLose){
            string result = "";
            if(winLose){
                result = "Pass Line won!  You won " + bets["pass"] + " chips!";
                player.chips += bets["pass"]*2;
            } else {
                result = "Pass Line lost. You lost your bet of " + bets["pass"] + " chips.";
            }
            bets.Remove("pass");
            return result;
        }

        private string DontPassLineResult(bool winLose){
            string result = "";
            if(winLose){
                result = "Don't Pass Line won!  You won " + bets["don't pass"] + " chips.";
                player.chips += bets["don't pass"]*2;
            } else {
                result = "Don't Pass Line lost. You lost your bet of " + bets["don't pass"] + " chips.";
            }
            bets.Remove("don't pass");
            return result;
        }

        public string ComeBetResult(){
            string result = "";
            if(throwTotal == 7 || throwTotal == 11){
                result = "Come bet wins! You won " + bets["come"] + " chips!";
                player.chips += bets["come"]*2;
            } else if(throwTotal == 2 || throwTotal == 3 || throwTotal == 12){
                result = "Come bet loses. You lost " + bets["come"] + " chips.";
            } else {
                result = "Your come bet is now a point.";
                comePointsList.Add(throwTotal);
                points.Add("come",comePointsList);
            }
            bets.Remove("come");
            return result;
        }

        public string DontComeBetResult(){
            string result = "";
            if(throwTotal == 7 || throwTotal == 11){
                result = "Don't Come bet loses. You lost " + bets["don't come"] + " chips.";
            } else if (throwTotal == 2 || throwTotal == 3){
                result = "Don't Come bet wins! You won " + bets["don't come"] + " chips!";
                player.chips += bets["don't come"]*2;
            } else if (throwTotal == 12){
                result = "Don't Come bet is a push. You neither win nor lose chips.";
                player.chips += bets["don't come"];
            } else {
                result = "Don't Come bet is now a point.";
                points.Add("don't come", throwTotal);                
            }
            bets.Remove("don't come");
            return result;
        }


        public string PassLineOddsResult(){
            string results = "";
            if(throwTotal == points["all"]){
                foreach(KeyValuePair<int,double> pair in passLineComePointOdds){
                    if(points["all"]== pair.Key){
                        int chipsWon = pair.Value*bets["pass odds"];
                        results = "Pass odds wins! Your point on " + pair.Key + " won " + chipsWon;
                        player.chips += (chipsWon + bets["pass odds"]);
                    }
                }
                bets.Remove("pass odds");
            } else if (throwTotal == 7) {
                results = "Your pass line odds lost.";
                bets.Remove("pass odds");
            }
            return results;
        }

        public string DontPassLineOddsResult(){
            string results = "";
            if(throwTotal == 7){
                foreach(KeyValuePair<int,double> pair in passLineComePointOdds){
                    if(points["all"]== pair.Key){
                        int chipsWon = pair.Value*bets["don't pass odds"];
                        results = "Don't pass odds wins! You won " + chipsWon;
                        player.chips += (chipsWon + bets["don't pass odds"]);
                    }
                }
                bets.Remove("don't pass odds");
            } else if (throwTotal == points["all"]) {
                results = "Your don't pass line odds lost.";
                bets.Remove("don't pass odds");
            }
            return results;
        }

        public string ComePointOddsResult(){
            
        }

        public string DontComePointOddsResult(){

        }

        public string PlaceResult(){
            string placeRes = "";
            int pChips = 0;
            if(throwTotal == 7){
                placeRes = "You lose your place bet!";
                bets.Remove("place");
            } else if (throwTotal == placeNumb){
                foreach(KeyValuePair<int,double> pair in fieldPayout){
                    if(throwTotal == pair.Key){
                        pChips += bets["place"]*pair.Value;
                    }
                }
                placeRes = "Your place bet on " + placeNumb + " wins! You win " + pChips + " chips!";
                player.chips += pChips + bets["place"];
                bets.Remove("place");
            }
        }

        public string FieldResult(){
            string fieldRes = "";
            foreach(KeyValuePair<int,double> pair in fieldPayout){
                if(throwTotal == pair.Key){
                    int fieldChips = bets["field"]*pair.Value;
                    fieldRes = "Your field bet on " + fieldNumb + " of " + bets["field"] + "won " + fieldChips;
                    player.chips += (bets["field"] + fieldChips);
                }
            }
            bets.Remove("field");
            fieldNumb = 0;
        }


        public void RollDice(){
            for(int i = 0; i<2; i++){
                Die die = new Die();
                dice[i].dieFace = die.RollDie();
            }
        }

        public int TotalDiceValue(){
            int total = 0;
            for(int i = 0; i<2; i++){
                total += (int)dice[i].dieFace;
            }
            return total;
        }


        public override void End(){
            Console.WriteLine("Thank you for playing Craps!  Returning to Main Menu");
        }

    }
}
