using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuiz
{
    internal class Scene
    {
        string backgroundImage;
        string sceneName;
        Position playerPosition;
        Position enemyPosition;

        public Scene(string backgroundImage, string sceneName, Position playerPosition, Position enemyPosition)
        {
            this.backgroundImage = backgroundImage;
            this.sceneName = sceneName;
            this.playerPosition = playerPosition;
            this.enemyPosition = enemyPosition;
        }
    }
}
