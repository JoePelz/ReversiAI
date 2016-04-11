using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class TimeProfiler {
        private GameState testRandom1;
        private GameState testRandom2;
        private GameState testRandom3;
        private GameState testRandom4;
        private GameState testBlackbox;
        private GameState testWhitebox;

        public TimeProfiler() {
            testRandom1 = new GameState();
            testRandom2 = new GameState();
            testRandom3 = new GameState();
            testRandom4 = new GameState();
            testBlackbox = new GameState();
            testWhitebox = new GameState();
            Random rng = new Random(-4);
            for (int x = 2; x < 6; x++) {
                for (int y = 2; y < 6; y++) {
                    testRandom1.squares[x | y << 3] = (byte)rng.Next(1, 3);
                }
            }
            rng = new Random(-3);
            for (int x = 2; x < 6; x++) {
                for (int y = 2; y < 6; y++) {
                    testRandom2.squares[x | y << 3] = (byte)rng.Next(1, 3);
                }
            }
            rng = new Random(-2);
            for (int x = 2; x < 6; x++) {
                for (int y = 2; y < 6; y++) {
                    testRandom3.squares[x | y << 3] = (byte)rng.Next(1, 3);
                }
            }
            rng = new Random(-1);
            for (int x = 2; x < 6; x++) {
                for (int y = 2; y < 6; y++) {
                    testRandom4.squares[x | y << 3] = (byte)rng.Next(1, 3);
                }
            }
            for (int x = 2; x < 6; x++) {
                for (int y = 2; y < 6; y++) {
                    testWhitebox.squares[x | y << 3] = 1;
                }
            }
            for (int x = 2; x < 6; x++) {
                for (int y = 2; y < 6; y++) {
                    testBlackbox.squares[x | y << 3] = 2;
                }
            }
        }

        public void runTests() {
            long time;
            time = Profile("Testing getValidMoves", 100000, timeGetValidMoves);
            decimal ticks_getValidMoves = time / (decimal)1200000.0;
            Console.Write("ticks_getValidMoves: " + ticks_getValidMoves + "\n");

            time = Profile("Testing anyValidMoves", 100000, timeAnyValidMoves);
            decimal ticks_anyValidMoves = time / (decimal)1200000.0;
            Console.Write("ticks_anyValidMoves: " + ticks_anyValidMoves + "\n");

            time = Profile("Testing getTransformedBoard", 100000, timeGetTransformedBoard);
            decimal ticks_getTransformedBoard = time / (decimal)1200000.0;
            Console.Write("ticks_getTransformedBoard: " + ticks_getTransformedBoard + "\n");
        }

        private void timeGetTransformedBoard() {
            GameState result = testRandom1;
            for (int i = 0; i < 64; i++) {
                result = GameState.getTransformedBoard(result, i & 7, i >> 3);
            }
        }

        private void timeGetValidMoves() {
            GameState.getValidMoves(testRandom1, 1);
            GameState.getValidMoves(testRandom1, 2);
            GameState.getValidMoves(testRandom2, 1);
            GameState.getValidMoves(testRandom2, 2);
            GameState.getValidMoves(testRandom3, 1);
            GameState.getValidMoves(testRandom3, 2);
            GameState.getValidMoves(testRandom4, 1);
            GameState.getValidMoves(testRandom4, 2);
            GameState.getValidMoves(testBlackbox, 1);
            GameState.getValidMoves(testBlackbox, 2);
            GameState.getValidMoves(testWhitebox, 1);
            GameState.getValidMoves(testWhitebox, 2);
        }

        private void timeAnyValidMoves() {
            GameState.anyValidMoves(testRandom1, 1);
            GameState.anyValidMoves(testRandom1, 2);
            GameState.anyValidMoves(testRandom2, 1);
            GameState.anyValidMoves(testRandom2, 2);
            GameState.anyValidMoves(testRandom3, 1);
            GameState.anyValidMoves(testRandom3, 2);
            GameState.anyValidMoves(testRandom4, 1);
            GameState.anyValidMoves(testRandom4, 2);
            GameState.anyValidMoves(testBlackbox, 1);
            GameState.anyValidMoves(testBlackbox, 2);
            GameState.anyValidMoves(testWhitebox, 1);
            GameState.anyValidMoves(testWhitebox, 2);
        }

        public static long Profile(string description, int iterations, Action func) {
            //Run at highest priority to minimize fluctuations caused by other processes/threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            // warm up 
            func();

            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            watch.Start();
            for (int i = 0; i < iterations; i++) {
                func();
            }
            watch.Stop();
            Console.Write(description);
            Console.WriteLine(" Time Elapsed {0} ms", watch.Elapsed.TotalMilliseconds);
            return watch.ElapsedTicks;
        }
    }
}
