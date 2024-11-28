using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuiz
{
    internal class Player
    {
        public string name;
        public int health;
        public int damage;
        public string imagePath;

        public Player(string name, int health, int damage, string imagePath)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.imagePath = imagePath;
        }
    }
}
