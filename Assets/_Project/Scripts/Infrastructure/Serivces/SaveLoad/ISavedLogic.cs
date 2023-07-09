namespace Subway.Infrastructure.Serivces.SaveLoad
{

    // SOLID -- I - interface segregation

    public interface ISavedLogic
    {
        void Save();
    }

    public interface ISavedLoadLogic : ISavedLogic
    {
        void Load();
    }
}