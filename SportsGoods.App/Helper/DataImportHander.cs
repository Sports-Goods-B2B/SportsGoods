using SportsGoods.Core.Interfaces;

namespace SportsGoods.App.Helper
{
    public abstract class DataImportHander : IDataImportHander
    {
        private IDataImportHander? _nextHandler;

        public void SetNextHandler(IDataImportHander handler)
        {
            _nextHandler = handler;
        }

        protected abstract bool IsDataImportSuccess();
        protected abstract Task ImportData();

        public async Task HandleDataFromXML()
        {
            if(!IsDataImportSuccess())
            {
                await ImportData();
            }

            if(IsDataImportSuccess() && _nextHandler != null)
            {
                await _nextHandler.HandleDataFromXML();
            }
        }
    }
}
