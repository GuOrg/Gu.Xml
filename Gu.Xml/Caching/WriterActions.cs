﻿namespace Gu.Xml
{
    using System;
    using System.IO;
    using System.Reflection;

    public class WriterActions
    {
        private readonly CastActions<TextWriter> simpleActions = new CastActions<TextWriter>();

        public bool TryGetSimple<TMember>(TMember value, out Action<TextWriter, TMember> writer)
        {
            return this.TryGetSimple(value?.GetType() ?? typeof(TMember), out writer);
        }

        public WriterActions SimpleClass<T>(Action<TextWriter, T> action)
            where T : class
        {
            this.simpleActions.RegisterClass(action);
            return this;
        }

        public WriterActions SimpleStruct<T>(Action<TextWriter, T> action)
            where T : struct
        {
            this.simpleActions.RegisterStruct(action);
            return this;
        }

        /// <summary>
        /// Try getting a writer for writing the element contents as a string.
        /// </summary>
        /// <typeparam name="TMember">
        /// The type of the member.
        /// If an int is stored in a property of type object <typeparamref name="TMember"/> will be <see cref="object"/> and <paramref name="type"/> will be <see cref="int"/>
        /// </typeparam>
        /// <param name="type">The type of the value.</param>
        /// <param name="writer"></param>
        /// <returns>True if a writer was found for <paramref name="type"/></returns>
        private bool TryGetSimple<TMember>(Type type, out Action<TextWriter, TMember> writer)
        {
            if (this.simpleActions.TryGet(type, out var castAction))
            {
                if (castAction.TryGet<TMember>(out writer))
                {
                    return true;
                }

                return this.TryGetSimple(typeof(TMember), out writer);
            }

            if (type.IsEnum)
            {
                // ReSharper disable once PossibleNullReferenceException
                _ = typeof(CastActionsExt).GetMethod(nameof(CastActionsExt.RegisterEnum), BindingFlags.Public | BindingFlags.Static)
                                             .MakeGenericMethod(type)
                                             .Invoke(null, new[] { this.simpleActions });
                return this.TryGetSimple(typeof(TMember), out writer);
            }

            writer = null;
            return false;
        }
    }
}