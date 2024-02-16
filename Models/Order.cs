namespace AutoWebApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public User Customer { get; set; } = null!;
        public List<SparePart> SpareParts { get; set; } = new List<SparePart>();

        public Order()
        {

        }
    }
}
