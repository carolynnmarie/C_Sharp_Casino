using System;

namespace BlackJack.Dice {

    public class Die {
        
        public DieFace dieFace {get;set;}

        public Die(){
            this.dieFace = DieFace.ONE;
        }

        public DieFace RollDie(){
            Random random = new Random();
            int face = random.Next(1,7);
            dieFace = face == 1 ? DieFace.ONE: face == 2 ? DieFace.TWO: face == 3 ? DieFace.THREE:face == 4 ? DieFace.FOUR: 
                    face == 5 ? DieFace.FIVE: DieFace.SIX;
            return dieFace;        
        }
        
    }
}