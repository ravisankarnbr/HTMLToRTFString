using System.Collections.Generic;
using HTMLToRTFString.Html.Dom;

namespace HTMLToRTFString
{
    public class RtfConverterSubject
    {
        public Dictionary<HtmlElementType, ElementSubject> ElementSubjects = new Dictionary<HtmlElementType, ElementSubject>();
        public string FontStyle;
    }
}