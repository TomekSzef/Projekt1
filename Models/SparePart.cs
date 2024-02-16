using System.ComponentModel.DataAnnotations;

namespace AutoWebApp.Models
{
    public class SparePart
    {
        [Key]
        public int PartID { get; set; }
        [Required]
        public string PartName { get; set; } = String.Empty;
        [Required]
        public string VehicleModel { get; set; } = String.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? Description { get; set; } = String.Empty;
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; } = new();
        public SparePart()
        {

        }
    }
}
