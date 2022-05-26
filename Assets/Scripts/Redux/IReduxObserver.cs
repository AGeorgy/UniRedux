namespace UniRedux.Redux
{
    public interface IReduxObserver<in T>
    {
        public void Invoke(T value);
        public void ForceInvoke(T value);
    }
}