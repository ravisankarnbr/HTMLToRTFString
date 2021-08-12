using System.Diagnostics;
using System.Drawing;
using System.Text;
using HTMLToRTFString.Html;
using HTMLToRTFString.Rtf;

namespace HTMLToRTFString.Rtf
{
    internal class RtfDocumentBuilder
    {
        private readonly StringBuilder _document = new StringBuilder();

        public RtfConverterSubject Subject { get; }


        public RtfDocumentContext Context { get; } = new RtfDocumentContext();

        public RtfDocumentBuilder(RtfConverterSubject subject)
            => Subject = subject;

        public RtfDocumentBuilder BackgroundColor(string htmlColor) => BackgroundColor(HtmlColorHelper.GetColor(htmlColor));
        public RtfDocumentBuilder BackgroundColor(Color color)
        {
            var colorNumber = Context.GetColorNumber(color);
            return Rtf($@"\chshdng10000\chcbpat{colorNumber}\chcfpat{colorNumber}\cb{colorNumber}");
        }
        public RtfDocumentBuilder HorizontalAlignment(HorizontalAlignment alignment)
        {
            if (alignment == HTMLToRTFString.Rtf.HorizontalAlignment.Left)
                _document.Append(@"\ql");
            else if (alignment == HTMLToRTFString.Rtf.HorizontalAlignment.Right)
                _document.Append(@"\qr");
            else if (alignment == HTMLToRTFString.Rtf.HorizontalAlignment.Center)
                _document.Append(@"\qc");
            else if (alignment == HTMLToRTFString.Rtf.HorizontalAlignment.Justify)
                _document.Append(@"\qj");
            return this;
        }

        public string Ignore(string text)
        {
            Debug.WriteLine("Ignoring " + text);
            return "";
        }

        public RtfDocumentBuilder ForegroundColor(string htmlColor) => ForegroundColor(HtmlColorHelper.GetColor(htmlColor));
        public RtfDocumentBuilder ForegroundColor(Color color) => Rtf($@"\cf{Context.GetColorNumber(color)}");
        public RtfDocumentBuilder Paragraph(Html.Dom.HtmlElementType element)
        {
           // System.Diagnostics.Debug.WriteLine("Completing paragraph! from:" + element);
            return Rtf(@"\par");
        }
        public RtfDocumentBuilder TextIndent(float indentSize)
        {
            int tabSize = 48; 
            string rtf = "";
            int count = 0;
            int numberTabs = (int)System.Math.Floor(indentSize / tabSize);

            while (count < numberTabs)
            {
                //System.Diagnostics.Debug.WriteLine("Adding Tab");
                rtf += @"\tab";
                count++;
            }

            return Rtf(rtf);


        }
        public RtfDocumentBuilder FontSize(float fontSize) => Rtf($@"\fs{fontSize * 2}");
        public RtfDocumentBuilder FontFamily(string font) => Rtf($@"\f{Context.GetFontNumber(font)}");

        public RtfDocumentBuilder Display(string attribute)
        {
            if (attribute == "inline-block")
            {
                return Rtf(@"\tab");

            }
            return Rtf("");

        }

        public RtfDocumentBuilder IgnoreAttribute(string text) => Rtf(Ignore(text));

        public RtfDocumentBuilder Bold() => Rtf(@"\b");
        public RtfDocumentBuilder Italic() => Rtf(@"\i");
        public RtfDocumentBuilder Underline() => Rtf(@"\ul");
        public RtfDocumentBuilder Striketrough() => Rtf(@"\strike");
        public RtfDocumentBuilder NewLine()
        {

            return Rtf(@"\pard\par");


            //This was giving issues with bullets
            //return Rtf(@"\line");
        }
        public RtfDocumentBuilder Text(string text) => Rtf("{" + text + "}");
        public RtfDocumentBuilder OpenContext() => Rtf("{");
        public RtfDocumentBuilder CloseContext() => Rtf("}");
        public RtfDocumentBuilder Rtf(string rtf)
        {
           // System.Diagnostics.Debug.WriteLine("Adding to RTF document: " + rtf);
            _document.Append(rtf);
            return this;
        }

        public string Build()
        {
            var fontTable = Context.GetFontTable(Subject.FontStyle);
            var colorTable = Context.GetColorTable();

            var value = new StringBuilder();
            value.AppendLine(@"{\rtf1\ansi\deff0 " + fontTable + colorTable);
            value.AppendLine(_document.ToString());
            value.AppendLine("}");
            return value.ToString();
        }
    }
}