using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vizeProje.Models;
using vizeProje.ViewModel;


namespace vizeProje.Controllers
{
    public class ServisController : ApiController
    {
        DB03Entities db = new DB03Entities();
        SonucModel sonuc = new SonucModel();

        #region Soru

        [HttpGet]
        [Route("api/soruliste")]
        public List<SoruModel> SoruListe()
        {
            List<SoruModel> liste = db.Soru.Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soru1 = x.soru1, 
                kategori=x.kategori,
                yazar=x.yazar,

                
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/sorubyid/{soruId}")]
        public SoruModel SoruById(int soruId)
        {
            SoruModel kayit = db.Soru.Where(s => s.soruId == soruId).Select(x => new SoruModel()
            {
                soru1 = x.soru1,
                kategori = x.kategori,
                yazar = x.yazar,

            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/soruekle")]
        public SonucModel SoruEkle(SoruModel model)
        {
            if (db.Soru.Count(c => c.soruId == model.soruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Soru Mevcuttur";
                return sonuc;
            }

            Soru yeni = new Soru();
            yeni.soru1 = model.soru1;
            yeni.yazar = model.yazar;
            yeni.kategori = model.kategori;



            db.Soru.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/soruduzenle")]
        public SonucModel SoruDuzenle(SoruModel model)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == model.soruId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Bulunmadı!";
                return sonuc;
            }

            kayit.soru1 = model.soru1;
            

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/sorusil/{soruId}")]
        public SonucModel SoruSil(int soruId)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == soruId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Bulunmadı!";
                return sonuc;
            }                 

            db.Soru.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Silndi";
            return sonuc;
        }

        #endregion

        #region Cevap
        [HttpGet]
        [Route("api/cevapliste")]
        public List<CevapModel> CevapListe()
        {
            List<CevapModel> liste = db.Cevap.Select(x => new CevapModel()
            {
                cevapId = x.cevapId,
                cevap1 = x.cevap1,
                cevapYazar = x.cevapYazar,


            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/cevapbyid/{cevapId}")]
        public CevapModel CevapById(int cevapId)
        {
            CevapModel kayit = db.Cevap.Where(s => s.cevapId == cevapId).Select(x => new CevapModel()
            {
                cevapId = x.cevapId,
                cevap1 = x.cevap1,
                cevapYazar = x.cevapYazar,


            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/cevapekle")]
        public SonucModel CevapEkle(CevapModel model)
        {
            if (db.Cevap.Count(c => c.cevap1 == model.cevap1) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Kayıtlıdır!";
                return sonuc;
            }

            Cevap yeni = new Cevap();
            yeni.cevapId = model.cevapId;
            yeni.cevap1 = model.cevap1;
            yeni.cevapYazar = model.cevapYazar;
            yeni.soruId = model.soruId;

            db.Cevap.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/cevapduzenle")]
        public SonucModel CevapDuzenle(CevapModel model)
        {
            Cevap kayit = db.Cevap.Where(s => s.cevapId == model.cevapId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Bulunamadı!";
                return sonuc;
            }

            kayit.cevapId = model.cevapId;
            kayit.cevap1 = model.cevap1;
            kayit.cevapYazar = model.cevapYazar;
            kayit.soruId = model.soruId;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/cevapsil/{cevapId}")]
        public SonucModel CevapSil(int cevapId)
        {
            Cevap kayit = db.Cevap.Where(s => s.cevapId == cevapId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Bulunamadı!";
                return sonuc;
            }

            

            db.Cevap.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Silindi";

            return sonuc;
        }
        #endregion

        #region Kullanici
        [HttpGet]
        [Route("api/kullanıcıliste")]
        public List<KullaniciModel> KullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {
               kullaniciId=x.kullaniciId,
               kullaiciad=x.kullaiciad,
               kullanicisoyad=x.kullanicisoyad,
               kullanicimail=x.kullanicimail,
               kullanicisifre=x.kullanicisifre,
               nickname=x.nickname,


            }).ToList();
            return liste;
        }
       

        [HttpPost]
        [Route("api/kullaniciekle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.kullaiciad == model.kullaiciad ) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kullanıcı kayıtlıdır.";
                return sonuc;
            }

            Kullanici yeni = new Kullanici();
            yeni.kullaniciId = model.kullaniciId;
            yeni.kullaiciad = model.kullaiciad;
            yeni.kullanicisoyad = model.kullanicisoyad;
            yeni.kullanicimail = model.kullanicimail;
            yeni.kullanicisifre = model.kullanicisifre;
            yeni.nickname = model.nickname;


            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı kaydedildi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.kullaniciId == model.kullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }


            kayit.kullaniciId = model.kullaniciId;
            kayit.kullaiciad = model.kullaiciad;
            kayit.kullanicisoyad = model.kullanicisoyad;
            kayit.kullanicimail = model.kullanicimail;
            kayit.kullanicisifre = model.kullanicisifre;
            kayit.nickname = model.nickname;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kullanicisil/{kullaniciId}")]
        public SonucModel KullaniciSil(int kullaniciId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.kullaniciId == kullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }

            if (db.Kullanici.Count(c => c.kullaniciId == kullaniciId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aktif kullanici silinemez";
                return sonuc;
            }

            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Silindi";

            return sonuc;
        }
        #endregion

        #region Kategori
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(c => c.kategoriId == model.kategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kategori bulunuyor";
                return sonuc;
            }

            Kategori yeni = new Kategori();
            yeni.kategoriId = model.kategoriId;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]
        public SonucModel KategoriSil(int kategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }


            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";

            return sonuc;
        }
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategori1 = x.kategori1,

            }).ToList();
            return liste;
        }
        #endregion
    }
}
