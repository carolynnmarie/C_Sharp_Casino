using System;

namespace BlackJack.Games{

    public class Craps{

        public Person player {get;set;}
        public Die[] dice {get;set;}
        public int throwTotal {get;set;}
        public int bet {get;set;}
        public string[] betTypes {get;set;}
        public Dictionary<string, int> secondRoleBets {get;set;}
        public Dictionary<string, Dictionary<int, double>> oddPayouts {get;set;}
        public Dictionary<int, double> fieldPayout {get;set;}
        

        public Craps(Person player){
            this.player = player;
            this.dice = new Die[2];
            this.bet = 5;
            this.crapsOdds = new CrapsOdds();
            this.throwTotal = 0;
            this.betTypes = {"Pass Line", "Don't Pass Line", "Come", "Don't Come", "Pass Line Odds", "Don't Pass Line Odds", 
                            "Come Odds", "Don't Come Odds", "Field","Place"};
            this.secondRoleBets = new Dictionary<string, int>();
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
            oddsPayouts.Add("place win", placeWin);
            oddsPayouts.Add("place lose", placeLose);
            oddsPayouts.Add("pass line odds", passLineComeOdds);
            oddsPayouts.Add("don't pass line odds", dontPassLineDontComeOdds);
            oddsPayouts.Add("come odds", passLineComeOdds);
            oddsPayouts.Add("don't come odds", dontPassLineDoneComeOdds);                                    
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