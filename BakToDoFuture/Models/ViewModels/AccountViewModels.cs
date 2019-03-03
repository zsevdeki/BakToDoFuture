using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BakToDoFuture.Models
{

    public class BaseEntityAccount
    {
        [Required(ErrorMessage = "E-Posta adresi zorunludur.")]
        [Display(Name = "E-Posta")]
        [EmailAddress(ErrorMessage = "Geçersiz E-Posta adresi.")]
        public string Email { get; set; }
    }
    public class ExternalLoginConfirmationViewModel:BaseEntityAccount
    {
        
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Sağlayıcı Gereklidir.")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Size gelen kodo giriniz.")]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Bu tarayıcı hatırlansın mı?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel:BaseEntityAccount
    {
        
    }

    public class LoginViewModel:BaseEntityAccount
    {
        

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırlasın mı?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel:BaseEntityAccount
    {
        

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır..", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Onayı")]
        [Compare("Password", ErrorMessage = "Girilen bilgiler birbiriyle eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel:BaseEntityAccount
    {
        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır..", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Onayı")]
        [Compare("Password", ErrorMessage = "Girilen bilgiler birbiriyle eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel:BaseEntityAccount
    {
        
    }
}
