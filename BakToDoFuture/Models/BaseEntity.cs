using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakToDoFuture.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }


        [DisplayName("Oluşturma Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        
        [DisplayName("Oluşturan Kişi")]
        public string CreatedBy { get; set; }

        
        [DisplayName("Güncelleme Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
        

        [DisplayName("Güncelleyen Kişi")]
        public string UpdateBy { get; set; }

    }
}