using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PTEcommerce.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "{0} phải từ {2} ký tự trở lên.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class CustomerInfoModel
    {
        public CustomerInfoModel()
        {
        }
        public int Id { get; set; }
        public string IdGuid { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [RegularExpression(@"^\d{10,12}", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        [StringLength(100, ErrorMessage = "{0} phải từ {2} ký tự trở lên.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Password.")]
        [StringLength(100, ErrorMessage = "{0} phải từ {2} ký tự trở lên.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "{0} và Password không giống nhau.")]
        public string ConfirmPassword { get; set; }
        public string BirthdayCustomer { get; set; }
    }

    public class RegisterViewModel
    {
        string EmailRequired;
        public RegisterViewModel()
        {
            this.EmailRequired = "Yêu cầu nhập Email.";
        }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^(0)[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [MinLength(10, ErrorMessage = "Số điện thoại tối thiểu 10 kí tự.")]
        [MaxLength(11, ErrorMessage = "Số điện thoại tối đa 11.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "{0} phải từ {2} ký tự trở lên.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^\S(.*\S)?$", ErrorMessage = "Mật khẩu không chứa khoảng trắng ở 2 đầu.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu và mật khẩu không giống nhau.")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //public string Captcha { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    
    public class MemberSession
    {
        public string SessionId { get; set; }
        public int AccID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
    public class AdminSession
    {
        public string SessionId { get; set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
    }
    [Serializable]
    public class SessionCustomer
    {
        public static string sessionName = "customer";
        public static MemberSession GetUser()
        {

            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session.Count > 0 &&
                HttpContext.Current.Session[SessionCustomer.sessionName] != null)
            {
                return HttpContext.Current.Session[SessionCustomer.sessionName] as MemberSession;
            }
            return null;
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetUser(MemberSession user)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SessionCustomer.sessionName);
                user.SessionId = HttpContext.Current.Session.SessionID;
            }
            HttpContext.Current.Session.Add(SessionCustomer.sessionName, user);
            return true;
        }

        /// <summary>
        /// Clears the session.
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Remove("customer");
        }
    }
    [Serializable]
    public class SessionAdmin
    {
        public static string sessionName = "manager";
        public static AdminSession GetUser()
        {

            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session.Count > 0 &&
                HttpContext.Current.Session[SessionAdmin.sessionName] != null)
            {
                return HttpContext.Current.Session[SessionAdmin.sessionName] as AdminSession;
            }
            return null;
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetUser(AdminSession user)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SessionAdmin.sessionName);
                user.SessionId = HttpContext.Current.Session.SessionID;
            }
            HttpContext.Current.Session.Add(SessionAdmin.sessionName, user);
            return true;
        }

        /// <summary>
        /// Clears the session.
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Remove("manager");
        }
    }
}