using System.Text;

namespace Lab5.Pages
{
    public class ResultPage : Page
    {
        private readonly string _angle;
        private readonly string _sin;

        public ResultPage(string angle, string sin)
        {
            _angle = angle;
            _sin = sin;
        }

        public override string View()
        {
            var builder = new StringBuilder();

            builder.Append($"<h2>Угол: {_angle}</h2>");
            builder.Append($"<h2>Синус: {_sin}</h2>");
            builder.Append("<a href='/'>Еще раз</a>");

            var body = builder.ToString();

            return string.Concat(SuccessHeaders(body.Length + 20), body);
        }
    }
}
