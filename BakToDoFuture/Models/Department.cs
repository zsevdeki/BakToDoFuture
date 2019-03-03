using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakToDoFuture.Models
{
    public class Department:BaseEntity
    {
        [StringLength(200)]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [DisplayName("Ad")]
        public string Name { get; set; }

        public virtual IList<TodoItem> TodoItems { get; set; }
    }
}