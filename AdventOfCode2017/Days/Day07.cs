using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
	public class Day07
	{
		private class Dude
		{
			public string Name;
			public int Weight;

			public Dude Parent;
			public List<Dude> Dudes;

			public int TotalWeight
			{
				get
				{
					return Weight + Dudes.Sum( d => d.TotalWeight );
				}
			}

			public bool Balanced
			{
				get
				{
					return Dudes.Count > 0 ? Dudes.Sum( d => d.TotalWeight ) / Dudes.Count == Dudes[ 0 ].TotalWeight : true;
				}
			}

			public Dude( string Name )
			{
				this.Name = Name;

				Weight = -1;
				Dudes = new List<Dude>();
			}

			public void AddChild( Dude ChildDude )
			{
				Dudes.Add( ChildDude );
				ChildDude.Parent = this;
			}
		}

		static Regex DudeDataRegex = new Regex( @"([a-z]+) \(([0-9]+)\)(?: -> ([a-z]+(?:, [a-z]+)*))?" );

		private static Dude FindOrCreateDude( Dictionary<string, Dude> Dudes, string Name )
		{
			if( Dudes.ContainsKey( Name ) )
			{
				return Dudes[ Name ];
			}
			else
			{
				var NewDude = new Dude( Name );
				Dudes[ Name ] = NewDude;
				return NewDude;
			}
		}

		private static Dude FindUnbalancedDude( List<Dude> Dudes )
		{
			foreach( var Dude in Dudes )
			{
				if( !Dude.Balanced )
				{
					var UnbalancedDude = FindUnbalancedDude( Dude.Dudes );
					if( UnbalancedDude != null )
					{
						return UnbalancedDude;
					}
					else
					{
						return Dude;
					}
				}
			}

			return null;
		}

		public static void Run()
		{
			Console.WriteLine( "Day 7" );

			Console.WriteLine( "Part 1" );
			var Dudes = new Dictionary<string, Dude>();

			using( var Reader = new StreamReader( @"input\day07.txt" ) )
			{
				while( !Reader.EndOfStream )
				{
					var DudeData = Reader.ReadLine();
					var DudeDataMatch = DudeDataRegex.Match( DudeData );

					if( DudeDataMatch.Success )
					{
						var DudeName = DudeDataMatch.Groups[ 1 ].Value;

						Dude NewDude = FindOrCreateDude( Dudes, DudeName );

						NewDude.Weight = int.Parse( DudeDataMatch.Groups[ 2 ].Value );

						if( DudeDataMatch.Groups[ 3 ].Success )
						{
							var ChildDudeNames = DudeDataMatch.Groups[ 3 ].Value.Split( new string[] {  ", " }, StringSplitOptions.None );
							foreach( var ChildDudeName in ChildDudeNames )
							{
								var ChildDude = FindOrCreateDude( Dudes, ChildDudeName );
								NewDude.AddChild( ChildDude );
							}
						}
					}
				}

				var CurrentDude = Dudes.First().Value;
				while( CurrentDude.Parent != null ) CurrentDude = CurrentDude.Parent;

				Console.WriteLine( "Root = {0}", CurrentDude.Name );

				Console.WriteLine( "Part 2" );

				var RootDudes = new List<Dude>();
				RootDudes.Add( CurrentDude );

				var UnbalancedDude = FindUnbalancedDude( RootDudes );

				Console.WriteLine( "Unbalanced = {0}", UnbalancedDude.Name );

				Dude Liar;
				var SortedDudes = UnbalancedDude.Dudes.OrderBy( d => d.TotalWeight ).ToList();

				var PotentialLowDude = SortedDudes.First();
				var PotentialHighDude = SortedDudes.Last();
				var CorrectDude = SortedDudes[ 1 ];

				if( PotentialLowDude.TotalWeight < CorrectDude.TotalWeight &&
					CorrectDude.TotalWeight == PotentialHighDude.TotalWeight )
				{
					Liar = PotentialLowDude;
				}
				else
				{
					Liar = PotentialHighDude;
				}

				var Adjustment = Liar.TotalWeight - CorrectDude.TotalWeight;
				var CorrectedWeight = Liar.Weight - Adjustment;

				Console.WriteLine( "CorrectedWeight = {0}", CorrectedWeight );
			}
		}
	}
}
