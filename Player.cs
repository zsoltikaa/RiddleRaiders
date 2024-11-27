using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuiz
{
    internal class Player
    {
        string name;
        int health;
        int damage;
        string imagePath;

        public Player(string name, int health, int damage, string imagePath)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.imagePath = imagePath;
        }
    }
}
