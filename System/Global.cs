#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Cor3;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace System
{

	public class Global : last_addon
	{
		static public PropertyGrid _props;
		static public PropertyGrid PropertyGrid { get { return _props; } set { _props=value; } }
	}
	namespace Cor3 {
		
		namespace Forms{
			
			/*
			 * Created by SharpDevelop.
			 * User: tfw
			 * Date: 12/26/2009
			 * Time: 11:54 AM
			 * 
			 * To change this template use Tools | Options | Coding | Edit Standard Headers.
			 */
			using System;
			using System.Collections.Generic;
			using System.Windows.Forms;
			using System.Xml.Serialization;

		}
		
		#pragma warning disable 1587,1591, 162
		public class last_addon : ConsoleColorC
		{
			static public string LastLocalFile;
			static public string LastLocalPath;
			/// <summary>Loads a new file info</summary>
			/// <param name="in_file"></param>
			static public void GetFileInfo(string in_file)
			{
				
			}
		}
		public class ConsoleColorC : ConsoleStatus
		{
			#region CSTAT
			[Conditional("CONSOLE")] public static void Cls(){Console.Clear();}
			[Conditional("CONSOLE")] public static void cstat(string filter, params object[] text)
			{
				cstat(Console.ForegroundColor,filter,text);
			}
			[Conditional("CONSOLE")] public static void statR(string format, params object[] value) { cstat(ConsoleColor.Red,format,value); }
			[Conditional("CONSOLE")] public static void statRd(string format, params object[] value) { cstat(ConsoleColor.DarkRed,format,value); }
			[Conditional("CONSOLE")] public static void statG(string format, params object[] value) { cstat(ConsoleColor.Green,format,value); }
			[Conditional("CONSOLE")] public static void statGd(string format, params object[] value) { cstat(ConsoleColor.DarkGreen,format,value); }
			[Conditional("CONSOLE")] public static void statB(string format, params object[] value) { cstat(ConsoleColor.Blue,format,value); }
			[Conditional("CONSOLE")] public static void statBd(string format, params object[] value) { cstat(ConsoleColor.DarkBlue,format,value); }
			[Conditional("CONSOLE")] public static void statC(string format, params object[] value) { cstat(ConsoleColor.Cyan,format,value); }
			[Conditional("CONSOLE")] public static void statCd(string format, params object[] value) { cstat(ConsoleColor.DarkCyan,format,value); }
			[Conditional("CONSOLE")] public static void statM(string format, params object[] value) { cstat(ConsoleColor.Magenta,format,value); }
			[Conditional("CONSOLE")] public static void statMd(string format, params object[] value) { cstat(ConsoleColor.DarkMagenta,format,value); }
			[Conditional("CONSOLE")] public static void statY(string format, params object[] value) { cstat(ConsoleColor.Yellow,format,value); }
			[Conditional("CONSOLE")] public static void statYd(string format, params object[] value) { cstat(ConsoleColor.DarkYellow,format,value); }
			[Conditional("CONSOLE")] public static void statGr(string format, params object[] value) { cstat(ConsoleColor.Gray,format,value); }
			[Conditional("CONSOLE")] public static void statGrd(string format, params object[] value) { cstat(ConsoleColor.DarkGray,format,value); }
			[Conditional("CONSOLE")] public static void cstat(ConsoleColor clr, object text)
			{
				cstat(clr,"{0}",text);
			}
			[Conditional("CONSOLE")] public static void cstat(ConsoleColor clr, string filter, params object[] text)
			{
				ConsoleOutputText = string.Format(filter,text);
				ConsoleColor clx = Console.ForegroundColor;
				Console.ForegroundColor=clr;
				stat(filter,text);
				Console.ForegroundColor=clx;
			}
			#endregion
		}
		public class ConsoleStatus {
			static public string ConsoleOutputText = string.Empty;
			#region ' STAT '
			[Conditional("CONSOLE")] public static void stat(string filter, object data)
			{
				ConsoleOutputText = string.Format(filter,new object[] {data});
				Console.WriteLine(filter,data); return;
				#if NO_PLUGINS
				PlugIns.Opoo.Main.pop(ConsoleOutputText);
				#endif
			}
			[Conditional("CONSOLE")] public static void stat(string filter, params object[] data)
			{
				ConsoleOutputText = string.Format(filter,data);
				Console.WriteLine(ConsoleOutputText); return;
				#if NO_PLUGINS
				PlugIns.Opoo.Main.pop(string.Format(filter,data),false);
				#endif
			}
			[Conditional("CONSOLE")] public static void stat(string msg)
			{
				ConsoleOutputText = msg;
				Console.WriteLine(ConsoleOutputText); return;
				#if NO_PLUGINS
				PlugIns.Opoo.Main.pop(msg,false);
				#endif
			}
			[Conditional("CONSOLE")] public static void stat(object msg)
			{
				Console.WriteLine(msg.ToString()); return;
				//PlugIns.Opoo.Main.pop(msg,false);
			}
			[Conditional("CONSOLE")] public static void stat(string msg, bool Activate)
			{
				ConsoleOutputText = msg;
				#if (CONSOLE)
				Console.WriteLine(ConsoleOutputText); return;
				#endif
				#if NO_PLUGINS
				PlugIns.Opoo.Main.pop(msg,Activate);
				#endif
			}
			#region ' ApiCaller '
			static public string GetAsmNfoStr()
			{
				return String.Format(
					"{3} v{1}.{2} build {0}",
					System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build,
					System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major,
					System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor,
					System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
				);
			}
			//		#region ' ApiCaller '
			//		static public void ApiCaller<T>(IApi<T> obj, T api) { obj.Call(api); }
			//		static public void ApiCaller<T>(IApi<T> obj, T api, params object[] data) { obj.Call(api,data); }
			//		#endregion
			#endregion
			#endregion
		}
		#pragma warning restore 1587,1591, 162
		public delegate void ThreadInfo();
		public delegate void NotifyProgress(int current, int destination);
		public class ThreadClass
		{
			public Thread theThread;
			protected bool _fin;
			public bool IsFinished { get { return _fin; } }
			
			public event NotifyProgress Progress;
			public event ThreadInfo Begat;
			public event ThreadInfo Complete;
			
			public ThreadClass() : this(false)
			{
			}
			public ThreadClass(bool AutoStart)
			{
				InitializeThread();
				if (AutoStart) theThread.Start();
			}
			
			virtual public void InitializeThread() {  theThread = new Thread(Begin); }
			virtual public void Begin() { OnBegat(); }
			virtual public void Terminate() { if (theThread.IsAlive) theThread.Abort(); }
			
			protected virtual void OnProgress(int current, int destination)
			{
				if (Progress != null) Progress(current, destination);
				if (current==destination) OnComplete();
			}
			protected virtual void OnBegat() { if (Begat!=null) Begat(); }
			protected virtual void OnComplete() { if (Complete!=null) Complete(); }
			
		}
	}
	public class FileLoadedEventArgs : EventArgs
	{
		public FileInfo FileInfo;
		public FileLoadedEventArgs(FileInfo fi)
		{
			FileInfo = fi;
		}
	}
	public class EventStringArgs : EventArgs
	{
		public string data;
		public string[] info;
		public EventStringArgs(string _data) { data = _data; }
		public EventStringArgs(string _data, params string[] _info) :this(_data) { info = _info; }
	}

}
