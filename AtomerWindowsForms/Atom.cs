using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomerWindowsForms
{
	/// <summary>
	/// Atom är en behållare för en rad data från tblAtom.
	/// </summary>
	public class Atom
	{
		//instansvariabel
		int atomnummer;
		string namn;
		string förkortning;
		decimal atomvikt;
		decimal kokpunkt;
		decimal smältpunkt;

		//egenskaper
		public int Atomnummer { get { return atomnummer; } set { atomnummer = value;  } }
		public string Namn { get { return namn; } set { namn = value; } }
		public string Förkortning { get { return förkortning; } set { förkortning = value; } }
		public decimal Atomvikt { get { return atomvikt; } set { atomvikt = value; } }
		public decimal Kokpunkt { get { return kokpunkt; } set { kokpunkt = value; } }
		public decimal Smältpunkt { get { return smältpunkt; } set { smältpunkt = value; } }
	}
}
