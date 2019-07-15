using System;
using BlackJack.Cards;
using BlackJack.Games;
using BlackJack;

namespace BlackJack{

    class Program{

        static void Main(string[] args){
             Person me = new Person("Carolynn");
             MainMenu casino = new MainMenu();
             casino.Welcome();
        }
    }
}
