using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakToDoFuture.Models
{
    public class Category:BaseEntity
    {

        [StringLength(200)]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [DisplayName("Kategori Adı")]
        public string Name { get; set; }

        public virtual ICollection<TodoItem> TodoItems { get; set; }

    }
}