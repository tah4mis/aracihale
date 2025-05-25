using System.ComponentModel.DataAnnotations;

namespace VehicleAuction.Web.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka alanı zorunludur")]
        [Display(Name = "Marka")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model alanı zorunludur")]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yıl alanı zorunludur")]
        [Display(Name = "Yıl")]
        [Range(1900, 2024, ErrorMessage = "Yıl 1900 ile 2024 arasında olmalıdır")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Kilometre alanı zorunludur")]
        [Display(Name = "Kilometre")]
        [Range(0, int.MaxValue, ErrorMessage = "Kilometre 0'dan büyük olmalıdır")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur")]
        [Display(Name = "Fiyat")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Price { get; set; }

        [Display(Name = "Tahmini Fiyat")]
        [Range(0, double.MaxValue, ErrorMessage = "Tahmini fiyat 0'dan büyük olmalıdır")]
        public decimal? EstimatedPrice { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Resim")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Satışta mı?")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedAt { get; set; }

        // Güvenlik Özellikleri
        [Display(Name = "ABS")]
        public bool ABS { get; set; }

        [Display(Name = "ASR")]
        public bool ASR { get; set; }

        [Display(Name = "Çocuk Kilidi")]
        public bool CocukKilidi { get; set; }

        [Display(Name = "Distronic")]
        public bool Distronic { get; set; }

        [Display(Name = "ESP / VSA")]
        public bool ESP { get; set; }

        [Display(Name = "Gece Görüş Sistemi")]
        public bool GeceGorusSistemi { get; set; }

        [Display(Name = "Hava Yastığı (Sürücü)")]
        public bool HavaYastigiSurucu { get; set; }

        [Display(Name = "Hava Yastığı (Yolcu)")]
        public bool HavaYastigiYolcu { get; set; }

        [Display(Name = "Immobilizer")]
        public bool Immobilizer { get; set; }

        [Display(Name = "Isofix")]
        public bool Isofix { get; set; }

        [Display(Name = "Kör Nokta Uyarı Sistemi")]
        public bool KorNoktaUyariSistemi { get; set; }

        [Display(Name = "Merkezi Kilit")]
        public bool MerkeziKilit { get; set; }

        [Display(Name = "Şerit Takip Sistemi")]
        public bool SeritTakipSistemi { get; set; }

        [Display(Name = "Yokuş Kalkış Desteği")]
        public bool YokusKalkisDesteği { get; set; }

        [Display(Name = "Yorgunluk Tespit Sistemi")]
        public bool YorgunlukTespitSistemi { get; set; }

        [Display(Name = "Zırhlı Araç")]
        public bool ZirhliArac { get; set; }

        // İç Donanım
        public bool HidrolikDireksiyon { get; set; }
        public bool UcuncuSiraKoltuklar { get; set; }
        public bool DeriKoltuk { get; set; }
        public bool KumasKoltuk { get; set; }
        public bool ElektrikliCamlar { get; set; }
        public bool Klima { get; set; }
        public bool OtmKararanDikizAynasi { get; set; }
        public bool OnGorusKamerasi { get; set; }
        public bool OnKoltukKolDayamasi { get; set; }
        public bool AnahtarsizGiris { get; set; }
        public bool FonksiyonelDireksiyon { get; set; }
        public bool IsitmaliDireksiyon { get; set; }
        public bool KoltuklarElektrikli { get; set; }
        public bool KoltuklarHafizali { get; set; }
        public bool KoltuklarIsitmali { get; set; }
        public bool KoltuklarSogutmali { get; set; }
        public bool HizSabitlemeSistemi { get; set; }
        public bool SogutmaliTorpido { get; set; }
        public bool YolBilgisayari { get; set; }
        public bool HeadUpDisplay { get; set; }
        public bool StartStop { get; set; }
        public bool GeriGorusKamerasi { get; set; }

        // Dış Donanım
        public bool AyaklaAcilanBagajKapagi { get; set; }
        public bool Hardtop { get; set; }
        public bool FarAdaptif { get; set; }
        public bool AynalarElektrikli { get; set; }
        public bool AynalarIsitmali { get; set; }
        public bool AynalarHafizali { get; set; }
        public bool ParkSensoruArka { get; set; }
        public bool ParkSensoruOn { get; set; }
        public bool ParkAsistani { get; set; }
        public bool Sunroof { get; set; }
        public bool AkilliBagajKapagi { get; set; }
        public bool PanoramikCamTavan { get; set; }
        public bool RomorkCekiDemiri { get; set; }

        // Multimedya
        public bool AndroidAuto { get; set; }
        public bool AppleCarPlay { get; set; }
        public bool Bluetooth { get; set; }
        public bool UsbAux { get; set; }

        public string? FuelType { get; set; }
        public string? Transmission { get; set; }
        public int? EngineSize { get; set; }
        public int? Horsepower { get; set; }
        public string? Color { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal CalculateEstimatedPrice()
        {
            decimal basePrice = Price; // Başlangıç fiyatı olarak mevcut fiyatı al
            decimal multiplier = 1.0m;

            // Yıl bazlı değer kaybı (her yıl için %5 değer kaybı)
            int age = DateTime.Now.Year - Year;
            multiplier *= (decimal)Math.Pow(0.95, age);

            // Kilometre bazlı değer kaybı (her 100.000 km için %10 değer kaybı)
            decimal mileageFactor = 1.0m - ((Mileage / 100000m) * 0.1m);
            multiplier *= Math.Max(0.5m, mileageFactor); // En fazla %50 değer kaybı

            // Güvenlik özellikleri için değer artışı
            if (ABS) multiplier *= 1.02m;
            if (ESP) multiplier *= 1.03m;
            if (HavaYastigiSurucu && HavaYastigiYolcu) multiplier *= 1.02m;
            if (KorNoktaUyariSistemi) multiplier *= 1.02m;
            if (SeritTakipSistemi) multiplier *= 1.02m;
            if (YorgunlukTespitSistemi) multiplier *= 1.01m;

            // Konfor özellikleri için değer artışı
            if (Klima) multiplier *= 1.02m;
            if (DeriKoltuk) multiplier *= 1.03m;
            if (ElektrikliCamlar) multiplier *= 1.01m;
            if (KoltuklarIsitmali) multiplier *= 1.02m;
            if (KoltuklarSogutmali) multiplier *= 1.02m;
            if (PanoramikCamTavan) multiplier *= 1.03m;
            if (Sunroof) multiplier *= 1.02m;

            // Teknoloji özellikleri için değer artışı
            if (AndroidAuto || AppleCarPlay) multiplier *= 1.02m;
            if (HeadUpDisplay) multiplier *= 1.02m;
            if (GeriGorusKamerasi) multiplier *= 1.01m;
            if (ParkAsistani) multiplier *= 1.01m;

            // Yakıt tipi bazlı değer artışı
            if (FuelType?.ToLower() == "elektrik") multiplier *= 1.05m;
            else if (FuelType?.ToLower() == "hibrit") multiplier *= 1.03m;

            // Vites tipi bazlı değer artışı
            if (Transmission?.ToLower() == "otomatik") multiplier *= 1.02m;

            decimal estimatedPrice = basePrice * multiplier;
            return Math.Round(estimatedPrice, 2);
        }
    }
} 