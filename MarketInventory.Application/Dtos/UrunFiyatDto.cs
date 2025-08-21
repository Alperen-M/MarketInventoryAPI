using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketInventory.Application.DTOs.UrunFiyat
{
    public class UrunFiyatDto
    {
        public int Id { get; set; }
        public bool AktifMi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? SonTarih { get; set; }
        public decimal Fiyat { get; set; }
        public int? UrunId { get; set; }
        public string? UrunAdi { get; set; }
    }

    public class CreateUrunFiyatDto
    {
        public bool AktifMi { get; set; } = true;
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? SonTarih { get; set; }
        public decimal Fiyat { get; set; }
        public int? UrunId { get; set; }
    }

    public class UpdateUrunFiyatDto
    {
        public int Id { get; set; }
        public bool AktifMi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? SonTarih { get; set; }
        public decimal Fiyat { get; set; }
        public int? UrunId { get; set; }
    }
}