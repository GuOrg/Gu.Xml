namespace Gu.Xml
{
    using System;
    using System.Reflection;

    internal struct FieldOrProperty
    {
        private readonly MemberInfo member;

        public FieldOrProperty(FieldInfo member)
        {
            this.member = member;
        }

        public FieldOrProperty(PropertyInfo member)
        {
            this.member = member;
        }

        public Type SourceType => this.member.ReflectedType;

        public Type ValueType
        {
            get
            {
                switch (this.member)
                {
                    case FieldInfo field:
                        return field.FieldType;
                    case PropertyInfo property:
                        return property.PropertyType;
                    default:
                        throw new ArgumentOutOfRangeException("Never getting here.");
                }
            }
        }

        public Delegate CreateGetter()
        {
            switch (this.member)
            {
                case FieldInfo field:
                    return field.CreateGetter();
                case PropertyInfo property:
                    return property.CreateGetter();
                default:
                    throw new ArgumentOutOfRangeException("Never getting here.");
            }
        }
    }
}
