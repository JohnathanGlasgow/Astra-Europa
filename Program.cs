﻿using System;
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




		static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
