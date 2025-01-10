using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Models.Dtos.Client
{
    public class EditClientDto
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
