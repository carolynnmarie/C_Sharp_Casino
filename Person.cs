using System;
using System.Collections.Generic;

namespace BlackJack {
    
    public class Person{

        public string name {get;set;}
        public int chips {get;set;}
        public ScoreBoard gameScores {get;set;}
        

        public Person(string name){
            this.name = name;
            this.chips = 0;
            this.gameScores = new ScoreBoard();
        }
        
        public Person(){
            this.name = "";
            this.chips = 0;
            this.gameScores = new ScoreBoard();
        }



    }
}