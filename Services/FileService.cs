using Model;
using System;
using System.IO;

namespace Services
{
    public class FileService
    {
        private readonly LoggingService _LoggingService;

        public FileService(LoggingService myLoggingService)
        {
            _LoggingService = myLoggingService;
        }

        public void Save(string filename, byte[] fileContent)
        {
            try
            {
                string fullPath = GetPath(filename);
                string dirName = Path.GetDirectoryName(fullPath);

                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);

                File.WriteAllBytes(fullPath, fileContent);
            }
            catch (Exception ex)
            {
                _LoggingService.Write(ex.Message, true);
                throw;
            }
        }

        public byte[] ReadFromFile(string filename)
        {
            try
            {
                return File.ReadAllBytes(GetPath(filename));
            }
            catch (Exception ex)
            {
                _LoggingService.Write(ex.Message, true);
                throw;
            }
        }

        public string GetPath(string fileName)
        {
            try
            {
                return string.Format("{0}{1}.{2}", Constants.TempDirectory, fileName, Constants.DwgFileExtension);
            }
            catch (Exception ex)
            {
                _LoggingService.Write(ex.Message, true);
                throw;
            }
        }
    }
}