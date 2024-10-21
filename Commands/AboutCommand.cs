using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell.Interop;

namespace Ixxtension
{
	[Command(PackageIds.AboutCommand)]
	internal sealed class AboutCommand : IxxtensionCommand<AboutCommand>
	{
		protected override void Perform(object sender)
		{
			string message = 
				"Ixxtention\n" +
                "Visual studio extension to toggle between ixx/cpp files\n" +
				"Author: Alexander Tuzhik\n" +
				"alprog@hotmail.com";
			VS.MessageBox.Show("About", message, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK);
		}
	}
}
