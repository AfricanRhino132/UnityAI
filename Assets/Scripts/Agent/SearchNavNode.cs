using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SearchNavNode : NavNode
{
	public NavNode parent { get; set; }
	public bool visited { get; set; }
}
