using System.ComponentModel.DataAnnotations;

namespace backend.Models.Database.TableModels
{
    public class Company
    {
        [Key]
        public int ID { get; set; }
        public string ADI { get; set; }
    }
}
