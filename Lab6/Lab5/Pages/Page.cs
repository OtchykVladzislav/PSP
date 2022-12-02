using System.Text;

namespace Lab5.Pages
{
    public abstract class Page
    {
        public string SuccessHeaders(int contentLength)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("HTTP/1.1 200 OK").Append("\r\n");
            builder.Append("Date: ").Append(DateTime.Now).Append("\r\n");
            builder.Append("Content-Type: text/html; charset=UTF-8").Append("\r\n");
            builder.Append("Content-Length: ").Append(contentLength).Append("\r\n");
            builder.Append("Connection: close").Append("\r\n");
            builder.Append("\r\n");

            return builder.ToString();
        }

        public abstract string View();
    }
}
