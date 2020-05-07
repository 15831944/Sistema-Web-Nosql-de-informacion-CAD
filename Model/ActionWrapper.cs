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
    public class ActionWrapper : Action
    {
        public enum TypeEnum
        {
            AddLine,
            AddPolyLine,
            AddLayer,
            AddCircle,
            ReadDwgFile,
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

        public ActionWrapper() : base()
        {
        }
    }
}
