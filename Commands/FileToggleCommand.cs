
using Community.VisualStudio.Toolkit;

namespace Ixxtension
{
	[Command(PackageIds.FileToggleCommand)]
	internal sealed class FileToggleCommand : IxxtensionCommand<FileToggleCommand>
	{
		protected override void Perform(object sender)
		{
			VS.MessageBox.Show("Not implemented");
		}
	}
}
