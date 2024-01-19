using System.Reflection;
using Trade_Test.Models.Enums;
using Trade_Test.Utilities.Attributes;


namespace Trade_Test.Utilities.Extensions
{
    public static class ContentTypeExtensions
    {
        private static object GetMetadata(ContentTypes ct)
        {
            var type = ct.GetType();
            MemberInfo[] info = type.GetMember(ct.ToString());

            if ((info != null) && (info.Length > 0))
            {
                object[] attrs = info[0].GetCustomAttributes(typeof(Metadata), false);

                if ((attrs != null) && (attrs.Length > 0))
                {
                    return attrs[0];
                }
            }

            return null;
        }

        public static string ToValue(this ContentTypes ct)
        {
            var metadata = GetMetadata(ct);
            return (metadata != null) ? ((Metadata)metadata).Value : ct.ToString();
        }

        public static bool IsText(this ContentTypes ct)
        {
            var metadata = GetMetadata(ct);
            return (metadata != null) ? ((Metadata)metadata).IsText : true;
        }

        public static bool IsBinary(this ContentTypes ct)
        {
            var metadata = GetMetadata(ct);
            return (metadata != null) ? ((Metadata)metadata).IsBinary : false;
        }
    }
}
