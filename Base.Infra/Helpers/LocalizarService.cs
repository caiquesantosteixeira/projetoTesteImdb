using System;
using System.Collections.Generic;

namespace Base.Repository.Helpers
{
    public static class LocalizarService
    {
        private static IDictionary<string, Object> services = new Dictionary<string, Object>();

        public static T Get<T>(string id) => (T)services[id];

        public static bool Has(string id) => services.ContainsKey(id);

        public static void Registrar<T>(string id, T service) =>
            services.Add(new KeyValuePair<string, Object>(id, service));
    }
}
