using backend.Models;
using backend.Models.Database.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/cari")]
[ApiController]
public class CariController : Controller
{
    private readonly CariService _cariService;

    public CariController(CariService cariService)
    {
        _cariService = cariService;
    }

    // GET: api/cari
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allCari = await _cariService.GetAllCari();
        if (allCari == null || allCari.Count == 0)
            return NotFound("Hiç kayıt bulunamadı.");
        
        return Ok(allCari);
    }

    // POST: api/cari
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CARI cari)
    {
        var result = await _cariService.InsertCari(cari);
        if (result > 0)
            return Ok("Kayıt başarıyla eklendi.");

        return BadRequest("Ekleme işlemi başarısız.");
    }

    // PUT: api/cari
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CARI cari)
    {
        var result = await _cariService.UpdateCari(cari);
        if (result > 0)
            return Ok("Kayıt başarıyla güncellendi.");

        return NotFound("Güncellenecek kayıt bulunamadı.");
    }

    // DELETE: api/cari/{ca_kod}
    [HttpDelete("{ca_kod}")]
    public async Task<IActionResult> Delete(string ca_kod)
    {
        var result = await _cariService.DeleteCari(ca_kod);
        if (result > 0)
            return Ok("Kayıt başarıyla silindi.");

        return NotFound("Silinecek kayıt bulunamadı.");
    }
}