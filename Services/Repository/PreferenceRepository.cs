using System.Linq;
using Model;
using Services;

public class PreferenceRepository
{
    #region Variables and Properties

    public CadEntities _dataContext { get; set; }
    public LoggingService _loggingService { get; set; }

    #endregion

    #region Constructor

    public PreferenceRepository(CadEntities myDbContext, LoggingService myLoggingService)
    {
        _dataContext = myDbContext;
        _loggingService = myLoggingService;
    }

    #endregion

    #region Public Methods   

    public void LoadBasePreferences(ViewModelBase vmBase)
    {
        vmBase.ProcessContactEmail = GetPreferenceValue(Constants.ContactEmail);
        vmBase.ProccessContactMailSubject = GetPreferenceValue(Constants.ContactSubject);
        vmBase.ProcessContactName = GetPreferenceValue(Constants.ContactName);
    }

    public string GetPreferenceValue(string key)
    {
        return _dataContext.Preference.Where(item => item.Name.Equals(key)).FirstOrDefault().Value;
    }

    #endregion
}