using System;

namespace BlackJack.Games{

    public class Craps{

        public Person player {get;set;}
        public Die[] dice {get;set;}
        public int bet {get;set;}
        public CrapsOdds crapsOdds {get;set;}
        public int throwTotal {get;set;}
        public string[] betTypes {get;set;}

        public Craps(Person player){
            this.player = player;
            this.dice = new Die[2];
            this.bet = 5;
            this.crapsOdds = new CrapsOdds();
            this.throwTotal = 0;
            this.betTypes = {"Pass Line", "Don't Pass Line", "Come", "Don't Come", "Pass Line Odds", "Don't Pass Line Odds", 
                            "Come Odds", "Don't Come Odds", "Field","Place"};
            
        }
    }
}