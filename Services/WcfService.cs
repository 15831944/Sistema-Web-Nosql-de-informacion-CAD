using Model;
using Services;
using System;
using System.Collections.Generic;
using System.ServiceModel;

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
public class WcfService : IDisposable
{
    public static Action<List<ActionWrapper>> ActionDelegate { get; set; }
    public static ServiceHost serviceHost { get; set; }
    private LoggingService _LoggingService;
    public string SuccesfullCall = "Successful tag process!";

    public WcfService()
    {
    }

    public WcfService(LoggingService myLoggingService)
    {
        _LoggingService = myLoggingService;
    }

    public WcfService(Action<List<ActionWrapper>> _actionDelegate, LoggingService myLoggingService) : base()
    {
        try
        {
            ActionDelegate = _actionDelegate;
            string address = string.Format("net.pipe://localhost/{0}", Constants.BricscadExtensionProcessName);
            NetNamedPipeBinding Binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            serviceHost = new ServiceHost(typeof(WcfService));
            serviceHost.AddServiceEndpoint(typeof(ActionWrapper), Binding, address);
            serviceHost.Open();
        }
        catch (Exception ex)
        {
            _LoggingService.WriteWithInner(ex, true, "Error at WcfService (New)!");
        }

        _LoggingService.Write("WcfService started!", true);
    }

    public ActionWrapper GetClientChanel()
    {
        try
        {
            string address = string.Format("net.pipe://localhost/{0}", Constants.BricscadExtensionProcessName);
            NetNamedPipeBinding Binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);

            return ChannelFactory<ActionWrapper>.CreateChannel(Binding, ep);
        }
        catch (Exception ex)
        {
            _LoggingService.WriteWithInner(ex, true, "Error at WcfService (GetClientChanel)!");
        }

        return null;
    }

    public List<ActionWrapper> Process(List<ActionWrapper> myActionWrapper)
    {
        try
        {
            ActionDelegate?.Invoke(myActionWrapper);            
        }
        catch (Exception ex)
        {
            _LoggingService.WriteWithInner(ex, true, "Error at WcfService (Process)!");
        }

        _LoggingService.Write("WcfService Processed!", true);
        return myActionWrapper;
    }

    public void Dispose()
    {
        serviceHost?.Close();
        ((IDisposable)serviceHost).Dispose();
    }
}