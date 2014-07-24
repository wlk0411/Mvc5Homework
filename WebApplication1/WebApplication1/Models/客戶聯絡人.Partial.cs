namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
       
    
    public interface I客戶聯絡人更新
    {
        [Required]
        int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        string 電話 { get; set; }
    }




    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : I客戶聯絡人更新, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            //todo：這段驗證在一次新增一筆資料的情況下會正確，但是在批次更新下卻不對
            //原因在於這段會從資料庫撈資料，但是批次更新的值都在畫面上
            var repo = RepositoryHelper.Get客戶聯絡人Repository();

            if( repo.Where(x => x.Id != this.Id && x.客戶Id == this.客戶Id && x.Email == this.Email).Count() > 0 )
            {
                yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複！",
                    new[] { "Email" });
            }
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
