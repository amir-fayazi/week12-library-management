using System;
using System.Collections.Generic;
using System.Text;

namespace Golestan.Core.Extensions
{
    public static class String
    {
        public static bool IsValidText(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
