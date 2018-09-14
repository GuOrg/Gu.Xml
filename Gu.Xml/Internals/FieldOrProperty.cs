namespace Gu.Xml
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    [DebuggerDisplay("{this.SourceType}{this.MemberInfo.Name} ({this.ValueType})")]
    internal struct FieldOrProperty
    {
        internal readonly MemberInfo MemberInfo;

        internal FieldOrProperty(FieldInfo memberInfo)
        {
            this.MemberInfo = memberInfo;
        }

        internal FieldOrProperty(PropertyInfo memberInfo)
        {
            this.MemberInfo = memberInfo;
        }

        internal Type SourceType => this.MemberInfo.ReflectedType;

        internal Type ValueType
        {
            get
            {
                switch (this.MemberInfo)
                {
                    case FieldInfo field:
                        return field.FieldType;
                    case PropertyInfo property:
                        return property.PropertyType;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(this.MemberInfo), "Never getting here.");
                }
            }
        }

        public Delegate CreateGetter()
        {
            switch (this.MemberInfo)
            {
                case FieldInfo field:
                    return field.CreateGetter();
                case PropertyInfo property:
                    return property.CreateGetter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(this.MemberInfo), "Never getting here.");
            }
        }
    }
}
