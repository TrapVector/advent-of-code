using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
	public class IndecisionGenerator
	{
		private static string[] Results = {
			"I'm not sure...",
			"Hmm...",
			"Maybe it's... no...",
			"What could it be?",
			"Not done yet...",
			"Working on it...",
			"Almost got it...",
			"I don't got it...",
			"Uhh...",
			"I dunno...",
			"Beats me...",
			"42? Maybe?",
			"I wonder...",
			"Thinking...",
			"It's on the tip of my tongue...",
			"Nope, no idea..."
		};

		private static Random RandomGen = new Random();

		public static string GetResult()
		{
			return Results[ RandomGen.Next() % Results.Length ];
		}
	}
}
