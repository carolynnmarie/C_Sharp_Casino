using System;
using BlackJack.Cards;

namespace BlackJack{

    class Program{

        static void Main(string[] args){
            Deck deck = new Deck();
            //Console.WriteLine(deck.PrintDeck());
            deck.Shuffle();
            //Console.WriteLine(deck.PrintDeck());
            Console.WriteLine("\uD83C\uDCA1");
        }
    }
}
