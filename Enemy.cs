using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuiz
{
    internal class Enemy
    {
        public string name;
        public int health;
        public int damage;
        public string imagePath;
        public Position position;

        public Enemy(string name, int health, int damage, string imagePath, Position position)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.imagePath = imagePath;
            this.position = position;
        }

        public void TakeDamage(int damage)
        {

            health -= damage;

        }

    }
}
