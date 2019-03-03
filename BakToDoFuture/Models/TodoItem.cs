using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Windows.Forms.VisualStyles;

namespace BakToDoFuture.Models
{
    public class TodoItem : BaseEntity
    {
        [StringLength(200)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Başlık")]
        public string Title { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Durum")]
        public Status Status { get; set; }
        [DisplayName("Kategori Adı")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [DisplayName("Kategori Adı")]
        public virtual Category Category { get; set; }
        [StringLength(200)]
        [DisplayName("Dosya Eki")]
        public string Attachment { get; set; }

        [DisplayName("Departman")]
        public int? DepartmentId { get; set; }
        [DisplayName("Departman")]
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [DisplayName("Taraf")]
        public int? SideId { get; set; }
        [DisplayName("Taraf")]
        [ForeignKey("SideId")]
        public virtual Side Side { get; set; }

        [DisplayName("Müşteri")]
        public int? CustomerId { get; set; }

        [DisplayName("Müşteri")]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [DisplayName("Yönetici")]
        public int? ManagerId { get; set; }

        [DisplayName("Yönetici")]
        [ForeignKey("ManagerId")]
        public virtual Contact Manager { get; set; }



        [DisplayName("Organizatör")]
        public int? OrganizatorId { get; set; }


        [DisplayName("Organizatör")]
        [ForeignKey("OrganizatorId")]
        public virtual Contact Organizator { get; set; }






        [DataType("datetime-local")]
        [DisplayName("Toplantı Tarihi")]
        [Required(ErrorMessage = "Toplantı tarihi alanı gereklidir.")]
        public DateTime MeetingDate { get; set; }
        [DataType("datetime-local")]
        [DisplayName("Planlanan Tarihi")]
        [Required(ErrorMessage = "Planlanan tarihi alanı gereklidir.")]
        public DateTime PlannedDate { get; set; }

        [DataType("datetime-local")]
        [DisplayName("Bitirme Tarihi")]
        [Required(ErrorMessage = "Bitirme tarihi alanı gereklidir.")]
        public DateTime FinishDate { get; set; }

        [DataType("datetime-local")]
        [DisplayName("Revize Tarihi")]
        [Required(ErrorMessage = "Revize tarihi alanı gereklidir.")]
        public DateTime ReviseDate { get; set; }

        [DisplayName("Görüşme Konusu")]
        public string ConversationSubject { get; set; }
        [DisplayName("Destekleyen Firma")]
        public string SupporterCompany { get; set; }
        [DisplayName("Destekleyen Doktor")]
        public string SupporterDoctor { get; set; }
        [DisplayName("Katılımcı Sayısı")]
        public int ConversationAttendeeCount { get; set; }
        [DataType("datetime-local")]
        [Required(ErrorMessage = "Planlanan Organizasyon tarih alanı gereklidir.")]
        [DisplayName("Planlanan Organizasyon Tarihi")]
        public DateTime ScheduledOrganizationDate { get; set; }
        [DisplayName("Mail Konusu")]
        public string MailingSubject { get; set; }
        [DisplayName("Afiş Konusu")]
        public string PosterSubject { get; set; }
        [DisplayName("Afiş Sayısı")]
        [Required(ErrorMessage = "Afiş sayısı alanı gereklidir.")]
        public int PosterCount { get; set; }
        [DisplayName("Uzaktan Eğitim")]
        public string Elearning { get; set; }
        [DisplayName("Tarama Türleri")]
        public string TypesofScans { get; set; }
        [DisplayName("Taramalardaki ASO sayısı")]
        public string AsoCountInScans { get; set; }
        [DisplayName("Organizasyonlardaki ASO sayısı")]
        public string AsoCountInOrganizations { get; set; }
        [DisplayName("Aşı Organizasyonu Türleri")]
        public string TypesOfVaccinationOrganization { get; set; }
        [DisplayName("Aşı Organizasyonundaki ASO sayısı")]
        public string AsoCountInVaccinationOrganization { get; set; }
        [DisplayName("Afiş için Tazminat Miktarı")]
        public string AmountOfCompensationForPoster { get; set; }
        [DisplayName("Kurumsal Verimlilik Raporu")]
        public string CorporateProductivityReport { get; set; }
    }
}