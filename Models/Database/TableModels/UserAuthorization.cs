using System.ComponentModel.DataAnnotations;

namespace backend.Models.Database.TableModels
{
    public class UserAuthorization
    {
        [Key]
        public int KULLANICI_ID { get; set; }
        public int ISLEM_NO { get; set; }
        public string ISLEM_ADI { get; set; }
    }
}
