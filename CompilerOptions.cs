
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.VCProjectEngine;

namespace Ixxtension
{
	public static class CompilerOptions
	{
		public static void SyncFileConfigurations(EnvDTE.ProjectItem dstItem, EnvDTE.ProjectItem srcItem)
		{
			var dstCollections = GetFileConfigurations(dstItem);
			var srcCollections = GetFileConfigurations(srcItem);			
			SyncFileConfigurations(dstCollections, srcCollections);
		}

		public static IVCCollection GetFileConfigurations(EnvDTE.ProjectItem item)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return item.Properties.Item("FileConfigurations").Object as IVCCollection;
		}

		public static void SyncFileConfigurations(IVCCollection dstCollection, IVCCollection srcCollection)
		{
			for (int i = 1; i <= srcCollection.Count; i++)
			{
				var srcConfiguration = srcCollection.Item(i) as VCFileConfiguration;
				for (int j = 1; j <= dstCollection.Count; j++)
				{
					var dstConfiguration = dstCollection.Item(j) as VCFileConfiguration;
					if (dstConfiguration.Name == srcConfiguration.Name)
					{
						SyncFileConfiguration(dstConfiguration, srcConfiguration);
						break;
					}
				}
			}
		}

		public static void SyncFileConfiguration(VCFileConfiguration dstConfiguration, VCFileConfiguration srcConfiguration)
		{
			var src = srcConfiguration.Tool as VCCLCompilerTool;
			var dst = dstConfiguration.Tool as VCCLCompilerTool;
			if (src != null && dst != null)
			{
				dst.ForcedIncludeFiles = src.ForcedIncludeFiles;
			}
		}
	}
}