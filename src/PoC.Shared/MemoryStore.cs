namespace PoC.Shared
{
    public class MemoryStore : IStore
    {
        private readonly IList<string> store = new List<string>();
        public void Add(string item)
        {
            store.Add(item);
        }

        public void Clear()
        {
            store.Clear();
        }

        public IList<string> GetAll()
        {
            return store;
        }
    }
}
