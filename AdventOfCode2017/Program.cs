using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using AdventOfCode2017.Days;

namespace AdventOfCode2017
{
	class Program
	{


		static void Main( string[] args )
		{
			IDay[] Days = {
				new Day08( @"input\day08.txt" ),
				new Day09( @"input\day09.txt" ),
				new Day10()
			};

			//Day07.Run();

			foreach( var Day in Days )
			{
				Console.WriteLine( "------" );
				Console.WriteLine( "Day {0}", Day.Index.ToString().PadLeft( 2, '0' ) );
				Console.WriteLine( "------" );
				Console.WriteLine( "Part 1: {0}", Day.Part1() );
				Console.WriteLine( "Part 2: {0}", Day.Part2() );
				Console.WriteLine();
			}

			Console.WriteLine( "Press any key to continue..." );
			Console.ReadKey( true );
		}
	}
}
