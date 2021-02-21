using System;

namespace BlackJack.Cards{

    public struct Card{

        public Rank rank {get;set;}
        public Suit suit {get;set;}

        public Card(Rank rank, Suit suit){
            this.rank = rank;
            this.suit = suit;
        }

        public override string ToString(){
            int r = (int)rank;
            string rStr = r.ToString();
            rStr = (rStr == "1")? "A": (rStr == "11") ? "J":
            (rStr == "12")? "Q": (rStr == "13")? "K": rStr;
            
            string s = suit.ToString();
            s = (s == "Hearts")? "\u2665": (s == "Diamonds")? "\u2666": 
                (s == "Clubs")? "\u2663": (s == "Spades")?"\u2660":s;
            
            return rStr + s;
        }

        public string Display(){
            string uniCodeCard = ((int)suit == 3 && (int)rank == 1)? "\uD83C\uDCA1":
            ((int)suit == 4 && (int)rank == 2)? "\uD83C\uDCA2": ((int)suit == 4 && (int)rank == 3)? "\uD83C\uDCA3":
            ((int)suit == 4 && (int)rank == 4)? "\uD83C\uDCA3": ((int)suit == 4 && (int)rank == 5)? "\uD83C\uDCA5":
            ((int)suit == 4 && (int)rank == 6)? "\uD83C\uDCA6": ((int)suit == 4 && (int)rank == 7)? "\uD83C\uDCA7":
            ((int)suit == 4 && (int)rank == 8)? "\uD83C\uDCA8": ((int)suit == 4 && (int)rank == 9)? "\uD83C\uDCA9":
            ((int)suit == 4 && (int)rank == 10)? "\uD83C\uDCAA": ((int)suit == 4 && (int)rank == 11)? "\uD83C\uDCAB":
            ((int)suit == 4 && (int)rank == 12)? "\uD83C\uDCAD": ((int)suit == 4 && (int)rank == 13)? "\uD83C\uDCAE":
            ((int)suit == 3 && (int)rank == 1)? "\uD83C\uDCB1": ((int)suit == 3 && (int)rank == 2)? "\uD83C\uDCB2":
            ((int)suit == 3 && (int)rank == 3)? "\uD83C\uDCB3": ((int)suit == 3 && (int)rank == 4)? "\uD83C\uDCB4":
            ((int)suit == 3 && (int)rank == 5)? "\uD83C\uDCB5": ((int)suit == 3 && (int)rank == 6)? "\uD83C\uDCB6":
            ((int)suit == 3 && (int)rank == 7)? "\uD83C\uDCB7": ((int)suit == 3 && (int)rank == 8)? "\uD83C\uDCB8":
            ((int)suit == 3 && (int)rank == 9)? "\uD83C\uDCB9": ((int)suit == 3 && (int)rank == 10)? "\uD83C\uDCBA":
            ((int)suit == 3 && (int)rank == 11)? "\uD83C\uDCBB": ((int)suit == 3 && (int)rank == 12)? "\uD83C\uDCBD":
            ((int)suit == 3 && (int)rank == 13)? "\uD83C\uDCBE": ((int)suit == 2 && (int)rank == 1)? "\uD83C\uDCC1":
            ((int)suit == 2 && (int)rank == 2)? "\uD83C\uDCC2": ((int)suit == 2 && (int)rank == 3)? "\uD83C\uDCC3":
            ((int)suit == 2 && (int)rank == 4)? "\uD83C\uDCC4": ((int)suit == 2 && (int)rank == 5)? "\uD83C\uDCC5":
            ((int)suit == 2 && (int)rank == 6)? "\uD83C\uDCC6": ((int)suit == 2 && (int)rank == 7)? "\uD83C\uDCC7":
            ((int)suit == 2 && (int)rank == 8)? "\uD83C\uDCC8": ((int)suit == 2 && (int)rank == 9)? "\uD83C\uDCC9":
            ((int)suit == 2 && (int)rank == 10)? "\uD83C\uDCCA": ((int)suit == 2 && (int)rank == 11)? "\uD83C\uDCCB":
            ((int)suit == 2 && (int)rank == 12)? "\uD83C\uDCCD": ((int)suit == 2 && (int)rank == 13)? "\uD83C\uDCCE":
            ((int)suit == 1 && (int)rank == 1)? "\uD83C\uDCD1": ((int)suit == 1 && (int)rank == 2)? "\uD83C\uDCD2":
            ((int)suit == 1 && (int)rank == 3)? "\uD83C\uDCD3": ((int)suit == 1 && (int)rank == 4)? "\uD83C\uDCD4":
            ((int)suit == 1 && (int)rank == 5)? "\uD83C\uDCD5": ((int)suit == 1 && (int)rank == 6)? "\uD83C\uDCD6":
            ((int)suit == 1 && (int)rank == 7)? "\uD83C\uDCD7": ((int)suit == 1 && (int)rank == 8)? "\uD83C\uDCD8":
            ((int)suit == 1 && (int)rank == 9)? "\uD83C\uDCD9": ((int)suit == 1 && (int)rank == 10)? "\uD83C\uDCDA":
            ((int)suit == 1 && (int)rank == 11)? "\uD83C\uDCDB": ((int)suit == 1 && (int)rank == 12)? "\uD83C\uDCDD":
            ((int)suit == 1 && (int)rank == 13)? "\uD83C\uDCDE": "";
            
            return uniCodeCard;
        }
    }
    
}

/*
 CLUBS("clubs", "\u2663"),
    DIAMONDS("diamonds", "\u2666"),
    HEARTS("hearts", "\u2665"),
    SPADES("spades", "\u2660");
 */