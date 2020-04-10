using System.Web;

namespace KtpAcs.Infrastructure.Utilities
{
    public static class HtmlHelper
    {
        public static string Encode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return HttpUtility.HtmlEncode(value);
        }
    }
}