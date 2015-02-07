using System;
using System.Linq;
using System.Reflection;

namespace DataAnalysis.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString())
                                            .FirstOrDefault();

            if (memberInfo == null) 
                return null;

            var attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return attribute;
        }
    }
}
