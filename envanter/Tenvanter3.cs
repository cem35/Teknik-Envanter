namespace DegerlendirmeSoruları
{
    using System;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            int müşteriNumarası = 15000000;

            ÇalıştırmaMotoru.KomutÇalıştır("MuhasebeModulu", "MaaşYatır", new object[] { müşteriNumarası });

            ÇalıştırmaMotoru.KomutÇalıştır("MuhasebeModulu", "YıllıkÜcretTahsilEt", new object[] { müşteriNumarası });

            ÇalıştırmaMotoru.BekleyenİşlemleriGerçekleştir();
        }
    }

    public class ÇalıştırmaMotoru
    {

        public static object[] KomutÇalıştır(string modülSınıfAdı, string methodAdı, object[] inputs)
        {
            // ? 

           switch (modülSınıfAdı)
           {
                case "MaaşYatır":
                    MuhasebeModülü.MaaşYatır(inputs.müşteriNumarası);
                    break;

                case "YıllıkÜcretTahsilEt":
                    MuhasebeObjesi muhasebeObje = new MuhasebeObjesi
                    {
                        ModuleClassName = modülSınıfAdı,
                        MethodName = methodAdı,
                        CustomerNo = inputs.müşteriNumarası,
                        Status = 0;
                    };                    
                    Veritabanıİşlemleri.Ekle(muhasebeObje);
                    break;
                case "OtomatikÖdemeleriGerçekleştir":
                    MuhasebeModülü.OtomatikÖdemeleriGerçekleştir(inputs.müşteriNumarası);
                    break;
                default:
                    Console.WriteLine(string.Format("{0} numaralı müşteri için geçerli bir işlem seçilmemiştir.", müşteriNumarası));
                    break;  
           }


            //throw new NotImplementedException();
           /*
            sınıf isimlerinin orjinalleri türkçe karakter içierdiği için bu şekilde devam edilmiştir.
           */
        }

        public static void BekleyenİşlemleriGerçekleştir()
        {
            MuhasebeObjesi muhasebeObje = Veritabanıİşlemleri.Getir();
             while( muhasebeObje != null)
             {
                muhasebeObje = Veritabanıİşlemleri.Getir();
             }
        }
    }

    public class MuhasebeModülü
    {
        private void MaaşYatır(int müşteriNumarası)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine(string.Format("{0} numaralı müşterinin maaşı yatırıldı.", müşteriNumarası));
        }

        private void YıllıkÜcretTahsilEt(int müşteriNumarası)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine("{0} numaralı müşteriden yıllık kart ücreti tahsil edildi.", müşteriNumarası);
        }

        private void OtomatikÖdemeleriGerçekleştir(int müşteriNumarası)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine("{0} numaralı müşterinin otomatik ödemeleri gerçekleştirildi.", müşteriNumarası);
        }
    }

    public class Veritabanıİşlemleri
    {
        
        /*CREATE TABLE WaitingOperations(
            Id int primary key IDENTITY (1, 1),
            ModuleClassName varchar(255),
            MethodName varchar(255),
            CustomerNo int,
            Status int);*/

/* 
entitiyframe work olduğu varsayılmıştır. Ek olarak muhasebeobjesi adında bir entity olduğu varsayılmıştır.
*/
            public bool Ekle(MuhasebeObjesi item)
            {
                try
                {
                    using (MuhasebeContext context = new MuhasebeContext())
                    {
                        var addedEntitiy = context.Entry(entity);
                        addedEntitiy.State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("{0} numaralı müşterinin {1} işleminde hata oluşmuştur. Hata: {2}", item.müşteriNumarası, item.methodAdı, exception.Message);
                    return false;
                }
            return true;
                
            }
            public MuhasebeObjesi Getir()
            {
                MuhasebeObjesi donusObjesi = new MuhasebeObjesi();
                try
                {
                    using (MuhasebeContext context = new MuhasebeContext())
                    {
                         donusObjesi = context.WaitingOperations.where(x => x.Status == 0).SingleOrDefault();
                    }
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("Bekleyen işlem bulunmamaktadır");
                    return null;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("{0} numaralı müşterinin {1} işleminde hata oluşmuştur. Hata: {2}", donusObjesi.müşteriNumarası, donusObjesi.methodAdı, exception.Message);
                    return null;
                }
                finally
                {
                    donusObjesi.Status = 1;
                    using (MuhasebeContext context = new MuhasebeContext())
                    {
                        var updatedEntity = context.Entry(donusObjesi);
                        updatedEntity.State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                return donusObjesi;
            }

    }
}