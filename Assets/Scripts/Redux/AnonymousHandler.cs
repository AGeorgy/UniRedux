using System;

namespace UniRedux.Redux
{
    public interface IAnonymousHandler<in TState, in TAction>
    {
        void Handle(TState value, TAction action);
    }
    
    internal class AnonymousHandler<TState, TAction> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction> _action;

        public AnonymousHandler(Action<TState ,TAction> action)
        {
            _action = action;
        }

        public void Handle(TState state, TAction action)
        {
            _action.Invoke(state, action);
        }
    }

    internal class AnonymousHandler<TState, TAction, TService> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction, TService> _action;
        private readonly TService _service;

        public AnonymousHandler(Action<TState, TAction, TService> action, TService service)
        {
            _action = action;
            _service = service;
        }
        
        public void Handle(TState value, TAction action)
        {
            _action.Invoke(value, action, _service);
        }
    }

    internal class AnonymousHandler<TState, TAction, TService1, TService2> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction, TService1, TService2> _action;
        private readonly TService1 _service1;
        private readonly TService2 _service2;

        public AnonymousHandler(Action<TState, TAction, TService1, TService2> action, TService1 service1, TService2 service2)
        {
            _action = action;
            _service1 = service1;
            _service2 = service2;
        }
        
        public void Handle(TState value, TAction action)
        {
            _action.Invoke(value, action, _service1, _service2);
        }
    }
}