using System;
using BlackJack.Cards;
using BlackJack.Games;

namespace BlackJack{

    class Program{

        static void Main(string[] args){
            // Deck deck = new Deck();
            // Console.WriteLine(deck.PrintDeck());
            // deck.Shuffle();
            // Console.WriteLine(deck.PrintDeck());
            // Console.WriteLine("\uD83C\uDCA1");
             Person me = new Person("Carolynn");
             BlackJackGame blackJack = new BlackJackGame(me);
             me.chips = 10;
             blackJack.Start();
            // GoFishGame goFish = new GoFishGame(me);
            // goFish.Start();
        }
    }
}
