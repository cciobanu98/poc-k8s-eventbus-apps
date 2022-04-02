namespace PoC.Shared
{
    public interface IStore
    {
        void Add(string item);

        void Clear();

        IList<string> GetAll();
    }
}
