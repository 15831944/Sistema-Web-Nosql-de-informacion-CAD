using Model;
using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Services
{
    public class LoggingService
    {
        private static LogWriter LocalLogWriter { get; set; }
        private CadEntities _dbContext;
        private bool _onlyDB;

        public LoggingService(CadEntities myDbContext, bool onlydb)
        {
            _onlyDB = onlydb;
            Initialize(myDbContext);
        }

        public LoggingService(CadEntities myDbContext)
        {
            Initialize(myDbContext);
        }

        public void Initialize(CadEntities myDbContext)
        {
            try
            {
                _dbContext = myDbContext;

                if (LocalLogWriter == null && !_onlyDB)
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

        public void Write(string text)
        {
            try
            {
                if (!_onlyDB)
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
                    Write(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Write(ex.InnerException.Message);
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
                Write(title);
                if (ex != null)
                {
                    Write(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Write(ex.InnerException.Message);
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