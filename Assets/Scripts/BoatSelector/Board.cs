using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BoatSelector
{
    public class Board
    {

        public List<BoatPosition> boats { get; set; }

        public Board()
        {
            boats = new List<BoatPosition>();
        }

    }
}
