using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
	public class Day12 : IDay
	{
		private class Node
		{
			public int Index;

			public List<Node> Connections { get; } = new List<Node>();

			public Node( int Index )
			{
				this.Index = Index;
			}
		}

		private string InputFile;

		public int Index { get { return 12; } }

		public Day12( string InputFile )
		{
			this.InputFile = InputFile;
		}

		public string Part1()
		{
			var Nodes = new Dictionary<int, Node>();

			using( var Reader = new StreamReader( InputFile ) )
			{
				while( !Reader.EndOfStream )
				{
					var NodeData = Reader.ReadLine();
					var NodeParts = NodeData.Split( new string[] { "<->" }, StringSplitOptions.RemoveEmptyEntries );
					var NodeConnectionData = NodeParts[ 1 ].Split( ',' );

					var Index = int.Parse( NodeParts[ 0 ] ); 
					var Node  = GetOrCreateNode( Nodes, Index );
					foreach( var ConnectionData in NodeConnectionData )
					{
						var ConnectedIndex = int.Parse( ConnectionData );
						var ConnectedNode  = GetOrCreateNode( Nodes, ConnectedIndex );
						Node.Connections.Add( ConnectedNode );
					}
				}
			}

			return IndecisionGenerator.GetResult();
		}

		public string Part2()
		{
			return IndecisionGenerator.GetResult();
		}

		private Node GetOrCreateNode( Dictionary<int, Node> Nodes, int Index )
		{
			Node Node;
			if( !Nodes.TryGetValue( Index, out Node ) )
			{
				Node = new Node( Index );
				Nodes[ Index ] = Node;
			}

			return Node;
		}
	}
}
