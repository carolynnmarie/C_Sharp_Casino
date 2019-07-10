using System;

namespace BlackJack.Games{

    public class CardGame {

        public Person player {get;set;}

        public CardGame(Person player){
            this.player = player;
        }

        public abstract void Start();

        public abstract void Engine();

        public abstract void End();
    }
}