using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WbApiDemo3_22_5.Dtos;

namespace WbApiDemo3_22_5.Formatters
{
    public class CSVInputFormatter : TextInputFormatter
    {
        public CSVInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanReadType(Type type)
        => type == typeof(StudentAddDto);

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
       InputFormatterContext context, Encoding effectiveEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;


            using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
            string? line = null;
            try
            {
                _ = await ReadLineAsync(reader, context);
                line = await ReadLineAsync(reader, context);
                var splits = line.Split('-');
            

                var studentDto = new StudentAddDto()
                {
                    Fullname = splits[0],
                    SeriaNo = splits[1],
                    Age = int.Parse(splits[2]),
                    Score = int.Parse(splits[3]),
                };


                return await InputFormatterResult.SuccessAsync(studentDto);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static async Task<string> ReadLineAsync(StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();

            return line;
        }
    }
}
