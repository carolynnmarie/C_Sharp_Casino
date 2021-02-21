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
        Dictionary<string, int> points {get;set;}      
        Dictionary<string, int> bets {get;set;}
        Dictionary<int,int> comePointBets {get;set;}
        Dictionary<int,int> dontComePointBets {get;set;}
        Dictionary<string, int> fieldPlaceNumbers {get;set;}
        Dictionary<int, double> fieldPayout {get;set;}
        Dictionary<int, double> placeWinPayout {get;set;}
        Dictionary<int, double> placeLosePayout {get;set;}
        Dictionary<int, double> passComePntOddsPayout {get;set;}
        Dictionary<int, double> dontPassDontComePntOddsPayout {get;set;}

        public Craps(Person player){
            this.player = player;
            this.dice = new Die[2]{new Die(), new Die()};
            this.throwTotal = 0;
            this.bets = new Dictionary<string, int>();
            this.points = new Dictionary<string, int>();
            this.fieldPlaceNumbers = new Dictionary<string,int>();
            this.comePointBets = new Dictionary<int,int>();
            this.dontComePointBets = new Dictionary<int,int>();
            CrapsPayouts payouts = new CrapsPayouts();
            this.fieldPayout = payouts.fieldPayout;
            this.placeWinPayout = payouts.placeWin;
            this.placeLosePayout = payouts.placeLose;
            this.passComePntOddsPayout = payouts.passLineComePointOdds;
            this.dontPassDontComePntOddsPayout = payouts.dontPassLineDontComePointOdds;          
        }

        public override void Start(){
            Console.WriteLine("Welcome to Craps!  You currently have " + player.chips + " chips.");
            Engine();
            End();
        }

        public override void Engine(){
            PlaceFirstBet();
            ComeOutRoll();
            Console.WriteLine("Would you like to do a phase two roll? yes or no");            
            while(Console.ReadLine().ToLower() == "yes"){
                PhaseTwoRoll();
                Console.WriteLine("Would you like to roll again? yes or no");               
            }
        }

        public void PlaceFirstBet(){
            Console.WriteLine("First, you must place your initial bet on the Pass Line or the Don't Pass Line. " + 
            "To place a Pass Line bet, type 'pass'. To place a Don't Pass Line bet, type 'don't pass'.");
            if(Console.ReadLine().ToLower()== "pass"){
                MakeBet("pass");
            } else {
                MakeBet("don't pass");
            }
        }

        public void MakeBet(string betType){
            int answer = 0;            
            do{
                Console.WriteLine("How much would you like to bet?");
                Int32.TryParse(Console.ReadLine(),out answer);
                if(player.chips == 0){
                    Console.WriteLine("Out of funds");
                    break;
                } else if(answer > player.chips)
                    Console.WriteLine("Insufficient funds. You currently have " + player.chips + " chips.");
                if(answer == 0)
                    Console.WriteLine("Please enter numeric value.");               
                
            } while (answer == 0 || answer > player.chips);
            player.chips -= answer;
            bets.Add(betType,answer);
        }

        public void ComeOutRoll(){
            RollDice();
            throwTotal = TotalDiceValue();
            Console.WriteLine("Time to make your first roll! You rolled a " + throwTotal);
            if(bets.ContainsKey("pass")){
                PassLineRoll();
            } else {
                DontPassLineRoll();
            }
        }

        public void PhaseTwoRoll(){
            string type = GetPhaseTwoBetType();
            MakePhaseTwoBet(type);
            RollDice();
            int throwTotal = TotalDiceValue();
            Console.WriteLine("You rolled a " + throwTotal);
            PhaseTwoResultsHandler();
        }
        
        public string GetPhaseTwoBetType(){
            string[] betTypesArray = {"pass","don't pass","come","don't come","field","place to win", "place against","pass odds",
                                    "don't pass odds", "come point odds","don't come point odds"};
            List<string> betTypes = new List<string>(betTypesArray);
            string answer = "";
            do{
                Console.WriteLine("What type of bet would you like to place?\nPass, don't pass, come, don't come, field, "+
                "place to win, place against, pass odds, don't pass odds, come point odds, or don't come point odds?");
                answer = Console.ReadLine().ToLower();
            } while (!betTypes.Contains(answer));
            return answer;
        }

        private void MakePhaseTwoBet(string answer){
            Console.WriteLine("You have " + player.chips + "chips.")
            if(answer == "field"){
                Console.WriteLine("What number would you like to put your field bet on? 2, 3, 4, 9, 10, 11, or 12?");
                int fieldNumber;
                Int32.TryParse(Console.ReadLine(), out fieldNumber);
                fieldPlaceNumbers.Add("field", fieldNumber);
                MakeBet(answer);
            } else if(answer == "place to win"){
                GetPlaceNumber("place to win");
                MakeBet(answer);
            } else if (answer == "place against"){
                GetPlaceNumber("place against");
                MakeBet(answer);
            } else if(answer == "come point odds"){
                comePointBets = MakeComeDontComePointOddsBets(comePointBets, "come");
            } else if(answer == "don't come points odds" && dontComePointBets.Count == 0){
                dontComePointBets = MakeComeDontComePointOddsBets(dontComePointBets, "don't come");
            } else {
                MakeBet(answer);
            }
        }

        private void GetPlaceNumber(string betType){
                Console.WriteLine("What number would you like to put your " + betType + " bet on? 4, 5, 6, 8, 9, or 10?");
                int y;
                Int32.TryParse(Console.ReadLine(), out y);
                fieldPlaceNumbers.Add(betType, y);
                MakeBet(betType);
        }

        private Dictionary<int,int> MakeComeDontComePointOddsBets(Dictionary<int,int> betType, string pointType){
            StringBuilder builder = new StringBuilder("What" + pointType + " point would you like to bet on? The points you can bet on are ");
            foreach(KeyValuePair<int,int> pair in betType){
                builder.Append(pair.Key).Append(", ");
            }
            Console.WriteLine(builder.ToString());
            int cPAnswer = 0;
            Int32.TryParse(Console.ReadLine(), out cPAnswer);
            if(betType.ContainsKey(cPAnswer)){
                Console.WriteLine("How much would you like to bet?");
                int betAmnt = 0;
                Int32.TryParse(Console.ReadLine(),out betAmnt);
                betType[cPAnswer] = betAmnt;
            } else {
                Console.WriteLine("You don't have any " + pointType + " points on that number");
            }
            return betType;
        }

        public void PhaseTwoResultsHandler(){
            StringBuilder builder = new StringBuilder();            
            if(bets.ContainsKey("come"))
                builder.Append(ComeBetResult()).Append("\n");            
            if(bets.ContainsKey("don't come"))
                builder.Append(DontComeBetResult()).Append("\n");            
            if(bets.ContainsKey("pass"))
                builder.Append(PassLineRoll()).Append("\n");           
            if(bets.ContainsKey("don't pass"))
                builder.Append(DontPassLineRoll()).Append("\n");           
            if(bets.ContainsKey("field"))
                builder.Append(FieldResult()).Append("\n");                       
            if(bets.ContainsKey("pass odds"))
                builder.Append(PassLineOddsResult()).Append("\n");            
            if(bets.ContainsKey("don't pass odds"))
                builder.Append(DontPassLineOddsResult()).Append("\n");
            if(bets.ContainsKey("place to win"))
                builder.Append(PlaceToWinResult()).Append("\n");
            if(bets.ContainsKey("place against"))
                builder.Append(PlaceAgainstResult()).Append("\n");        
            if(comePointBets.Count >0)
                builder.Append(ComePointOddsResult()).Append("\n");            
            if(dontComePointBets.Count >0)
                builder.Append(DontComePointOddsResult()).Append("\n");                                       
            Console.WriteLine(builder.ToString());
        }

        private void PassLineRoll(){
            StringBuilder builder = new StringBuilder();
            if(throwTotal == 2 || throwTotal == 3){
                builder.Append("You crapped out. ")
                        .Append(PassLineResult(false));
              } else if (throwTotal == 7 || throwTotal == 11){
                builder.Append("You rolled a natural! ")
                        .Append(PassLineResult(true));           
             } else if (throwTotal == 12){
                builder.Append(PassLineResult(false));
            } else {
                builder.Append(ComeOutRollPoint(true));

            }
            bets.Remove("pass");
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
            return result;
        }

        public string ComeOutRollPoint(bool pointOrNot){
            StringBuilder builder = new StringBuilder();
            if(pointOrNot) {
                builder.Append("The point is now ")
                          .Append(throwTotal + ".");
                points.Add("all",throwTotal);
            }
            return builder.ToString();
        }

        private void DontPassLineRoll(){
            bool pointOrNot = true;
            StringBuilder builder = new StringBuilder();
            if(throwTotal == 2 || throwTotal == 3){
                builder.Append(". You crapped out. ")
                        .Append(DontPassLineResult(true));
            } else if (throwTotal == 7 || throwTotal == 11){
                builder.Append(". You rolled a natural. ")
                        .Append(DontPassLineResult(false));
            } else if (throwTotal == 12){
                builder.Append(". Don't Pass bets are pushed to next round.");
            } else {
                pointOrNot = false;
                builder.Append(ComeOutRollPoint(pointOrNot));
            }
            bets.Remove("don't pass");
            Console.WriteLine(builder.ToString());
        }

        private string DontPassLineResult(bool winLose){
            string result = "";
            if(winLose){
                result = "Don't Pass Line won!  You won " + bets["don't pass"] + " chips.";
                player.chips += bets["don't pass"]*2;
            } else {
                result = "Don't Pass Line lost. You lost your bet of " + bets["don't pass"] + " chips.";
            }
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
                comePointBets.Add(throwTotal,0);
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
                dontComePointBets.Add(throwTotal,0); 
            }
            bets.Remove("don't come");
            return result;
        }

        public string PassLineOddsResult(){
            string results = "";
            if(throwTotal == points["all"]){
                foreach(KeyValuePair<int,double> pair in passComePntOddsPayout){
                    if(points["all"]== pair.Key){
                        int chipsWon = (int)Math.Round(pair.Value*bets["pass odds"]);
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
                foreach(KeyValuePair<int,double> pair in passComePntOddsPayout){
                    if(points["all"]== pair.Key){
                        int chipsWon = (int)Math.Round(pair.Value*bets["don't pass odds"]);
                        results += "Don't pass odds wins! You won " + chipsWon;
                        player.chips += (chipsWon + bets["don't pass odds"]);
                    }
                }
                bets.Remove("don't pass odds");
            } else if (throwTotal == points["all"]) {
                results += "Your don't pass line odds lost.";
                bets.Remove("don't pass odds");
            }
            return results;
        }


        public string PlaceToWinResult(){
            string placeRes = "";
            int pChips = 0;
            if(throwTotal == 7){
                placeRes = "You lose your place to win bet!";
                bets.Remove("place to win");
                fieldPlaceNumbers.Remove("place to win");
            } else if (throwTotal == fieldPlaceNumbers["place to win"]){
                foreach(KeyValuePair<int,double> pair in placeWinPayout){
                    if(throwTotal == pair.Key){
                        pChips += (int)Math.Round(bets["place to win"]*pair.Value);
                    }
                }
                placeRes = "Your place to win bet on " + fieldPlaceNumbers["place to win"] + " wins! You win " + pChips + " chips!";
                player.chips += pChips + bets["place to win"];
                bets.Remove("place to win");
                fieldPlaceNumbers.Remove("place to win");
            }
            return placeRes;
        }

        //incomplete
        public string PlaceAgainstResult(){
            string placeAgainst = "";
            int chips = 0;
            if(throwTotal == fieldPlaceNumbers["place against"]){
                placeAgainst = "You lost your place against bet! You lost " + bets["place against"] + " chips.";
                fieldPlaceNumbers.Remove("place against");
                bets.Remove("place against");
            } else if (throwTotal == 7){
                placeAgainst = "You won your place against bet!";
                foreach(KeyValuePair<int, double> pair in placeLosePayout){
                    if(pair.Key == throwTotal){
                        chips += (int)Math.Round(pair.Value*bets["place against"]);
                    }
                }
                placeAgainst += "You won " + chips + " chips!";
                player.chips += chips + bets["place against"];
                fieldPlaceNumbers.Remove("place against");
                bets.Remove("place against");
            }
            return placeAgainst;
        }

        public string FieldResult(){
            string fieldRes = "";
            foreach(KeyValuePair<int,double> pair in fieldPayout){
                if(throwTotal == pair.Key){
                    int fieldChips = (int)Math.Round(bets["field"]*pair.Value);
                    fieldRes = "Your field bet on " + fieldPlaceNumbers["field"] + " of " + bets["field"] + "won " + fieldChips;
                    player.chips += (bets["field"] + fieldChips);
                }
            }
            bets.Remove("field");
            fieldPlaceNumbers.Remove("field");
            return fieldRes;
        }

        public string ComePointOddsResult(){
            string result = "";
            int chips2 = 0;
            if (throwTotal == 7){
                result += "Your come point odds bet lost.";
                foreach(KeyValuePair<int,int> pair in comePointBets){
                    chips2 += pair.Value;
                }
                result += "You lost " + chips2 + " chips";
                comePointBets.Clear();
            } else {
                foreach(KeyValuePair<int,double> pair in passComePntOddsPayout){
                    if(comePointBets.ContainsKey(throwTotal)){
                        if(comePointBets[throwTotal] !=0){
                            chips2 = (int)Math.Round(pair.Value*comePointBets[throwTotal]);
                            result += "Your come point odds bet on " + comePointBets[throwTotal] + " won " + chips2 + " chips!";
                            player.chips += chips2 + bets["come point odds"];
                            comePointBets.Remove(throwTotal);
                        }                        
                    } 
                }
            }             
            return result;
        }

        public string DontComePointOddsResult(){
            string result = "";
            int chips = 0;
            foreach(KeyValuePair<int,double> payoutPair in dontPassDontComePntOddsPayout){
                if(throwTotal == 7){
                    foreach(KeyValuePair<int,int> pointsPair in dontComePointBets){
                        if(pointsPair.Value !=0){
                            chips = (int)Math.Round(payoutPair.Value*pointsPair.Value);
                            result = "Your don't come point odds bet on " + pointsPair.Key + " won " + chips + " chips!";
                            chips += pointsPair.Value;
                        }
                        player.chips += chips;
                    }
                    dontComePointBets.Clear();
                } else if (dontComePointBets.ContainsKey(throwTotal)){
                    result = "Your don't come point odds bets lost.";
                    foreach(KeyValuePair<int,int> pair in dontComePointBets){
                        chips += pair.Value;
                    }
                    result += "You lost " + chips + " chips";
                    dontComePointBets.Clear();
                }
            }
            return result;
        }

        public void RollDice(){
            Die die = new Die();
            for(int i = 0; i<2; i++){
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
