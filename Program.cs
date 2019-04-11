using System;
using BlackJack.Cards;

namespace BlackJack{

    class Program{

        static void Main(string[] args){
            Deck deck = new Deck();
            Console.WriteLine(deck.printDeck());
            deck.shuffle();
            Console.WriteLine(deck.printDeck());
        }
    }
}
