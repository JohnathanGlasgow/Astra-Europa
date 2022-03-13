using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static int playerLocation, monster1Location = 3, monster2Location, playerHealth = 20, phaserAmmo = 5;
		// doors
		public static bool doorE3 = false, doorE13 = false, doorE16 = false, doorS17 = false, doorN14 = false, doorS20 = false, doorE74 = false, doorS13 = false;
		// puzzle doors:
		public static bool doorS21 = false, doorS22 = false, doorS23 = false, doorS24 = false, doorE25 = false;
		// test doors
		public static bool doorET1, doorST2;
		// monster status
		public static bool monster1 = true, monster2 = true;
		//fires
		public static bool fireWest = true, fireNorth = true, fireEast = true;
		//oxygen
		public static int oxygen = 100;
		//Controls Menu return value
		public static int controlsReturn = 1;
		// variables for wait time
		public static int pauseM = 2500, pauseS = 1500;
		//medkit
		public static bool medkit = false;
		/* - Notebook -
		 * Add notes/clues to the clues array.
		 * When the player finds a clue, pass the index of the clue from the clues array to the addNote() method.
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
			"Written on the side of the Power Couplings, I found this message: \"What can run but never walks\"",
			"I found this engraved into the Hyperdrive: \"has a mouth but never talks\".",
			"A note attached to the Fins read, \"has a head but never weeps\"",
			"I found a note on the Oxidizer which read, \"has a bed but never sleeps?\""
		};
		// This array, cluesFound, contains an equal number of false (default value for bool) values to the clues array.
		// When a note/clue is added to the notebook, the index in cluesFound that corresponds to to the clue added is changed to true,
		// this happens in the addNote() method.
		// So if clues[1] is added to the notebook with addNote(1), then cluesFound[1] becomes true.
		// We can check if the note is added by: if (cluesFound[1])
		public static bool[] cluesFound = new bool [clues.Length];
		// the shipParts array is used to check against the player inventory
		public static string[] shipParts = new string[] { "Power Couplings", "Hyperdrive", "Fins", "Oxidizer" };

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
                    Console.WriteLine("FOUND - " + s);
				}
                else
                {
					Console.WriteLine("MISSING - " + s);
				}
			}
            Console.WriteLine(notebook);
			Console.ReadLine();
		}

		// This method formats and displays important status information like health and current weapon
		public static void PlayerStatus()
        {
			string weapon = "Unarmed";
			string ammo = "N/A";
            if (inventory.Contains("Phaser"))
            {
				weapon = "Phaser";
				ammo = phaserAmmo.ToString() + "/5";
            }
            Console.WriteLine(
@$"Health: {playerHealth}/20
Weapon: {weapon}
Ammo:	{ammo}

Press enter to continue...
");
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
            string playerInput = Console.ReadLine().ToLower();
            playerInput = playerInput.Replace("pick up", "get");
			playerInput = playerInput.Replace("take", "get");
			if (playerInput.Contains("ex"))
			{
				playerInput = playerInput.Replace("extinguisher", "ex");
				playerInput = playerInput.Replace("extinguish", "ex");
				playerInput = playerInput.Replace("fire ex", "ex");
			}
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
					//Console.WriteLine("How to play:");
					//Console.ReadLine();
					Controls();
					playerInput = "";
					break;
					//case "options":
					//case "o":
					//    Console.WriteLine("Options Menu");
					//    Console.ReadLine();
					//    playerInput = "";
					//    break;

				case "medkit":
				case "use medkit":
					if (inventory.Contains("Medkit"))
					{
						if (playerHealth < 20)
						{
							Console.WriteLine(@"You open up the medkit and spend some time cleaning and bandaging your wounds. 
After a brief rest, your condition has improved considerably.");
							if (playerHealth > 13)
                            {
								playerHealth = 20;
                            }
                            else
                            {
								playerHealth += 7;
                            }
							wait();
							inventory.Remove("Medkit");
						}
						else
                        {
                            Console.WriteLine("You feel fine so there is no need to use the medkit.");
							Thread.Sleep(pauseM);
                        }
						playerInput = "";
					}
					break;
				case "status":
					PlayerStatus();
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
            //Console.WriteLine(playerInput);
			//Console.ReadKey();
            return playerInput;
        }

		// This methods pauses while a message is displayed then prompts the user to continue
		public static void wait()
        {
			Thread.Sleep(3000);
            Console.WriteLine("Press enter to continue...");
			Console.ReadLine();
        }

        /* -Inventory-
		 * To add items to inventory use inventory.Add("Item Name")
		 * To check if an item is in the inventory use inventory.Contains("Item Name") - this returns a bool
		 * */
        public static List<string> inventory = new List<string> { "Notebook" };
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

		public static void Reset()
        {
			// player 
			playerHealth = 20;
			phaserAmmo = 5;
			// doors
			doorE3 = false; doorE13 = false; doorE16 = false; doorS17 = false; doorN14 = false; doorS20 = false; doorE74 = false; doorS13 = false;
			// puzzle doors:
			doorS21 = false; doorS22 = false; doorS23 = false; doorS24 = false; doorE25 = false;
			// test doors
			doorET1 = false; doorST2 = false;
			// combat
			monster1 = true; monster2 = true;
			monster1Location = 3;
			phaserAmmo = 5;
			//fires
			fireWest = true; fireNorth = true; fireEast = true;
			//oxygen
			oxygen = 100;
			//Controls Menu return value
			controlsReturn = 1;
			// notebook
			notebook = "";
			cluesFound = new bool[clues.Length];
			pageNo = 2;
			//inventory
			inventory = new List<string> { "Notebook" };
			// medkit 
			medkit = false;
	}


		//COMBAT ONE
		public static void Combat1()
        {

            int monsterHealth = 15, playerHit, creatureHit;
            bool playerBlock;
            Random rand = new Random();

            Console.Clear();
            Console.WriteLine("A man in a space suit runs into you");
            Console.WriteLine("blood covers his torso, a crazed look in his eyes");
            Console.WriteLine("your instincts kick in, its you or him!");
			Thread.Sleep(2000);

            while (playerHealth > 0 && monsterHealth > 0)
            {
                bool combatInput = false;
                playerBlock = false;
                Console.Clear();
				if (inventory.Contains("Phaser"))
				{
					Console.WriteLine($"Your Health {playerHealth}");
					Console.WriteLine($"Crazed Spaceman's Health {monsterHealth}");
					Console.WriteLine($"{phaserAmmo} phaser ammo");
					Console.WriteLine("attack, block, shoot");
				}
				else
				{
					Console.WriteLine($"Your Health {playerHealth}");
					Console.WriteLine($"Crazed Spaceman's Health {monsterHealth}");
					Console.WriteLine("attack, block");
				}

				while (combatInput == false)
                {

                    switch (playerInput())
                    {
                        case "attack":
                            combatInput = true;
                            playerHit = rand.Next(7);
                            if (playerHit != 0)
                            {
                                Console.WriteLine($"You do {playerHit + 1} points of damage");
                                monsterHealth = monsterHealth - (playerHit + 1);
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.WriteLine("You miss your attack");
								Thread.Sleep(1000);
                            }
                            break;
                        case "block":
                            combatInput = true;
                            playerBlock = true;
                            Console.WriteLine("You brace for an incoming attack");
                            Thread.Sleep(1000);
                            break;
						case "shoot":
							if (inventory.Contains("Phaser"))
							{
								if (phaserAmmo == 0)
								{
									Console.WriteLine("Out of ammo!");
									Thread.Sleep(1000);
									combatInput = false;
								}
								else
								{
									combatInput = true;
									phaserAmmo--;
									playerHit = rand.Next(12);
									if (playerHit > 2)
									{
										Console.WriteLine($"You do {playerHit + 5} points of damage");
										monsterHealth = monsterHealth - (playerHit + 5);
										Thread.Sleep(1000);
									}
									else
									{
										Console.WriteLine("You miss your shot");
										Thread.Sleep(1000);
									}
								}

							}
							else
							{
								Console.WriteLine("Invalid Input");
								Thread.Sleep(1000);
								combatInput = false;
							}
							break;

						default:
                            Console.WriteLine("Invalid Input");
                            Thread.Sleep(1000);
                            break;
                    }

                }

                if (monsterHealth > 0)
                {
                    creatureHit = rand.Next(6);
                    if (creatureHit != 0)
                    {
                        if (playerBlock == true)
                        {
                            creatureHit = creatureHit - 4;
                            if (creatureHit <= 0)
                            {
                                Console.WriteLine($"You block, the crazed spaceman's attack!");
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                playerHealth = playerHealth - (creatureHit - 4);
                                Console.WriteLine($"You block some of the attack, taking {creatureHit - 4} damage");
								Thread.Sleep(1000);
							}

                        }
                        else
                        {

                            playerHealth = playerHealth - creatureHit;
                            Console.WriteLine($"You are hit for {creatureHit} points of damage");
                            Thread.Sleep(2000);
                        }

                    }
                    else
                    {
                        Console.WriteLine("The crazed spaceman misses");
                        Thread.Sleep(1000);
                    }
                }
            }

            if (playerHealth! >= 0 || monsterHealth! >= 0)
            {
                if (playerHealth > monsterHealth)
                {
                    Console.WriteLine("You defeat the crazed spaceman");
                    monster1 = false;
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("You are over powered and defeated");
                    Thread.Sleep(2000);
					genericDeath();

				}

            }
        }

		//COMBAT TWO
		public static void Combat2()
		{
			int monsterHealth = 35, playerHit, creatureHit;
			bool playerBlock;
			Random rand = new Random();

			Console.Clear();
			Console.WriteLine("Fear takes you as you a giant rabid grizzly bear");
			Console.WriteLine("faces you, some sort of twisted experiment gone wrong");
			Console.WriteLine("what were they doing here?");
			Thread.Sleep(2000);
			while (playerHealth > 0 && monsterHealth > 0)
			{
				bool combatInput = false;
				playerBlock = false;
				Console.Clear();
				if (inventory.Contains("Phaser"))
				{
					Console.WriteLine($"Your Health {playerHealth}");
					Console.WriteLine($"Bears Health {monsterHealth}");
					Console.WriteLine($"{phaserAmmo} phaser ammo");
					Console.WriteLine("attack, block, shoot");
				}
				else
				{
					Console.WriteLine($"Your Health {playerHealth}");
					Console.WriteLine($"Bears Health {monsterHealth}");
					Console.WriteLine("attack, block");
				}
				while (combatInput == false)
				{

					switch (playerInput())
					{
						case "attack":
							combatInput = true;
							playerHit = rand.Next(7);
							if (playerHit != 0)
							{
								Console.WriteLine($"You do {playerHit + 1} points of damage");
								monsterHealth = monsterHealth - (playerHit + 1);
								Thread.Sleep(1000);
							}
							else
							{
								Console.WriteLine("You miss your attack");
								Thread.Sleep(1000);
							}
							break;
						case "block":
							combatInput = true;
							playerBlock = true;
							Console.WriteLine("You brace for an incoming attack");
							Thread.Sleep(1000);
							break;
						case "shoot":
							if (inventory.Contains("Phaser"))
							{
								if (phaserAmmo == 0)
								{
									Console.WriteLine("Out of ammo!");
									Thread.Sleep(1000);
									combatInput = false;
								}
								else
								{
									combatInput = true;
									phaserAmmo--;
									playerHit = rand.Next(14);
									if (playerHit > 1)
									{
										Console.WriteLine($"You do {playerHit + 5} points of damage");
										monsterHealth = monsterHealth - (playerHit + 5);
										Thread.Sleep(1000);
									}
									else
									{
										Console.WriteLine("You miss your shot");
										Thread.Sleep(1000);
									}
								}

							}
							else
							{
								Console.WriteLine("Invalid Input");
								Thread.Sleep(1000);
								combatInput = false;
							}
							break;
						default:
							Console.WriteLine("Invalid Input");
							Thread.Sleep(1000);
							break;
					}
				}

				if (monsterHealth > 0)
				{
					creatureHit = rand.Next(9);
					if (creatureHit != 0)
					{
						if (playerBlock == true)
						{
							creatureHit = creatureHit - 4;
							if (creatureHit <= 0)
							{
								Console.WriteLine($"You block, the bears attack!");
								Thread.Sleep(2000);
							}
							else
							{
								playerHealth = playerHealth - (creatureHit - 4);
								Console.WriteLine($"You block some of the attack, taking {creatureHit - 4} damage");
								Thread.Sleep(2000);
							}
						}
						else
						{
							playerHealth = playerHealth - creatureHit;
							Console.WriteLine($"You are hit for {creatureHit} points of damage");
							Thread.Sleep(2000);
						}
					}
					else
					{
						Console.WriteLine("The bear roars with madness!");
						Thread.Sleep(1000);
					}
				}
			}

			if (playerHealth! >= 0 || monsterHealth! >= 0)
			{
				if (playerHealth > monsterHealth)
				{
					Console.WriteLine("The bear slumps over, finally free of it's torment");
					monster2 = false;
					Thread.Sleep(2000);
				}
				else
				{
                    Console.WriteLine("You are easily overpowered by the monsterous bear, this is going to hurt");
					Thread.Sleep(2000);
					genericDeath();

				}
			}
		}

		//Oxygen on Planet Surface
		//Need to review when saves/resets get implemented
		public static int oxygenReturn(int oxygen)
        {
			if (oxygen <= 0)
            {
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("You have run out of oxygen");
				Thread.Sleep(1500);
				Console.ForegroundColor = ConsoleColor.White;
				genericDeath();
            }
            else
            {
				oxygen = oxygen - 15;
            }
			return oxygen;
        }


		// Have continued playerLocation count from Room 26 
		// Ship
		public static void ship()
		{
			controlsReturn = 2;
			playerLocation = 27;
			while (playerLocation == 27)
			{
				Console.Clear();
				if (fireEast == true)
                {
					Console.WriteLine("You are standing in the command centre of your ship, there are electrical fires and sparks littered everywhere.");
					Console.WriteLine("The first task is to stamp these fires out.");
                }
                else
                {
					Console.WriteLine("You are standing in the command centre of your ship, sparks continue to flash around you but the fires are no longer an issue");
                }
				switch (playerInput())
				{
					case "":
						break;
					case "south":
						escapePod();
						break;

					case "east":
						//Bit messy at the moment, may need to clean up code but still functions as needed -Devon
						if (!inventory.Contains("Fire Extinguisher"))
                        {
                            Console.WriteLine("There is a raging fire blocking the way. It seems to be fueled by the other fires somehow.");
							Thread.Sleep(pauseM);
                        }
                        else if (inventory.Contains("Fire Extinguisher") && (fireWest == true || fireNorth == true))
                        {
                            Console.WriteLine("There are still fires to get rid of elsewhere.");
							Thread.Sleep(pauseS);
						}
						else if (inventory.Contains("Fire Extinguisher") && (fireEast == true && fireWest == false && fireNorth == false))
						{
                            Console.WriteLine("You still have a fire blocking the way but it seems smaller than before.");
							Thread.Sleep(pauseM);
						}
                        else
                        {
							airlock();
                        }
						break;

					case "ex east":
					case "ex e":
					case "ex fire to east":
						if (inventory.Contains("Fire Extinguisher") && (fireEast == true && fireWest == false && fireNorth == false))
                        {
							fireEast = false;
							Console.WriteLine("You manage to get the last fire under control");
							Thread.Sleep(pauseS);
						}
                        else 
						{
							Console.WriteLine("There are still fires to get rid of elsewhere.");
							Thread.Sleep(pauseS);
						}
						break;

					case "north":
                        if (fireNorth == true)
                        {
							Console.WriteLine("There is a fire blocking any further access this way");
							Thread.Sleep(pauseS);
                        }
                        else
                        {
							Console.WriteLine("You see that beyond the fire is an empty storage panel");
							Console.WriteLine("Doesn’t seem to be anything useful over here.");
							Thread.Sleep(pauseM);
                        }
						break;

					case "ex north":
					case "ex n":
					case "ex fire to north":
						if (inventory.Contains("Fire Extinguisher"))
						{
							fireNorth = false;
							Console.WriteLine("You manage to get the fire under control");
							Thread.Sleep(pauseS);
						}
						else
						{
							Console.WriteLine("You have no way to put out this fire");
							Thread.Sleep(pauseS);
						}
						break;

					case "west":
						if (fireWest == true)
                        {
							Console.WriteLine("There is a fire blocking any further access this way");
							Thread.Sleep(pauseS);
                        }
                        else
                        {
							Console.WriteLine("You look out the front window to see nothing but a sheer cliff side of a monumental mountain range. ");
							Console.WriteLine("Doesn’t seem to be anything useful over here.");
							Thread.Sleep(pauseM);
                        }
						break;

					case "ex west":
					case "ex w":
					case "ex fire to west":
						if (inventory.Contains("Fire Extinguisher"))
						{
							fireWest = false;
							Console.WriteLine("You manage to get the fire under control");
							Thread.Sleep(pauseS);
						}
						else
						{
							Console.WriteLine("You have no way to put out this fire");
							Thread.Sleep(pauseS);
						}
						break;

					case "use ex":
					case "ex fire":
					case "ex":
						if (inventory.Contains("Fire Extinguisher"))
						{
							if (fireEast)
							{
								Console.WriteLine("You need to point the extinguisher in the direction of a fire.");
								if (fireNorth && fireWest)
								{
									Console.WriteLine("There are fires to your north, east, and west.");
								}
								else if (fireNorth && !fireWest)
								{
									Console.WriteLine("There are fires to your north and east.");
								}
								else if (!fireNorth && fireWest)
								{
									Console.WriteLine("There are fires to your east and west.");
								}
								else
								{
									Console.WriteLine("A fire remains to your east.");
								}
								wait();
							}
							else
							{
								Console.WriteLine("You have already put out the fires.");
								Thread.Sleep(pauseS);
							}
							
						}
						else
                        {
                            Console.WriteLine("You lack the means to extinguish the fire.");
							Thread.Sleep(pauseS);
						}
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
		//
		public static void escapePod()
		{
			playerLocation = 28;
			while (playerLocation == 28)
			{
				Console.Clear();
				if (inventory.Contains("Fire Extinguisher"))
				{
					Console.WriteLine("You are standing in the escape pod. There seems to be nothing of use left here.");
                    Console.WriteLine("Up the steps to the north is the command centre.");
				}
				else if (!inventory.Contains("Fire Extinguisher"))
				{
					Console.WriteLine("You walk down the steps of the command centre to find something useful. ");
					Console.WriteLine("You notice a shiny red object ahead of you located in the escape pod section. ");
					Thread.Sleep(pauseM);
					Console.WriteLine("\nIn the corridor that leads to the escape pod room you locate the red object, ");
					Console.WriteLine("it’s a fire extinguisher.");
				}
				switch (playerInput())
				{
					case "":
						break;
					case "get fire ex":
					case "get ex":
						Console.WriteLine("You pick up the fire extinguisher, ");
						Console.WriteLine("This will be vital in saving what remains of your command centre, no time to waste.");
						inventory.Add("Fire Extinguisher");
						wait();
						break;
					case "go up steps":
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
		// I've added a little to the description -JG
		public static void airlock()
		{
			playerLocation = 29;
			while (playerLocation == 29)
			{
				Console.Clear();
                Console.WriteLine("You are in the airlock of your ship.");
				Console.WriteLine("Now the blazes are under control its time to set off in search of the crucial components needed for repairs.");
                Console.WriteLine("To your east is the alien terrain.");
				switch (playerInput())
				{
					case "":
						break;
					case "west":
						ship();
						break;

					case "east":
                        Console.WriteLine("You brace yourself as the airlock cycles.");
                        Console.WriteLine("The external door slowly creaks open and an unforgiving desert emerges.");
                        Console.WriteLine("You take a deep breath and proceed into the barren wasteland.");
						wait();
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
				Console.WriteLine();
				Console.WriteLine("Your oxygen meter alerts that the surface air is Toxic");
				oxygen = oxygenReturn(oxygen);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
				Console.ForegroundColor = ConsoleColor.White;
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
				oxygen = oxygenReturn(oxygen);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
				Console.ForegroundColor = ConsoleColor.White;
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

		//Wasteland - North Twice from main path 
		public static void wasteN2()
		{
			playerLocation = 32;
			while (playerLocation == 32)
			{
				Console.Clear();
                Console.WriteLine("Your oxygen levels are getting low, your next decision is vital.");
				oxygen = oxygenReturn(oxygen);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
				Console.ForegroundColor = ConsoleColor.White;
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
				oxygen = oxygenReturn(oxygen);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
				Console.ForegroundColor = ConsoleColor.White;
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
			playerLocation = 34;
			while (playerLocation == 34)
			{
				Console.Clear();
				Console.WriteLine("As you get closer and closer to this unusual rock you discover a harrowing sight,");
                Console.WriteLine("another marooned ship but a much older class of your own ship. The surrounding area of the ship");
                Console.WriteLine("is covered with the remains of its crew.");
				oxygen = oxygenReturn(oxygen);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
				Console.ForegroundColor = ConsoleColor.White;
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
                Console.WriteLine("to be a structure of some kind. The structure begins to reveal its shape more and more with");	
                Console.WriteLine("each step you take. Finally, you reach the structure, and it is now clear to you that is");
                Console.WriteLine("an abandoned colony station. In front of you see a door with a lever.");
				oxygen = oxygenReturn(oxygen);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
				Console.ForegroundColor = ConsoleColor.White;
				switch (playerInput())
				{
					case "":
						break;

					case "use lever":
					case "pull lever":
                        Console.WriteLine("You pull the lever and the door opens.");
						Console.WriteLine("You step into the airlock of the colony station, waiting patiently for the air to return.");
						Console.WriteLine("The second door opens, and you remove your helmet and move ahead.");
						wait();
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
			if (playerLocation == monster1Location && monster1 == true)
            {
				Combat1();
            }

			while (playerLocation == 1)
			{
				Console.Clear();
				Console.WriteLine("You enter this section seeing two paths south or east.");
				if (!medkit)
				{
					Console.WriteLine("In the corner, you spot a package wrapped in wax paper.");
				}
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
					case "get package":
						if (!medkit)
						{
							Console.WriteLine("You pick up the package and unwrap it...");
							Console.WriteLine("It is a medkit- this could make the difference between life and death.");
							Console.WriteLine("You add the medkit to your supplies.");
							wait();
							inventory.Add("Medkit");
							medkit = true;
						}
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
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 2)
			{
				Console.Clear();
				Console.WriteLine("An empty hallway that shows signs of a past struggle. Your options are south or north.");
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

		// Room 3, if statement needed for when player revisits room 3
		// Consider blocking off wasteland after player enters colony -JG
		public static void room3()
		{
			playerLocation = 3;
			
			while (playerLocation == 3)
			{
				Console.Clear();
				Console.WriteLine("There are three doors before you, north, east, and south. \nThe one to the east has a red icon on the control panel.");
                Console.WriteLine("To the west is the airlock to the surface.");
				// I have added this description after the lever in the wasteland because of possibility player goes north initially and returns without keycard -JG
				//if(!inventory.Contains("red keycard"))
				//            {
				//					Console.WriteLine("You step into the airlock of the colony station,");
				//					Console.WriteLine("waiting patiently for the air to return. The second door opens, and you");
				//					Console.WriteLine("remove your helmet and move ahead.");
				//					Console.WriteLine( "There are three doors before you, the one to the") ;
				//					Console.WriteLine("east has a red icon on the control panel, an item is needed before entry is granted, it says.");

				//}
				//            else { Console.WriteLine("You have entered the first section of the colony station where the airlock to the surface is located."); }
				switch (playerInput())
				{
					case "":
						break;
					case "west":
                        Console.WriteLine("You decide to head back to the wasteland and attempt to cycle the airlock.");
                        Console.WriteLine("To your dismay the control panel is completely unresponsive.");
                        Console.WriteLine("Your only choice is to explore the colony and hopefully find another way back to your ship.");
						wait();
						break;
					case "north":
						room2();
						break;
					case "east":
						if (doorE3 == true)
						{
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door opens automatically and you proceed through.");
							Thread.Sleep(pauseM);
							room8();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "south":
                        Console.WriteLine("You walk through the automatic door.");
						Thread.Sleep(pauseS);
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
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 4)
			{
				Console.Clear();
				// I moved the traversal description to after "south" in room 3 -JG
                Console.WriteLine("A long daunting hallway flows before you. Your options are north or south.");
				//Console.WriteLine("You walk through the automatic door; another long daunting hallway flows from this section to the next. Your options are north or south.");
				switch (playerInput())
				{
					case "":
						break;
					case "south":
						Console.WriteLine("As you head southward you are concerned by what appears to be traces of blood smeared on the wall");
						Thread.Sleep(pauseM);
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
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 5)
			{
				Console.Clear();
				Console.WriteLine("You see some damage has been inflicted to the walls of this section, looks like deep slashes.");
				Console.WriteLine("Faint pools of blood stain the floor but no bodies. Your options are corridors to the east or north.");


				switch (playerInput())
				{
					case "":
						break;
					case "east":
                        Console.WriteLine("You head through the corridor to the east.");
						Thread.Sleep(pauseM);
						room10();
						break;
					case "north":
						Console.WriteLine("You head north up the corridor.");
						Thread.Sleep(pauseM);
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
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 6)
			{
				Console.Clear();
				Console.WriteLine("This section of the station has a large window with the view of the barren surface of this harsh planet.");
                Console.WriteLine("The dust ensconced couches indicate this was an observation lounge.");
                Console.WriteLine("Your options are east or west.");
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
					case "use couch":
					case "sit on couch":
					case "sit down":
					case "sit down on couch":
					case "sit":
					case "couch":
						Console.WriteLine(@"You clear the dust from one of the couches and settle in.
As you think of the colonists who once sat here gazing at this unforgiving terrain,
your eyes grow heavy and you nearly doze off. You snap yourself back to the present and stand up,
firm in your resolve to find the parts you need to escape this place.");
						wait();
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
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 7)
			{
				Console.Clear();
				Console.WriteLine("To the west is a staircase that goes up to what seems to be a second floor.");
                Console.WriteLine("South leads back to the dining room.");
				switch (playerInput())
				{
					case "":
						break;
					case "south":
						room8();
						break;
					case "west":
					case "go upstairs":
                        Console.WriteLine("You head up the stairs.");
						Thread.Sleep(pauseS);
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
				Console.WriteLine("You enter the first section of the second floor of the colony station.");
                Console.WriteLine("There is a door to the west and a passage to the north.");
                Console.WriteLine("To the east stairs descend down to the first floor.");
				switch (playerInput())
				{
					case "":
						break;
					case "north":
						room73();
						break;
					case "west":
						Console.WriteLine("You step through the automatic door that struggles to open.");
						Thread.Sleep(pauseM);
						room72();
						break;
					case "east":
					case "go downstairs":
						Console.WriteLine("You go back down the stairs.");
						Thread.Sleep(pauseS);
						room7();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 7.2 if statment needed if player revisits room.
		// i changed a few things here to make getting the item work with the description -JG
		public static void room72()
		{
			playerLocation = 72;
			while (playerLocation == 72)
			{
				Console.Clear();
                Console.WriteLine("You are in a dimly lit room.");
				if (!inventory.Contains("Power Couplings"))
                {
					Console.WriteLine("Sitting on a table in the corner of this dark section is a ship component, the Power Couplings");
				}
                else { Console.WriteLine("In the corner is the table where you found the Power Couplings."); }
                Console.WriteLine("To the east a door leads back to from whence you came.");
				switch (playerInput())
				{
					case "":
						break;
					case "east":
						room71();
						break;
					case "get couplings":
					case "get power couplings":
						Console.WriteLine(@"You take the Power Couplings. 
On the side is scrawled ""What can run but never walks"". 
You record it in your notebook.");
						wait();
						inventory.Add("Power Couplings");
						addNote(0);
						break;
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
				Console.WriteLine("This room has more evidence of something horrific happening in it, you can tell by the smell. ");
                Console.WriteLine("To the west is another door with a red control panel. A passage to the south leads back to the stairs."); 
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
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door stutters open and you proceed through.");
							Thread.Sleep(pauseM);
							room74();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					default:
						Console.WriteLine("Invalid Input");
						break;
				}
			}
		}

		// Room 7.4 if statement needed if player revisits
		// I've changed this a bit to make getting the keycard work better with the description -JG
		public static void room74()
		{
			playerLocation = 74;
			while (playerLocation == 74)
			{
				Console.Clear();
				Console.WriteLine("You are in the security room. A mangled body, probably the guard, lays on the ground.");
				if (!inventory.Contains("Blue Keycard"))
                {
					Console.WriteLine("Around the neck of the corpse hangs a blue keycard.");
				}
				else
                {
                    Console.WriteLine("The previous room is through the door to the east.");
                }
                //else { Console.WriteLine("You are back in the room where you found the blue key card on the mangled body."); }
					
				switch (playerInput())
				{
					case "":
						break;
					case "east":
						room73();
						break;
					case "get blue keycard":
					case "get keycard":
						Console.WriteLine("You remove the keycard from the body.");
						inventory.Add("Blue Keycard");
						Thread.Sleep(pauseM);
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

		// Room 8 if statement needed if player revisits
		// I've changed this a bit to make getting the item work better with the description -JG
		public static void room8()
		{
			playerLocation = 8;
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 8)
			{
				Console.Clear();
				Console.WriteLine("This is a plain dining room, probably where the regular crew ate their meals.");
                Console.WriteLine("There is a regular door to the east, a red door to the west, and an open passage to the north.");
				if (!inventory.Contains("Hyperdrive"))
                {
                    Console.WriteLine("Laying on the table in the middle of the room is a ship component, the Hyperdrive.");
					Console.WriteLine("You remember how vital repairing your ship is.");
                }
                //else { Console.WriteLine("You are passing back through the room where you found the Hyperdrive with the clue."); }
				switch (playerInput())
				{
					case "":
						break;
					case "west":
						if (doorE3 == true)
						{
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door opens automatically and you proceed through.");
							Thread.Sleep(pauseM);
							room3();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "east":
						Console.WriteLine("You open the door and head west down a long hallway which leads to another door.");
						Thread.Sleep(pauseM);
						Console.WriteLine("You open it and pass through.");
						Thread.Sleep(pauseS);
						room13();
						break;
					case "north":
						room7();
						break;
					case "get hyperdrive":
                        Console.WriteLine("You pick up the Hyperdrive. A message has been etched into its surface: \"has a mouth but never talks\".");
                        Console.WriteLine("This seems important so you record it in your notebook.");
						inventory.Add("Hyperdrive");
						addNote(1);
						wait();
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		// Room 9 if statement needed if player revisits
		// I've changed this room because we already had a keycard on a body -JG
		public static void room9()
		{
            
			playerLocation = 9;
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 9)
			{
				Console.Clear();
				//if(!inventory.Contains("red keycard"))
				//            {
				//	Console.WriteLine("You enter this section and the first thing that catches your eye is a body,");
				//	Console.WriteLine("you step slowly towards it and find a red key card, this could be useful.");
				//}
				//            else { Console.WriteLine("You are back in the room where you found the red key card on the body"); }
				Console.WriteLine("You are in an office of some kind. It has been totally ransacked. Smashed furniture is scattered across the floor.");
				if (!inventory.Contains("Red Keycard"))
                {
                    Console.WriteLine("Amongst the debris you spot what appears to be a red keycard.");
                }
                Console.WriteLine("The junction room is through the jammed door to the south.");
					switch (playerInput())
				{
					case "":
						break;
					case "south":
                        Console.WriteLine("You slide back through the stuck door.");
						Thread.Sleep(pauseS);
						room10();
						break;
					case "get red keycard":
					case "get keycard":
						Console.WriteLine("You pick up the red keycard.");
						inventory.Add("Red Keycard");
						Thread.Sleep(pauseM);
						doorE3 = true;
						doorE13 = true;
						doorS17 = true;
						//Breach door
						//doorS20 = true;
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
			if (playerLocation == monster1Location && monster1 == true)
			{
				Combat1();
			}
			while (playerLocation == 10)
			{
				Console.Clear();
				// The original description would be confusing if you had entered the room from the north or east so I've changed it -JG
				Console.WriteLine("This is a junction room with 3 directions you can choose.");
				Console.WriteLine("An automatic door to the north is stuck partway open, you can probably squeeze through.");
                Console.WriteLine("To the west a corridor is marked by traces of blood.");
                Console.WriteLine("To the east, a voltage sign is stenciled on to the rusty door.");
				//Console.WriteLine("This is a junction room with 2 directions you can choose, north and east.");
				switch (playerInput())
				{
					case "":
						break;
					case "north":
						Console.WriteLine("You shimmy through the door into the next room.");
						Thread.Sleep(pauseM);
						room9();
						break;
					case "east":
                        Console.WriteLine("You pull open the rusty door. It shuts behind you as you step through.");
						Thread.Sleep(pauseM);
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
				Console.WriteLine("This section has flickering lights that are hanging from the ceiling.");
				if (!inventory.Contains("Phaser"))
				{
					Console.WriteLine("During a brief moment of light, you spy what looks like a phaser.");
				}

                Console.WriteLine("A hallway to the west leads to the observation room.");
                Console.WriteLine("You spy a bunkbed through the slightly ajar door to the south.");
				switch (playerInput())
				{
					case "":
						break;
					case "south":
                        Console.WriteLine("The door is stuck but you are able to pull it open.");
						Thread.Sleep(pauseS);
						Console.WriteLine("It closes after you pass through.");
						Thread.Sleep(pauseM);
						room12();
						break;
					case "west":
						Console.WriteLine("You head down the narrow hallway and enter the observation room.");
						Thread.Sleep(pauseM);
						room6();
						break;
					case "north":
                        Console.WriteLine("You go up the ladder to an upper section.");
						Thread.Sleep(pauseM);
						room111();
						break;
					case "get phaser":
                        Console.WriteLine("You pick up the phaser, looks like it's good for five shots.");
						Thread.Sleep(pauseM);
						inventory.Add("Phaser");
						break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}
		//Room 11.1 is located above room 11 and is an observation deck with an item that lists parts needed to open a weapons cache
		public static void room111()
        {
			playerLocation = 111;
			while(playerLocation == 111)
            {
				Console.Clear();
                Console.WriteLine("You reach the top of the ladder and, and are blinded by the light of the outside world, a stark contrast to the dark areas found bellow. ");
				Console.WriteLine("The room you are in is lit due to the 360 degree view out the windows. On the floor though, a message written in blood reads ");
                Console.WriteLine("'Ahead you'll find a weapons cache but it is locked, you'll need to find 3 componets in order to unluck its and reveil its secrets. These parts are a Fuse, a Capacitor and a Codex Microchip'.");
                Console.WriteLine("You have one direction before you, east or you can head back down the ladder, south.");
				switch (playerInput())
				{
					case "":
						break;
					case "east":
                        Console.WriteLine("You squezze your body through the small hatch that leads to the next section");
						room112();
						Thread.Sleep(pauseM);
						break;
					case "south":
                        Console.WriteLine("You head back down the ladder.");
						room11();
						Thread.Sleep(pauseM);
						break;
					default :
                        Console.WriteLine("Invalid Input");
						Thread.Sleep(pauseM);
						break;
				}
            }
        }

		// Room 11.2 flows on from room111, here you find a sleeping quarters and a new weapon a stun baton.
		public static void room112()
        {
			playerLocation = 112;
			while (playerLocation == 112)
            {
				Console.Clear();
                Console.WriteLine("You climb out of the hatch to find yourself in what seems to be a sleeping quarters for the crew that was stationed here.");
                Console.WriteLine("You notice that all but one of the footlockers have been ransacked, almost as if someone was looking for something?");
                Console.WriteLine("To the north you will find the unopened locker, and to the south an open door that leads onwards.");

				switch(playerInput())
                {
					case "":
						break;
					case "north":
                        Console.WriteLine("You inspect footlocker and notice that the latch is locked but you arent so easily deterred.");
                        Console.WriteLine("After giving the latch a heafty wack, the lid springs open revealing a new weapon, a Stun Baton.");
						Thread.Sleep(pauseM);
						break;
					case "get Stun Baton":
                        Console.WriteLine("You pick up the Stun Baton, this can be used to breifly incapacitate enemies.");
						Thread.Sleep(pauseM);
						inventory.Add("Stun Baton");
						break;
					case "south":
                        Console.WriteLine("You step through a pratically closed door.");
						Thread.Sleep(pauseM);
						room113();
						break;
					case "west":
                        Console.WriteLine("This way leads back to the 360 degree viewing room.");
						Thread.Sleep(pauseM);
						room111();
						break;
					default:
                        Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
                }
            }
        }
		// Room 11.3 flows on from room11.2, its a storage room which the player has to find two components used to repair the weapons cache.
		private static void room113()
        {
			playerLocation = 113;
			while (playerLocation == 113)
            {
				Console.Clear();
                Console.WriteLine("This section seems to be a supply depot with shelves stacked from floor to ceiling with various parts and components, you think to yourself this place could be useful.");
                Console.WriteLine("You can search a shelf to the east and to the west. South takes you onwards.");

				switch (playerInput())
                {
					case "":
						break;
					case "east":
                        Console.WriteLine("This shelf is labeled with the sign 'Electrical Components'.");
                        Console.WriteLine("You spend some time here, methodically reviewing each box and each shelf then you stuck it, A fuse.");
						Thread.Sleep(pauseM);
						break;
					case "get fuse":
						Console.WriteLine("You pick up the fuse buried under countless other parts. But this is but one peice of the puzzle.");
						Thread.Sleep(pauseM);
						inventory.Add("fuse");
						break;
					case "west":
                        Console.WriteLine("This shelf is labeled with the sign 'Spare Parts'.");
                        Console.WriteLine("Cautionsly, you rummage through the 10 or so containers looking for what you need. In the second to last box you find what you are looking for, a Capacitor.");
						Thread.Sleep(pauseM);
						break;
					case "get capacitor":
                        Console.WriteLine("You pick up the Capacitor and store it away for later use. But this is but one peice of the puzzle.");
						Thread.Sleep(pauseM);
						inventory.Add("capacitor");
						break;
					case "south":
                        Console.WriteLine("As you approach the door to leave this room you notice the next room looks longer than any other room?");
						Thread.Sleep(pauseM);
						room114();
						break;
					case "north":
                        Console.WriteLine("You travel back to the sleeping quarters.");
						Thread.Sleep(pauseM);
						room112();
						break;
					default :
                        Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
        }
		
		//room 11.4 flows on from room 11.3, its a firing range that also has one part to be found in it
		public static void room114()
        {
			playerLocation = 114;
			while (playerLocation == 114)
            {
				Console.Clear();
                Console.WriteLine("You step through the door and enter a long section, longer than any other section before. The automated system spings to life, the lights come on and you here the wurring sound of machines.");
                Console.WriteLine("The lights reveal this place to be a firing range, the targets zip forward towards you. The gun racks are empty and there is no weapons cache in sight, it must be close now?");
                Console.WriteLine("There are some lockers standing against the south wall and yet another door located to the east.");

				switch(playerInput())
                {
					case "":
						break;
					case "south":
                        Console.WriteLine("You open each locker hoping almost expecting something to be hidden with in one of them. Then on the bottom shelf of the final locker you find something spectacular.");
                        Console.WriteLine("You discover the Codex Microchip.");
						Thread.Sleep(pauseM);
						break;
					case "get codex microchip":
                        Console.WriteLine("You grab the Codex Microchip, knowing that this brings you one step closer to unlocking the yet to be discoverd weapons cache.");
						Thread.Sleep(pauseM);
						inventory.Add("Codex Microchip");
						break;
					case "east":
                        Console.WriteLine("You eagerly walk towards the next door, anticipating that you will dicover what you have been looking for.");
						Thread.Sleep(pauseM);
						room115();
						break;
					case "north":
                        Console.WriteLine("You walk back into the supply depot.");
						Thread.Sleep(pauseM);
						room113();
						break;
					default:
                        Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;


                }
            }
        }

		//room11.5 is the armoury and where the weapons cache box is located and needed to be repaired
		private static void room115()
        {

        }
		// Room 12
		public static void room12()
		{
			playerLocation = 12;
			while (playerLocation == 12)
			{
				Console.Clear();
				Console.WriteLine("This room looks like an old bedroom. Musty bunkbeds are built into the walls.");
				Console.WriteLine("To the east is a hallway.");
                Console.WriteLine("You make out faint flashing through the outline of the door to the north.");
				if (!inventory.Contains("Fins"))
				{				
					Console.WriteLine("On one of the beds lies some fins for a ship.");
				}		

				switch (playerInput())
				{
					case "":
						break;
					case "north":
						Console.WriteLine("The door is stuck but you are able pull it open.");
						Thread.Sleep(pauseS);
						Console.WriteLine("It closes after you pass through.");
						Thread.Sleep(pauseM);
						room11();
						break;
					case "east":
						Console.WriteLine("You head east through a hallway into a cramped room.");
						Thread.Sleep(pauseM);
						room17();
						break;
					case "get fins":
						Console.WriteLine("A note is attached to the fins. It reads, \"has a head but never weeps\".");
						Console.WriteLine("Who knows, might come in handy later. You jot it down in your notebook");
						inventory.Add("Fins");
						addNote(2);
						wait();
						break;
					case "use bed":
					case "use bunkbed":
					case "get in bed":
					case "get in bunkbed":
					case "sleep":
					case "sleep in bed":
					case "sleep in bunkbed":
					case "sleep on bed":
					case "sleep on bunkbed":
					case "rest":
					case "rest in bed":
					case "rest in bunkbed":
					case "rest on bed":
					case "rest on bunkbed":
						Console.WriteLine("This is no time for dozing.");
						Thread.Sleep(pauseS);
						Console.WriteLine("Besides, who knows what horrors lurk beneath those sheets.");
						Thread.Sleep(pauseM);
						break;
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
				Console.WriteLine("Not much going on in this room, although it does give you a feeling that you are on the right track.");
				Console.WriteLine("Regular door to the west, blue door to the south, red door to the east.");
				switch (playerInput())
				{
					case "":
						break;
					case "east":
						if (doorE13 == true)
						{
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door whirs opens and you proceed through.");
							Thread.Sleep(pauseM);
							room18();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "south":
						if (doorS13 == true)
						{
							Console.WriteLine("You hold up the blue keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The magnetic lock disengages allowing you to pull it open and pass through.");
							Thread.Sleep(pauseM);
							room14();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a blue keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "west":
						Console.WriteLine("You open the door and head west down a long hallway which leads to another door.");
						Thread.Sleep(pauseM);
						Console.WriteLine("You open it and pass through.");
						Thread.Sleep(pauseS);
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
				Console.WriteLine("This room has a clear roof, you get a nice view of the stars");
				
				if (!inventory.Contains("Oxidizer"))
				{
					Console.WriteLine("In the corner of the room lies an Oxidizer, very key to survival onboard the ship...");	
				}
                else
                {
					Console.WriteLine("Your only option is to head north");
				}
				switch (playerInput())
				{
					case "":
						break;
					case "north":
						Console.WriteLine("You move back north.");
						Thread.Sleep(pauseS);
						room13();
						break;
					case "get oxidizer":
						Console.WriteLine("You pick up the oxidizer and notice a note is attached.");
						Console.WriteLine("The note reads, \"has a bed but never sleeps?\"");
						Console.WriteLine("Seems to be some kind of riddle or something.");
                        Console.WriteLine("You record it in your notebook.");
						inventory.Add("Oxidizer");
						addNote(3);
						wait();
						break;
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
                Console.WriteLine("Electrical cabinets line this small old room.");
				Console.WriteLine("A corridor leads east and a rusty door is to the west... where to go?");
				switch (playerInput())
				{
					case "":
						break;
					case "east":
						Console.WriteLine("You walk down the corridor.");
						Thread.Sleep(pauseS);
						room20();
						break;
					case "west":
						Console.WriteLine("You pull open the door and step through.");
                        Console.WriteLine("It creaks closed behind you.");
						Thread.Sleep(pauseM);
						room10();
						break;
					case "look at cabinets":
					case "look at electrical cabinets":
					case "use cabinets":
					case "use electrical cabinets":
					case "use cabinet":
					case "use electrical cabinet":
					case "open cabinet":
					case "open electrical cabinet":
					case "look in cabinet":
					case "look in electrical cabinet":
                        Console.WriteLine("All you see is a tangled mess of wires.");
                        Console.WriteLine("You close the cabinet.");
						Thread.Sleep(pauseM);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 16)
			{
				Console.Clear();
                Console.WriteLine("A ping pong table is smashed upon the floor...");
                Console.WriteLine("This would of have been a place of respite for the colonists...");
				Console.WriteLine("But nothing is of use here now.");
                Console.WriteLine("A door to the east is coloured blue,");
				Console.WriteLine("or you can head south through a narrow hallway");
				switch (playerInput())
				{
					case "":
						break;
					case "east":
						if (doorE16 == true)
						{
							Console.WriteLine("You hold up the blue keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The magnetic lock disengages allowing you to pull it open and pass through.");
							Thread.Sleep(pauseM);
							room21();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a blue keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "south":
						Console.WriteLine("You head south.");
						Thread.Sleep(pauseS);
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
		// I've added a case for north to room 16 and changed the description accordingly
		public static void room17()
		{
			playerLocation = 17;
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 17)
			{
				Console.Clear();
				Console.WriteLine("This room is a perfect square... no windows... no nothing.");
				Console.WriteLine("Corridors lead north, west, or south");
				switch (playerInput())
				{
					case "":
						break;
					case "south":
						Console.WriteLine("You travel south down a long windy hallway");
						Thread.Sleep(pauseM);
						if (doorS17 == true)
						{
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door slides opens and you proceed through.");
							Thread.Sleep(pauseM);
							room18();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
							Console.WriteLine("Disappointed, you make the long walk back to the square room.");
							Thread.Sleep(pauseM);
						}
						break;
					case "west":
                        Console.WriteLine("You travel west.");
						Thread.Sleep(pauseS);
						room12();
						break;
					case "north":
						Console.WriteLine("You travel north.");
						Thread.Sleep(pauseS);
						room16();
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 18)
			{
				Console.Clear();
				Console.WriteLine("Seems to be an old computer room, very dusty, as though no one has been around for a while...");
				Console.WriteLine("There are doors to the north and west painted red.");
                Console.WriteLine("A sign pointing south reads: MEDICAL BAY");
				switch (playerInput())
				{
					case "":
						break;
					case "north":
						if (doorS17 == true)
						{
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door slides open you travel down a long windy hallway.");
							Thread.Sleep(pauseM);
							room17();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "south":
						Console.WriteLine("You travel south.");
						Thread.Sleep(pauseS);
						room19();
						break;
					case "west":
						if (doorE13 == true)
						{
							Console.WriteLine("You hold up the red keycard to the panel adjacent the door.");
							Thread.Sleep(pauseS);
							Console.WriteLine("The door whirs opens and you proceed through.");
							Thread.Sleep(pauseM);
							room13();
						}
						else
						{
							Console.WriteLine("The door is locked, looks like you need a red keycard.");
							Thread.Sleep(pauseS);
						}
						break;
					case "computer":
					case "use computer":
					case "turn on computer":
					case "look at computer":
                        Console.WriteLine(@"To your surprise the dusty old console boots up.
However the only software installed is an old text based adventure game.
You quickly grow bored of it and power down the machine.");
						wait();
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 19)
			{
				Console.Clear();
				Console.WriteLine("This room is a medical bay. It contains a medipod capable of automatically healing injuries. ");
				if (playerHealth < 20)
                {
                    Console.WriteLine("You are injured, perhaps you should try the medipod.");
                }
				Console.WriteLine("Your options are north or south");
				switch (playerInput())
				{
					case "":
						break;
					case "use medipod":
					case "get in medipod":
					case "activate medipod":
					case "enter medipod":
						if (playerHealth < 20)
						{
							Console.WriteLine(@"You climb into the pod. At first nothing happens, but then the lid suddenly lowers.");
							Thread.Sleep(pauseM);
                            Console.WriteLine(	 @"The pod fills with electromagnetic gas.
Lights flash as you are scanned by the medipod.");
							Thread.Sleep(pauseM);
                            Console.WriteLine(	@"A robotic voice announces:
""Subject, Human... Age, Unknown... Initiating healing protocol""
You feel a series of rapid shocks and pass out.");
							Thread.Sleep(4000);
							Console.WriteLine(@"When you come to, the pod is open again and you feel totally refreshed and revitalized.
You exit the pod and continue on your way.");
							playerHealth = 20;
							wait();
						}
						else
                        {
                            Console.WriteLine("You feel fine so have no reason to use the pod.");
							Thread.Sleep(pauseM);
						}
						break;
					case "medipod":
					case "look at medipod":
                        Console.WriteLine("This is a standard medipod capable of treating a variety of lifeforms.");
						Thread.Sleep(pauseM);
						Console.WriteLine("To use, the subject simply enters the pod and is treated automatically.");
						Thread.Sleep(pauseM);
						break;
					case "north":
						Console.WriteLine("You travel north.");
						Thread.Sleep(pauseS);
						room18();
						break;
					case "south":
						Console.WriteLine("You head south down a staggered hallway and open the door.");
						Thread.Sleep(pauseM);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 20)
			{
				Console.Clear();
				Console.WriteLine("You notice two options in this tight room:");
                Console.WriteLine("To the north through a door... Or west down a corridor marked by a voltage sign...");
				//Removed Breach
				//Console.WriteLine("The way south appears to be blocked by another keypad. You hear a strong wind pushing on the door");
				switch (playerInput())
				{
					case "":
						break;
					case "north":
                        Console.WriteLine("You open the door and head north up a staggered hallway.");
						Thread.Sleep(pauseM);
						room19();
						break;

					//Removed 'Breach' Door
					//case "south":
					//	if (doorS20 == true)
					//	{
					//		Console.WriteLine("You scan the keycard through and push the door with all your might");
					//		Console.WriteLine("A large area of the facility appears to be exposed");
					//		Thread.Sleep(1500);
					//		breach();
					//	}
					//	else
					//	{
					//		Console.WriteLine("The door is locked, looks like you need a red keycard.");
					//		Thread.Sleep(500);
					//	}
					//	break;

					case "west":
						Console.WriteLine("You travel west through the corridor.");
						Thread.Sleep(pauseM);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 21)
			{
				Console.Clear();
				Console.WriteLine("This room would be bare if not for a sign, a door to the west,");
                 Console.WriteLine("and a door to the south with a computer console alongside it.");
				Console.WriteLine("The sign states:");
                //Console.WriteLine("'player must have all 4 clues and 4 ship parts in order to answer all questions'");
                Console.WriteLine("\n\"Four parts and four clues. The order of the riddle... 'What can' be true?\"\n");
				if (!doorS21)
				{
					Console.WriteLine("A red light is flashing, indicating the door to the south is locked...");
					Console.WriteLine("Slightly to the right of the door is the console....");
					Console.WriteLine("The screen reads: \"Enter the first line of the riddle...\"");
				}
				else
                {
                    Console.WriteLine("A green light flashes above the southern door indicating it is unlocked.");
                }
				switch (playerInput())
				{
					case "":
						break;
					case "what can run but never walks":
						doorS21 = true;
						break;
					case "south":
						if (doorS21 == true)
						{
                            Console.WriteLine("You travel south into the next room");
							Thread.Sleep(pauseS);
							room22();
						}
						else
						{
							Console.WriteLine("The door will not budge, you need to unlock it somehow.");
							Thread.Sleep(pauseM);
						}
						break;
					case "west":
                        Console.WriteLine("You head west back to the rec room.");
						Thread.Sleep(pauseS);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 22)
			{
				Console.Clear();
				Console.WriteLine("Like the previous room, this room contains little but");
				Console.WriteLine("a door to the north and a door to the south.");
                Console.WriteLine("There is a sign on the wall saying: \n");
				Console.WriteLine("\"A mouth may never speak, 'It is the second line I seek...'");
				Console.WriteLine("But the spacebar you won't need, let the words together squeeze...\"\n");
                
                Console.WriteLine("A computer console sits upon a desk to the south.");
				if (!doorS22)
				{
					Console.WriteLine("The cursor blinks, awaiting some input...");
				}
                else
                {
                    Console.WriteLine("The light above the southern door flashes green.");
                }
				switch (playerInput())
				{
					case "":
						break;
					case "hasamouthbutnevertalks":
						doorS22 = true;
						break;
					case "north":
                        Console.WriteLine("You head north.");
						Thread.Sleep(pauseS);
						room21();
						break;
					case "south":
						if (doorS22 == true)
						{
                            Console.WriteLine("You make your way south.");
							Thread.Sleep(pauseS);
							room23();
						}
						else
						{
							Console.WriteLine("You throw your shoulder against the door,");
                            Console.WriteLine("but it does not give, you need to unlock it somehow.");
							Thread.Sleep(pauseM);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 23)
			{
				Console.Clear();
				Console.WriteLine("You see a poster on the wall that reads:");
				Console.WriteLine("\n\"If only you had a head, then surely you would see,");
				Console.WriteLine("That all the boring vowels are just the same as P.");
				//Console.WriteLine("Thp thprd lpnp ps rpqpprpd...\"\n");
				Console.WriteLine("The third line is required...\"\n");
				if (!doorS23)
				{
					Console.WriteLine("The keyboard and monitor await your input...");
					Console.WriteLine("You realise that these rooms must be leading you somewhere...");
					Console.WriteLine("If you can unlock the next door you may progress further south.");
					Console.WriteLine("Otherwise you may go north to return to the previous room.");
				}
				else
                {
                    Console.WriteLine("The light above the southern door flashes green.");
                    Console.WriteLine("The northern door remains unlocked.");
                }


				switch (playerInput())
				{
					case "":
						break;
					case "hps p hppd bpt npvpr wppps":
						doorS23 = true;
						break;
					case "north":
						Console.WriteLine("You head northward...");
						Thread.Sleep(pauseS);
						room22();
						break;
					// puzzle door
					case "south":
						if (doorS23 == true)
						{
                            Console.WriteLine("You proceed further south.");
							Thread.Sleep(pauseS);
							room24();
						}
						else
						{
							Console.WriteLine("The door is shut tight. You curse these damned riddles");
							Thread.Sleep(pauseM);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 24)
			{
				Console.Clear();
				Console.WriteLine("This room is four times larger than all the others but has a similar setup to it... ");

				Console.WriteLine("No posters adorn the walls, though the console has a note stuck to it.");
				Console.WriteLine("The note reads: \n\n\"Combine the previous two rules to further your quest, ");
				Console.WriteLine("The last line has a crib but does not rest.\"\n");
				if (!doorS24)
				{
					Console.WriteLine("A light atop the southern door flashes red, or you can head back north.");
				}
				else
				{
					Console.WriteLine("The light to the south blinks green. The door to the north remains unlocked.");
				}

					switch (playerInput())
					{
						case "":
							break;
						case "hpspbpdbptnpvprslppps":
						case "hpspbpdbptnpvprslppps?":
						doorS24 = true;
							break;
						case "north":
							Console.WriteLine("You travel north.");
							Thread.Sleep(pauseS);
							room23();
							break;
						// puzzle door
						case "south":
							if (doorS24 == true)
							{
								Console.WriteLine("You travel south.");
								Thread.Sleep(pauseS);
								room25();
							}
							else
							{
								Console.WriteLine("The door is locked... Will these tests never end?");
								Thread.Sleep(pauseM);
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
			if (playerLocation == monster2Location && monster2 == true)
			{
				Combat2();
			}
			while (playerLocation == 25)
			{
				Console.Clear();
				if (doorE25 == false)
                {
					Console.WriteLine("As you enter you notice this room is different... ");
					Console.WriteLine("It has a sign above the door stating 'FINAL ROOM...'");
					Console.WriteLine("The monitor reads...");
                    Console.WriteLine("\n\"Although your quest may not have been nice,");
                    Console.WriteLine("In the answer to the riddle, you can never step twice.\"\n");
                    Console.WriteLine("A red light blinks above the southern door, or you may go back north...");
				}
                else
                {
					Console.WriteLine("The light flashes green, a wee smile comes onto your face..");
					Thread.Sleep(pauseM);
					EndScreen();
					//Console.WriteLine("You can either head east into what is called the 'final room' or back north");
				}
				switch (playerInput())
				{
					case "":
						break;
					case "a river":
					case "river":
						Thread.Sleep(pauseS);
						doorE25 = true;
						break;
					case "north":
						Console.WriteLine("You travel north.");
						Thread.Sleep(pauseS);
						room24();
						break;
					// puzzle door
					//case "east":
					//	if (doorE25 == true)
					//	{
     //                       Console.WriteLine("You head down a long hallway and enter room 26");
					//		EndScreen();
					//	}
					//	else
					//	{
					//		Console.WriteLine("The door is locked, you need to answer a question.");
					//		Thread.Sleep(500);
					//	}
					//	break;
					default:
						Console.WriteLine("Invalid Input");
						Thread.Sleep(500);
						break;
				}
			}
		}

		//// Room 26
		//public static void room26()
		//{
		//	playerLocation = 26;
		//	while (playerLocation == 26)
		//	{
		//		Console.Clear();
		//		Console.WriteLine("'Door slams behind you..' ");
  //              Console.WriteLine("Do you wish to enter the teleport? (Yes/No)");
		//		string temp = Console.ReadLine().ToLower();
		//		switch (temp)
		//		{
		//			case "":
		//				break;
		//			// teleporter
		//			case "yes":
  //                      Console.WriteLine("Rumbling starts. The green light is flashing. Your life flashes before you...");
  //                      Console.WriteLine("You see all the good memories and the bad ones.You are now in room 3");
		//				room3();
		//				break;
		//			case "no":
  //                      Console.WriteLine("Your mind starts asking questions.. why do you wish to stay here... ");
  //                      Console.WriteLine("why would you not return to your family... your body takes over.. you start the teleporter and jump in...");
  //                      Console.WriteLine("you reappear with a smile one your face in room 3");
		//				room3();
		//				break;
		//			case "west":
  //                      Console.WriteLine("Door is locked");
		//				room25();
		//				break;
		//			default:
		//				Console.WriteLine("Invalid Input");
		//				Thread.Sleep(500);
		//				break;
		//		}
		//	}
		//}

		//Breach
		//Needs story/potential combat
		//public static void breach()
		//{
		//	playerLocation = 36;
		//	while (playerLocation == 36)
		//	{
		//		Console.Clear();
		//		Console.WriteLine("YOU ARE IN A DAMAGED AREA OF THE FACILITY");
		//		oxygen = oxygenReturn(oxygen);
		//		Console.ForegroundColor = ConsoleColor.Cyan;
		//		Console.WriteLine($"\nOxygen Supply at {oxygen}% capacity\n");
		//		Console.ForegroundColor = ConsoleColor.White;
		//		switch (playerInput())
		//		{
		//			case "":
		//				break;

		//			case "north":
		//				room20();
		//				break;

		//			default:
		//				Console.WriteLine("Invalid input");
		//				Thread.Sleep(500);
		//				break;
		//		}
		//	}

		//}

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
			Console.WriteLine("              .   .      .    .     .     .║               ║   .      .   .      . .  .  -+-        .      ");
			Console.WriteLine("                .           .   .        . ║    PLAY       ║         .          /         :  .             ");
			Console.WriteLine("          . .        .  .      /.   .      ║               ║.      .   .    .  /        . ' .              ");
			Console.WriteLine("              .  +       .    /     .      ║               ║   .          .   /      .                     ");
			Console.WriteLine("             .            .  /         .   ║    CONTROLS   ║        .        *   .         .     .         ");
			Console.WriteLine("            .   .      .    *     .     .  ║               ║ .      .   .       .  .                       ");
			Console.WriteLine("                .           .           .  ║               ║        .           .         +  .             ");
			Console.WriteLine("        . .        .  .       .   .      . ║    CREDITS    ║  .     .     .    .      .   .                ");
			Console.WriteLine("       .   +      .          ___/\\_._/~~\\_.║               ║.__/\\__.._._/~\\        .         .   .         ");
			Console.WriteLine("             .          _.--'              ║               ║                `--./\\          .   .          ");
			Console.WriteLine("                 / ~~\\/~\\                  ║    EXIT       ║                         `-/~\\_            .     ");
			Console.WriteLine("       .      .-'                          ║               ║                              `-/\\_               ");
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
					Reset();
					while (monster1Location == 3)
					{
						monster1Location = rand.Next(1, 11);
						monster2Location = rand.Next(16, 25);
					}
					Console.Clear();
					introScreen();
					break;

				//case "CONTINUE":
				//	Console.Clear();
				//	//LoadGame();
				//	Console.WriteLine("This is will be LoadGame()");
				//	break;

				case "CONTROLS":
					Console.Clear();
					Controls();
					//               Console.WriteLine("Press i to see intro screen tests");
					//               Console.WriteLine("Press g to see the generic death screen");
					//temp = Console.ReadLine().ToUpper();
					//switch (temp)
					//               {
					//	case "I":
					//		introScreen();
					//		break;

					//	case "G":
					//		genericDeath();
					//		break;
					//               }
					//Console.WriteLine("This is will be Controls()");
					break;

				case "CREDITS":
					Console.Clear();
					Credits();
					//Console.WriteLine("This is will be Credits()");
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

		//Modified the intro to prompt use if they want to see the intro/what version
		public static void introScreen()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			string introText1 = "The year is 2221, \n\nYou are an intrepid explorer navigating the vast emptiness of space on a mission of discovery and adventure. \n\nYou have set course on your lengthy pursuit for the outer reaches of an uncharted star system but during this arduous   journey you encounter an electrical storm that fries many electrical components and damages your ship, \nThe Astra Europa. \n\nThe solar winds are too forceful for your already damaged ship, and you are shunted off course. \n\nWhen you awake after the ordeal you find yourself crash landed on a mysterious yet almost familiar planet.";
			string introText2 = "\n\nYour ship is in desperate need of 4 new components and repairs if you ever wish to leave this unruly planet.";
			string introText3 = "\n\nYou set off in search for any signs of civilization. But remember due to the damages sustained to your ship you have no life support and";
			string introText4 = " only one oxygen tank ";
			string introText5 = "so choose carefully in which direction you take because wrong turns can spell     disaster.";
			string introText6 = "\n\nGameplay is simple, navigation is controlled by typing commands like North, South, East, and West.\nThere will be various objects that you may hold in your inventory that will aid you on your adventure.";
			string introText7 = "\n\n(Should you get stuck, type “help” or “info” for some aiding tips).";

			Console.WriteLine("Press ENTER to view intro");
			Console.WriteLine("Press Q to view quick intro");
			Console.WriteLine("Press S to skip intro");
			string temp1 = Console.ReadLine().ToUpper();
			if (temp1 == "Q")
			{
				Console.Clear();
				Console.Write(introText1);
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(introText2);
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(introText3);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(introText4);
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(introText5);
				Console.Write(introText6);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(introText7);
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("\n\nPress ENTER to Proceed...");
				Console.ReadLine();
			}
			else if (temp1 == "S")
			{
				ship();
			}
			else
			{
				//Long Intro
				Console.Clear();
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
				foreach (char i in introText6)
				{
					Console.Write(i);
					Thread.Sleep(1);
				}
				//Quick guide to help menu
				Console.ForegroundColor = ConsoleColor.Green;
				foreach (char i in introText7)
				{
					Console.Write(i);
					Thread.Sleep(1);
				}
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("\n\nPress ENTER to Proceed...");
				Console.ReadLine();
			}
			ship();
		}

		public static void EndScreen()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			string endText1 = "You step through the final door after completing the riddle and what stands before you is a teleporter pad where you\ncan input your ships coordinates.";
			string endText2 = "You stand on the pad and press the initiate button. A countdown begins 5…4…3…2…1\nand before you know it you are sent back to your ships command centre. You put the components you found to good use\nand begin repairing your damaged ship.";
			string endText3 = "It takes you only a few hours to complete the repairs, you ignite your ships\nthrusters and you are to be off that forsaken planet and plot a course towards the nearest space station for resupply.\nYou hope that will be the only detour you take on your expedition but only time will tell… ";

			//Console.WriteLine("Press ENTER to view ending");
			//Console.WriteLine("Press Q to view quick ending");
			//Console.WriteLine("Press S to skip ending");
			//string temp1 = Console.ReadLine().ToUpper();
			//if (temp1 == "Q")
			//{
			//	Console.Clear();
   //             Console.Write(endText1);
			//	Console.Write(endText2);
			//	Console.Write(endText3);
			//	Console.WriteLine("\n\nPress ENTER to Proceed...");
			//	Console.ReadLine();
			//}
			//else if (temp1 == "S")
			//{
			//	TitleScreen();
			//}
			//else
			//{
				//Long Intro
				Console.Clear();
				foreach (char i in endText1)
				{
					Console.Write(i);
					Thread.Sleep(1);
				}
				
				foreach (char i in endText2)
				{
					Console.Write(i);
					Thread.Sleep(1);
				}
				
				foreach (char i in endText3)
				{
					Console.Write(i);
					Thread.Sleep(1);
				}
				Console.WriteLine("\n\nPress ENTER to Proceed...");
				Console.ReadLine();
		//	}
			TitleScreen();
		}
	
			
			//Scripted Death 1 - North before entering main building
			public static void deathNorth()

			{		Console.Clear();
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
			Console.WriteLine(" 10/100");
			Console.WriteLine("\tYou’ve been walking for hours and are starting to pass in and out of consciousness");
            Console.WriteLine("\tdue to lack of oxygen,you’ve gone too far to turn back now this is the end of your journey.");
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
			Console.WriteLine("\t\tYou approach the ships airlock but before you reach the");
            Console.WriteLine("\t\tdoor handle you soon realise this is a hostile planet. ");
            Console.WriteLine("\t\tYou are attacked by an alien creature and are ripped to shreds!!!");
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

		//Viewed in the Menu or typing help/info ingame
		public static void Controls()
		{
			Console.Clear();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("                    ██████╗ ██████╗ ███╗   ██╗████████╗██████╗  ██████╗ ██╗     ███████╗");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                   ██╔════╝██╔═══██╗████╗  ██║╚══██╔══╝██╔══██╗██╔═══██╗██║     ██╔════╝");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("                   ██║     ██║   ██║██╔██╗ ██║   ██║   ██████╔╝██║   ██║██║     ███████╗");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                   ██║     ██║   ██║██║╚██╗██║   ██║   ██╔══██╗██║   ██║██║     ╚════██║");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("                   ╚██████╗╚██████╔╝██║ ╚████║   ██║   ██║  ██║╚██████╔╝███████╗███████║");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("                    ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚══════╝");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Tasks".PadRight(25));
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Player Input".PadRight(35));
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Directions/Movement".PadRight(25));
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Player Input");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("──────".PadRight(25));
			Console.Write("────────────────".PadRight(35));
			Console.Write("───────────────────".PadRight(25));
			Console.WriteLine("────────────────");
			Console.Write("Collecting Items:".PadRight(25));
			Console.Write("'Take' or 'Get'".PadRight(35));
			Console.Write("Moving North:".PadRight(25));
			Console.WriteLine("'north' or 'n'");
			Console.Write("Open Inventory:".PadRight(25));
			Console.Write("'inventory' or 'i'".PadRight(35));
			Console.Write("Moving East:".PadRight(25));
			Console.WriteLine("'east'  or 'e'");
			Console.Write("Checking the Notebook:".PadRight(25));
			Console.Write("'notebook' or 'read notebook'".PadRight(35));
			Console.Write("Moving South:".PadRight(25));
			Console.WriteLine("'south' or 's'");
			Console.Write("Controls Menu:".PadRight(25));
			Console.Write("'help' or 'info'".PadRight(35));
			Console.Write("Moving West:".PadRight(25));
			Console.WriteLine("'west'  or 'w'");
			Console.Write("Options Menu (saving):".PadRight(25));
			Console.WriteLine("'options' or 'o'".PadRight(35));
			Console.WriteLine("\n");
			if (inventory.Contains("Fire Extinguisher"))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Combat".PadRight(25));
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("Player Input".PadRight(35));
				Console.ForegroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Fires".PadRight(25));
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Player Input");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("────────".PadRight(25));
				Console.Write("────────────────".PadRight(35));
				Console.Write("────────────".PadRight(25));
				Console.WriteLine("────────────────");
				Console.Write("Attacking:".PadRight(25));
				Console.Write("'attack'".PadRight(35));
				Console.Write("Extinguishing:".PadRight(25));
				Console.WriteLine("'Extinguish *direction*'");
				Console.Write("Blocking:".PadRight(25));
				Console.Write("'block'".PadRight(60));
				Console.WriteLine("eg: 'extinguish east'");
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Combat".PadRight(25));
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Player Input");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("──────".PadRight(25));
				Console.WriteLine("────────────────");
				Console.Write("Attacking:".PadRight(25));
				Console.WriteLine("'attack'");
				Console.Write("Blocking:".PadRight(25));
				Console.WriteLine("'block'");
			}
			Console.ReadLine();
			if (controlsReturn == 1)
			{
				TitleScreen();
			}
			else if (controlsReturn == 2)
			{
				return;
			}
		}

		//Seen in the Menu
		//Could add to the end of the game upon completion
		public static void Credits()
		{
			Console.Clear();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("				 ██████╗██████╗ ███████╗██████╗ ██╗████████╗███████╗");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("				██╔════╝██╔══██╗██╔════╝██╔══██╗██║╚══██╔══╝██╔════╝");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("				██║     ██████╔╝█████╗  ██║  ██║██║   ██║   ███████╗");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("				██║     ██╔══██╗██╔══╝  ██║  ██║██║   ██║   ╚════██║");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("				╚██████╗██║  ██║███████╗██████╔╝██║   ██║   ███████║");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("				 ╚═════╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝   ╚═╝   ╚══════╝");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
			Console.WriteLine("					  ASTRA EUROPA TEXT ADVENTURE GAME");
			Console.WriteLine("					     Studio One Project 'Team 3'\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                                  Bryn Chambers");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                                      (Storyline and Additional Programming)\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                                Darryl Pentecost");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                                          (Main Programming and Combat)\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                            Devon Partridge-Officer");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                                     (Additional Programming and Artwork)\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                                Johnathan Glasgow");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                                      (Main Programming and User Input/Inventory)\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                                  Jordan Hand");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                                       (Storyline and Additional Programming)\n");
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("                                    Press ENTER to return to the Title Screen");
			Console.ForegroundColor = ConsoleColor.White;
			string temp1 = Console.ReadLine();
			if (temp1 == "")
			{
				TitleScreen();
			}
			else
			{
				Credits();
			}
		}

		static void Main(string[] args)
        {
			//testRoom1();

			TitleScreen();
			//room1();
			//Comment out Title and uncomment airlock to skip ship section for faster testing

        }
		/* Thoughts after playtesting:
		 GRIZZLY BEAR IS OP, needs nerf and/or there should be medkits/health potions to mitigate damage
		The dialog was sometimes too fast to read, it might be best to use Console.ReadLine() after longer passages.
		I have commented above the rooms where room numbers are used in dialog, this should be changed eventually.
		Also we need a method to reset all the values in the game, because currently after you die and restart the inventory/doors/etc remain the same. done Jg
		Otherwise it is all looking good
		-JG
		*/
    }
}
