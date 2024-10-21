using Community.VisualStudio.Toolkit;
using System;

namespace Ixxtension
{
	abstract class IxxtensionCommand<T> : BaseCommand<T> where T : class, new()
	{
		abstract protected void Perform(object sender);

		protected override void Execute(object sender, EventArgs e)
		{
			try
			{
				Perform(sender);
			}
			catch (Exception ex)
			{
				ErrorLog.Error(ex.Message);
			}
			ErrorLog.Flush();
		}
	}
}
