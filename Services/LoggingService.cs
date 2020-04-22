using Model;
using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Services
{
    public class LoggingService
    {
        private static LogWriter LocalLogWriter { get; set; }
        private readonly CadEntities _dbContext;

        public LoggingService(CadEntities myDbContext)
        {
            try
            {
                _dbContext = myDbContext;

                if (LocalLogWriter == null)
                {
                    LocalLogWriter = new LogWriterFactory().Create();
                    Logger.SetLogWriter(LocalLogWriter, false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Write(string text)
        {
            try
            {
                Logger.Write(text);
            }
            catch (Exception) { }
        }

        public void Write(string text, bool bToDatabase)
        {
            Write(text);
            if (bToDatabase)
            {
                AddAudit(text);
            }
        }

        public void WriteWithInner(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    Logger.Write(ex);
                    if (ex.InnerException != null)
                    {
                        Logger.Write(ex.InnerException);
                    }
                }
            }
            catch (Exception) { }
        }

        public void WriteWithInner(Exception ex, bool bToDatabase, String Text)
        {
            WriteWithInner(ex);
            if (bToDatabase)
            {
                AddAudit(Text + " " + ex.Message);
            }
        }

        public void WriteInnerExAndTitle(Exception ex, string title)
        {
            try
            {
                Logger.Write(title);
                if (ex != null)
                {
                    Logger.Write(ex);
                    if (ex.InnerException != null)
                    {
                        Logger.Write(ex.InnerException);
                    }
                }
            }
            catch (Exception) { }
        }

        public void AddAudit(string Text)
        {
            Audit objExecution = new Audit { Date = DateTime.Now, Text = Text };
            _dbContext.Audit.Add(objExecution);
            _dbContext.SaveChanges();
        }
    }
}