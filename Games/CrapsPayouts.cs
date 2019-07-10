using System;

namespace BlackJack.Games{

    public class CrapsPayouts {

        public Dictionary<int, double> placeWin {get;set;}
        public Dictionary<int, double> placeLose {get;set;}
        public Dictionary<int, double> passLineComeBetOdds {get;set;}
        public Dictionary<int, double> dontPassLineDoneComeBetOdds {get;set;}
        public Dictionary<int, double> field {get;set;}
        
        public CrapsPayouts(){
            this.placeWin = new Dictionary<int, double>();
            this.placeLose = new Dictionary<int, double>();
            this.passLineComeOdds = new Dictionary<int, double>();
            this.dontPassLineDontComeOdds = new Dictionary<int, double>();
            this.field = new Dictionary<int, double>();
        }
        
    }
}