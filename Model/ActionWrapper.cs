using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Model
{
    [ServiceContract(Namespace = "Model")]
    public interface IInteroperability
    {
        [OperationContract]
        List<ActionWrapper> Process(List<ActionWrapper> myWrappedActions);
    }

    [Serializable()]
    public class ActionWrapper 
    {
        public enum TypeEnum
        {
            AddLine,
            AddPolyLine,
            AddLayer,
            AddCircle,
            ReadDwgFile,
            SaveDwgFile,
            CreateTable,
            PerfomranceTest
        }

        public enum StatusEnum
        {
            Server,
            Client,
            Ok,
            Exception,
            DataError
        }

        public TypeEnum Type { get; set; }
        public StatusEnum Status { get; set; }
        public string Error { get; set; }
        public string Parameter { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }

        public ActionWrapper() 
        {
        }
    }
}
