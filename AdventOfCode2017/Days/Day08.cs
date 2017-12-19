using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
	public class Day08 : IDay
	{
		private enum Operation
		{
			Increment,
			Decrement
		}

		private enum Comparison
		{
			EqualTo,
			NotEqualTo,
			LessThan,
			LessThanOrEqualTo,
			GreaterThan,
			GreaterThanOrEqualTo
		}

		private string InputFile;

		private Dictionary<string, Operation> Operations = new Dictionary<string, Operation>
		{
			{ "inc", Operation.Increment },
			{ "dec", Operation.Decrement }
		};

		private Dictionary<string, Comparison> Comparisons = new Dictionary<string, Comparison>
		{
			{ "==", Comparison.EqualTo },
			{ "!=", Comparison.NotEqualTo },
			{ "<" , Comparison.LessThan },
			{ "<=", Comparison.LessThanOrEqualTo },
			{ ">" , Comparison.GreaterThan },
			{ ">=", Comparison.GreaterThanOrEqualTo }
		};

		public Day08( string InputFile )
		{
			this.InputFile = InputFile;
		}

		public int Index { get { return 8; } }

		public string Part1()
		{
			var Registers = new Dictionary<string, int>();

			RunProgram( Registers );

			return Registers.Values.Max().ToString();
		}

		public string Part2()
		{
			var Registers = new Dictionary<string, int>();

			var MaxValue = RunProgram( Registers );

			return MaxValue.ToString();
		}

		private int RunProgram( Dictionary<string, int> Registers )
		{
			var MaxValue  = int.MinValue;

			using( var Reader = new StreamReader( InputFile ) )
			{
				while( !Reader.EndOfStream )
				{
					var Instruction  = Reader.ReadLine();
					var Parts        = Instruction.Split( ' ' );

					var Register     = Parts[ 0 ];
					var Op           = Operations[ Parts[ 1 ] ];
					var Value        = int.Parse( Parts[ 2 ] );
					// Skip Parts[3]: constant "if"
					var CompRegister = Parts[ 4 ];
					var Comp         = Comparisons[ Parts[ 5 ] ];
					var CompValue    = int.Parse( Parts[ 6 ] );

					if( CompareRegisterValue( Registers, CompRegister, Comp, CompValue ) )
					{
						var NewValue = AdjustRegisterValue( Registers, Register, Op, Value );
						if( NewValue > MaxValue ) MaxValue = NewValue;
					}
				}
			}

			return MaxValue;
		}

		private int GetRegisterValue( Dictionary<string, int> Registers, string Name )
		{
			int Value;
			if( Registers.TryGetValue( Name, out Value ) )
			{
				return Value;
			}

			return 0;
		}

		private int AdjustRegisterValue( Dictionary<string, int> Registers, string Name, Operation Op, int Value )
		{
			if( Op == Operation.Decrement ) Value = -Value;

			var NewRegisterValue = GetRegisterValue( Registers, Name ) + Value;
			Registers[ Name ] = NewRegisterValue;

			return NewRegisterValue;
		}

		private bool CompareRegisterValue( Dictionary<string, int> Registers, string Name, Comparison Comp, int Value )
		{
			var RegisterValue = GetRegisterValue( Registers, Name );

			switch( Comp )
			{
			case Comparison.EqualTo:              return RegisterValue == Value;
			case Comparison.NotEqualTo:           return RegisterValue != Value;
			case Comparison.LessThan:             return RegisterValue < Value;
			case Comparison.LessThanOrEqualTo:    return RegisterValue <= Value;
			case Comparison.GreaterThan:          return RegisterValue > Value;
			case Comparison.GreaterThanOrEqualTo: return RegisterValue >= Value;
			}

			throw new InvalidOperationException( "Unknown comparison." );
		}
	}
}
