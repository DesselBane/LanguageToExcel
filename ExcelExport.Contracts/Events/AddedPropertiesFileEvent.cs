using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;

namespace ExcelExport.Contracts.Events
{
    public class AddedPropertiesFileEvent : PropertiesFileEventBase
    {
        public AddedPropertiesFileEvent(IPropertiesFile propertiesFile)
            : base(propertiesFile)
        {
        }
    }
}
