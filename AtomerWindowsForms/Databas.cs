using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection; //IO och Reflection används för att hämta applikationsmapp
using System.Windows.Forms; //för MessageBox
using System.Data.SqlClient; //för Microsoft SQL databas
using System.Data; //för DataSet, DataRow, etc.

namespace AtomerWindowsForms
{
	/// <summary>
	/// Databas klassen kopplar upp sig till databasen och läser rader från tabellen tblAtomer
	/// till Dictionary(int,Atom). Man behöver inte ändra något här om man inte vill - det är
	/// bara en uppvisning på hur man kan nå databasen utan det inbyggda systemet i Visual Studio.
	/// 
	/// Vill man använda MySQL versionen av tblAtomer istället finns där anvisningar från sidan
	/// 24-28 i häftet från Databashantering. Mest viktigt är att man använda
	/// using MySql.Data.MySqlClient,
	/// MySqlConnection,
	/// en annan kopplingssträng,
	/// och en MySqlDataAdapter
	/// </summary>
	public class Databas
	{
		private string kopplingssträngen = @"Data Source=(LocalDB)\v11.0;"
			+ "AttachDbFilename=" + Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
			+ "\\dbAtomer.mdf;" + "Integrated Security=True;";
		private Dictionary<int, Atom> atomer;
		private SqlConnection kopplingen;

		/// <summary>
		/// Constructor för Databas
		/// </summary>
		public Databas()
		{
			//initialisera Dictionary atomer
			this.atomer = new Dictionary<int, Atom>();
			//initialisera kopplingen till databasen (öppnas inte än)
			kopplingen = new SqlConnection(kopplingssträngen);
		}

		//get till atomer
		public Dictionary<int, Atom> Atomer { get { return atomer; } }

		/// <summary>
		/// Öppnar kopplingen till databasen och returnerar sann om det
		/// lyckades och annars visar en felmedellande och returnerar falsk.
		/// </summary>
		/// <returns>sann om det lyckades, annars falsk</returns>
		private bool ÖppnaKopplingen()
		{
			try
			{
				//Öppna kopplingen och skickar tillbaka sann
				kopplingen.Open();
				return true;
			}
			catch (SqlException ex)
			{
				//Öppnades inte.  Skickar tillbaka falsk
				MessageBox.Show("Kan inte koppla till databasen.  Kontakt administratören. " + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Stängar kopplingen till databasen och returnerar sann om det
		/// lyckades och annars visar en felmedellande och returnerar falsk.
		/// </summary>
		/// <returns>sann om det lyckades, annars falsk</returns>
		private bool StängKopplingen()
		{
			try
			{
				//Stänger koppling och skickar tillbaka sann
				kopplingen.Close();
				return true;
			}
			catch (SqlException ex)
			{
				//Stängdes inte.  Skickar tillbaka falsk
				MessageBox.Show("Databasen stängdes inte ner! " + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Läser in rader från databasen och förvandla de till Atom objekt som sedan
		/// sätts i Dictionary atomer med Atomnummer som nyckel till objektet.
		/// Returnerar sann om det lyckades att öonna kopplingen, om tabellen existerar,
		/// om tabellen har rader, och om tabeller har en kolumn fldAtomnummer. Annars
		/// returneras falsk.
		/// </summary>
		/// <returns>sann om det lyckades, annars falsk</returns>
		public bool LäsaAtomer()
		{
			//Öppna databasen
			bool lyckades = ÖppnaKopplingen();

			//Om man inte kunde öppna databasen, slutar och returnerar falsk
			if (!lyckades) return false;

			//DataSet är en behållare/mellansteg för inläst databas-data (kan
			//innehålla flera tabeller)
			DataSet dataSet = new DataSet();

			//En ny DataAdapter skapas med ett select sql command redan inbyggt
			//(DataAdapter används sedan för att fylla en DataSet) 
			SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM tblAtomer", kopplingen);

			//Fill metoden på DataAdapter används för att faktiskt utföra fyllning
			//av Datasetet ds från tabellen Produkter
			dataAdapter.Fill(dataSet, "tblAtomer");

			//Om där finns inga tabeller, returnerar falsk
			if (dataSet.Tables.Count == 0)
				return false;

			//Om där finns inga rader i första tabellen, returnera falsk
			var table = dataSet.Tables[0];
			if (table.Rows.Count == 0)
				return false;

			//Om där finns ingen ID kolumnen, returnera falsk
			if (!table.Columns.Contains("fldAtomnummer"))
				return false;

			//Om där finns ingenting i ID kolumnen, returnera falsk
			var row = dataSet.Tables[0].Rows[0];
			if (row.IsNull("fldAtomnummer"))
				return false;

			//Temporär Atom variabel
			Atom atomTemp;

			//Loopa genom varje rad i tblAtomer tabellen (Tables[0] för att enligt
			//SELECT frågan är tblAtomer första (och enda) tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Läser värdena från en rad till Atom objektet
				atomTemp = new Atom();
				atomTemp.Atomnummer = int.Parse(dataRow["fldAtomnummer"].ToString());
				atomTemp.Namn = dataRow["fldNamn"].ToString();
				atomTemp.Förkortning = dataRow["fldForkortn"].ToString();
				atomTemp.Atomvikt = decimal.Parse(dataRow["fldAvikt"].ToString());
				atomTemp.Kokpunkt = decimal.Parse(dataRow["fldKokpkt"].ToString());
				atomTemp.Smältpunkt = decimal.Parse(dataRow["fldSmaltpkt"].ToString());

				//Sätt atomTemp objekt i Dictionary atomer med Atomnummer som nyckel
				//(om en Atomnummer redan finns i atomer, uppdatera bara värden)
				if (!atomer.ContainsKey(atomTemp.Atomnummer))
					atomer.Add(atomTemp.Atomnummer, atomTemp);
				else
					atomer[atomTemp.Atomnummer] = atomTemp;
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}

	}
}
