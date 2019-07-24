using System;
using BlackJack.Cards;
using BlackJack.Games;
using BlackJack;

namespace BlackJack{

    class Program{

        static void Main(string[] args){
             Person me = new Person("Carolynn");
             me.chips = 100;
             //MainMenu casino = new MainMenu();
             //casino.Welcome();
             //Slots slots = new Slots(me);
             //slots.Start();
             Craps craps = new Craps(me);
             craps.Start();
             
        }
    }
}
