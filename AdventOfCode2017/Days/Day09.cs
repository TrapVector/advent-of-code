using System.IO;

namespace AdventOfCode2017.Days
{
	public class Day09 : IDay
	{
		private string InputFile;

		public int Index { get { return 9; } }

		public Day09( string InputFile )
		{
			this.InputFile = InputFile;
		}

		public string Part1()
		{
			var TotalScore = 0;

			using( var Reader = new StreamReader( InputFile ) )
			{
				var GroupScore = 0;
				var IsGarbage  = false;
				var Canceled   = false;

				while( !Reader.EndOfStream )
				{
					var Character = (char)Reader.Read();

					if( Canceled )
					{
						Canceled = false;
						continue;
					}

					switch( Character )
					{
					case '{':
						if( IsGarbage ) continue;
						GroupScore++;
						TotalScore += GroupScore;
						break;
					case '}':
						if( IsGarbage ) continue;
						GroupScore--;
						break;
					case '<':
						IsGarbage = true;
						break;
					case '>':
						IsGarbage = false;
						break;
					case '!':
						Canceled = true;
						break;
					}
				}
			}

			return TotalScore.ToString();
		}

		public string Part2()
		{
			var TotalGarbage = 0;

			using( var Reader = new StreamReader( InputFile ) )
			{
				var IsGarbage = false;
				var Canceled  = false;

				while( !Reader.EndOfStream )
				{
					var Character = (char)Reader.Read();

					if( Canceled )
					{
						Canceled = false;
						continue;
					}

					switch( Character )
					{
					case '<':
						if( IsGarbage ) TotalGarbage++;
						IsGarbage = true;
						break;
					case '>':
						IsGarbage = false;
						break;
					case '!':
						Canceled = true;
						break;
					default:
						if( IsGarbage ) TotalGarbage++;
						break;
					}
				}
			}

			return TotalGarbage.ToString();
		}
	}
}
