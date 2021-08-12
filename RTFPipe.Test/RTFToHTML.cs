using RtfPipe;
using System;
using System.Text;
using Xunit;

namespace RTFPipe.Test
{
    public class RTFToHTML
    {
        public RTFToHTML()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        [Fact]
        public void Test1()
        {
            var rtfString = @"{\rtf1\ansi\deff0 {\fonttbl;{\f1 Monotype Corsiva;}}{\colortbl;}{\fs24\f1{{\qc\fs120{{\b{{\i{{Hello,}{\pard\par{}}{World!}}}}}}}}}}";
            var html = Rtf.ToHtml(rtfString);
            Assert.NotNull(html);
            //actual : <div style="font-size:12pt;font-family:&quot;Monotype Corsiva&quot;;"><p style="text-align:center;font-size:60pt;margin:0;"><strong><em>Hello,</em></strong></p><p style="font-size:60pt;margin:0;"><strong><em>World!</em></strong></p></div>
            // expected : <div style=""font-size:12pt;font-family:&quot;Monotype Corsiva&quot;;""><p style=""text-align:center;font-size:60pt;margin:0;""><strong><em>Hello,<br>World!</em></strong></p></div>
        }
    }
}
