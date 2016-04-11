using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    class GameStats {
        public double turnsRepresented;
        public double Branching;
        public double TimePerTurn;
        
        public void mergeWeightedStats(GameStats other) {
            Branching = (Branching * turnsRepresented + other.Branching * other.turnsRepresented) / (turnsRepresented + other.turnsRepresented);
            TimePerTurn = (TimePerTurn * turnsRepresented + other.TimePerTurn * other.turnsRepresented) / (turnsRepresented + other.turnsRepresented);
        }
    }
}
