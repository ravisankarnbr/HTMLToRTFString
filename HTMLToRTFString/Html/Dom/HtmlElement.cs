using System.Collections.Generic;

namespace HTMLToRTFString.Html.Dom
{
    public class HtmlElement : HtmlDomEntity
    {
        public string Name { get; set; }
        public List<HtmlDomEntity> Children { get; set; } = new List<HtmlDomEntity>();
        public List<HtmlAttribute> Attributes { get; set; } = new List<HtmlAttribute>();
    }
}