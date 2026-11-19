using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKy_Nhom08.Models
{
    // ===== Trang Blog công khai =====
    public class BlogCardViewModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsFeatured { get; set; }
        public string Excerpt { get; set; }
    }

    public class BlogListViewModel
    {
        public string Query { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<BlogCardViewModel> Featured { get; set; }
        public List<BlogCardViewModel> Posts { get; set; }

        public BlogListViewModel()
        {
            Featured = new List<BlogCardViewModel>();
            Posts = new List<BlogCardViewModel>();
        }
    }

    // ===== Quản trị blog =====
    public class AdminBlogRowViewModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class AdminBlogListViewModel
    {
        public string Filter { get; set; }
        public string Query { get; set; }
        public int CountAll { get; set; }
        public int CountPublished { get; set; }
        public int CountDraft { get; set; }
        public int CountFeatured { get; set; }
        public List<AdminBlogRowViewModel> Rows { get; set; }

        public AdminBlogListViewModel()
        {
            Rows = new List<AdminBlogRowViewModel>();
        }
    }

    public class BlogPostFormViewModel
    {
        public int PostID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        [StringLength(300, ErrorMessage = "Tiêu đề tối đa 300 ký tự.")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
        [AllowHtml]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        public string Thumbnail { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public HttpPostedFileBase ThumbnailFile { get; set; }

        public bool RemoveThumbnail { get; set; }

        [Display(Name = "Xuất bản (Published)")]
        public bool IsPublished { get; set; }

        [Display(Name = "Bài viết nổi bật (Featured)")]
        public bool IsFeatured { get; set; }
    }

    // ===== Giới thiệu =====
    public class ProfileFormViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        [StringLength(100, ErrorMessage = "Tối đa 100 ký tự.")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [StringLength(200, ErrorMessage = "Tối đa 200 ký tự.")]
        [Display(Name = "Chức danh / Sinh viên ngành")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Giới thiệu bản thân")]
        public string Description { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "Ảnh cá nhân")]
        public HttpPostedFileBase AvatarFile { get; set; }

        public bool RemoveAvatar { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [StringLength(100, ErrorMessage = "Tối đa 100 ký tự.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Tối đa 20 ký tự.")]
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [StringLength(200, ErrorMessage = "Tối đa 200 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }

    // ===== Đổi mật khẩu =====
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu mới.")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không trùng khớp.")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        public string ConfirmPassword { get; set; }
    }
}
