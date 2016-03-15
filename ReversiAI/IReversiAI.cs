using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    interface IReversiAI {
        byte getNextMove(GameState state);
    }
}
