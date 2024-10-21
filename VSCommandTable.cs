
using System;

namespace Ixxtension
{
	/// <summary>
	/// Helper class that exposes all GUIDs used across VS Package.
	/// </summary>
	internal sealed partial class PackageGuids
	{
		public const string IxxtensionString = "4066f664-73f0-49d5-8bc7-f1078efffe7c";
		public static Guid Ixxtension = new Guid(IxxtensionString);
	}
	/// <summary>
	/// Helper class that encapsulates all CommandIDs uses across VS Package.
	/// </summary>
	internal sealed partial class PackageIds
	{
		public const int ExtensionsGroup = 101;
		public const int SettingsMenu = 102;
		public const int SettingsGroup = 103;
		public const int AboutCommand = 104;
		public const int FileToggleCommand = 105;
	}
}