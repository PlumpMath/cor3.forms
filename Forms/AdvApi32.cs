/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 8/12/2009
 * Time: 3:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace System
{
	partial class WindowsInterop
	{
		/// <summary>
		/// Description of advapi32_interop.
		/// </summary>
		public class AdvApi32
		{
			public const int SC_MANAGER_ALL_ACCESS = 0xF003F;
			public const int SERVICE_ALL_ACCESS = 0xF01FF;
			public const int SERVICE_NO_CHANGE = -1;
			const string advapi = "advapi32.dll";
			
			#region Misc Win32 Interop Stuff
			/// Win32 function to unlock the service database.
			[DllImport(advapi)]
			static public extern bool UnlockServiceDatabase(IntPtr hSCManager);
			
			/// Win32 function to lock the service database to perform
			/// write operations.
			[DllImport(advapi)]
			static public extern IntPtr
				LockServiceDatabase(IntPtr hSCManager);
			
			/// Win32 function to open the service control manager
			[DllImport(advapi)]
			static public extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, int dwDesiredAccess);
			
			// CreateService
			/// Win32 function to close a service related handle.
			[DllImport(advapi)]
			static public extern bool CloseServiceHandle(IntPtr hSCObject);
			
			[DllImport(advapi)]
			static public extern bool ChangeServiceConfigA(
				int hService, ServiceType dwServiceType, int dwStartType,
				int dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup,
				int lpdwTagId, string lpDependencies, string lpServiceStartName,
				string lpPassword, string lpDisplayName);
			
			[DllImport("advapi32", SetLastError = true)]
			static public extern IntPtr CreateService(
				IntPtr hSCManager, string
				serviceName, string displayName,
				uint dwDesiredAccess, uint serviceType, uint startType, uint
				errorControl,
				string lpBinaryPathName, string lpLoadOrderGroup, string lpdwTagId,
				string lpDependencies,
				string lpServiceStartName, string lpPassword);
			#endregion
		}
	}
}
