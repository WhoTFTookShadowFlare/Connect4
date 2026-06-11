using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    internal abstract class APlayer
    {
        public string Name { get; private set; }

        protected APlayer(string name)
        {
            Name = name;
        }

        public abstract byte? TakeTurn(ConsoleKey input);
    }
}
