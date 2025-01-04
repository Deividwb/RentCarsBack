
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RentCars_Back.Models;

namespace RentCars_Back.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("generate-report")]
    public async Task<IActionResult> GenerateReport([FromBody] ReportRequest request)
    {
        // Recebe os dados enviados na requisição
        // int userId = request.UserId;
        // string reportType = request.ReportType;

        try
        
        {
            // Gera o PDF com base nos dados recebidos
            var pdfData = _reportService.GeneratePdfReport(request);

            // Retorna o PDF gerado como resposta
            return File(pdfData, "application/pdf", "relatorio.pdf");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
