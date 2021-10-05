using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static int playerLocation;
		// doors
		public static bool doorE3 = false, doorE13 = false, doorE16 = false, doorS17 = false, doorN14 = false, doorS20 = false, doorE74 = false;
		// puzzle doors:
		public static bool doorS21 = false, doorS22 = false, doorS23 = false, doorS24 = false, doorE25 = false;
		// test doors
		public static bool doorET1, doorST2;

		/* - Notebook -
		 * Add notes/clues to the clues array.
		 * When the player finds a clue, pass the index of the clue from the array to the addNote() method.
		 * This will record the note and update the cluesFound array.
		 * To check if the player has found a clue, use: if (cluesFound[clueIndex] == clueIndex)
		 * See the test rooms for examples
		 */
		public static string notebook;
		// the pageNo is used for formatting. The first page lists the missing ship parts so this value begins at 2
		public static int pageNo = 2;
		// Notes go here, pass the index of the note to the addNote() method to record the note in the notebook
		public static string[] clues = new string[]
		{
			"This is a clue to riddle",
			"This is another clue",
			"Information about solving a riddle"
		};
		// the addNote() method changes the -1 values of the cluesFound[] to their corresponding index in the clues array
		public static int[] cluesFound = new int[] { -1, -1, -1 };
		// the shipParts array is used to check against the player inventory
		// the part names I have used here are just placeholders
		public static string[] shipParts = new string[] { "Windscreen", "Engine", "Exhaust Pipe", "Warp Drive", "Flux Capacitor" };

		// This method add notes/clues to the notebook string and updates the cluesFound array
		public static void addNote(int clueInd)
		{
			notebook += $"\n- Page {pageNo} -\n";
			pageNo++;
			notebook += clues[clueInd] + "\n";
			cluesFound[clueInd] = clueInd;
		}

		// This method formats and displays the notebook
		public static void readNotebook()
        {
            Console.WriteLine("\n- Page 1 -\nShip parts:");
			foreach (string s in shipParts)
			{
				if (inventory.Contains(s))
				{
                    Console.WriteLine("FOUND: " + s);
				}
                else
                {
					Console.WriteLine("MISSING: " + s);
				}
			}
            Console.WriteLine(notebook);
			Console.ReadLine();
		}
		/*  -Player Input Method-
		 * This method is called in the rooms when we ask for the next command, i.e. switch(playerInput())
		 * It checks if the command is to open the inventory, open the help text, open the options, read the notebook,
		 * or any other command that is universal across the game. 
		 * If a universal command is triggered, the method returns an empty string.
		 * The rooms will have an empty case/break for the empty string (so they don't default to "Invalid Command" after a universal command is triggered)
		 */
		public static string playerInput()
        {
            Console.Write("What next? ");
            string playerInput = Console.ReadLine();
            playerInput = playerInput.Replace("pick up", "get");
			playerInput = playerInput.Replace("take", "get");
			switch (playerInput)
            {
                case "inventory":
                case "i":
                    showInventory();
                    playerInput = "";
                    break;
                case "notebook":
				case "read notebook":
					readNotebook();
                    playerInput = "";
                    break;
                case "help":
                case "info":
                    Console.WriteLine("How to play:");
                    Console.ReadLine();
                    playerInput = "";
                    break;
                case "options":
                case "o":
                    Console.WriteLine("Options Menu");
                    Console.ReadLine();
                    playerInput = "";
                    break;
				case "n":
					playerInput = "north";
					break;
				case "e":
					playerInput = "east";
					break;
				case "s":
					playerInput = "south";
					break;
				case "w":
					playerInput = "west";
					break;
					
			}
            return playerInput;
        }

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
			while (playerLocation == 27)
			{
				Console.Clear();
				Console.WriteLine("SHIP DESCRIPTION");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 28)
			{
				Console.Clear();
				Console.WriteLine("ESCAPE POD DESCRIPTION");
				if (!inventory.Contains("Fire Extinguisher"))
				{
					Console.WriteLine("YOU NOTICE FIRE EXTINGUISHER");
				}
				switch (playerInput())
				{
					case "":
						break;
					case "get fire extinguisher":
					case "get extinguisher":
						Console.WriteLine("YOU PICK UP THE FIRE EXTINGUISHER");
						inventory.Add("Fire Extinguisher");
						Thread.Sleep(500);
						break;
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

		//Airlock
		public static void airlock()
		{
			playerLocation = 29;
			while (playerLocation == 29)
			{
				Console.Clear();
				Console.WriteLine("AIRLOCK DESCRIPTION");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 30)
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (OUTSIDE SHIP) DESCRIPTION");
				switch (playerInput())
				{
					case "":
						break;
					case "west":
						airlock();
						break;
					case "east":
						Console.WriteLine("YOU CONTINUE EAST ... LEVER");
						switch (playerInput())
						{
							case "":
								break;
							case "use lever":
							case "pull lever":
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
			while (playerLocation == 31)
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (NORTH1) DESCRIPTION");
				switch (playerInput())
				{
					case "":
						break;

					case "north":
						deathNorth();
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
			while (playerLocation == 32)
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (SOUTH1) DESCRIPTION");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 33)
			{
				Console.Clear();
				Console.WriteLine("WASTELAND (SOUTH2) DESCRIPTION");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 1)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 1");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 2)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 2");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 3)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 3");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 4)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 4");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 5)
			{
				Console.Clear();
				Console.WriteLine("You see some damage has been inflicted to the walls of this section, looks like deep slashes.");
				Console.WriteLine("Faint pools of blood stain the floor but no bodies.");

				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 6)
			{
				Console.Clear();
				Console.WriteLine("This section of the station has a window with the view of the barren surface of this harsh planet");
				switch (playerInput())
				{
					case "":
						break;
					case "west":
						room1();
						break;
					case "east":
						room11();
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
			while (playerLocation == 7)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 7");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 71)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 71");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 72)
			{
				Console.Clear();

				Console.WriteLine("ROOM DESCRIPTION 72");
				switch (playerInput())
				{
					case "":
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

		// Room 7.3
		public static void room73()
		{
			playerLocation = 73;
			while (playerLocation == 73)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 73");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 74)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 74");
				switch (playerInput())
				{
					case "":
						break;
					case "east":
						room73();
						break;
					case "get blue keycard":
						Console.WriteLine("You pick up the blue keycard.");
						inventory.Add("Blue Keycard");
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
			while (playerLocation == 8)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 8");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 9)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 9");
				switch (playerInput())
				{
					case "":
						break;
					case "south":
						room10();
						break;
					case "get red keycard":
						Console.WriteLine("You pick up the red keycard.");
						inventory.Add("Red Keycard");
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
			while (playerLocation == 10)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 10");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 11)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 11");
				switch (playerInput())
				{
					case "":
						break;
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
			while (playerLocation == 12)
			{
				Console.Clear();
				Console.WriteLine("ROOM DESCRIPTION 12");
				switch (playerInput())
				{
					case "":
						break;
					case "north":
						room11();
						break;
					case "east":
						room17();
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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
				switch (playerInput())
				{
					case "":
						break;
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

		// Test Room 1 
		public static void testRoom1()
		{
			playerLocation = -1;
			while (playerLocation == -1)
			{
				Console.Clear();
				Console.WriteLine("Test Room 1");
				Console.WriteLine("There is scribbling on the wall. There is a door to your east.");
				if (cluesFound[0] != 0)
				{
					Console.WriteLine("You see a crumpled piece of paper on the ground in the corner.");
				}
				if (!inventory.Contains("Red Keycard"))
				{
					Console.WriteLine("You see a red keycard on the ground");
				}
				if (!inventory.Contains("Windscreen"))
				{
					Console.WriteLine("In the corner rests a windscreen suitable for a spaceship.");
				}
				switch (playerInput())
				{
					case "":
						break;
					case "get paper":
						Console.WriteLine($"You uncrumple the paper and find a message:\n\"{clues[0]}\"");
                        Console.WriteLine("You record it in your notebook.");
						Console.ReadKey();
						addNote(0);
						break;
					case "look at wall":
					case "wall":
						addNote(2);
						Console.WriteLine($"You squint at the wall and make out a hastily scrawled message:\n\"{clues[2]}\"");
						Console.WriteLine("You record it in your notebook.");
						Console.ReadKey();
						break;
					case "get windscreen":
                        Console.WriteLine("You take the windscreen.");
						Thread.Sleep(1000);
						inventory.Add("Windscreen");
						break;
					case "get red keycard":
					case "get keycard":
						Console.WriteLine("You pick up the red keycard.");
						inventory.Add("Red Keycard");
						Thread.Sleep(1000);
						doorET1 = true;
						break;
					case "east":
						if (!doorET1)
						{
							Console.WriteLine("Looks like you need a red keycard");
							Thread.Sleep(1000);
						}
						else
						{
							Console.WriteLine("You scan the red keycard and go through the door");
							Thread.Sleep(1000);
							testRoom2();
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Test Room 2 
		public static void testRoom2()
		{
			playerLocation = -2;
			while (playerLocation == -2)
			{
				Console.Clear();
				Console.WriteLine("Test Room 2");
				Console.WriteLine("There are doors to your west and south.");
				if (!inventory.Contains("Flux Capacitor"))
				{
                    Console.WriteLine("You spot a flux capacitor underfoot.");
				}
				if (cluesFound[1] != 1)
				{
					Console.WriteLine("There is a paper airplane on the ground.");
				}
				if (!inventory.Contains("Blue Keycard"))
				{
					Console.WriteLine("You see a blue keycard on the ground");
				}
				switch (playerInput())
				{
					case "":
						break;
					case "get flux capacitor":
					case "get capacitor":
						Console.WriteLine("You pick up the flux capacitor.");
						inventory.Add("Flux Capacitor");
						Thread.Sleep(1000);
						break;
					case "get airplane":
						addNote(1);
						Console.WriteLine($"You pick up the airplane and unfold it. A note is written inside:\n\"{clues[1]}\"");
                        Console.WriteLine("Your record the note in your notebook.");
						Console.ReadKey();
						break;
					case "get blue keycard":
					case "get keycard":
						Console.WriteLine("You pick up the blue keycard.");
						inventory.Add("Blue Keycard");
						Thread.Sleep(1000);
						doorST2 = true;
						break;
					case "west":
						if (!doorET1)
						{
							Console.WriteLine("Looks like you need a red keycard");
							Thread.Sleep(1000);
						}
						else
						{
							Console.WriteLine("You scan the red keycard and go through the door");
							Thread.Sleep(1000);
							testRoom1();
						}
						break;
					case "south":
						if (!doorST2)
						{
							Console.WriteLine("Looks like you need a blue keycard");
							Thread.Sleep(1000);
						}
						else
						{
							Console.WriteLine("You scan the blue keycard and go through the door");
							Thread.Sleep(1000);
							testRoom3();
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Test Room 3 
		public static void testRoom3()
		{
			playerLocation = -3;
			while (playerLocation == -3)
			{
				Console.Clear();
				Console.WriteLine("Test Room 3");
				Console.WriteLine("There is a door to your north.");
				if (!inventory.Contains("Warp Drive"))
                {
                    Console.WriteLine( "You spot a warp drive rated for interdimensional travel.");
                }
				switch (playerInput())
				{
					case "":
						break;
					case "get warp drive":
                        Console.WriteLine("You pick up the warp drive.");
						Thread.Sleep(1000);
						inventory.Add("Warp Drive");
						break;
					case "north":
						if (!doorST2)
						{
							Console.WriteLine("Looks like you need a blue keycard");
							Thread.Sleep(1000);
						}
						else
						{
							Console.WriteLine("You scan the blue keycard and go through the door");
							Thread.Sleep(1000);
							testRoom2();
						}
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
             * Need to add Continue/Saving, Controls and Credits
            */
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
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
                    Console.WriteLine("Press i to see intro screen tests");
                    Console.WriteLine("Press g to see the generic death screen");
					temp = Console.ReadLine().ToUpper();
					switch (temp)
                    {
						case "I":
							introScreen();
							break;

						case "G":
							//genericDeath();
							break;
                    }
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

		/*
		 * Added currently as a test in the "CONTROLS" option on the main menu.
		 * Need to decide if it will go before/after menu as a group
		 */
		public static void introScreen()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			string introText1 = "The year is 2221, \n\nYou are an intrepid explorer navigating the vast emptiness of space on a mission of discovery and adventure. \n\nYou have set course on your lengthy pursuit for the outer reaches of an uncharted star system but during this arduous   journey you encounter an electrical storm that fries many electrical components and damages your ship, \nThe Astra Europa. \n\nThe solar winds are too forceful for your already damaged ship, and you are shunted off course. \n\nWhen you awake after the ordeal you find yourself crash landed on a mysterious yet almost familiar planet.";
			string introText2 = "\n\nYour ship is in desperate need of 5 new components and repairs if you ever wish to leave this unruly planet.";
			string introText3 = "\n\nYou set off in search for any signs of civilization. But remember due to the damages sustained to your ship you have no life support and";
			string introText4 = " only one oxygen tank ";
			string introText5 = "so choose carefully in which direction you take because wrong turns can spell     disaster.";

			foreach (char i in introText1)
			{
				Console.Write(i);
				Thread.Sleep(1);
			}
			//Different colour to show importance of ship parts
			Console.ForegroundColor = ConsoleColor.Yellow;
			foreach (char i in introText2)
			{
				Console.Write(i);
				Thread.Sleep(1);
			}
			Console.ForegroundColor = ConsoleColor.White;
			foreach (char i in introText3)
			{
				Console.Write(i);
				Thread.Sleep(1);
			}
			//Different colour to show importance of oxygen
			Console.ForegroundColor = ConsoleColor.Cyan;
			foreach (char i in introText4)
			{
				Console.Write(i);
				Thread.Sleep(1);
			}
			Console.ForegroundColor = ConsoleColor.White;
			foreach (char i in introText5)
			{
				Console.Write(i);
				Thread.Sleep(1);
			}
			Console.WriteLine("\n\nPress ENTER to Proceed...");
			Console.ReadLine();
			ship();
		}

		//Scripted Death 1 - North before entering main building
		public static void deathNorth()
		{
			Console.Clear();
			Console.WriteLine(@"                                                              
                                  :-=+*############*+=-:.                                 
                            .=+###+=-:.            .:-=+*##*=:                            
                        .=*%*=:.                           :=*%#=.                        
                     :+##=.                                    .=#%+:                     
              +:   =%#=.                                           -#%=                   
               =*+%+.                                                .+%+.                
              .+%*%=                                                    =%*.              
             =@+...+%+.              .:-=++****++=-:                      =@+             
           :%#:......*@+.        :+###+=-:.:...:--=+###+-                  .#%:           
          -@= .....:..:#@*.  .=#%+-                   .:+%#=.                -@=          
         +@-......:::::.-%@#*%+:                          .=%#:               :%+         
        *@:.....::::::::.:@@@%-                              -#%-              .%*        
       +@......::::::::.*%- =@@%=                              -%*.             :@*       
      -@-.....:::::::.:%*    .+@@@+                              +@:             :@-      
      %* ...::::::::.-@+       .*@@@+.                            -@=             +@.     
     +@....::::::::::@=          :#@@@*:                           -@-             %*     
     @+ .::::::::::.%*             -%@@@#- :--:.                    +@.            =@.    
    -@:.::::::::::.=@.               -%@@@@@@@@@%=                   @+            .@=    
    +@ .......:::: %*                  %@@@@@@@@@@#                  +@             %*    
    #@#############@-                 :@@@@@@@@@@@@-                 :@#############@%    
                                      .@@@@@@@@@@@@:                                      
                                       -@@@@@@@@@@@*:                                     
                                         =#@@@@%@@@@@#:                                   
                                                -%@@@=                                    
                                                  -=                                      ");
			Console.WriteLine("\t\t\t\tOxygen:  ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("\t\t\t\t█");
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.Write("░░░░░░░░░░░░░░░░░░░");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" 5/100");
			Console.WriteLine("\tYou see your oxygen is almost empty, you have been walking for too long.");
			Thread.Sleep(6000);
			Console.Clear();
			Console.WriteLine(@"                                                              
                                  :-=+*############*+=-:.                                 
                            .=+###+=-:.            .:-=+*##*=:                            
                        .=*%*=:.                           :=*%#=.                        
                     :+##=.                                    .=#%+:                     
              +:   =%#=.                                           -#%=                   
               =*+%+.                                                .+%+.                
              .+%*%=                                                    =%*.              
             =@+...+%+.              .:-=++****++=-:                      =@+             
           :%#:......*@+.        :+###+=-:.:...:--=+###+-                  .#%:           
          -@= .....:..:#@*.  .=#%+-                   .:+%#=.                -@=          
         +@-......:::::.-%@#*%+:                          .=%#:               :%+         
        *@:.....::::::::.:@@@%-                              -#%-              .%*        
       +@......::::::::.*%- =@@%=                              -%*.             :@*       
      -@-.....:::::::.:%*    .+@@@+                              +@:             :@-      
      %* ...::::::::.-@+       .*@@@+.                            -@=             +@.     
     +@....::::::::::@=          :#@@@*:                           -@-             %*     
     @+ .::::::::::.%*             -%@@@#- :--:.                    +@.            =@.    
    -@:.::::::::::.=@.               -%@@@@@@@@@%=                   @+            .@=    
    +@ .......:::: %*                  %@@@@@@@@@@#                  +@             %*    
    #@#############@-                 :@@@@@@@@@@@@-                 :@#############@%    
                                      .@@@@@@@@@@@@:                                      
                                       -@@@@@@@@@@@*:                                     
                                         =#@@@@%@@@@@#:                                   
                                                -%@@@=                                    
                                                  -=                                      ");
			Console.WriteLine("\t\t\t\tOxygen:  ");
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.Write("\t\t\t\t░░░░░░░░░░░░░░░░░░░░");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" 0/100");
			Console.Write("\t\t\t\tYour Oxygen is ");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("GONE");
			Thread.Sleep(4000);
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();

			Console.WriteLine(@"


		  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
		 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
		▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
		░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
		░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
		 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
		  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
		░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
			  ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
															 ░                   
");
			//May need to change with how save/load/resets works
			Console.WriteLine("\t\t\t\t\tPress ENTER to start again");
			Console.ReadLine();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			TitleScreen();
		}

		static void Main(string[] args)
        {
			//testRoom1();
			TitleScreen();
        }
    }
}
