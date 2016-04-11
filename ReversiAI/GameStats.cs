using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    public class GameStats {
        public int turnsRepresented;
        public int branches;

        public int maxLeaves;
        public long leaves;

        public int maxDepth = 0;
        public int depth;

        public double maxTime = 0;
        public double timeElapsed;

        public void mergeStats(GameStats other) {
            turnsRepresented += other.turnsRepresented;
            branches += other.branches;

            if (other.maxLeaves > maxLeaves) maxLeaves = other.maxLeaves;
            leaves += other.leaves;

            if (other.maxDepth > maxDepth) maxDepth = other.maxDepth;
            depth += other.depth;

            if (other.maxTime > maxTime) maxTime = other.maxTime;
            timeElapsed += other.timeElapsed;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Turns Represented: " + turnsRepresented);
            sb.AppendLine("====================");
            
            sb.AppendLine(String.Format("Average branching: {0:0.000}", ((double)branches / turnsRepresented)));
            if (leaves != 0) {
                sb.AppendLine("Max leaves visited: " + maxLeaves);
                sb.AppendLine("Avg leaves visited: " + (int)((double)leaves / turnsRepresented));
            }
            if (maxDepth != 0) {
                sb.AppendLine("Max depth searched: " + maxDepth);
                sb.AppendLine("Avg depth searched: " + (int)((double)depth / turnsRepresented));
            }
            if (maxTime != 0) {
                sb.AppendLine("Max turn time: " + maxTime);
                sb.AppendLine(String.Format("Avg turn time: {0:0.000}s", ((double)timeElapsed / turnsRepresented)));
            }
            return sb.ToString();
        }

        public void augmentLeaves(int newLeaves) {
            leaves += newLeaves;
            if (newLeaves > maxLeaves) maxLeaves = newLeaves;
        }

        public void augmentDepth(int newDepth) {
            if (newDepth > maxDepth) maxDepth = newDepth;
            depth += newDepth;
        }

        public void augmentTime(double newTime) {
            if (newTime > maxTime) maxTime = newTime;
            timeElapsed += newTime;
        }
    }
}
