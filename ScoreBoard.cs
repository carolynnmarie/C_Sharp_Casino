using System;
using System.Collections.Generic;

namespace BlackJack {

    public class ScoreBoard{

        Dictionary<string, int> scores {get;set;}

        public ScoreBoard(){
            this.scores = new Dictionary<string,int>();
        }
    }
    
}