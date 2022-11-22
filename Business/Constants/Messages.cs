using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string EmployeeAdded = "Çalışan başarıyla eklendi";
        public static string EmployeeNotAdded = "Çalışan eklenemedi";
        public static string EmployeeUpdated = "Çalışan başarıyla güncellendi.";
        public static string EmployeeCountError = "Çalışan sayısı 150'den fazla olamaz.";
        public static string EmployeeNameExistError = "Aynı isimle çalışan eklenemez.";
        public static string AuthorizationDenied = "Yetki yok.";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

    }
}
