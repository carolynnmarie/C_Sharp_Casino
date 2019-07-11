using System;

namespace BlackJack.Games{

    public class CrapsPayouts {


        
        public CrapsPayouts(){

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
            this.fieldPayout = new Dictionary<int, double>();
            fieldPayout.Add(3, 1.0);
            fieldPayout.Add(4, 1.0);
            fieldPayout.Add(9, 1.0);
            fieldPayout.Add(10, 1.0);
            fieldPayout.Add(11, 1.0);
            fieldPayout.Add(2, 2.0);
            fieldPayout.Add(12, 2.0);            
        }
        
    }
}

}