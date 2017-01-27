using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;

namespace ExcelExport.Contracts.Events
{
    public class RemovedPropertiesFileEvent : PropertiesFileEventBase
    {
        public RemovedPropertiesFileEvent(IPropertiesFile propertiesFile)
            : base(propertiesFile)
        {
        }
    }
}
