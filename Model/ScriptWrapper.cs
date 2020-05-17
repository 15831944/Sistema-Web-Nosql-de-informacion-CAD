using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Model
{
    public class ScriptWrapper : Script
    {
        public string ActionAggregate { get; set; }

        public ScriptWrapper() : base()
        {
        }
    }
}
