
using RentCars_Back.Models;

public interface IReportService
{
    byte[] GeneratePdfReport(ReportRequest request);
}
