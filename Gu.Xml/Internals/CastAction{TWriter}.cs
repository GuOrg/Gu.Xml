namespace Gu.Xml
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// A wrapper for Action{TWriter, TValue} that allows getting a casting action for boxed values.
    /// </summary>
    /// <typeparam name="TWriter"></typeparam>
    [DebuggerDisplay("{this.raw}")]
    internal class CastAction<TWriter>
    {
        private readonly Delegate raw;
        private readonly Action<TWriter, object> boxing;

        private CastAction(Delegate raw, Action<TWriter, object> boxing)
        {
            this.raw = raw;
            this.boxing = boxing;
        }

        internal static CastAction<TWriter> Create<T>(Action<TWriter, T> action)
        {
            return new CastAction<TWriter>(
                action,
                (writer, value) => action(writer, (T)value));
        }

        internal Action<TWriter, T> Get<T>()
        {
            if (this.TryGet<T>(out var action))
            {
                return action;
            }

            throw new InvalidOperationException($"Calling CastAction.Get<{typeof(T).Name}> wrong argument types. Expected <{typeof(T).FullName}>.\r\n" +
                                                $"Bug in Gu.Xml.");
        }

        /// <summary>
        /// Try casting the inner action and boxing action  to <see cref="Action{TWriter, T}"/>.
        /// Returns true if any of the casts are successful.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        internal bool TryGet<T>(out Action<TWriter, T> action)
        {
            if (this.raw is Action<TWriter, T> rawMatch)
            {
                action = rawMatch;
                return true;
            }

            if (this.boxing is Action<TWriter, T> boxingMatch)
            {
                action = boxingMatch;
                return true;
            }

            action = null;
            return false;
        }

        internal void Invoke<TValue>(TWriter writer, TValue value)
        {
            if (this.TryGet<TValue>(out var action))
            {
                action.Invoke(writer, value);
            }
            else
            {
                throw new InvalidOperationException($"Calling invoke on a cast action with wrong argument types. Expected <{typeof(TWriter).FullName}, {this.raw.GetType().GenericTypeArguments[1].FullName}> was <{typeof(TWriter).FullName}, {typeof(TValue).FullName}>.\r\n" +
                                                    $"Bug in Gu.Xml.");
            }
        }
    }
}