using System.Collections;
using System.Reflection;

namespace ADO.NetDemoConsoleApp
{
    public static class ConsolePainter
    {
        public static void Write(
            string text,
            ConsoleColor? foreground = null,
            ConsoleColor? background = null)
        {
            var originalForeground = Console.ForegroundColor;
            var originalBackground = Console.BackgroundColor;

            if (foreground.HasValue)
                Console.ForegroundColor = foreground.Value;

            if (background.HasValue)
                Console.BackgroundColor = background.Value;

            Console.Write(text);

            Console.ForegroundColor = originalForeground;
            Console.BackgroundColor = originalBackground;
        }

        public static void WriteLine(
            string text,
            ConsoleColor? foreground = null,
            ConsoleColor? background = null)
        {
            Write(text, foreground, background);
            Console.WriteLine();
        }

        public static void WriteTable(
            IEnumerable items,
            ConsoleColor? headerColor = null,
            ConsoleColor? rowColor = null)
        {
            var headerClr = headerColor ?? ConsoleColor.White;
            var rowClr = rowColor ?? ConsoleColor.White;

            var itemList = items
                .Cast<object?>()
                .Where(x => x != null)
                .ToList();

            if (!itemList.Any())
            {
                Console.WriteLine("(no data)");
                return;
            }

            var firstNonNull = itemList.First();
            var itemType = firstNonNull.GetType();

            WriteLine($"{itemType.Name}s : ");

            if (IsSimpleType(itemType))
            {
                string header = "Value";

                int maxLen = Math.Max(
                    header.Length,
                    itemList.Max(x => FormatValue(x).Length));

                string divider =
                    "+" + new string('-', maxLen + 2) + "+";

                WriteLine(divider, headerClr);
                WriteLine(
                    "| " + header.PadRight(maxLen) + " |",
                    headerClr);
                WriteLine(divider, headerClr);

                foreach (var item in itemList)
                {
                    WriteLine(
                        "| " + FormatValue(item).PadRight(maxLen) + " |",
                        rowClr);

                    WriteLine(divider, headerClr);
                }

                return;
            }

            var props = GetOrderedPropertiesByInheritance(itemType);
            var headers = props.Select(p => p.Name).ToArray();

            var rows = itemList
                .Select(item =>
                {
                    return props
                        .Select(p =>
                        {
                            try
                            {
                                var value = p.GetValue(item);
                                return FormatValue(value);
                            }
                            catch
                            {
                                return "";
                            }
                        })
                        .ToArray();
                })
                .ToList();

            int colCount = headers.Length;
            int[] maxWidths = new int[colCount];

            for (int i = 0; i < colCount; i++)
            {
                int headerWidth = headers[i].Length;

                int maxCell = rows
                    .Select(r => r[i]?.Length ?? 0)
                    .DefaultIfEmpty(0)
                    .Max();

                maxWidths[i] = Math.Max(
                    headerWidth,
                    maxCell);
            }

            string tableDivider =
                "+" +
                string.Join(
                    "+",
                    maxWidths.Select(
                        w => new string('-', w + 2))) +
                "+";

            void WriteRow(
                IEnumerable<string> cols,
                ConsoleColor? fg)
            {
                var cells = cols.Select(
                    (col, idx) =>
                        " " +
                        col.PadRight(maxWidths[idx]) +
                        " ");

                Write(
                    "|" + string.Join("|", cells) + "|",
                    fg);

                Console.WriteLine();
            }

            WriteLine(tableDivider, headerClr);
            WriteRow(headers, headerClr);
            WriteLine(tableDivider, headerClr);

            foreach (var row in rows)
            {
                WriteRow(row, rowClr);
                WriteLine(tableDivider, headerClr);
            }
        }

        private static string FormatValue(object? value)
        {
            if (value == null)
                return "";

            var valueType = value.GetType();

            if (IsSimpleType(valueType))
                return value.ToString() ?? "";

            if (value is IEnumerable collection &&
                value is not string)
            {
                var values = new List<string>();

                foreach (var item in collection)
                {
                    values.Add(FormatCollectionItem(item));
                }

                return string.Join(", ", values);
            }

            return FormatComplexObject(value);
        }

        private static string FormatCollectionItem(object? item)
        {
            if (item == null)
                return "null";

            var itemType = item.GetType();

            if (IsSimpleType(itemType))
                return item.ToString() ?? "";

            return FormatComplexObject(item);
        }

        private static string FormatComplexObject(object value)
        {
            var type = value.GetType();

            var props = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance);

            if (props.Length == 0)
                return value.ToString() ?? "";

            var parts = new List<string>();

            foreach (var prop in props)
            {
                try
                {
                    var propValue = prop.GetValue(value);

                    string formattedValue;

                    if (propValue is IEnumerable enumerable &&
                        propValue is not string)
                    {
                        var innerValues = new List<string>();

                        foreach (var innerItem in enumerable)
                        {
                            innerValues.Add(
                                FormatCollectionItem(innerItem));
                        }

                        formattedValue =
                            "[" +
                            string.Join(", ", innerValues) +
                            "]";
                    }
                    else
                    {
                        formattedValue =
                            propValue?.ToString() ?? "";
                    }

                    parts.Add(
                        $"{prop.Name}: {formattedValue}");
                }
                catch
                {
                    parts.Add($"{prop.Name}: ");
                }
            }

            return string.Join(" | ", parts);
        }

        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive ||
                   type.IsEnum ||
                   type == typeof(string) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(decimal) ||
                   type == typeof(Guid) ||
                   type == typeof(DateOnly) ||
                   type == typeof(TimeOnly);
        }

        private static List<PropertyInfo>
            GetOrderedPropertiesByInheritance(Type type)
        {
            var props = new List<PropertyInfo>();
            var typeStack = new Stack<Type>();

            while (type != null &&
                   type != typeof(object))
            {
                typeStack.Push(type);
                type = type.BaseType!;
            }

            while (typeStack.Count > 0)
            {
                var currentType = typeStack.Pop();

                props.AddRange(
                    currentType.GetProperties(
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.DeclaredOnly));
            }

            return props;
        }
    }
}
