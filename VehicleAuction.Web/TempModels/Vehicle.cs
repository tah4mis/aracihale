using System;
using System.Collections.Generic;

namespace VehicleAuction.Web.TempModels;

public partial class Vehicle
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public int Mileage { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Abs { get; set; }

    public bool Asr { get; set; }

    public bool CocukKilidi { get; set; }

    public bool Distronic { get; set; }

    public bool Esp { get; set; }

    public bool GeceGorusSistemi { get; set; }

    public bool HavaYastigiSurucu { get; set; }

    public bool HavaYastigiYolcu { get; set; }

    public bool Immobilizer { get; set; }

    public bool Isofix { get; set; }

    public bool KorNoktaUyariSistemi { get; set; }

    public bool MerkeziKilit { get; set; }

    public bool SeritTakipSistemi { get; set; }

    public bool YokusKalkisDesteği { get; set; }

    public bool YorgunlukTespitSistemi { get; set; }

    public bool ZirhliArac { get; set; }

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

    public bool AndroidAuto { get; set; }

    public bool AppleCarPlay { get; set; }

    public bool Bluetooth { get; set; }

    public bool UsbAux { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
