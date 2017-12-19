using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
	interface IDay
	{
		int Index { get; }

		string Part1();
		string Part2();
	}
}
