using System.ComponentModel.DataAnnotations;

namespace backend.Models.Database.TableModels
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string KOD { get; set; }
        public string ADI { get; set; }
        public string SOYADI { get; set; }
        public string SIFRE { get; set; }
    }
}
