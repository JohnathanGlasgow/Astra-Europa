using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public static class Auditorium
    {
        private const int SLEEP = 100;
        private const int REPEAT = 10;
        private static bool brk;
        private static bool success;

        // this method clears any keys waiting to be input
        public static void ClearInput()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            //ClearLine();
        }

        public static bool Outcome(string command, ConsoleKey k)
        {
            brk = false;
            success = false;
            Console.Clear();

            for (int i = 0; i < REPEAT && !brk; i++)
            {
                Console.Clear();
                Console.WriteLine(command);
                Thread.Sleep(SLEEP);
                if ((Console.KeyAvailable && Console.ReadKey(true).Key == k))
                {
                    success = true;
                    brk = true;
                }
                //Console.WriteLine($"i: {i} brk: {brk}");
            }
            //Console.WriteLine("Success: " + success);
            return success;
        }

        private static void qTSetup(string setup, string failure, string prompt, ConsoleKey ck)
        {
            Console.Clear();
            Console.WriteLine(setup);
            Console.WriteLine("[PRESS ENTER]");
            Console.ReadLine();
            if (!(Outcome(prompt, ck)))
            {
                ClearInput();
                Console.WriteLine(failure);
                Console.WriteLine("Press enter to try again...");
                Console.ReadLine();
                Run();
            }
        }

        public static void Run()
        {
            qTSetup("You enter a dimly lit theater. The stench of stale popcorn abounds.\n" + 
                "You hear movement all around you. Suddenly there is warm air on your neck.",
    "Too late! Something jumps on your back and bites off your head!", "Press T to turn around!", ConsoleKey.T);
            qTSetup("You turn and see a giant spider hanging before you.",
    "You're too slow! The spider pounces on you and sucks your blood.", "Press R to run away!", ConsoleKey.R);
            qTSetup("You run down the center aisle. You can hear the spider clambering behind you.\n" +
                "In the darkness you make out debris blocking your path.",
    "You're too slow! The spider catches up to you and drags you back to its web.", "Press L to leap over the debris!", ConsoleKey.L);
            qTSetup("You athletically clear the debris. The dirty and torn cinema screen rises before you.\n" +
    "To your horror, a throng of spiders begin to descend the screen. More debris blocks the way to the left.",
"The spiders descend upon you. You're torn apart as they fight over their prey.", "Press -> to peel off to the right!", ConsoleKey.RightArrow);
            //e2();
        }


        //private static void e1()
        //{
        //    Console.WriteLine("You enter a dimly lit room. You hear movement all around you. You feel warm air on your neck.");
        //    Console.WriteLine("[PRESS ENTER]");
        //    Console.ReadLine();
        //    if (!(new QTE("Press T to turn around!", ConsoleKey.T).Success))
        //    {
        //        Console.WriteLine("Too late! Something jumps on your back and bites off your head!");
        //        Console.WriteLine("Press enter to try again...");
        //        Console.ReadLine();
        //        Run();
        //    }
        //}
        //private static void e2()
        //{
        //    Console.WriteLine("You turn and see a giant spider is hanging behind you.");
        //    Console.ReadLine();
        //    if (!(new QTE("Press R to run away!", ConsoleKey.R).Success))
        //    {
        //        Console.WriteLine("You're too slow! The spider pounces on you and sucks your blood.");
        //        Console.WriteLine("Press enter to try again...");
        //        Console.ReadLine();
        //        Run();
        //    }
        //    else
        //    {

        //    }
        //}
    }
}
