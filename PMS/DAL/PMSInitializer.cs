using PMS.DAL;
using PMS.Migrations;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL
{
    public class PMSInitializer : MigrateDatabaseToLatestVersion<PMSContext, Configuration>
    {
        //protected override void Seed(OnlineShopContext context)
        //{
        //    SeedOnlineShop(context);
        //    base.Seed(context);
        //}
        /// <summary>
        /// Ta metoda jest wywoływana w Configuration po dodaniu migracji, można ją tam przenieść całą
        /// </summary>
        /// <param name="context"></param>
        public static void SeedPMS(PMSContext context)
        {

            var userRole = new List<UserRole>
                {
                    new UserRole() { UserRoleID=1, Name="Admin"},
                    new UserRole() { UserRoleID=2, Name="Manager"},
                    new UserRole() { UserRoleID=3, Name="Senior"}
                };
            
            userRole.ForEach(ur => context.UserRole.AddOrUpdate(ur));
            context.SaveChanges();


            var user = new List<User>
                {
                    new User() { UserID=1, UserRoleID=1, FirstName="Daniel", LastName="Brzeźnicki", Salary=8000.00, 
                        Login="admin1", Password="admin1", PhoneNumber="604-419-555", Email="d.brzeznicki@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Przemysłowa", ResidenceHouseNumber="7", ResidenceApartmentNumber=null, ResidencePostcode="08-410", ResidenceCity="Wola Rębkowska",
                        CorrespondenceStreet="Stefana Żeromskiego", CorrespondenceHouseNumber="42", CorrespondenceApartmentNumber="7", CorrespondencePostcode="25-370", CorrespondenceCity="Kielce"
                    },

                    new User() { UserID=2, UserRoleID=2, FirstName="Kamil", LastName="Manager", Salary=6000.00,
                        Login="manager1", Password="manager1", PhoneNumber="6s4-569-342", Email="k.manager@wp.pl",
                        AccountCreationDate=new DateTime(2010, 04, 25), HiredDate=new DateTime(2010, 04, 24), FiredDate=new DateTime(2015, 04, 24),
                        ResidenceStreet="Długa", ResidenceHouseNumber="23", ResidenceApartmentNumber=null, ResidencePostcode="08-400", ResidenceCity="Garwolin",
                        CorrespondenceStreet="Stefana Okrzei", CorrespondenceHouseNumber="64A", CorrespondenceApartmentNumber="6", CorrespondencePostcode="25-360", CorrespondenceCity="Kielce"
                    },
                    new User() { UserID=3, UserRoleID=2, FirstName="Patryk", LastName="Kowalski", Salary=6320.00,
                        Login="manager2", Password="manager2", PhoneNumber="432-666-123", Email="p.kowalski@gmail.com",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Krótka", ResidenceHouseNumber="12", ResidenceApartmentNumber="2", ResidencePostcode="00-310", ResidenceCity="Skarżysko Kamienna",
                        CorrespondenceStreet="Warszawska", CorrespondenceHouseNumber="142", CorrespondenceApartmentNumber=null, CorrespondencePostcode="23-362", CorrespondenceCity="Starachowice"
                    },

                    new User() { UserID=4, UserRoleID=3, FirstName="Marek", LastName="Nowak", Salary=4000.00,
                        Login="employee1", Password="employee1", PhoneNumber="333-222-765", Email="m.nowak@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Radomska", ResidenceHouseNumber="33", ResidenceApartmentNumber=null, ResidencePostcode="08-555", ResidenceCity="Warszawa",
                        CorrespondenceStreet="Radomska", CorrespondenceHouseNumber="33", CorrespondenceApartmentNumber=null, CorrespondencePostcode="08-555", CorrespondenceCity="Warszawa"
                    },
                    new User() { UserID=5, UserRoleID=3, FirstName="Karol", LastName="Zaporowicz", Salary=3400.00,
                        Login="employee2", Password="employee2", PhoneNumber="604-443-123", Email="k.zaporowicz@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Zakopianka", ResidenceHouseNumber="7", ResidenceApartmentNumber=null, ResidencePostcode="08-610", ResidenceCity="Zakopianka",
                        CorrespondenceStreet="Zakopianka", CorrespondenceHouseNumber="7", CorrespondenceApartmentNumber=null, CorrespondencePostcode="08-610", CorrespondenceCity="Zakopianka"
                    },
                    new User() { UserID=6, UserRoleID=3, FirstName="Daniel", LastName="Szaniawski", Salary=4200.00,
                        Login="employee3", Password="employee3", PhoneNumber="634-333-523", Email="d.szaniawski@interia.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Bezowa", ResidenceHouseNumber="43", ResidenceApartmentNumber="5", ResidencePostcode="02-222", ResidenceCity="Gdańsk",
                        CorrespondenceStreet="Kwiatowa", CorrespondenceHouseNumber="71", CorrespondenceApartmentNumber="4", CorrespondencePostcode="25-856", CorrespondenceCity="Kielce"
                    },
                    new User() { UserID=7, UserRoleID=3, FirstName="Grzegorz", LastName="Kołdys", Salary=5000.00,
                        Login="employee4", Password="employee4", PhoneNumber="213-324-543", Email="g.koldys@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Lipowa", ResidenceHouseNumber="1", ResidenceApartmentNumber="2", ResidencePostcode="02-410", ResidenceCity="Wola Rębkowska",
                        CorrespondenceStreet="Lipowa", CorrespondenceHouseNumber="1", CorrespondenceApartmentNumber="2", CorrespondencePostcode="02-410", CorrespondenceCity="Wola Rębkowska"
                    },
                    new User() { UserID=8, UserRoleID=3, FirstName="Kamil", LastName="Olejnik", Salary=4500.00,
                        Login="employee5", Password="employee5", PhoneNumber="235-419-635", Email="k.olejnik@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Słoneczna", ResidenceHouseNumber="7", ResidenceApartmentNumber=null, ResidencePostcode="08-410", ResidenceCity="Wola Rębkowska",
                        CorrespondenceStreet="Słoneczna", CorrespondenceHouseNumber="42", CorrespondenceApartmentNumber="7", CorrespondencePostcode="08-410", CorrespondenceCity="Wola Rębkowska"
                    },
                    new User() { UserID=9, UserRoleID=3, FirstName="Adrianna", LastName="Czyżewska", Salary=5500.00,
                        Login="employee6", Password="employee6", PhoneNumber="602-413-555", Email="a.czyzewska@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=null,
                        ResidenceStreet="Osadowa", ResidenceHouseNumber="9", ResidenceApartmentNumber=null, ResidencePostcode="08-330", ResidenceCity="Pilawa",
                        CorrespondenceStreet="Osadowa", CorrespondenceHouseNumber="9", CorrespondenceApartmentNumber=null, CorrespondencePostcode="08-330", CorrespondenceCity="Pilawa"
                    }
                };
            user.ForEach(u => context.User.AddOrUpdate(u));
            context.SaveChanges();



        }
    }
}
