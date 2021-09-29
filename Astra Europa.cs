using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static string inv, playerInput;
        public static int playerLocation;
        public static bool doorE3 = false, doorE13 = false, doorE16 = false, doorS17 = false, doorN14 = false, doorS20 = false, doorE74 = false;

		public static void room1()
		{
			while (playerInput != "south" || playerInput != "east")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 1");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room2();
						break;
					case "east":
						room6();
						break;
					default:
						Console.WriteLine("Invaild Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		public static void room2()
		{

			while (playerInput != "south" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 2");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room3();
						break;
					case "north":
						room1();
						break;
					default:
						Console.WriteLine("Invaild input");
						Thread.Sleep(500);
						break;
				}
			}
		}


		public static void room3()
		{
			while (playerInput != "south" || playerInput != "east" || playerInput != "north" || playerInput != "west")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 3");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "west":
						//method for outside
						break;
					case "north":
						room2();
						break;
					case "east":
						if (doorE3 == true)
						{
							room8();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					case "south":
						room4();
						break;
					default:
						Console.WriteLine("Invaild Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		public static void room4()
		{
			while (playerInput != "south" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 4");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room5();
						break;
					case "north":
						room3();
						break;
					default:
						Console.WriteLine("Invaild input");
						Thread.Sleep(500);
						break;
				}
			}

		}

		public static void room5()
		{
			while (playerInput != "east" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("You see some damage has been inflicted to the walls of this section, looks like deep slashes.");
				Console.WriteLine("Faint pools of blood stain the floor but no bodies.");	                 

				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						room5();
						break;
					case "north":
						room3();
						break;
					default:
						Console.WriteLine("Invaild input");
						Thread.Sleep(500);
						break;
				}
			}

		}

		public static void room6()
		{
			while (playerInput != "west" || playerInput != "east")
			{
				Console.Clear();
				Console.WriteLine("This section of the station has a window witht the view of the barren surface of this harsh planet");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "west":
						room1();
						break;
					case "east":
						room11();
						break;
					default:
						Console.WriteLine("Invaild input");
						Thread.Sleep(500);
						break;
				}
			}

		}


		// Room 13 (To be added by Johnathan)
		// Room 14 (To be added by Johnathan)
		// Room 15 (To be added by Johnathan)
		// Room 16 (To be added by Johnathan)
		// Room 17 (To be added by Johnathan)
		// Room 18 (To be added by Johnathan)
		// Room 19 (To be added by Johnathan)
		// Room 20 (To be added by Johnathan)
		// Room 21 (To be added by Johnathan)
		// Room 22 (To be added by Johnathan)
		// Room 23 (To be added by Johnathan)
		// Room 24 (To be added by Johnathan)
		// Room 25 (To be added by Johnathan)

		public static void TitleScreen()
		{
			/* Devon 28/09/2021
             * 
             * Credit to patorjk.com for ASCII Title Creation
             * Ignore any yellow backslashes, used as escape characters
             * for optimal output
             * 
            */
			Console.Clear();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("          █████╗ ███████╗████████╗██████╗  ██████╗     ███████╗██╗   ██╗██████╗  ██████╗ ██████╗  █████╗  ");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("         ██╔══██╗██╔════╝╚══██╔══╝██╔══██╗██╔═══██╗    ██╔════╝██║   ██║██╔══██╗██╔═══██╗██╔══██╗██╔══██╗ ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("         ███████║███████╗   ██║   ██████╔╝██║   ██║    █████╗  ██║   ██║██████╔╝██║   ██║██████╔╝███████║ ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("         ██╔══██║╚════██║   ██║   ██╔══██╗██║   ██║    ██╔══╝  ██║   ██║██╔══██╗██║   ██║██╔═══╝ ██╔══██║ ");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("         ██║  ██║███████║   ██║   ██║  ██║╚██████╔╝    ███████╗╚██████╔╝██║  ██║╚██████╔╝██║     ██║  ██║ ");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("         ╚═╝  ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝     ╚══════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝  ╚═╝ ");
			Console.WriteLine("\n");
			Console.WriteLine("      .         _  .          .          . ╔═══════════════╗   +     .          .          .      .        ");
			Console.WriteLine("              .(_)          .            . ║               ║          .            .       :               ");
			Console.WriteLine("              .   .      .    .     .     .║   PLAY        ║   .      .   .      . .  .  -+-        .      ");
			Console.WriteLine("                .           .   .        . ║   or          ║         .          /         :  .             ");
			Console.WriteLine("          . .        .  .      /.   .      ║   CONTINUE    ║.      .   .    .  /        . ' .              ");
			Console.WriteLine("              .  +       .    /     .      ║               ║   .          .   /      .                     ");
			Console.WriteLine("             .            .  /         .   ║               ║        .        *   .         .     .         ");
			Console.WriteLine("            .   .      .    *     .     .  ║   CONTROLS    ║ .      .   .       .  .                       ");
			Console.WriteLine("                .           .           .  ║               ║        .           .         +  .             ");
			Console.WriteLine("        . .        .  .       .   .      . ║               ║  .     .     .    .      .   .                ");
			Console.WriteLine("       .   +      .          ___/\\_._/~~\\_.║   CREDITS     ║.__/\\__.._._/~\\        .         .   .         ");
			Console.WriteLine("             .          _.--'              ║               ║                `--./\\          .   .          ");
			Console.WriteLine("                 / ~~\\/~\\                  ║               ║                         `-/~\\_            .     ");
			Console.WriteLine("       .      .-'                          ║   EXIT        ║                              `-/\\_               ");
			Console.WriteLine("       _ /\\.-'                             ║               ║                                 __/~\\/\\-.__        ");
			Console.WriteLine("   .'                                      ╚═══════════════╝                                             `.       ");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\n\t\t\t\tWould you like to begin?");
			Console.Write("\t\t\t\tType in what you would like to do:   ");
			Console.ForegroundColor = ConsoleColor.Green;
			string temp = Console.ReadLine().ToUpper();

			//Using temporary method names for loading etc
			switch (temp)
			{
				case "PLAY":
					Console.Clear();
					room1();
					break;

				case "CONTINUE":
					Console.Clear();
					//LoadGame();
					Console.WriteLine("This is will be LoadGame()");
					break;

				case "CONTROLS":
					Console.Clear();
					//Controls();
					Console.WriteLine("This is will be Controls()");
					break;

				case "CREDITS":
					Console.Clear();
					//Credits();
					Console.WriteLine("This is will be Credits()");
					break;

				case "EXIT":
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("\t\t\tAre you sure you would like to ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("EXIT? ");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("( Y / N ): ");
					temp = Console.ReadLine().ToUpper();
					switch (temp)
					{
						case "YES":
						case "Y":
							Environment.Exit(0);
							break;

						case "NO":
						case "N":
							TitleScreen();
							break;

						default:
							Console.Clear();
							Console.WriteLine("Sorry that was not an available option. Try Again...");
							TitleScreen();
							break;
					}
					break;

				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("\t\t\t\t\t\t\t\tINVALID CHOICE       Press ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("ENTER ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("to continue");
					Console.ReadLine();
					Console.Clear();
					TitleScreen();
					break;
			}
		}

		static void Main(string[] args)
        {
			TitleScreen();
			Console.WriteLine("Hello World!");
        }
    }
}
