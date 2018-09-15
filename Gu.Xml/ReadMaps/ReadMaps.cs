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

        internal bool TryGet(Type type, out ReadMap map)
        {
            map = this.maps.GetOrAdd(type, x => Create(x));
            return true;

            ReadMap Create(Type t)
            {
                // ReSharper disable once PossibleNullReferenceException
                return (ReadMap)typeof(ReadMap).GetMethod(nameof(ReadMap.CreateComplex))
                                               .MakeGenericMethod(t)
                                               .Invoke(null, new object[] { this });
            }
        }

        internal ReadMaps RegisterSimple<T>(Func<string, T> parse)
        {
            this.maps[typeof(T)] = ReadMap.CreateSimple(parse);
            return this;
        }
    }
}