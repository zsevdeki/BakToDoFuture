using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakToDoFuture.Models
{
    public class Media:BaseEntity
    {
        [StringLength(200)]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [StringLength(200)]
        [DisplayName("Uzantı")]
        public string Extension { get; set; }
        [StringLength(200)]
        [DisplayName("Dosya Yolu")]
        public string FilePath { get; set; }
        [DisplayName("Dosya Boyutu")]
        public float FileSize { get; set; }
        [DisplayName("Yıl")]
        public int Year { get; set; }
        [DisplayName("Ay")]
        public int Month { get; set; }
        [StringLength(200)]
        [DisplayName("İçerik Tipi")]
        public string ContentType { get; set; }
    }
}