// using RentCars_Back.Repository;


// var builder = WebApplication.CreateBuilder(args);




// // Add services to the container.

// builder.Services.AddControllers();
// builder.Services.AddScoped<IDriveRepository, DriveRepository>();
// builder.Services.AddControllers();
// builder.Services.AddScoped<ICarRepository, CarRepository>();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddScoped<IReportService, PdfReportService>();

// #region [Cors]
// builder.Services.AddCors();
// #endregion

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// #region [Cors]
// app.UseCors(c =>
// {
//     c.AllowAnyHeader();
//     c.AllowAnyMethod();
//     c.AllowAnyOrigin();
// });
// #endregion

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();

//     endpoints.MapGet("/generate-report", async context =>
//     {
//         var reportService = context.RequestServices.GetRequiredService<IReportService>();
//         var reportContent = reportService.GeneratePdfReport();

//         context.Response.Headers.Add("Content-Disposition", "inline; filename=hello.pdf");
//         await context.Response.Body.WriteAsync(reportContent);
//     });
// });

// app.Run();


using QuestPDF.Infrastructure;
using RentCars_Back.Models;
using RentCars_Back.Repository;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IDriveRepository, DriveRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReportService, PdfReportService>();

// Enable CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseRouting();  // Ensure UseRouting is called before UseEndpoints

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    // Atualizando a rota para aceitar parâmetros pela URL (query string)
    _ = endpoints.MapGet("/generate-report", async context =>
    {
        // Deserializa o corpo da requisição para o objeto ReportRequest
        var reportRequest = await context.Request.ReadFromJsonAsync<ReportRequest>();

        if (reportRequest == null)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Invalid request data.");
            return;
        }

        var reportService = context.RequestServices.GetRequiredService<IReportService>();
        var reportContent = reportService.GeneratePdfReport(reportRequest);

        // Configura o cabeçalho para download do PDF
        context.Response.Headers.Add("Content-Disposition", "inline; filename=relatorio.pdf");
        context.Response.ContentType = "application/pdf";

        // Envia o conteúdo do PDF como resposta
        await context.Response.Body.WriteAsync(reportContent);
    });
});


app.Run();

