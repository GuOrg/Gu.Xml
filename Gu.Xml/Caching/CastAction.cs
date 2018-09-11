namespace Gu.Xml
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// A wrapper for Action{TWriter, TValue} that allows getting a casting action for boxed values.
    /// </summary>
    /// <typeparam name="TWriter"></typeparam>
    [DebuggerDisplay("{this.raw}")]
    public class CastAction<TWriter>
    {
        private readonly object raw;
        private readonly Action<TWriter, object> boxedAction;

        private CastAction(object raw, Action<TWriter, object> boxedAction)
        {
            this.raw = raw;
            this.boxedAction = boxedAction;
        }

        public static CastAction<TWriter> Create<T>(Action<TWriter, T> action)
        {
            return new CastAction<TWriter>(
                action,
                (writer, value) => action(writer, (T)value));
        }

        public Action<TWriter, TMember> Raw<TMember>() => (Action<TWriter, TMember>)this.raw;

        public Action<TWriter, object> Boxed() => this.boxedAction;
    }
}