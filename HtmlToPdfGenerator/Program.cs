using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace HtmlToPdfGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ValidateArguments(args))
            {
                string _baseUrl = args[0];
                string _viewsPath = args[1];
                string _basePdfPath = args[2];
                string _pdfHtmlToPdfExePath = args[3];
                string _wkHtmlToPdfParams = args[4];

                var urls = GetUrls(_viewsPath);
                foreach (var url in urls)
                {
                    Console.WriteLine(string.Format("Converting {0}", _baseUrl + url));
                    string pdfFileName = string.Format(@"{0}\{1}.pdf", _basePdfPath, url.Replace(@"/", @"\"));

                    if (!Directory.Exists(Path.GetDirectoryName(pdfFileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(pdfFileName));

                    HtmlToPdf(pdfFileName, string.Format("{0}{1}?format=PDF", _baseUrl, url), _pdfHtmlToPdfExePath, _wkHtmlToPdfParams);
                }
                
                Environment.ExitCode = 0;
            }
            else
            {
                Environment.ExitCode = 1;
            }
        }

        private static bool ValidateArguments(string[] args)
        {
            if(args.Length!=5)
            {
                ShowArgumentError(string.Empty);
                return false;
            }
            if(!Uri.IsWellFormedUriString(args[0], UriKind.Absolute))
            {
                ShowArgumentError("Error: Invalid [BaseUrl]");
                return false;
            }
            if(!Directory.Exists(args[1]))
            {
                ShowArgumentError("Error: [ViewsPath] folder does not exist");
                return false;
            }
            if (!Directory.Exists(args[2]))
            {
                ShowArgumentError("Error: [PdfPath] folder does not exist");
                return false;
            }
            if (!File.Exists(args[3]))
            {
                ShowArgumentError("Error: [PdfLibraryExe] file was not found");
                return false;
            }

            return true;
        }

        private static void ShowArgumentError(string error)
        {
            Console.WriteLine("Usage: HtmlToPdfGenerator [BaseUrl] [ViewsPath] [PdfPath] [PdfLibraryExe]");
            if(error!=string.Empty)
            {
                Console.WriteLine("Error: " + error);
            }
        }

        private static List<string> GetUrls(string viewsPath)
        {
            var result = new List<string>();

            foreach (var file in Directory.GetFiles(viewsPath, "*.cshtml", SearchOption.AllDirectories))
            {
                if (!Path.GetFileName(file).StartsWith("_"))
                {
                    string[] subfolders = Path.GetDirectoryName(file).Replace(viewsPath, string.Empty).Split('\\');
                    var url = string.Format("{0}/{1}", subfolders[1], Path.GetFileNameWithoutExtension(file));
                    result.Add(url);
                }
            }                       
            return result;
        }

        private static void HtmlToPdf(string pdfFileName, string url, string pdfHtmlToPdfExePath, string wkHtmlToPdfParams)
        {
            string urlsSeparatedBySpaces = string.Empty;
            try
            {
                if ((url == null)||(url == string.Empty))
                {
                    throw new Exception("No input URLs provided for HtmlToPdf");
                }

                var p = new System.Diagnostics.Process()
                {
                    StartInfo =
                    {
                        FileName = pdfHtmlToPdfExePath,
                        Arguments = string.Format("{0} {1} {2}",wkHtmlToPdfParams, url, pdfFileName),
                        UseShellExecute = false, 
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true, 
                        WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                    }
                };

                p.Start();

                var output = p.StandardOutput.ReadToEnd();
                var errorOutput = p.StandardError.ReadToEnd();

                int returnCode = p.ExitCode;
                p.Close();

                if ((returnCode != 0) && (returnCode != 2))
                    throw new Exception(errorOutput);
            }
            catch (Exception exc)
            {
                Console.Write(String.Format("Error:{0} ",exc.Message));
                //throw new Exception(" Error generating PDF, URL: " + url , exc);
            }
        }
    }



}
