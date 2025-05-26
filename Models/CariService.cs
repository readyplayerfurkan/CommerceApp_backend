using backend.Models.Database.DatabaseModels;
using backend.Models.Database.TableModels;
using Dapper;

namespace backend.Models;

public class CariService
{
    private readonly Test2025DbContext _context;

    public CariService(Test2025DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CARI>> GetAllCari()
    {
        var query = "SELECT * FROM API_CARI";

        using var connection = _context.CreateConnection();
        
        var allCari = await connection.QueryAsync<CARI>(query);
        
        return allCari.ToList();
    }

    public async Task<CARI?> GetCariByKod(string ca_kod)
    {
        var query = "SELECT * FROM API_CARI WHERE ca_kod = @Kod";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<CARI>(query, new { Kod = ca_kod });
    }

    public async Task<int> InsertCari(CARI cari)
    {
        var query = @"INSERT INTO API_CARI (ca_kod, ca_adi, ca_ilce, ca_il, ca_vergi_dairesi, ca_vergi_no, ca_email_adresi)
                      VALUES (@ca_kod, @ca_adi, @ca_ilce, @ca_il, @ca_vergi_dairesi, @ca_vergi_no, @ca_email_adresi)";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteAsync(query, cari);
    }

    public async Task<int> UpdateCari(CARI cari)
    {
        var query = @"UPDATE API_CARI SET 
                        ca_adi = @ca_adi,
                        ca_ilce = @ca_ilce,
                        ca_il = @ca_il,
                        ca_vergi_dairesi = @ca_vergi_dairesi,
                        ca_vergi_no = @ca_vergi_no,
                        ca_email_adresi = @ca_email_adresi
                      WHERE ca_kod = @ca_kod";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteAsync(query, cari);
    }

    public async Task<int> DeleteCari(string ca_kod)
    {
        var query = "DELETE FROM API_CARI WHERE ca_kod = @Kod";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteAsync(query, new { Kod = ca_kod });
    }
}