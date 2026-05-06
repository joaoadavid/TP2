using System.Net;
using TP2.Application.Contracts;
using TP2.Application.Models;

namespace TP2.Presentation.Reporting;

public sealed class HtmlReportBuilder : IHtmlReportBuilder
{
    public string Build(IReadOnlyList<ComparisonRow> rows)
    {
        var lines = new List<string>
        {
            "<html>",
            "<head>",
            "<title>PAA - Trabalho 2</title>",
            "</head>",
            "<body>",
            "<table border=1>",
            "<tr>",
            "<th>Arquivo</th>",
            "<th>BubbleSort</th>",
            "<th>QuickSort</th>",
            "<th>BucketSort</th>",
            "</tr>"
        };

        foreach (ComparisonRow row in rows)
        {
            lines.Add("<tr>");
            lines.Add("<td> " + WebUtility.HtmlEncode(row.FileName) + " </td>");
            lines.Add("<td> " + row.BubbleSortOps + " </td>");
            lines.Add("<td> " + row.QuickSortOps + " </td>");
            lines.Add("<td> " + row.BucketSortOps + " </td>");
            lines.Add("</tr>");
        }

        lines.Add("</table>");
        lines.Add("</body>");
        lines.Add("</html>");
        return string.Join(Environment.NewLine, lines);
    }
}
