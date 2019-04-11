using System;
using System.Collections.Generic;

namespace BlackJack {
    
    public class Person{

        string name {get;set;}
        int chips {get;set;}
        ScoreBoard gameScores {get;set;}
        

        public Person(string name){
            this.name = name;
            this.chips = 0;
            this.gameScores = new ScoreBoard();
        }



    }
}