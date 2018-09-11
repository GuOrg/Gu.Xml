namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// A cache of <see cref="CastAction{TWriter}"/>
    /// </summary>
    public class CastActions<TWriter>
    {
        private readonly ConcurrentDictionary<Type, CastAction<TWriter>> cache = new ConcurrentDictionary<Type, CastAction<TWriter>>();

        public void RegisterClass<T>(Action<TWriter, T> action)
            where T : class
        {
            this.cache[typeof(T)] = CastAction<TWriter>.Create(action);
        }

        public void RegisterStruct<T>(Action<TWriter, T> action)
            where T : struct
        {
            this.cache[typeof(T)] = CastAction<TWriter>.Create(action);
            if (!this.cache.ContainsKey(typeof(T?)))
            {
                this.cache[typeof(T?)] = CastAction<TWriter>.Create(new Action<TWriter, T?>((writer, value) => action(writer, value.Value)));
            }
        }

        public bool TryGet(Type type, out CastAction<TWriter> castAction)
        {
            return this.cache.TryGetValue(type, out castAction);
        }
    }
}