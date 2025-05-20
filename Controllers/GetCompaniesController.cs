using backend.Models.Database.DatabaseModels;
using Dapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GetCompaniesController : ControllerBase
{
    private readonly SistemDbContext _sistemDbContext;

    public GetCompaniesController(SistemDbContext sistemDbContext)
    {
        _sistemDbContext = sistemDbContext;
    }

    [HttpGet("{currentUserID}")]
    public async Task<IActionResult> Get(int currentUserID)
    {
        if (currentUserID <= 0)
            return BadRequest("Geçersiz kullanıcı ID");
        
        var query = @"
        SELECT DISTINCT c.ADI
        FROM KULLANICI u
        JOIN KULLANICI_YETKI ua ON u.ID = ua.KULLANICI_ID
        JOIN FIRMA c ON ua.ISLEM_NO = c.ID
        WHERE ua.ISLEM_ADI = @IslemAdi
          AND ua.KULLANICI_ID = @UserId
          AND c.ID >= 1";

        using var connection = _sistemDbContext.CreateConnection();

        var companies = await connection.QueryAsync<string>(query, new
        {
            IslemAdi = "Firma Yetki",
            UserId = currentUserID
        });

        if (companies.Any())
        {
            return Ok(companies);
        }
        else
        {
            return NotFound("Müşterinin yetkili olduğu herhangi bir firma bulunamadı.");
        }
    }
}
