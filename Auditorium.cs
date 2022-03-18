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
        }

        // this method flashes a message to press a certain key onscreen
        // if the key is pressed in time, TRUE is returned, and FALSE if not
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

        // this method displays a message and waits for the user to press enter
        // at that point a QTE happens
        private static void qTSetup(string setup, string failure, string prompt, ConsoleKey ck)
        {
            ClearInput();
            Console.Clear();
            Console.WriteLine(setup);
            Console.WriteLine("[PRESS ENTER]");
            Console.ReadLine();
            if (!(Outcome(prompt, ck)))
            {
                Console.Clear();
                Console.WriteLine(failure);
                Console.WriteLine("Press enter to try again...");
                Console.ReadLine();
            }
        }

        // this method runs through a series of QTEs
        // if any are failed, the goto blocks are triggered and the player must start from REPEAT
        public static void Run()
        {
            REPEAT:
            qTSetup("You enter the dimly lit auditorium. A musty stench abounds.\n" +
"You hear movement all around you. Suddenly there is warm air on your neck.",
"Too late! Something jumps on your back and bites off your head!", "Press T to turn around!", ConsoleKey.T);
            if(!success)
            { goto REPEAT; }
            qTSetup("You turn and see a giant spider hanging before you.",
"You're too slow! The spider pounces on you and sucks your blood.", "Press R to run away!", ConsoleKey.R);
            if (!success)
            { goto REPEAT; }
            qTSetup("You run down the center aisle. You can hear the spider clambering behind you.\n" +
"In the darkness you make out debris blocking your path.",
"You're too slow! The spider catches up to you and drags you back to its web.", "Press L to leap over the debris!", ConsoleKey.L);
            if (!success)
            { goto REPEAT; }
            qTSetup("You athletically clear the debris. The dirty and torn cinema screen rises before you.\n" +
"To your horror, a throng of spiders begin to descend the screen. More debris blocks the way to the left.",
"The spiders descend upon you. You're torn apart as they fight over their prey.", "Press → to peel off to the right!", ConsoleKey.RightArrow);
            if (!success)
            { goto REPEAT; }
            qTSetup("You shoot off to the right. The mob of spiders is close behind while you hear the sound of metal twisting ahead.\n" +
"A rafter crashes down before you, blocking your path.",
"The spiders descend upon you. You cease to move as they inject their paralyzing venom.", "Press S to slide beneath the rafter!", ConsoleKey.S);
            if (!success)
            { goto REPEAT; }
            qTSetup("You slide neatly below the wreckage.\n" +
"You manage to gain some distance on the spiders and see double doors marked 'Employees Only'.\n" +
"Unfortunately, tiles fall around you as it becomes apparent the ceiling is about to collapse.",
"You are crushed under the weight of the falling ceiling.", "Press P to pounce through the doors!", ConsoleKey.P);
            if (!success)
            { goto REPEAT; }
            qTSetup("You crash through the doors to safety. You hear the spiders squeal as they are crushed by the ceiling.\n" +
"However, your sense of relief quickly fades as you realise you are surrounded by thick webbing. \n" +
"You hear a thousand tiny legs rattling through the ducts above.\n" +
"It grows louder and louder until... A flood of baby spiders comes pouring out a vent above!\n" +
"The only way forward is a narrow corridor leading further into the nest.",
"You are swarmed by the tiny spiders. Soon you will be nothing but spider baby food.", "Press B to bolt down the corridor", ConsoleKey.B);
            if (!success)
            { goto REPEAT; }
            qTSetup("You bound off down the corridor. The baby spiders are nipping at your heels.\n" +
"You come across a door reading 'PROJECTOR ROOM'. You attempt to prize it open but it is stuck shut with webbing.",
"You are swarmed by baby spiders. Even though you will soon be eaten alive, you can't help thinking they are pretty cute.",
"Press Y to yank open the door!", ConsoleKey.Y);
            if (!success)
            { goto REPEAT; }
            qTSetup("With a herculean pull, the door flings open. You flee inside, slamming it behind you.\n" +
"In this moment of respite, you find yourself att the base of a narrow stairwell.\n" +
"It seems you have company. A skeleton is slumped in the corner.\n" +
"From its distinctive red cap you discern it is the usher.\n" +
"Or was the usher. You spot a flashlight in its bony grip.\n" +
"You pick it up, flick it on, and are shocked at the bright beam of light that emerges.\n" +
"Emboldened by the find, you head up the creaky stairs.\n" +
"You nearly reach the top when a stair breaks beneath your weight.\n" +
"You hang perilously above what you can only assume is a long drop.",
"You slip and fall through the darkness...", "Press ↑ to hoist yourself back up!", ConsoleKey.UpArrow);
            if (!success)
            { goto REPEAT; }
            qTSetup("You pull yourself to safety. You now stand before the door to the projector room.\n" +
"You slowly open the door and step inside. The rusted projector lies in disarray.\n" +
"Film canisters are strewn amongst the copious webbing. You hear a grumble above you...\n" +
"You look up, and see dozens of skeletons in thick cocoons adhered to the ceiling.\n" +
"An enormous black shape traverses the ceiling towards you. As it descends, you realize you have found the Spider Queen.",
"One bite is all it takes. You succumb to the poison and are quickly taken to join the colonists on the ceiling.",
"Press F to shine the flashlight at the Spider Queen!", ConsoleKey.F);
            if (!success)
            { goto REPEAT; }
            Console.Clear();
            Console.WriteLine("The Spider Queen recoils and unleashes a horrific screech.");
            Console.WriteLine("You step back until you feel a metallic panel behind you. ");
            Console.WriteLine("Just as you think your journey has reached it's end, the panel gives way.");
            Console.WriteLine("You fall through an old service shaft, and find yourself back at the entrance to the theater, bruised but alive.");
            Console.WriteLine("[PRESS ENTER]");
            Console.ReadLine();
        }


    }
}
