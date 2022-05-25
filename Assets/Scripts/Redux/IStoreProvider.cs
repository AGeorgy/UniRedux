namespace Redux
{
    public interface IStoreProvider
    {
        IDispatcherSelector<TStoreState> GetStore<TStoreState>();
    }
}