using System;
using System.Collections.Generic;

namespace BlackJack.Games{

    public struct CrapsPayouts{

        public Dictionary<int, double> fieldPayout {get;set;}
        public Dictionary<int, double> placeWin {get;set;}
        public Dictionary<int, double> placeLose {get;set;}
        public Dictionary<int, double> passLineComePointOdds {get;set;}
        public Dictionary<int, double> dontPassLineDontComePointOdds {get;set;}


        public CrapsPayouts(){
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
    }
}