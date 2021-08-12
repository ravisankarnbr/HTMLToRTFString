using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HTMLToRTFString;

namespace HTMLToRTFString.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
			var content = @"<div style=""font-size:12pt;font-family:&quot;Monotype Corsiva&quot;;""><p style=""text-align:center;font-size:60pt;margin:0;""><strong><em>Hello,<br>World!</em></strong></p></div>";

			content = Regex.Replace(content, @"( |\t|\r?\n)\1+", "$1");
			string sanitizHTML = string.Empty;
			HtmlDocument doc = new HtmlDocument();
			doc.OptionAutoCloseOnEnd = true;
			doc.OptionWriteEmptyNodes = true;
			doc.OptionFixNestedTags = true;
		  

			doc.LoadHtml(content);
			sanitizHTML = doc.DocumentNode.OuterHtml;

			var converter = new RtfConverterBuilder().BuildConverter();
			var result = converter.Convert(doc.DocumentNode.ChildNodes);
			Console.WriteLine(result);
		}
    }
}
