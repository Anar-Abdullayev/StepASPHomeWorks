using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WbApiDemo3_22_5.Dtos;

namespace WbApiDemo3_22_5.Formatters
{
    public class CSVOutputFormatter : TextOutputFormatter
    {
        public CSVOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.ASCII);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();
            if (context.Object is IEnumerable<StudentDto> list)
            {
                sb.AppendLine("Id-Fullname-SeriaNo-Age-Score");
                foreach (var student in list)
                {
                    FormatCSV(student, sb);
                }
            }
            else if (context.Object is StudentDto student)
            {
                sb.AppendLine("Id-Fullname-SeriaNo-Age-Score");
                FormatCSV(student, sb);
            }

            await response.WriteAsync(sb.ToString());
        }

        private void FormatCSV(StudentDto student, StringBuilder sb)
        {
            sb.AppendLine($"{student.Id}-{student.Fullname}-{student.SeriaNo}-{student.Age}-{student.Score}");
        }
    }
}
