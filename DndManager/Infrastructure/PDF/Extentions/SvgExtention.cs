using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace Infrastructure.PDF.Extentions
{
    public static class SvgExtension
    {
        public static void Svg(this IContainer container, SKPicture svg)
        {
            container
                .AlignCenter()
                .AlignMiddle()
                .ScaleToFit()
                .Width(svg.CullRect.Width)
                .Height(svg.CullRect.Height)
                .Canvas((canvas, space) => canvas.DrawPicture(svg));
        }
    }
}
