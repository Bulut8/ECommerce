using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electro.BOL.Entities
{
    [Table("Admin")]
    public class Admin
    {
        [Key, StringLength(30), Column(TypeName = "Varchar"), Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        public string KullaniciAdi { get; set; }

        [StringLength(50), Column(TypeName = "Varchar"), Display(Name = "Yetkili Adı Soyadı"), Required(ErrorMessage = "Ad boş geçilemez.")]
        public string AdSoyad { get; set; }

        [StringLength(20), Column(TypeName = "Varchar"), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre boş geçilemez."), DataType(DataType.Password)]
        public string Sifre { get; set; }

        [NotMapped, Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor"), DataType(DataType.Password)]
        public string Sifre2 { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Son Giriş IP Adresi")]
        public DateTime SonGirisTarih { get; set; }

        [StringLength(20), Column(TypeName = "Varchar"), Display(Name = "Son Giriş IP Adresi")]
        public string SonGirisIP { get; set; }

        [StringLength(10), Column(TypeName = "Varchar"), Display(Name = "Rolü")]
        public string Rol { get; set; }
    }
}
