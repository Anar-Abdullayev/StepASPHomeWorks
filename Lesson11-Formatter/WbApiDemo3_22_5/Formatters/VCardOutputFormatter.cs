using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WbApiDemo3_22_5.Dtos;

namespace WbApiDemo3_22_5.Formatters
{
    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response=context.HttpContext.Response;
            var sb=new StringBuilder();
            if(context.Object is IEnumerable<StudentDto> list)
            {
                foreach (var item in list)
                {
                    FormatVCard(sb, item);
                }
            }
            else if(context.Object is StudentDto student)
            {
                FormatVCard(sb, student);
            }
            await response.WriteAsync(sb.ToString());
        }

        private void FormatVCard(StringBuilder sb, StudentDto item)
        {
            sb.AppendLine("BEGIN:VCARD");
            sb.AppendLine($"FN:{item.Fullname}");
            sb.AppendLine($"SNO:{item.SeriaNo}");
            sb.AppendLine($"AGE:{item.Age}");
            sb.AppendLine($"SCORE:{item.Score}");
            sb.AppendLine($"UID:{item.Id}");
            sb.AppendLine("END:VCARD");
        }
    }
}
