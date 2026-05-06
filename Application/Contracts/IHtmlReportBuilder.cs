using TP2.Application.Models;

namespace TP2.Application.Contracts;

public interface IHtmlReportBuilder
{
    string Build(IReadOnlyList<ComparisonRow> rows);
}
