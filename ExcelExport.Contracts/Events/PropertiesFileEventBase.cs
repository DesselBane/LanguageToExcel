using ExcelExport.Contracts.Model;

namespace ExcelExport.Contracts.Events
{
    public abstract class PropertiesFileEventBase
    {
        #region Vars

        private IPropertiesFile _propertiesFile;

        #endregion

        #region Properties

        public IPropertiesFile PropertiesFile => _propertiesFile;

        #endregion

        #region Constructors

        protected PropertiesFileEventBase(IPropertiesFile propertiesFile)
        {
            _propertiesFile = propertiesFile;
        }

        #endregion
    }
}