using System;
using System.ComponentModel.DataAnnotations;

namespace alintaApi.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string firstName { get; set; }
        [Required]
        [StringLength(30)]
        public string lastName { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd")]
        public DateTime dateOfBirth { get; set; }
    }
}
