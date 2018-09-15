namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;

    internal class ReadMaps
    {
        private readonly ConcurrentDictionary<Type, ReadMap> maps = new ConcurrentDictionary<Type, ReadMap>();

        internal bool TryGet<T>(out ReadMap map)
        {
            map = this.maps.GetOrAdd(typeof(T), _ => ReadMap.CreateComplex<T>(this));
            return true;
        }

        internal ReadMaps RegisterSimple<T>(Func<string, T> parse)
        {
            this.maps[typeof(T)] = ReadMap.CreateSimple(parse);
            return this;
        }
    }
}