using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIConfiguration {
        public int player;
        public Controller.Player AI;
        public int maxDepth = 2;
        public int maxTime;
        public bool ABPruning;
    }
}
