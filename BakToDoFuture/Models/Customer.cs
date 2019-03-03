using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakToDoFuture.Models
{
    public class Customer:BaseEntity
    {
        [StringLength(200)]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [DisplayName("Ad")]
        public string Name { get; set; }
        [StringLength(200)]
        [DisplayName("E-Posta")]
        public string Email { get; set; }
        [StringLength(20)]
        [DisplayName("Telefon")]
        public string Phone { get; set; }
        [StringLength(20)]
        [DisplayName("Faks")]
        public string Fax { get; set; }
        [StringLength(200)]
        [DisplayName("Web Sitesi")]
        public string Website { get; set; }
        [StringLength(4000)]
        [DisplayName("Adres")]
        public string Address { get; set; }

        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}