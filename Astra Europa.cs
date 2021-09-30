using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static string inv, playerInput;
        public static int playerLocation;
		public static bool doorE3 = false, doorE13 = false, doorE16 = false, doorS17 = false, doorN14 = false, doorS20 = false, doorE74 = false;
		// puzzle doors:
		public static bool doorS21 = false, doorS22 = false, doorS23 = false, doorS24 = false, doorE25 = false;
		//Taking the fire extinguisher
		public static bool fireEx = false;

        /* 
		 * -Player Input Method-
		 * This method is called in the rooms when we ask for the next command, i.e. switch(playerInput())
		 * It checks if the command is to open the inventory, open the help text, open the options, read the notebook,
		 * or any other command that is universal across the game. 
		 * If a universal command is triggered, the method returns an empty string.
		 * The rooms will have an empty case/break for the empty string (so they don't default to "Invalid Command" after a universal command is triggered)
		 */
        //public static string playerInput()
        //{
        //    Console.Write("What next? ");
        //    string playerInput = Console.ReadLine();
        //    switch (playerInput)
        //    {
        //        case "inventory":
        //        case "i":
        //            showInventory();
        //            playerInput = "";
        //            break;
        //        case "notebook":
        //        case "n":
        //            Console.WriteLine("Contents of Notebook");
        //            Console.ReadLine();
        //            playerInput = "";
        //            break;
        //        case "help":
        //        case "info":
        //            Console.WriteLine("How to play:");
        //            Console.ReadLine();
        //            playerInput = "";
        //            break;
        //        case "options":
        //        case "o":
        //            Console.WriteLine("Options Menu");
        //            Console.ReadLine();
        //            playerInput = "";
        //            break;
        //    }
        //    return playerInput;
        //}

        /* -Inventory-
		 * To add items to inventory use inventory.Add("Item Name")
		 * To check if an item is in the inventory use inventory.Contains("Item Name") - this returns a bool
		 * */
        public static List<string> inventory = new List<string> { "Notebook", "Pencil", "Radio" };
		// This method formats a string that contains the inventory items
		public static void showInventory()
        {
			string showInv = "";
            Console.WriteLine("Inventory:");
			// add a comma and a space for each item and add it to the string
			foreach (string s in inventory)
            {
				showInv += s + ", ";
            }
			// remove the last comma and space
            showInv = showInv[0..^2];
            Console.WriteLine(showInv);
			Console.ReadLine();
        }

		// Have continued playerLocation count from Room 26 
		// Ship --Need to add extinguishing fires
		public static void ship()
		{
			playerLocation = 27;
			while (playerInput != "south" || playerInput != "east")
			{
				Console.Clear();
				Console.WriteLine("SHIP DESCRIPTION");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						escapePod();
						break;

					case "east":
						airlock();
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//Escape Pod
		//Will need additional options for replacing parts
		public static void escapePod()
		{
			playerLocation = 28;
			while (playerInput != "north")
			{
				Console.Clear();
				//Checks for first visit to room before putting out fires
				if (fireEx != true)
				{
					Console.WriteLine("YOU NOTICE FIRE EXTINGUISHER");
					playerInput = Console.ReadLine();
					switch (playerInput)
					{
						case "get fire extinguisher":
							fireEx = true;
							Console.WriteLine("YOU PICK UP THE FIRE EXTINGUISHER");
							Thread.Sleep(500);
							break;

						default:
							Console.WriteLine("Invalid Input");
							Thread.Sleep(500);
							break;
					}
				}
				else
				{
					Console.WriteLine("ESCAPE POD DESCRIPTION");
					playerInput = Console.ReadLine();
					switch (playerInput)
					{
						case "north":
							ship();
							break;
						default:
							Console.WriteLine("Invalid Input");
							Thread.Sleep(500);
							break;
					}
				}
			}
		}

		//Airlock
		public static void airlock()
		{
			playerLocation = 29;
			while (playerInput != "west" || playerInput != "east")
			{
				Console.Clear();
				Console.WriteLine("AIRLOCK DESCRIPTION");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "west":
						ship();
						break;

					case "east":
						wasteland();
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//Wasteland - Outside Ship
		public static void wasteland()
		{
			playerLocation = 30;
			while (playerInput != "west" || playerInput != "east" || playerInput != "north" || playerInput != "south")
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (OUTSIDE SHIP) DESCRIPTION");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "west":
						airlock();
						break;
					case "east":
						Console.WriteLine("YOU CONTINUE EAST ... LEVER");
						playerInput = Console.ReadLine();
						switch (playerInput)
						{
							case "use lever":
								room3();
								break;

							default:
								Console.WriteLine("Invalid Input");
								Thread.Sleep(500);
								break;
						}
						break;

					case "north":
						wasteN1();
						break;

					case "south":
						wasteS1();
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//Wasteland - North Once from main path
		public static void wasteN1()
		{
			playerLocation = 31;
			while (playerInput != "south" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (NORTH1) DESCRIPTION");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						Console.WriteLine("YOU HAVE GONE TOO FAR"); //DEATH
						Console.WriteLine("WILL RESET HERE");
						Thread.Sleep(2000);
						//EXIT BACK TO TITLESCREEN WITH RESET VALUES
						break;

					case "south":
						wasteland();
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//Wasteland - South Once from main path
		public static void wasteS1()
		{
			playerLocation = 32;
			while (playerInput != "south" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (SOUTH1) DESCRIPTION");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						wasteland();
						break;

					case "south":
						wasteS2();
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//Wasteland - South Twice from main path
		public static void wasteS2()
		{
			playerLocation = 33;
			while (playerInput != "south" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (SOUTH2) DESCRIPTION");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						wasteS1();
						break;

					case "south":
						Console.WriteLine("YOU HAVE GONE TOO FAR"); //DEATH
						Console.WriteLine("WILL RESET HERE");
						Thread.Sleep(2000);
						//EXIT BACK TO TITLESCREEN WITH RESET VALUES
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 1
		public static void room1()
		{
			playerLocation = 1;
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
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 2
		public static void room2()
		{
			playerLocation = 2;
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
						Console.WriteLine("Invalid input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 3
		public static void room3()
		{
			playerLocation = 3;
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
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 4
		public static void room4()
		{
			playerLocation = 4;
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
						Console.WriteLine("Invalid input");
						Thread.Sleep(500);
						break;
				}
			}

		}

		// Room 5
		public static void room5()
		{
			playerLocation = 5;
			while (playerInput != "east" || playerInput != "north")
			{
				Console.Clear();
				Console.WriteLine("You see some damage has been inflicted to the walls of this section, looks like deep slashes.");
				Console.WriteLine("Faint pools of blood stain the floor but no bodies.");	                 

				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						room10();
						break;
					case "north":
						room4();
						break;
					default:
						Console.WriteLine("Invalid input");
						Thread.Sleep(500);
						break;
				}
			}

		}

		// Room 6
		public static void room6()
		{
			playerLocation = 6;
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
					case "i":
						showInventory();
						break;
					default:
						Console.WriteLine("Invalid input");
						Thread.Sleep(500);
						break;
				}
			}

		}

		// Room 7
		public static void room7()
		{
			playerLocation = 7;
			while (playerInput != "south" || playerInput != "west")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 7");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room8();
						break;
					case "west":
						room71();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 7.1
		public static void room71()
		{
			playerLocation = 71;
			while (playerInput != "north" || playerInput != "east" || playerInput != "west")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 71");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room73();
						break;
					case "west":
						room72();
						break;
					case "east":
						room7();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 7.2
		public static void room72()
		{
			playerLocation = 72;
			while (playerInput != "west")
			{
				Console.Clear();

				Console.WriteLine("ROOM DESCRIPTION 72");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						room71();
						break;
					//case to get item
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 7.3
		public static void room73()
		{
			playerLocation = 73;
			while (playerInput != "west" || playerInput != "south")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 73");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room71();
						break;

					case "west":
						if (doorE74 == true)
						{
							room74();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						break;
				}
			}
		}

		// Room 7.4
		public static void room74()
		{
			playerLocation = 74;
			while (playerInput != "east")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 74");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						room73();
						break;
					case "get blue keycard":
						Console.WriteLine("You pick up the blue keycard.");
						Thread.Sleep(500);
						doorE16 = true;
						doorS17 = true;
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 8
		public static void room8()
		{
			playerLocation = 8;
			while (playerInput != "north" || playerInput != "east" || playerInput != "west")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 8");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "west":
						if (doorE3 == true)
						{
							room3();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					case "east":
						room13();
						break;
					case "north":
						room7();
						break;
					//case to get item
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 9
		public static void room9()
		{
			playerLocation = 9;
			while (playerInput != "south")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 9");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room10();
						break;
					case "get red keycard":
						Console.WriteLine("You pick up the red keycard.");
						Thread.Sleep(500);
						doorE3 = true;
						doorE13 = true;
						doorS17 = true;
						doorS20 = true;
						doorE74 = true;
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 10
		public static void room10()
		{
			playerLocation = 10;
			while (playerInput != "north" || playerInput != "east" || playerInput != "west")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 10");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room9();
						break;
					case "east":
						//room15();
						break;
					case "west":
						room5();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 11
		public static void room11()
		{
			playerLocation = 11;
			while (playerInput != "south" || playerInput != "west")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 11");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						room12();
						break;
					case "west":
						room6();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 12
		public static void room12()
		{
			playerLocation = 12;
			while (playerInput != "north" || playerInput != "east")
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 12");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room11();
						break;
					case "east":
						room71();
						break;
					//case to get item
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 13
		public static void room13()
		{
			playerLocation = 13;
			while (playerLocation == 13)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 13");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						if (doorE13 == true)
						{
							room18();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					case "south":
						if (doorN14 == true)
						{
							room14();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a blue keycard.");
							Thread.Sleep(500);
						}
						break;
					case "west":
						room8();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 14
		public static void room14()
		{
			playerLocation = 14;
			while (playerLocation == 14)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 14");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room13();
						break;
					//case to get item
					default:
                        Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 15 
		public static void room15()
		{
			playerLocation = 15;
			while (playerLocation == 15)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 15");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						room20();
						break;
					case "west":
						room10();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 16
		public static void room16()
		{
			playerLocation = 16;
			while (playerLocation == 16)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 16");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "east":
						if (doorE16 == true)
						{
							room21();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a blue keycard.");
							Thread.Sleep(500);
						}
						break;
					case "south":
						room17();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 17
		public static void room17()
		{
			playerLocation = 17;
			while (playerLocation == 17)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 17");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "south":
						if (doorS17 == true)
						{
							room18();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					case "west":
						room12();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 18
		public static void room18()
        {
			playerLocation = 18;
			while (playerLocation == 18)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 18");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						if (doorS17 == true)
						{
							room17();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					case "south":
						room19();
						break;
					case "west":
						if (doorE13 == true)
						{
							room13();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(500);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 19
		public static void room19()
		{
			playerLocation = 19;
			while (playerLocation == 19)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 19");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room18();
						break;
					case "south":
						room20();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
        }

		// Room 20
		public static void room20()
		{
			playerLocation = 20;
			while (playerLocation == 20)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 20");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room19();
						break;
					/* case for the breach */
                    //case "south":
                    //	if (doorS20 == true)
                    //	{
                    //		breach();
                    //	}
                    //	else
                    //	{
                    //		Console.WriteLine("The door is locked, looks like you need a red keycard.");
                    //		Thread.Sleep(500);
                    //	}
                    //	break;
                    case "west":
						room15();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 21 
		public static void room21()
		{
			playerLocation = 21;
			while (playerLocation == 21)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 21");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					// puzzle door
					case "south":
						if (doorS21 == true)
						{
							room22();
						}
						else
						{
							Console.WriteLine("The door is locked, you need to answer a question.");
							Thread.Sleep(500);
						}
						break;
					case "west":
						room16();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 22 
		public static void room22()
		{
			playerLocation = 22;
			while (playerLocation == 22)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 22");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room21();
						break;
					// puzzle door
					case "south":
						if (doorS22 == true)
						{
							room23();
						}
						else
						{
							Console.WriteLine("The door is locked, you need to answer a question.");
							Thread.Sleep(500);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 23 
		public static void room23()
		{
			playerLocation = 23;
			while (playerLocation == 23)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 23");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room22();
						break;
					// puzzle door
					case "south":
						if (doorS23 == true)
						{
							room24();
						}
						else
						{
							Console.WriteLine("The door is locked, you need to answer a question.");
							Thread.Sleep(500);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 24 
		public static void room24()
		{
			playerLocation = 24;
			while (playerLocation == 24)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 24");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room23();
						break;
					// puzzle door
					case "south":
						if (doorS24 == true)
						{
							room25();
						}
						else
						{
							Console.WriteLine("The door is locked, you need to answer a question.");
							Thread.Sleep(500);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 25 
		public static void room25()
		{
			playerLocation = 25;
			while (playerLocation == 25)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 25");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					case "north":
						room24();
						break;
					// puzzle door
					case "east":
						if (doorE25 == true)
						{
							room26();
						}
						else
						{
							Console.WriteLine("The door is locked, you need to answer a question.");
							Thread.Sleep(500);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 26
		public static void room26()
		{
			playerLocation = 26;
			while (playerLocation == 26)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 26");
				playerInput = Console.ReadLine();
				switch (playerInput)
				{
					// teleporter
					case "teleport":
						room3();
						break;
					case "west":
						room25();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

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
			Console.WriteLine("         █████╗ ███████╗████████╗██████╗   █████╗      ███████╗██╗   ██╗██████╗  ██████╗ ██████╗  █████╗  ");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("        ██╔══██╗██╔════╝╚══██╔══╝██╔══██╗ ██╔══██╗     ██╔════╝██║   ██║██╔══██╗██╔═══██╗██╔══██╗██╔══██╗ ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("        ███████║███████╗   ██║   ██████╔╝ ███████║     █████╗  ██║   ██║██████╔╝██║   ██║██████╔╝███████║ ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("        ██╔══██║╚════██║   ██║   ██╔══██╗ ██╔══██║     ██╔══╝  ██║   ██║██╔══██╗██║   ██║██╔═══╝ ██╔══██║ ");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("        ██║  ██║███████║   ██║   ██║  ██║ ██║  ██║     ███████╗╚██████╔╝██║  ██║╚██████╔╝██║     ██║  ██║ ");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("        ╚═╝  ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═╝  ╚═╝     ╚══════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝  ╚═╝ ");
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
			Console.ForegroundColor = ConsoleColor.White;

			//Using temporary method names for loading etc
			switch (temp)
			{
				case "PLAY":
					Console.Clear();
					ship();
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
        }
    }
}
