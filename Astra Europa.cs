using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static int playerLocation, monster1Location = 3, monster2Location, playerHealth = 20;
		// doors
		public static bool doorE3 = false, doorE13 = false, doorE16 = false, doorS17 = false, doorN14 = false, doorS20 = false, doorE74 = false, doorS13 = false;
		// puzzle doors:
		public static bool doorS21 = false, doorS22 = false, doorS23 = false, doorS24 = false, doorE25 = false;
		// test doors
		public static bool doorET1, doorST2;
		// monster status
		public static bool monster1 = true, monster2 = true;
		/* - Notebook -
		 * Add notes/clues to the clues array.
		 * When the player finds a clue, pass the index of the clue from the array to the addNote() method.
		 * This will record the note and update the cluesFound array.
		 * To check if the player has found a clue, use: if (cluesFound[clues Index]), see comments above cluesFound[] for more detail
		 * See the test rooms for examples
		 */
		public static string notebook;
		// the pageNo is used for formatting. The first page of the notebook lists the missing ship parts so this value begins at 2
		public static int pageNo = 2;
		// Notes/clues go here, pass the index of the note to the addNote() method to record the note in the notebook
		public static string[] clues = new string[]
		{
			"This is a clue to riddle",
			"This is another clue",
			"Information about solving a riddle"
		};
		// This array, cluesFound, contains an equal number of false (default value for bool) values to the clues array.
		// When a note/clue is added to the notebook, the index in cluesFound that corresponds to to the clue added is changed to true,
		// this happens in the addNote() method.
		// So if clues[1] is added to the notebook with addNote(1), then cluesFound[1] becomes true.
		// We can check if the note is added by: if (cluesFound[1])
		public static bool[] cluesFound = new bool [clues.Length];
		// the shipParts array is used to check against the player inventory
		// the part names I have used here are just placeholders
		public static string[] shipParts = new string[] { "Windscreen", "Engine", "Exhaust Pipe", "Warp Drive", "Flux Capacitor" };

		// This method add notes/clues to the notebook string and updates the cluesFound array
		public static void addNote(int clueInd)
		{
			notebook += $"\n- Page {pageNo} -\n";
			pageNo++;
			notebook += clues[clueInd] + "\n";
			cluesFound[clueInd] = true;
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



		//COMBAT ONE
		//public static void Combat1()
		//{

		//	int monsterHealth = 15, playerHit, creatureHit;
		//	bool playerBlock;
		//	Random rand = new Random();

		//	Console.Clear();
  //          Console.WriteLine("MONSTER INTRODUCTION");

		//	while (playerHealth > 0 && monsterHealth > 0)
		//	{
		//		bool combatInput = false;
		//		playerBlock = false;
		//		Console.Clear();
		//		Console.WriteLine($"Health {playerHealth}");
		//		Console.WriteLine($"Creature health {monsterHealth}");
		//		Console.WriteLine("hit or block?");

		//		while (combatInput == false)
		//		{
					
		//			switch (playerInput())
		//			{
		//				case "hit":
		//					combatInput = true;
		//					playerHit = rand.Next(7);
		//					if (playerHit != 0)
		//					{
		//						Console.WriteLine($"You do {playerHit + 1} damage");
		//						monsterHealth = monsterHealth - (playerHit + 1);
		//						Thread.Sleep(1000);
		//					}
		//					else
		//					{
		//						Console.WriteLine("You miss your attack");
		//					}
		//					break;
		//				case "block":
		//					combatInput = true;
		//					playerBlock = true;
		//					Console.WriteLine("You brace for an incoming attack");
		//					Thread.Sleep(1000);
		//					break;
		//				default:
		//					Console.WriteLine("Invaild Input");

		//					Thread.Sleep(1000);
		//					break;
		//			}

		//		}

		//		if (monsterHealth > 0)
		//		{
		//			creatureHit = rand.Next(6);
		//			if (creatureHit != 0)
		//			{
		//				if (playerBlock == true)
		//				{
		//					creatureHit = creatureHit - 4;
		//					if (creatureHit <= 0)
		//					{
		//						Console.WriteLine($"You block, the creatures attack!");
		//						Thread.Sleep(2000);
		//					}
		//					else
		//					{
		//						playerHealth = playerHealth - (creatureHit - 4);
		//						Console.WriteLine($"You block some of the attack, taking {creatureHit - 4} damage");
		//					}

		//				}
		//				else
		//				{

		//					playerHealth = playerHealth - creatureHit;
		//					Console.WriteLine($"You are hit for {creatureHit} points of damage");
		//					Thread.Sleep(2000);
		//				}

		//			}
		//			else
		//			{
		//				Console.WriteLine("The creature misses");
		//				Thread.Sleep(1000);
		//			}
		//		}
		//	}

		//	if (playerHealth! >= 0 || monsterHealth! >= 0)
		//	{
		//		if (playerHealth > monsterHealth)
		//		{
		//			Console.WriteLine("You defeat the creature");
		//			monster1 = false;
		//			Thread.Sleep(2000);
		//		}
		//		else
		//		{
		//			Console.WriteLine("DEATH SCREEN HERE");
		//			Thread.Sleep(2000);
					
		//		}

		//	}
		//}


		// Have continued playerLocation count from Room 26 
		// Ship --Need to add extinguishing fires
		public static void ship()
		{
			playerLocation = 27;
			while (playerLocation == 27)
			{
				Console.Clear();
				Console.WriteLine("You are standing in the command centre of your ship, there are electrical fires and sparks littered everywhere.");
                Console.WriteLine("The first task is to stamp these fires out."); 
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
				Console.WriteLine("You walk down the steps of the command centre to find something useful. ");
                Console.WriteLine("You notice a shiny red object ahead of you located in the escape pod section. ");	
				if (!inventory.Contains("Fire Extinguisher"))
				{
					Console.WriteLine("In the corridor that leads to the escape pod room you locate the red object, ");
                    Console.WriteLine("it’s a fire extinguisher.");	
				}
				switch (playerInput())
				{
					case "":
						break;
					case "get fire extinguisher":
					case "get extinguisher":
						Console.WriteLine("You pick up the fire extinguisher, ");
                        Console.WriteLine("This will be vital in saving what remains of your command centre, no time to waste.");	
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
				Console.WriteLine("Now the blazes are under control its time to set off in");
                Console.WriteLine("search of the crucial components needed for repairs.");	
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
				Console.WriteLine("You are now standing on the planet’s surface with your fragmented ship behind you,");
                Console.WriteLine("you survey the area noticing behind the ship to the west is the impassable mountain range. ");
                Console.WriteLine("Before you are 3 paths, choose wisely.");	
				switch (playerInput())
				{
					case "":
						break;
					case "west":
						airlock();
						break;
					case "east":
						wasteE1();
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
				Console.WriteLine("You plant your feet with each step, trying to regain your composure.");
                Console.WriteLine("You continue through a small canyon which is spattered with jagged rocks.");
                Console.WriteLine("The view is concerning. No signs of life or civilisation can be seen ahead.");
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

		//Wasteland - North Twice from main path as well as GAME OVER section needs to be added
		public static void wasteN2()
		{
			playerLocation = 32;
			while (playerLocation == 32)
			{
				Console.Clear();
				Console.WriteLine("You’ve been walking for hours and are starting to pass in and out of consciousness due to lack of oxygen");
                Console.WriteLine(",you’ve gone too far to turn back now this is the end of your journey.");
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
			playerLocation = 33;
			while (playerLocation == 33)
			{
				Console.Clear();
				Console.WriteLine("You perch yourself upon a rock and notice a path that leads down to a");
                Console.WriteLine("wasteland area, you reach the end of this path where the vast openness");
                Console.WriteLine("of the wasteland begins. This wasteland you find yourself in seems to be");
                Console.WriteLine("an endless expanse until you notice an unusually shaped rock in the distance.");
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

		//Wasteland - South Twice from main path and GAME OVER SECTION NEEDS TO BE ADDED
		public static void wasteS2()
		{
			playerLocation = 34;
			while (playerLocation == 34)
			{
				Console.Clear();
				Console.WriteLine("As you get closer and closer to this unusual rock you discover a harrowing sight,");
                Console.WriteLine("another marooned ship but a much older class of your own ship. The surrounding area of the ship");
                Console.WriteLine("is covered with the remains of its crew. You approach the ships airlock but before you reach the");
                Console.WriteLine(" door handle you soon realise this is a hostile planet. You are attacked by an alien creature and");
                Console.WriteLine(" are ripped to shreds!!!");
				switch (playerInput())
				{
					case "":
						break;
					case "north":
						wasteS1();
						break;

					case "south":
						deathSouth();
						break;

					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//Wasteland - East Once from first exiting airlock
		public static void wasteE1()
		{
			playerLocation = 35;
			while (playerLocation == 35)
			{
				Console.Clear();
				Console.WriteLine("With each breath you take your oxygen levels are gradually depleting,");
                Console.WriteLine("you make your way down a steep slope. Once you are at the bottom you notice what seams");
                Console.WriteLine("to be a structure of some kind. The structure begins to revel is shape more and more with");	
                Console.WriteLine("each step you take. Finally, you reach the structure, and it is now clear to you that is");
                Console.WriteLine("an abandoned colony station. In front of you see a door with a lever.");
				switch (playerInput())
				{
					case "":
						break;

					case "use lever":
					case "pull lever":
                        Console.WriteLine("YOU PULL THE LEVER AND ENTER");
						Thread.Sleep(500);
						room3();
						break;

					case "west":
						wasteland();
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
						doorS13 = true;
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
						room15();
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
						if (doorS13 == true)
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
				if (!cluesFound[0])
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
				if (!cluesFound[1])
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
			Random rand = new Random();
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
					while (monster1Location == 3)
					{
						monster1Location = rand.Next(1, 11);
					}
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
							genericDeath();
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

		public static void deathSouth()
		{
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(@"     
                                  .-        .--.                                          
                       .+:         -*+:       =%#+=:.                                     
                         **:        .*%*:.     .*%%%*=.                                   
                          =#+:.       :*%+:.     .*%%%+                                   
                           :##*-..      :##*-..    ##%#+=-.                               
                             *%%#=:...   :#%#*=:    -+%%#%#:                              
                              +%%%#*+-.    -*%##-.     +%%%*.                             
                               =%#%%%%=:.    :###*=:.   :##%#=                            
                           .    --=*%%##+:..  .*%#%#*-.   =#%#*:                          
                           :*=:     *#%#%#=:.   -*%%#%*-.   :+#%-                         
                            .##*:.   +%%%#%#=:.   .+%###+      :=-                        
                             :+%%#-    =#%%%##+:    .*##%=                                
                               .*%#+:    =%%%%##:.      :#=.                              
                                .%%##+-.  -*%%%#*=:.     .*#=                             
                                 .#%%#%*:   :+#%%##+.      .-:                            
                                   .*%%#%+=.  .=#%%%#-.                                   
                                     :+%#%%*.    -+#%%*:                                  
                                       :#%%#*:       :+#=                                 
                                         -*#%#:         =+.                               
                                            -*%=          .                               
                                              .+:                                         
                                                                                          
");
			Console.WriteLine();
			Console.WriteLine("\t\t\t\tYOU HAVE BEEN CLAWED TO DEATH");
			Thread.Sleep(5000);
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


		//This will be the main death method called when a player dies in combat etc
		public static void genericDeath()
		{
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
