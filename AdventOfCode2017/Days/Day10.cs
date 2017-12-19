using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
	public class Day10 : Days.IDay
	{
		private int[]  Part1Input = { 106, 16, 254, 226, 55, 2, 1, 166, 177, 247, 93, 0, 255, 228, 60, 36 };
		private string Part2Input = "106,16,254,226,55,2,1,166,177,247,93,0,255,228,60,36";
		private int[]  AddedLengths = { 17, 31, 73, 47, 23 };

		public int Index { get { return 10; } }

		public string Part1()
		{
			var Data = ComputeSparseHash( Part1Input, 1 );

			return ( Data[ 0 ] * Data[ 1 ] ).ToString();
		}

		private int[] ComputeSparseHash( int[] Lengths, int Rounds )
		{
			var Data = new int[] {
				  0,   1,   2,   3,   4,   5,   6,   7,   8,   9,  10,  11,  12,  13,  14,  15,
				 16,  17,  18,  19,  20,  21,  22,  23,  24,  25,  26,  27,  28,  29,  30,  31,
				 32,  33,  34,  35,  36,  37,  38,  39,  40,  41,  42,  43,  44,  45,  46,  47,
				 48,  49,  50,  51,  52,  53,  54,  55,  56,  57,  58,  59,  60,  61,  62,  63,
				 64,  65,  66,  67,  68,  69,  70,  71,  72,  73,  74,  75,  76,  77,  78,  79,
				 80,  81,  82,  83,  84,  85,  86,  87,  88,  89,  90,  91,  92,  93,  94,  95,
				 96,  97,  98,  99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111,
				112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127,
				128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143,
				144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
				160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175,
				176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191,
				192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207,
				208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223,
				224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
				240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255
			};

			var CurrentIndex = 0;
			var SkipSize = 0;

			for( var i = 0; i < Rounds; i++ )
			{
				RunHashRound( Data, Lengths, ref CurrentIndex, ref SkipSize );
			}

			return Data;
		}

		private void RunHashRound( int[] Data, int[] Lengths, ref int CurrentIndex, ref int SkipSize )
		{
			foreach( var Length in Lengths )
			{
				var SubData = ReadData( Data, CurrentIndex, Length );
				SubData = SubData.Reverse().ToArray();
				WriteData( Data, SubData, CurrentIndex );

				CurrentIndex = ( CurrentIndex + Length + ( SkipSize++ ) ) % Data.Length;
			}
		}

		private int[] ReadData( int[] Data, int Start, int Length )
		{
			var ReadData = new int[ Length ];

			int ReadSize = 0;
			while( ReadSize < Length )
			{
				ReadData[ ReadSize ] = Data[ ( Start + ReadSize ) % Data.Length ];
				ReadSize++;
			}

			return ReadData;
		}

		private void WriteData( int[] Data, int[] WriteData, int Offset )
		{
			int WriteSize = 0;
			while( WriteSize < WriteData.Length )
			{
				Data[ ( Offset + WriteSize ) % Data.Length ] = WriteData[ WriteSize ];
				WriteSize++;
			}
		}

		public string Part2()
		{
			var InputLengths = Part2Input.Select( c => (int)c ).Concat( AddedLengths );
			var Data = ComputeSparseHash( InputLengths.ToArray(), 64 );

			return ComputeDenseHash( Data );
		}

		private string ComputeDenseHash( int[] Data )
		{
			var Hash = "";

			for( var i = 0; i < 16; i++ )
			{
				int HashBlock = 0;
				for( var j = 0; j < 16; j++ )
				{
					HashBlock ^= Data[ i * 16 + j ];
				}

				Hash += string.Format( "{0:x2}", HashBlock );
			}

			return Hash;
		}
	}
}
