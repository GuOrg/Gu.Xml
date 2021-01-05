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

        internal static bool TryGetAttributeName(FieldOrProperty member, out string name)
        {
            if (TryGetNameFromAttribute<System.Xml.Serialization.XmlAttributeAttribute>(member.MemberInfo, x => x.AttributeName, out name) ||
                TryGetNameFromAttribute<System.Xml.Serialization.SoapAttributeAttribute>(member.MemberInfo, x => x.AttributeName, out name))
            {
                if (name == string.Empty)
                {
                    name = member.MemberInfo.Name;
                }

                return true;
            }

            return false;
        }

        internal static bool TryGetElementName(FieldOrProperty member, out string name)
        {
            if (TryGetNameFromAttribute<System.Xml.Serialization.XmlElementAttribute>(member.MemberInfo, x => x.ElementName, out name) ||
                TryGetNameFromAttribute<System.Xml.Serialization.SoapElementAttribute>(member.MemberInfo, x => x.ElementName, out name) ||
                TryGetNameFromAttribute<System.Xml.Serialization.XmlArrayAttribute>(member.MemberInfo, x => x.ElementName, out name) ||
                TryGetNameFromAttribute<System.Runtime.Serialization.DataMemberAttribute>(member.MemberInfo, x => x.Name, out name))
            {
                return true;
            }

            if (name is null)
            {
                if (member.MemberInfo.TryGetCustomAttribute<System.Xml.Serialization.XmlIgnoreAttribute>(out _) ||
                    member.MemberInfo.TryGetCustomAttribute<System.Xml.Serialization.XmlAttributeAttribute>(out _) ||
                    member.MemberInfo.TryGetCustomAttribute<System.Xml.Serialization.SoapIgnoreAttribute>(out _) ||
                    member.MemberInfo.TryGetCustomAttribute<System.Xml.Serialization.SoapAttributeAttribute>(out _) ||
                    member.MemberInfo.TryGetCustomAttribute<System.Runtime.Serialization.IgnoreDataMemberAttribute>(out _))
                {
                    return false;
                }

                name = member.MemberInfo.Name;
            }
            else if (name == string.Empty)
            {
                name = member.MemberInfo.Name;
            }

            if (name.TryLastIndexOf('.', out var i))
            {
                name = name.Substring(i + 1);
            }

            return name.Length > 0;
        }

        internal string AttributeName()
        {
            if (TryGetAttributeName(this, out var name))
            {
                return name;
            }

            throw new InvalidOperationException($"Member {this.MemberInfo} does not map to an XML attribute.");
        }

        internal string ElementName()
        {
            if (TryGetElementName(this, out var name))
            {
                return name;
            }

            throw new InvalidOperationException($"Member {this.MemberInfo} does not map to an XML element.");
        }

        internal Delegate CreateGetter()
        {
            switch (this.MemberInfo)
            {
                case FieldInfo field:
                    return field.CreateGetter();
                case PropertyInfo property:
                    return property.CreateGetter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(this.MemberInfo), "Never getting here. Bug in Gu.Xml.");
            }
        }

        internal bool TryCreateSetter(out Delegate setter)
        {
            switch (this.MemberInfo)
            {
                case FieldInfo field:
                    return field.TryCreateSetter(out setter);
                case PropertyInfo property:
                    return property.TryCreateSetter(out setter);
                default:
                    throw new ArgumentOutOfRangeException(nameof(this.MemberInfo), "Never getting here. Bug in Gu.Xml.");
            }
        }

        private static bool TryGetNameFromAttribute<TAttribute>(MemberInfo member, Func<TAttribute, string> getName, out string name)
            where TAttribute : Attribute
        {
            if (member.TryGetCustomAttribute(out TAttribute attribute))
            {
                name = getName(attribute);
                if (string.IsNullOrEmpty(name))
                {
                    name = member.Name;
                }

                return true;
            }

            name = null;
            return false;
        }
    }
}
