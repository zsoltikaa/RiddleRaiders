using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiddleRaiders
{
    internal class PowerUp
    {
        public int owned;
        public bool disabled;

        public PowerUp(int owned)
        {
            this.owned = owned;
        }
    }
}
