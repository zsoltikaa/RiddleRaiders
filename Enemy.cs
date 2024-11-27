using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuiz
{
    internal class Enemy
    {
        string name;
        int health;
        int damage;
        string imagePath;

        public Enemy(string name, int health, int damage, string imagePath)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.imagePath = imagePath;
        }
    }
}
