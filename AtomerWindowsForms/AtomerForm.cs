using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtomerWindowsForms
{
	/// <summary>
	/// AtomForm klassen visar upp data från tblAtomer med namn i en combobox som styr
	/// vad som stor på labels för de andra attribut.
	/// </summary>
	public partial class AtomerForm : Form
	{
		/// <summary>
		/// Constructor för AtomerForm tar emot en instans av atomerProgram. Man har
		/// tillgång till en instans av databasklassen som egenskap som i sitt tur
		/// har tillgång till egenskapen Atomer som är en Dictionary(int,Atom) av
		/// alla rader från databasen.
		/// </summary>
		/// <param name="atomerProgram"></param>
		public AtomerForm(AtomerProgram atomerProgram)
		{
			//Initialisera AtomerForm
			InitializeComponent();

			//Test: visar att allt fungerar som det ska och data läsas från tabellen
			MessageBox.Show(atomerProgram.AtomDatabas.Atomer[1].Namn + " hittades!");
		}
	}
}
