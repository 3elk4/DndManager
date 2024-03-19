using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDF
{
    static class TableExtentions
    {
        private static IContainer Cell(this IContainer container, bool dark)
        {
            return container
                .Border((float)0.75)
                .Background(dark ? Colors.Orange.Lighten4 : Colors.Orange.Lighten5)
                .Padding(5);
        }

        // displays only text label
        public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

        // allows you to inject any type of content, e.g. image
        public static IContainer ValueCell(this IContainer container) => container.Cell(false);
        public static void ValueCell(this IContainer container, string text) => container.Cell(false).Text(text);
        [Obsolete]
        public static void ValueCell(this IContainer container, double value) => container.Cell(false).Text(value);
    }
}
