using System.ComponentModel.DataAnnotations;

namespace backend.Models.Database.TableModels;

public class CARI
{
    [Key]
    public int ca_idx { get; set; }
    public string ca_adi { get; set; }
    public string ca_vergi_no { get; set; }
    public string ca_vergi_dairesi { get; set; }
    public string ca_email_adresi { get; set; }
    public string ca_telefon { get; set; }
    public string ca_il { get; set; }
}