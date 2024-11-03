
using Community.VisualStudio.Toolkit;
using System;
using Microsoft.VisualStudio.Text;
using System.IO;
using Microsoft.VisualStudio.Shell.Internal.FileEnumerationService;
using System.Collections.Generic;

namespace Ixxtension
{
	[Command(PackageIds.FileToggleCommand)]
	internal sealed class FileToggleCommand : IxxtensionCommand<FileToggleCommand>
	{
        static List<string> headerExtensions = new List<string> { ".ixx", ".h", ".hpp" };
        static List<string> implementationExtensions = new List<string> { ".cpp", ".c" };

        async protected override void Perform(object sender)
		{
			DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();

			if (docView == null)
			{
				// no current document opened
				return;
			}

			var openedFilePath = docView.FilePath;

			var directory = Path.GetDirectoryName(openedFilePath);
			var originalFileName = Path.GetFileNameWithoutExtension(openedFilePath);
			var originalExtension = Path.GetExtension(openedFilePath);

			bool isHeader = headerExtensions.Contains(originalExtension);
			var candidateExtensions = isHeader ? implementationExtensions : headerExtensions;

			foreach (var candidateExtension in candidateExtensions )
			{
				var candidate = Path.Combine(directory, originalFileName + candidateExtension);
                if ( File.Exists( candidate ))
                {
                    await VS.Documents.OpenAsync(candidate);
					return;
                }
            }            
		}

    }
}