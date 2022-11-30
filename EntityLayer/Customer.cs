using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdentityNo { get; set; }
        public int IdentityNoVerified { get; set; }
        public int StatusId { get; set; }

    }
}