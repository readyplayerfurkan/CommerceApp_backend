using backend.Models.Database.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class GetCompaniesController : ControllerBase
{
    private readonly GWSistemDbContext _gwSistemDbContext;

    public GetCompaniesController(GWSistemDbContext gwSistemDbContext)
    {
        _gwSistemDbContext = gwSistemDbContext;
    }

    [HttpGet("{currentUserID}")]
    public async Task<IActionResult> Get(int currentUserID)
    {
        if (currentUserID <= 0)
            return BadRequest("Geçersiz kullanıcı ID");

        var companies = await (
            from user in _gwSistemDbContext.Users
            join userAuth in _gwSistemDbContext.UserAuthorizations on user.ID equals userAuth.KULLANICI_ID
            join company in _gwSistemDbContext.Companies on userAuth.ISLEM_NO equals company.ID
            where userAuth.ISLEM_ADI == "Firma Yetki"
               && userAuth.KULLANICI_ID == currentUserID
               && company.ID >= 1
            select company.ADI
        ).Distinct().ToListAsync();

        if (companies.Count > 0)
        {
            return Ok(companies);
        }
        else
        {
            return NotFound("Müşterinin yetkili olduğu herhangi bir firma bulunamadı.");
        }
    }
}
