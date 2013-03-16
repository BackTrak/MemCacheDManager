using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Permissions;
using System.IO;

namespace MemCacheDManager
{
	


	public class ImpersonateUser
	{
		private enum SECURITY_IMPERSONATION_LEVEL : int
		{
			SecurityAnonymous = 0,
			SecurityIdentification = 1,
			SecurityImpersonation = 2,
			SecurityDelegation = 3
		}

		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool LogonUser(
			String lpszUsername,
			String lpszDomain,
			String lpszPassword,
			int dwLogonType,
			int dwLogonProvider,
			ref IntPtr phToken);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public extern static bool CloseHandle(IntPtr handle);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public extern static bool DuplicateToken(
			IntPtr ExistingTokenHandle,
			int SECURITY_IMPERSONATION_LEVEL,
			ref IntPtr DuplicateTokenHandle);

		private bool _isImpersonating = false;
		public bool IsImpersonating
		{
			get { return _isImpersonating; }
		}


		private IntPtr _tokenHandle = new IntPtr(0);
		private IntPtr _duplicateTokenHandle = new IntPtr(0);
		private WindowsImpersonationContext _impersonatedUser;

		// If you incorporate this code into a DLL, be sure to demand that it
		// runs with FullTrust.
		[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
		public ImpersonateUser(string userAndDomanName, string password)
		{
			string userName = string.Empty;
			string domainName = string.Empty;

			if (userAndDomanName.Contains("\\") == true)
			{
				domainName = userAndDomanName.Split('\\')[0];
				userName = userAndDomanName.Split('\\')[1];
			}
			else
			{
				userName = userAndDomanName;
			}

			// Use the unmanaged LogonUser function to get the user token for
			// the specified user, domain, and password.
			// To impersonate a user on this machine, use the local machine
			// name for the domain name.
			const int LOGON32_PROVIDER_DEFAULT = 0;
			// Passing this parameter causes LogonUser to create a primary
			// token.
			const int LOGON32_LOGON_INTERACTIVE = 2;
			_tokenHandle = IntPtr.Zero;
			// Call  LogonUser to obtain a handle to an access token.
			bool returnValue = LogonUser(
				userName,
				domainName,
				password,
				LOGON32_LOGON_INTERACTIVE,
				LOGON32_PROVIDER_DEFAULT,
				ref _tokenHandle);

			if (false == returnValue)
			{
				int ret = Marshal.GetLastWin32Error();
				throw new System.ComponentModel.Win32Exception(ret);
			}

			_duplicateTokenHandle = IntPtr.Zero;
			returnValue = DuplicateToken(_tokenHandle,
			(int)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation,
				ref _duplicateTokenHandle);

			if (false == returnValue)
			{
				int ret = Marshal.GetLastWin32Error();
				throw new System.ComponentModel.Win32Exception(ret);
			}

			WindowsIdentity newId = new WindowsIdentity(_duplicateTokenHandle);
			_impersonatedUser = newId.Impersonate();
			_isImpersonating = true;
		}

		public void Undo()
		{
			_impersonatedUser.Undo();

			// Free the tokens.
			if (_tokenHandle != IntPtr.Zero)
				CloseHandle(_tokenHandle);

			if (_duplicateTokenHandle != IntPtr.Zero)
				CloseHandle(_duplicateTokenHandle);

			_isImpersonating = false;
		}
	}

}
