using System;
using System.Collections.Generic;

namespace UniRedux.Redux
{
    public class ServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new();

        public void Add<TService>(Func<TService> serviceConstructor)
        {
            var key = typeof(TService);
            if (!_services.ContainsKey(key))
            {
                _services.Add(key, new Lazy<TService>(serviceConstructor));
            }
        }

        public TService Get<TService>()
        {
            var key = typeof(TService);
            if (_services.ContainsKey(key))
            {
                var service = (Lazy<TService>) _services[key];
                return service.Value;
            }

            return default;
        }
    }
}