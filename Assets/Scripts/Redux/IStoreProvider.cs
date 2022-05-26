namespace UniRedux.Redux
{
    public interface IStoreProvider
    {
        IDispatcherSelector<TStoreState> GetStore<TStoreState>();
    }
}