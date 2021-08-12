using System.Drawing;
using HTMLToRTFString.Rtf;

namespace HTMLToRTFString
{
    public class ElementBuilder : Builder<ElementSubject>
    {
        public ElementBuilder ForegroundColor(Color color)
            => Apply<ElementBuilder, Color>(ref Subject.ForegroundColor, color);
        public ElementBuilder BackgroundColor(Color color)
            => Apply<ElementBuilder, Color>(ref Subject.BackgroundColor, color);
        public ElementBuilder FontSize(float fontSize)
            => Apply<ElementBuilder, float>(ref Subject.FontSize, fontSize);
        public ElementBuilder FontStyle(string fontStyle)
            => Apply<ElementBuilder, string>(ref Subject.FontStyle, fontStyle);
        public ElementBuilder HorizontalAligment(HorizontalAlignment horizontalAlignment)
            => Apply<ElementBuilder, HorizontalAlignment>(ref Subject.HorizontalAlignment, horizontalAlignment);
    }
}