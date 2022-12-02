using System.Text;

namespace Lab5.Pages
{
    public class BadPage : Page
    {
        public override string View()
        {
            var bodyBuilder = new StringBuilder();

            bodyBuilder.Append("<h4>Something went wrong</h4>");
            bodyBuilder.Append("<p>Yor data is not correct</p>");

            string body = bodyBuilder.ToString();

            return string.Concat(SuccessHeaders(body.Length), body);
        }
    }
}
