using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtomerWindowsForms
{
	/// <summary>
	/// AtomerProgram innehåller en instans av Databas klassen och en Main metod som startar
	/// AtomerForm om det gick bra att läsa från databas.
	/// </summary>
	public class AtomerProgram
	{
		//instansvariabler
		private Databas databas = new Databas();

		//egenskaper
		public Databas AtomDatabas { get { return databas; } }

		/// <summary>
		/// Main metoden gör en instans av AtomerProgram och försöker anropa LäsaProdukter
		/// från Databas klassen. Om det lyckas köra AtomerForm med en referens till
		/// AtomerProgram för att nå AtomDatabas egenskapen och vad som helst annat man vill.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//instans av AtomerProgram
			AtomerProgram atomerProgram = new AtomerProgram();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//Om det gick bra att läsa från databasen, starta formen
			if (atomerProgram.databas.LäsaAtomer() == true)
			{
				Application.Run(new AtomerForm(atomerProgram));
			}
			//Annars visar en felmedellande
			else
				MessageBox.Show("Kopplingen till databasen misslyckades.");
		}
	}
}
