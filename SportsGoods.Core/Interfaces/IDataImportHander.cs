namespace SportsGoods.Core.Interfaces
{
    public interface IDataImportHander
    {
        void SetNextHandler(IDataImportHander handler);
        Task HandleDataFromXML();
    }
}
