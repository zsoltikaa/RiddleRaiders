using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuiz
{
    internal class Scene
    {
        public readonly string backgroundImage;
        public readonly string sceneName;
        public readonly Position playerPosition;
        public readonly Enemy enemy;
        public string chat;

        public Scene(string backgroundImage, string sceneName, Position playerPosition, Enemy enemy, string chat)
        {
            this.backgroundImage = backgroundImage;
            this.sceneName = sceneName;
            this.playerPosition = playerPosition;
            this.enemy = enemy;
            this.chat = chat;
        }
    }
}
