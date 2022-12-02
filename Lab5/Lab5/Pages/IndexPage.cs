using System.Text;

namespace Lab5.Pages
{
    public class IndexPage : Page
    {
        public override string View()
        {
            var bodyBuilder = new StringBuilder();

            bodyBuilder.Append("<form method='post'>");
            bodyBuilder.Append("<h4>Найти синус угла: </h4>");
            bodyBuilder.Append("<input type='number' name='angle' />");
            bodyBuilder.Append("<input type='submit' value='Найти' />");
            bodyBuilder.Append("</form>");

            var body = bodyBuilder.ToString();

            return string.Concat(SuccessHeaders(body.Length + 20), body);
        }
    }
}
