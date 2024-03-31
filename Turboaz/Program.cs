using System;
using Turboaz.Enums;
using TurboAz.enums;
using TurboAz.Enums;
using TurboAz.Extensions;
using TurboAz.Models.Auditable;
using TurboAz.Models.Data;
using TurboAz.Models.Entities;

namespace TurboAz
{
    internal class Program
    {
        static AppDbContext db = new AppDbContext();
        private static object item;

        static void Main(string[] args)
        {
            
            do
            {
                Menu answer = Helper.ChooseOption<Menu>("Menu-dan secim edin:");


                switch (answer)
                {
                    case Menu.GetAllMarka:
                        GetAllMarka();
                        break;
                    case Menu.AddNewMarka:
                        AddNewMarka();
                        break;
                    case Menu.DeleteMarka:
                        DeleteMarka();
                        break;
                    case Menu.GetMarkaByID:
                        GetMarkaById();
                        break;
                    case Menu.EditMarka:
                        EditMarka();
                        break;
                    case Menu.AddNewModel:
                        AddNewModel();
                        break;
                    case Menu.DeleteModel:
                        DeleteModel();
                        break;
                    case Menu.GetAllModels:
                        GetAllModels();
                        break;
                    case Menu.GetModelById:
                        GetModelById();
                        break;
                    case Menu.EditModel:
                        EditModel();
                        break;
                    case Menu.AddAnnouncement:
                        AddAnnouncement();
                        break;
                    case Menu.DeleteAnnouncement:
                        DeleteAnnouncement();
                        break;
                    case Menu.EditAnnouncement:
                        EditAnnouncement();
                        break;
                        case Menu.GetAnnouncementById:
                        GetAnnouncementById(); 
                        break;
                    case Menu.GetAllAnnouncement:
                        GetAllAnnouncement();
                        break;
                    default:
                        break;
                }

            } while (true);


        }

        private static void AddAnnouncement()
        {
            int modelId;
            double price;
            int march;
            int fuelTypeNum;
            int gearBoxNum;
            int transmissionNum;
            int bannerNum;
            string dateString;

            Console.WriteLine("Elan yaratmaq ucun Modellerden birini secin");

            List<Model> models = db.Models.ToList();
            List<Announcement> announcements = db.Announcements.ToList();

            foreach (var item in models)
            {
                Console.WriteLine($"Id - {item.Id} Adi - {item.Name}  Marka - {item.MarkaId}");
            }
        l1:
            modelId = Helper.ReadInt("Modelin Id-sini daxil edin", "Sehv daxil etdiniz");
            Model? model = models.FirstOrDefault(m => m.Id == modelId);
            if (model == null)
            {
                Console.WriteLine("Secdiyiniz Id-ile model yoxdur !");
                goto l1;
            }

        l2:
            price = Helper.ReadDouble("Qiymeti daxil edin", "Sehv daxil etdiniz!");
            if (price < 300)
            {
                Console.WriteLine("Daxil etdiyiniz giymet minimumdan balacadi!");
                goto l2;
            }
        l3:
            march = Helper.ReadInt("Avtomobilin yurushunu daxil edin!", "Sehv daxil etdiniz!");
            if (march < 0)
            {
                Console.WriteLine("Yurush 0-dan balaca ola bilmez!");
                goto l3;
            }


            foreach (var item in Enum.GetValues(typeof(FuelType)))
            {
                Console.WriteLine($"{(int)item}-{item}");
            }
            FuelType fuelType;
        l4:
            fuelTypeNum = Helper.ReadInt("FuelType Secin:", "Sehv daxil etdiniz!");

            if (Enum.IsDefined(typeof(FuelType), fuelTypeNum))
            {
                fuelType = (FuelType)fuelTypeNum;
            }
            else
            {
                Console.WriteLine("Sehv secim etdiniz1 yeniden cehd edin!");
                goto l4;
            }

            GearBox gearBox;
        l5:
            foreach (var item in Enum.GetValues(typeof(GearBox)))
            {
                Console.WriteLine($"{(int)item}-{item}");
            }
            gearBoxNum = Helper.ReadInt("Suretler qutusunu Secin:", "Sehv daxil etdiniz!");

            if (Enum.IsDefined(typeof(GearBox), gearBoxNum))
            {
                gearBox = (GearBox)gearBoxNum;
            }
            else
            {
                Console.WriteLine("Sehv secim etdiniz1 yeniden cehd edin!");
                goto l5;
            }

            Transmission transmission;
            foreach (var item in Enum.GetValues(typeof(Transmission)))
            {
                Console.WriteLine($"{(int)item}-{item}");
            }
        l6:
            transmissionNum = Helper.ReadInt("Oturucunu Secin:", "Sehv daxil etdiniz!");

            if (Enum.IsDefined(typeof(Transmission), transmissionNum))
            {
                transmission = (Transmission)transmissionNum;
            }
            else
            {
                Console.WriteLine("Sehv secim etdiniz1 yeniden cehd edin!");
                goto l6;
            }
        l7:
            Banner banner;
            foreach (var item in Enum.GetValues(typeof(Banner)))
            {
                Console.WriteLine($"{(int)item}-{item}");
            }

            bannerNum = Helper.ReadInt("Ban novunu Secin:", "Sehv daxil etdiniz!");

            if (Enum.IsDefined(typeof(Banner), bannerNum))
            {
                banner = (Banner)bannerNum;
            }
            else
            {
                Console.WriteLine("Sehv secim etdiniz1 yeniden cehd edin!");
                goto l7;
            }

            int year;
        l8:
            year = Helper.ReadInt("Ili daxil edin:", "Il sehvdir!");
            if (year < 1900)
            {
                Console.WriteLine("Il 1900'dan asagi ola bilmez!");
                goto l8;
            }

            Announcement announcement = new Announcement();
            announcement.Banner = banner;
            announcement.Transmission = transmission;
            announcement.Price = price;
            announcement.GearBox = gearBox;
            announcement.FuelType = fuelType;
            announcement.March = march;
            announcement.ModelId = modelId;
            announcement.Year = year;

            announcement.CreatedBy = 1;
            announcement.CreatedAt = DateTime.Now;

            db.Announcements.Add(announcement);
            db.SaveChanges();

            Console.Clear();
            Console.WriteLine("Yeni elan elave edildi !");
            Console.WriteLine($"Info : Id-{announcement.Id}\n Banner-{announcement.Banner}\n Yurush - {announcement.March} \n" +
                $" Suretler qutusu novu - {announcement.GearBox}\n Fuel Type - {announcement.FuelType}\n Modeli - {announcement.ModelId}\n" +
                $"Marka - {announcement.ModelId}\n  Qiymeti - {announcement.Price}\n Oturucu novu - {announcement.Transmission}\n Avtomobilin ilin: {announcement.Year}");

        }

        private static void DeleteAnnouncement()
        {
            List<Announcement> announcements = db.Announcements.ToList();
            int deleteId;
            Console.WriteLine();
            foreach (var item in announcements)
            {
                Console.WriteLine($"Id - {item.Id}  March - {item.March} Price - {item.Price} ModelId - {item.ModelId} Banner - {item.Banner} FuelType - {item.FuelType} GearBox - {item.GearBox} Transmission - {item.Transmission} Year - {item.Year}");
            }
        l1:
            deleteId = Helper.ReadInt("Silmek istediyiniz modelin Id-sini daxil edin!", "Sehv daxil etdiniz !");
            Announcement announcement = announcements.FirstOrDefault(m => m.Id == deleteId);
            if (announcement is null)
            {
                Console.WriteLine("Daxil etdiyiniz Id- ile model movcud deil!");
                goto l1;
            }

            announcement.DeletedAt = DateTime.Now;
            announcement.DeletedBy = 1;
            db.SaveChanges();
            Console.WriteLine("Silindi!\n");

        }

        private static void EditAnnouncement()
        {

            List<Model> models = db.Models.ToList();
            List<Marka> marks = db.Marks.ToList();
            List<Announcement> announcements = db.Announcements.ToList();

            foreach (var item in announcements)
            {
                Console.WriteLine($"Id - {item.Id}  March - {item.March} Price - {item.Price} ModelId - {item.ModelId} Banner - {item.Banner} FuelType - {item.FuelType} GearBox - {item.GearBox} Transmission - {item.Transmission} Year - {item.Year}");
            }
        l1:
            int announcementId = Helper.ReadInt("Duzelish etmek istediyiniz elanın Id-sini daxil edin !", "Sehv daxil etdiniz");
            Announcement announcement = announcements.FirstOrDefault(m => m.Id == announcementId);
            if (announcement == null)
            {
                Console.WriteLine($"{announcement} - Id li Elan siyahida yoxdur!");
                goto l1;
            }

            int newModelId = Helper.ReadInt("Elanın yeni modelini daxil edin!", "Sehv daxil etdiniz");

            foreach (var item in announcements)
            {
                Console.WriteLine($"Id - {item.Id}, Model - {item.ModelId}");
            }

            int markaId;


            announcement.ModelId = newModelId;


            announcement.LastModifiedAt = DateTime.Now;
            announcement.LastModifiedBy = 1;

            db.SaveChanges();

            Console.WriteLine("Deyisiklik edildi ! \n");
        }
        private static void GetAnnouncementById()
        {
            int announcementId;
            List<Announcement> announcements = db.Announcements.ToList();

        l1:
            announcementId = Helper.ReadInt("Tapmaq istediyiniz Elanin Id-sini daxil edin", "Sehv daxil etdiniz");

            Announcement announcement = announcements.FirstOrDefault(x => x.Id == announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Bu Id-ile model tapilmadi!");
                goto l1;
            }

            Console.WriteLine($"Id - {announcement.Id}  March - {announcement.March} Price - {announcement.Price} ModelId - {announcement.ModelId} Banner - {announcement.Banner} FuelType - {announcement.FuelType} GearBox - {announcement.GearBox} Transmission - {announcement.Transmission} Year - {announcement.Year}");
        
            Console.WriteLine("\n");

        }

        private static void GetAllAnnouncement()
        {
            List<Announcement> announcements = db.Announcements.ToList();
            if (announcements.Count > 0)
            {
                foreach (var item in announcements)
                {
                    Console.WriteLine($"Id - {item.Id},  - ModelId {item.ModelId}");
                }
            }
            else
            {
                Console.WriteLine("Elan siyahisi boshdu ! \n");
            }


        }


        private static void EditModel()
        {
            int modelId;
            List<Model> models = db.Models.ToList();
            List<Marka> marks = db.Marks.ToList();

            foreach (var item in models)
            {
                Console.WriteLine($"Id - {item.Id} Adi - {item.Name}  Marka - {item.MarkaId}");
            }
        l1:
            modelId = Helper.ReadInt("Duzelish etmek istediyiniz modelin Id-sini daxil edin !", "Sehv daxil etdiniz");
            Model? model = models.FirstOrDefault(m => m.Id == modelId);
            if (model == null)
            {
                Console.WriteLine($"{modelId} - Id li Model siyahida yoxdur!");
                goto l1;
            }

            string newModelName = Helper.ReadString("Modelin yeni adini daxil edin!", "Sehv daxil etdiniz");

            foreach (var item in marks)
            {
                Console.WriteLine($"Id - {item.Id}, Adi - {item.Name}");
            }

            int markaId;

        l2:
            markaId = Helper.ReadInt("Yeni markanin Id-sini daxil ele", "Sehv daxil etdiniz!");
            Marka? marka = marks.FirstOrDefault(m => m.Id == markaId);
            if (marka == null)
            {
                Console.WriteLine($"{markaId} - Id li Marka siyahida yoxdur!");
                goto l2;
            }

            model.MarkaId = markaId;
            model.Name = newModelName;

            model.LastModifiedAt = DateTime.Now;
            model.LastModifiedBy = 1;

            db.SaveChanges();

            Console.WriteLine("Deyisiklik edildi ! \n");


        }

        private static void GetModelById()
        {
            int modelId;
            List<Model> models = db.Models.ToList();

        l1:
            modelId = Helper.ReadInt("Tapmaq istediyiniz Modelin Id-sini daxil edin", "Sehv daxil etdiniz");

            Model model = models.FirstOrDefault(x => x.Id == modelId);
            if (model == null)
            {
                Console.WriteLine("Bu Id-ile model tapilmadi!");
                goto l1;
            }

            Console.WriteLine($"Id - {model.Id} Adi - {model.Name}  Markasi - {model.MarkaId}");
            Console.WriteLine("\n");

        }

        private static void GetAllModels()
        {
            List<Model> models = db.Models.ToList();
            if (models.Count < 1)
            {
                Console.WriteLine("Model siyahisi boshdur!");
                return;
            }

            foreach (var item in models)
            {
                Console.WriteLine($"Id - {item.Id} Adi - {item.Name}  Marka - {item.MarkaId}");
            }

            Console.WriteLine("\n");

        }

        private static void DeleteModel()
        {
            List<Model> models = db.Models.ToList();
            int deleteId;
            Console.WriteLine();
            foreach (var item in models)
            {
                Console.WriteLine($"Id - {item.Id} Adi - {item.Name}  Marka - {item.MarkaId}");
            }
        l1:
            deleteId = Helper.ReadInt("Silmek istediyiniz modelin Id-sini daxil edin!", "Sehv daxil etdiniz !");
            Model model = models.FirstOrDefault(m => m.Id == deleteId);
            if (model is null)
            {
                Console.WriteLine("Daxil etdiyiniz Id- ile model movcud deil!");
                goto l1;
            }

            model.DeletedAt = DateTime.Now;
            model.DeletedBy = 1;
            db.SaveChanges();
            Console.WriteLine("Silindi!\n");

        }

        private static void AddNewModel()
        {
            List<Marka> marks = db.Marks.ToList();
            if (marks.Count < 1)
            {
                Console.WriteLine("Marka siyahisi boshdu ! Zehmet olmasa Marka elave edin!");
                return;
            }

            string modelName = Helper.ReadString("Modelin adini daxil edin :", "Sehv daxil etdiniz");
            int markaId;
            foreach (var item in marks)
            {
                Console.WriteLine($"Id - {item.Id}, Adi - {item.Name}");
            }

        l1:
            markaId = Helper.ReadInt("Modelin Markasini secin !", "Sehv daxil etdiniz !");
            Marka marka = marks.FirstOrDefault(m => m.Id == markaId);
            if (marka is null)
            {
                Console.WriteLine("Sehv Id- daxil etmisiz!");
                goto l1;
            }

            Model model = new Model();
            model.MarkaId = markaId;
            model.Name = modelName;
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = 1;

            db.Models.Add(model);
            db.SaveChanges();

            Console.WriteLine("Elave olundu ! \n");

        }

        private static void EditMarka()
        {
            List<Marka> marks = db.Marks.ToList();
            int markaId;
            foreach (Marka item in marks)
            {
                Console.WriteLine($"Id - {item.Id}, Adi - {item.Name}");
            }
        l1:
            markaId = Helper.ReadInt("Deyisiklik etmek istediyiniz Markanin Id-sini daxil edin", "Sehv daxil etdiniz");
            Marka marka = marks.FirstOrDefault(m => m.Id == markaId);
            if (marka is null)
            {
                Console.WriteLine($"{markaId}-li marka tapilmadi");
                goto l1;

            }

            string newMarkaName = Helper.ReadString("Markanin yeni adini daxil edin:", "Sehv daxil etdiniz");
            marka.Name = newMarkaName;
            marka.LastModifiedAt = DateTime.Now;
            db.SaveChanges();

            Console.WriteLine("Deyisiklik edildi! \n");

        }

        private static void GetMarkaById()
        {
            List<Marka> marks = db.Marks.ToList();
            int markaId = Helper.ReadInt("Markanin Id-sini daxil edin", "Sehv daxil etdiniz");
            Marka marka = marks.FirstOrDefault(m => m.Id == markaId);
            if (marka is null)
            {
                Console.WriteLine($"{markaId}-li marka tapilmadi");
            }
            else
            {
                Console.WriteLine($"Id - {marka.Id} Adi - {marka.Name} \n");
            }

        }

        private static void DeleteMarka()
        {
            List<Marka> marks = db.Marks.ToList();
            if (marks.Count < 1)
            {

                Console.WriteLine("Siyahida marka yoxdu !");
                return;
            }

        l1:
            int markaId = Helper.ReadInt("Markanin Id-sini daxil edin", "Sehv daxil etdiniz");
            Marka marka = marks.FirstOrDefault(m => m.Id == markaId);

            if (marka is null)
            {
                Console.WriteLine($"{markaId}-li marka tapilmadi");
                goto l1;
                
            }

            marka.DeletedAt = DateTime.Now;
            marka.DeletedBy = 1;

            db.SaveChanges();
            Console.WriteLine("Silindi ! \n");

        }

        private static void GetAllMarka()
        {
            List<Marka> marks = db.Marks.ToList();
            if (marks.Count > 0)
            {
                foreach (var item in marks)
                {
                    Console.WriteLine($"Id - {item.Id}, Adi - {item.Name}");
                }
            }
            else
            {
                Console.WriteLine("Marka siyahisi boshdu ! \n");
            }


        }

        private static void AddNewMarka()
        {
            string markaName;
        l1:
            Console.Write("Markanin adini daxil edin: ");
            markaName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(markaName) || markaName.Length < 2)
            {
                Console.WriteLine("Bosh olmaz ve minimum simvol 3 eded !");
                goto l1;
            }

            Marka marka = new Marka()
            {
                Name = markaName,
                CreatedAt = DateTime.Now,
                CreatedBy=1
            };

            db.Marks.Add(marka);
            db.SaveChanges();

            Console.WriteLine("Elave olundu! \n");

        }
    }
}
