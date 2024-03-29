﻿using System;

namespace UniRedux.Redux
{
    public interface IAnonymousHandler<in TState, in TAction>
    {
        void Handle(TState value, TAction action);
    }

    internal class AnonymousHandler<TState, TAction> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction> _action;

        public AnonymousHandler(Action<TState, TAction> action)
        {
            _action = action;
        }

        public void Handle(TState state, TAction action)
        {
            _action?.Invoke(state, action);
        }
    }

    internal class AnonymousHandler<TState, TAction, TService> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction, TService> _action;
        private readonly ServiceProvider _serviceProvider;

        public AnonymousHandler(Action<TState, TAction, TService> action, ServiceProvider serviceProvider)
        {
            _action = action;
            _serviceProvider = serviceProvider;
        }

        public void Handle(TState value, TAction action)
        {
            var service = _serviceProvider.Get<TService>();
            if (service != null) _action?.Invoke(value, action, service);
        }
    }

    internal class AnonymousHandler<TState, TAction, TService1, TService2> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction, TService1, TService2> _action;
        private readonly ServiceProvider _serviceProvider;

        public AnonymousHandler(Action<TState, TAction, TService1, TService2> action, ServiceProvider serviceProvider)
        {
            _action = action;
            _serviceProvider = serviceProvider;
        }

        public void Handle(TState value, TAction action)
        {
            var service1 = _serviceProvider.Get<TService1>();
            var service2 = _serviceProvider.Get<TService2>();
            if (service1 != null && service2 != null) _action?.Invoke(value, action, service1, service2);
        }
    }

    internal class AnonymousHandler<TState, TAction, TService1, TService2, TService3> : IAnonymousHandler<TState, TAction>
    {
        private readonly Action<TState, TAction, TService1, TService2, TService3> _action;
        private readonly ServiceProvider _serviceProvider;

        public AnonymousHandler(Action<TState, TAction, TService1, TService2, TService3> action, ServiceProvider serviceProvider)
        {
            _action = action;
            _serviceProvider = serviceProvider;
        }

        public void Handle(TState value, TAction action)
        {
            var service1 = _serviceProvider.Get<TService1>();
            var service2 = _serviceProvider.Get<TService2>();
            var service3 = _serviceProvider.Get<TService3>();
            if (service1 != null && service2 != null && service3 != null) _action?.Invoke(value, action, service1, service2, service3);
        }
    }
}