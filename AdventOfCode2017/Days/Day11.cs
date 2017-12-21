using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
	public class Day11 : IDay
	{
		private struct Position
		{
			public int X;
			public int Y;
			public int Z;

			public Position( int X, int Y, int Z )
			{
				this.X = X;
				this.Y = Y;
				this.Z = Z;
			}
		}

		private enum Direction
		{
			North,
			NorthEast,
			NorthWest,
			South,
			SouthEast,
			SouthWest
		}

		private Dictionary<string, Direction> Directions = new Dictionary<string, Direction>
		{
			{ "n" , Direction.North },
			{ "ne", Direction.NorthEast },
			{ "nw", Direction.NorthWest },
			{ "s" , Direction.South },
			{ "se", Direction.SouthEast },
			{ "sw", Direction.SouthWest }
		};

		private string InputFile;

		public Day11( string InputFile )
		{
			this.InputFile = InputFile;
		}

		public int Index { get { return 11; } }

		public string Part1()
		{
			IEnumerable<Direction> Instructions;

			using( var Reader = new StreamReader( InputFile ) )
			{
				Instructions = ReadDirections( Reader.ReadToEnd() );
			}

			var CurrentPosition = new Position();

			foreach( var Instruction in Instructions )
			{
				CurrentPosition = UpdatePosition( CurrentPosition, Instruction );
			}

			var Distance = ComputeDistance( CurrentPosition );

			return Distance.ToString();
		}

		public string Part2()
		{
			IEnumerable<Direction> Instructions;

			using( var Reader = new StreamReader( InputFile ) )
			{
				Instructions = ReadDirections( Reader.ReadToEnd() );
			}

			var CurrentPosition = new Position();
			var MaxDistance = int.MinValue;

			foreach( var Instruction in Instructions )
			{
				CurrentPosition = UpdatePosition( CurrentPosition, Instruction );

				var Distance = ComputeDistance( CurrentPosition );
				if( Distance > MaxDistance ) MaxDistance = Distance;
			}

			return MaxDistance.ToString();
		}

		private static Position UpdatePosition( Position CurrentPosition, Direction Instruction )
		{
			switch( Instruction )
			{
			case Direction.North:
				CurrentPosition.Y++;
				CurrentPosition.Z--;
				break;
			case Direction.NorthEast:
				CurrentPosition.X++;
				CurrentPosition.Z--;
				break;
			case Direction.NorthWest:
				CurrentPosition.X--;
				CurrentPosition.Y++;
				break;
			case Direction.South:
				CurrentPosition.Y--;
				CurrentPosition.Z++;
				break;
			case Direction.SouthEast:
				CurrentPosition.X++;
				CurrentPosition.Y--;
				break;
			case Direction.SouthWest:
				CurrentPosition.X--;
				CurrentPosition.Z++;
				break;
			default:
				break;
			}

			return CurrentPosition;
		}

		private static int ComputeDistance( Position Position )
		{
			return ( Math.Abs( Position.X ) + Math.Abs( Position.Y ) + Math.Abs( Position.Z ) ) / 2;
		}

		private IEnumerable<Direction> ReadDirections( string Instructions )
		{
			var Instruction = new StringBuilder( 2 );

			foreach( var Character in Instructions )
			{
				if( Character == ',' || Character == '\n' )
				{
					yield return Directions[ Instruction.ToString() ];
					Instruction.Clear();
				}
				else
				{
					Instruction.Append( Character );
				}
			}

			yield break;
		}
	}
}
