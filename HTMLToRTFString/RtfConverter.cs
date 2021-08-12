using HTMLToRTFString.Rtf;
using HtmlAgilityPack;

namespace HTMLToRTFString
{
    /// <summary>
    /// RTF Converter
    /// </summary>    
    public class RtfConverter
    {
        private readonly RtfConverterSubject _subject;

        public RtfConverter()
            : this(new RtfConverterSubject()) { }
        internal RtfConverter(RtfConverterSubject subject)
            => _subject = subject;

      
        /// <summary>
        /// Convert html string to rtf
        /// </summary>
        /// <param name="dom"></param>
        /// <returns>string</returns>
        public string Convert(HtmlNodeCollection dom)
            => new RtfDocumentBuilder(_subject)
                .Html(dom)
                .Build();
    }
}