/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace System.Cor3.Forms
{
	public enum AMPM
	{
		AM,PM
	}
	public class DateTimeInfo
	{
		//connectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\journal_test.mdf;Integrated Security=True;User Instance=True"
		DateTime _datetime = DateTime.Now;
		public DateTime DateTimeValue { get { return _datetime; } set { _datetime = value; } }

		public bool IsPM { get { return (DateTimeValue.Hour > 12); } }
		public bool IsAM { get { return (DateTimeValue.Hour < 13); } }
		bool _fmt_military;
		public bool UseMilitary { get { return _fmt_military; } set { _fmt_military = value; } }
		public bool IsHourZero { get { return DateTimeValue.Hour == 0; } }
		public bool IsHourPM { get { return DateTimeValue.Hour > 12; } }

		public int Value_Month { get { return DateTimeValue.Month; } }
		public int Value_Day { get { return DateTimeValue.Day; } }
		public int Value_Year { get { return DateTimeValue.Year; } }

		public int Value_Hour
		{
			get
			{
				if (!UseMilitary)
				{
					if (IsHourZero) return 12;
					if (IsPM) return DateTimeValue.Hour - 12;
				}
				return DateTimeValue.Hour;
			}
		}

		public int Value_Minute { get { return DateTimeValue.Minute; } }
		public int Value_Second { get { return DateTimeValue.Second; } }
		public int Value_Millisecond { get { return DateTimeValue.Millisecond; } }

		#region 
		public int TranslateMonth(string name)
		{
			for (int i=0;i<12;i++) { if ( MonthNames[i].ToLower()==name.ToLower()) return i; }
			return -1;
		}
		/// <summary>Simple String.Format function for Month</summary>
		/// <param name="month">the number to format</param>
		/// <returns>Zero-pads a two digit number</returns>
		string MonthFormat(int month) { return string.Format("{0:00}",month); }
		/// <summary>Simple String.Format function for Year</summary>
		/// <param name="month">the number to format</param>
		/// <returns>Zero-pads a four digit number</returns>
		string YearFormat(int yr) { return string.Format("{0:2000}",yr); }
		
		public static string[] MonthNames = new string[]
		{
			"January","February","March","April","May","June",
			"July","August","September","October","November","December"
		};

		public DateTimeInfo(DateTime dtime)
		{
			_datetime = dtime;
		}
		public DateTimeInfo() : this(DateTime.Now)
		{
			
		}
		#endregion
	}
}
