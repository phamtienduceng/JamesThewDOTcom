namespace JamesRecipes.Models.Book
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        // Địa chỉ khách hàng (nếu cần)
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        // Các đơn hàng của khách hàng
        public List<Order> Orders { get; set; }
    }

}
