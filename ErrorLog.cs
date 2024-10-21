
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.Generic;

namespace Ixxtension
{
	public static class ErrorLog
	{
		private static List<string> Errors = new List<string>();
		private static List<string> Warnings = new List<string>();

		public static void Error(string message)
		{
			Errors.Add(message);
		}

		public static void Warning(string message)
		{
			Warnings.Add(message);
		}

		public static bool Flush()
		{
			OLEMSGICON icon = OLEMSGICON.OLEMSGICON_INFO;

			string message = string.Empty;

			if (Errors.Count > 0)
			{
				icon = Min(OLEMSGICON.OLEMSGICON_CRITICAL, icon);
				message += "Errors:\r\n" + string.Join("\r\n", Errors);
				Errors.Clear();
			}

			if (Warnings.Count > 0)
			{
				icon = Min(OLEMSGICON.OLEMSGICON_WARNING, icon);
				if (message != string.Empty)
				{
					message += "\r\n\r\n";
				}
				message += "Warnings:\r\n" + string.Join("\r\n", Warnings);
				Warnings.Clear();
			}

			if (message != string.Empty)
			{
				VS.MessageBox.Show("Ixxtension", message, icon, OLEMSGBUTTON.OLEMSGBUTTON_OK);
				return true;
			}
			return false;
		}

		public static void InformOrWarn(string successMessage)
		{
			if (!Flush())
			{
				VS.MessageBox.Show("Ixxtension", successMessage, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK);
			}
		}

		private static OLEMSGICON Min(OLEMSGICON a, OLEMSGICON b)
		{
			return a < b ? a : b;
		}

		//--------------------------

		public static void Error(string format, object arg)
		{
			Error(string.Format(format, arg));
		}

		public static void Error(string format, object arg1, object arg2)
		{
			Error(string.Format(format, arg1, arg2));
		}

		public static void Warning(string format, object arg)
		{
			Warning(string.Format(format, arg));
		}

		public static void Warning(string format, object arg1, object arg2)
		{
			Warning(string.Format(format, arg1, arg2));
		}
	}
}
