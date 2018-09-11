namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// A cache of <see cref="CastAction{TextWriter}"/>
    /// </summary>
    public class CastActions<TWriter>
    {
        protected readonly ConcurrentDictionary<Type, CastAction<TWriter>> Cache = new ConcurrentDictionary<Type, CastAction<TWriter>>();

        public void RegisterClass<T>(Action<TWriter, T> action)
            where T : class
        {
            this.Cache[typeof(T)] = CastAction<TWriter>.Create(action);
        }

        public void RegisterStruct<T>(Action<TWriter, T> action)
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