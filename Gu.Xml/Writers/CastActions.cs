namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;

    public class CastActions<TWriter>
    {
        protected readonly ConcurrentDictionary<Type, CastAction<TWriter>> Cache = new ConcurrentDictionary<Type, CastAction<TWriter>>();

        public void RegisterClass<T>(Action<TWriter, T> action)
            where T : class
        {
            this.Cache[typeof(T)] = CastAction<TWriter>.Create(action);
        }

        public void Register<T>(Action<TWriter, T> action)
            where T : struct
        {
            this.Cache[typeof(T)] = CastAction<TWriter>.Create(action);
            if (!this.Cache.ContainsKey(typeof(T?)))
            {
                this.Cache[typeof(T?)] = CastAction<TWriter>.Create(new Action<TWriter, T?>((writer, value) => action(writer, value.Value)));
            }
        }

        public bool TryGet(Type type, out CastAction<TWriter> castAction)
        {
            return this.Cache.TryGetValue(type, out castAction);
        }
    }
}