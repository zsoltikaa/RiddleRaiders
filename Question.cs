using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiddleRaiders
{
    internal class Question
    {
        public readonly string question;
        public readonly string[] answers;
        public readonly string right_answer;

        public Question(string line)
        {
            string[] splitLine = line.Split(';');

            question = splitLine[0];
            answers = new string[] { splitLine[1], splitLine[2], splitLine[3], splitLine[4] };
            right_answer = splitLine[5];
        }
    }
}
