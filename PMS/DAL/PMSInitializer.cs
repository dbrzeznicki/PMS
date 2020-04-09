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
                    new UserRole() { UserRoleID=3, Name="Employee"}
                };

            userRole.ForEach(ur => context.UserRole.AddOrUpdate(ur));
            context.SaveChanges();

            var projectStatus = new List<ProjectStatus>
                {
                    new ProjectStatus() { ProjectStatusID=1, Name="New"},
                    new ProjectStatus() { ProjectStatusID=2, Name="In progress"},
                    new ProjectStatus() { ProjectStatusID=3, Name="Completed"}
                };

            projectStatus.ForEach(ps => context.ProjectStatus.AddOrUpdate(ps));
            context.SaveChanges();

            var subtaskStatus = new List<SubtaskStatus>
                {
                    new SubtaskStatus() { SubtaskStatusID=1, Name="New"},
                    new SubtaskStatus() { SubtaskStatusID=2, Name="In progress"},
                    new SubtaskStatus() { SubtaskStatusID=3, Name="Completed"}
                };

            subtaskStatus.ForEach(ss => context.SubtaskStatus.AddOrUpdate(ss));
            context.SaveChanges();

            var vacationType = new List<VacationType>
                {
                    new VacationType() { VacationTypeID=1, Name="Holiday leave"},
                    new VacationType() { VacationTypeID=2, Name="Maternity leave"},
                    new VacationType() { VacationTypeID=3, Name="Parental leave"},
                    new VacationType() { VacationTypeID=4, Name="Unpaid leave"},
                    new VacationType() { VacationTypeID=5, Name="L4"}
                };

            vacationType.ForEach(vt => context.VacationType.AddOrUpdate(vt));
            context.SaveChanges();


            var team = new List<Team>
                {
                    new Team() { TeamID = 1, Name="Team 1", NumberOfPeople=3, NumberOfProjects=2},
                    new Team() { TeamID = 2, Name="Team 2", NumberOfPeople=5, NumberOfProjects=1}
                };

            team.ForEach(t => context.Team.AddOrUpdate(t));
            context.SaveChanges();


            var user = new List<User>
                {
                    new User() { UserID=1, UserRoleID=1, TeamID=null, FirstName="Daniel", LastName="Brzeźnicki", Salary=8000.00,
                        Login="admin1", Password="admin1", PhoneNumber="604-419-555", Email="d.brzeznicki@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Przemysłowa", ResidenceHouseNumber="7", ResidenceApartmentNumber=null, ResidencePostcode="08-410", ResidenceCity="Wola Rębkowska",
                        CorrespondenceStreet="Stefana Żeromskiego", CorrespondenceHouseNumber="42", CorrespondenceApartmentNumber="7", CorrespondencePostcode="25-370", CorrespondenceCity="Kielce"
                    },

                    new User() { UserID=2, UserRoleID=2, TeamID=1, FirstName="Kamil", LastName="Manager", Salary=6000.00,
                        Login="manager1", Password="manager1", PhoneNumber="6s4-569-342", Email="k.manager@wp.pl",
                        AccountCreationDate=new DateTime(2010, 04, 25), HiredDate=new DateTime(2010, 04, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Długa", ResidenceHouseNumber="23", ResidenceApartmentNumber=null, ResidencePostcode="08-400", ResidenceCity="Garwolin",
                        CorrespondenceStreet="Stefana Okrzei", CorrespondenceHouseNumber="64A", CorrespondenceApartmentNumber="6", CorrespondencePostcode="25-360", CorrespondenceCity="Kielce"
                    },
                    new User() { UserID=3, UserRoleID=2, TeamID=2, FirstName="Patryk", LastName="Kowalski", Salary=6320.00,
                        Login="manager2", Password="manager2", PhoneNumber="432-666-123", Email="p.kowalski@gmail.com",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Krótka", ResidenceHouseNumber="12", ResidenceApartmentNumber="2", ResidencePostcode="00-310", ResidenceCity="Skarżysko Kamienna",
                        CorrespondenceStreet="Warszawska", CorrespondenceHouseNumber="142", CorrespondenceApartmentNumber=null, CorrespondencePostcode="23-362", CorrespondenceCity="Starachowice"
                    },

                    new User() { UserID=4, UserRoleID=3, TeamID=1, FirstName="Marek", LastName="Nowak", Salary=4000.00,
                        Login="employee1", Password="employee1", PhoneNumber="333-222-765", Email="m.nowak@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Radomska", ResidenceHouseNumber="33", ResidenceApartmentNumber=null, ResidencePostcode="08-555", ResidenceCity="Warszawa",
                        CorrespondenceStreet="Radomska", CorrespondenceHouseNumber="33", CorrespondenceApartmentNumber=null, CorrespondencePostcode="08-555", CorrespondenceCity="Warszawa"
                    },
                    new User() { UserID=5, UserRoleID=3, TeamID=1, FirstName="Karol", LastName="Zaporowicz", Salary=3400.00,
                        Login="employee2", Password="employee2", PhoneNumber="604-443-123", Email="k.zaporowicz@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Zakopianka", ResidenceHouseNumber="7", ResidenceApartmentNumber=null, ResidencePostcode="08-610", ResidenceCity="Zakopianka",
                        CorrespondenceStreet="Zakopianka", CorrespondenceHouseNumber="7", CorrespondenceApartmentNumber=null, CorrespondencePostcode="08-610", CorrespondenceCity="Zakopianka"
                    },
                    new User() { UserID=6, UserRoleID=3, TeamID=2, FirstName="Daniel", LastName="Szaniawski", Salary=4200.00,
                        Login="employee3", Password="employee3", PhoneNumber="634-333-523", Email="d.szaniawski@interia.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Bezowa", ResidenceHouseNumber="43", ResidenceApartmentNumber="5", ResidencePostcode="02-222", ResidenceCity="Gdańsk",
                        CorrespondenceStreet="Kwiatowa", CorrespondenceHouseNumber="71", CorrespondenceApartmentNumber="4", CorrespondencePostcode="25-856", CorrespondenceCity="Kielce"
                    },
                    new User() { UserID=7, UserRoleID=3, TeamID=2, FirstName="Grzegorz", LastName="Kołdys", Salary=5000.00,
                        Login="employee4", Password="employee4", PhoneNumber="213-324-543", Email="g.koldys@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Lipowa", ResidenceHouseNumber="1", ResidenceApartmentNumber="2", ResidencePostcode="02-410", ResidenceCity="Wola Rębkowska",
                        CorrespondenceStreet="Lipowa", CorrespondenceHouseNumber="1", CorrespondenceApartmentNumber="2", CorrespondencePostcode="02-410", CorrespondenceCity="Wola Rębkowska"
                    },
                    new User() { UserID=8, UserRoleID=3, TeamID=2, FirstName="Kamil", LastName="Olejnik", Salary=4500.00,
                        Login="employee5", Password="employee5", PhoneNumber="235-419-635", Email="k.olejnik@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Słoneczna", ResidenceHouseNumber="7", ResidenceApartmentNumber=null, ResidencePostcode="08-410", ResidenceCity="Wola Rębkowska",
                        CorrespondenceStreet="Słoneczna", CorrespondenceHouseNumber="42", CorrespondenceApartmentNumber="7", CorrespondencePostcode="08-410", CorrespondenceCity="Wola Rębkowska"
                    },
                    new User() { UserID=9, UserRoleID=3, TeamID=2, FirstName="Adrianna", LastName="Czyżewska", Salary=5500.00,
                        Login="employee6", Password="employee6", PhoneNumber="602-413-555", Email="a.czyzewska@wp.pl",
                        AccountCreationDate=new DateTime(2010, 03, 25), HiredDate=new DateTime(2010, 03, 24), FiredDate=new DateTime(2018, 04, 24),
                        ResidenceStreet="Osadowa", ResidenceHouseNumber="9", ResidenceApartmentNumber=null, ResidencePostcode="08-330", ResidenceCity="Pilawa",
                        CorrespondenceStreet="Osadowa", CorrespondenceHouseNumber="9", CorrespondenceApartmentNumber=null, CorrespondencePostcode="08-330", CorrespondenceCity="Pilawa"
                    }
                };
            user.ForEach(u => context.User.AddOrUpdate(u));
            context.SaveChanges();

            var vacation = new List<Vacation>
                {
                    new Vacation() { VacationID=1, UserID=1 ,VacationTypeID=1,  StartVacation=new DateTime(2010, 11, 24), EndVacation=new DateTime(2010, 11, 30)},
                    new Vacation() { VacationID=2, UserID=2 ,VacationTypeID=1,  StartVacation=new DateTime(2011, 11, 11), EndVacation=new DateTime(2011, 11, 18)},
                    new Vacation() { VacationID=3, UserID=3 ,VacationTypeID=1,  StartVacation=new DateTime(2017, 08, 05), EndVacation=new DateTime(2017, 08, 20)},
                    new Vacation() { VacationID=4, UserID=5 ,VacationTypeID=1,  StartVacation=new DateTime(2017, 12, 30), EndVacation=new DateTime(2018, 01, 10)},
                    new Vacation() { VacationID=5, UserID=5 ,VacationTypeID=4,  StartVacation=new DateTime(2012, 03, 24), EndVacation=new DateTime(2012, 04, 24)},
                    new Vacation() { VacationID=6, UserID=6 ,VacationTypeID=1,  StartVacation=new DateTime(2013, 07, 11), EndVacation=new DateTime(2013, 07, 23)},
                    new Vacation() { VacationID=7, UserID=6 ,VacationTypeID=3,  StartVacation=new DateTime(2016, 06, 02), EndVacation=new DateTime(2016, 06, 07)},
                    new Vacation() { VacationID=8, UserID=9 ,VacationTypeID=2,  StartVacation=new DateTime(2012, 03, 25), EndVacation=new DateTime(2012, 04, 10)}
                };

            vacation.ForEach(v => context.Vacation.AddOrUpdate(v));
            context.SaveChanges();


            var client = new List<Client>
                {
                    new Client() { ClientID=1, FirstName="Kamil", LastName="Kozdrój", CompanyName="InfLab",
                    NIP="2347545675", REGON="456852159",
                    Street="Krakowska", HouseNumber="14", ApartmentNumber=null, Postcode="03-500", City="Kraków"
                    },
                    new Client() { ClientID=2, FirstName="Adrian", LastName="Kot", CompanyName="VirPro",
                    NIP="1236553575", REGON="753654258",
                    Street="Aleja", HouseNumber="22", ApartmentNumber="1", Postcode="02-333", City="Lublin"
                    }
                };

            client.ForEach(c => context.Client.AddOrUpdate(c));
            context.SaveChanges();

            var project = new List<Project>
                {
                    new Project() { ProjectID=1, ClientID=1, ProjectStatusID=2, TeamID=1, Name="Online Shop Mix",
                    StartTime=new DateTime(2011, 04, 01), EndTime=new DateTime(2012, 01, 01),
                    Cost=83500.00, Resources=new Dictionary<string, double> {
                        {"Software", 5000.00},
                        {"Prototype" , 4000.00},
                        {"Testing", 4000.00},
                        {"Maintenance", 10000.00},
                        {"Implementation", 6000.00},
                        {"Other", 3000.00} }
                },

                new Project() { ProjectID=2, ClientID=1, ProjectStatusID=1, TeamID=1, Name="System zarządzania firmą",
                    StartTime=new DateTime(2012, 01, 10), EndTime=new DateTime(2012, 07, 20),
                    Cost=12000.00, Resources=new Dictionary<string, double> {
                        {"Software", 10000.00},
                        {"Prototype" , 10000.00},
                        {"Testing", 15000.00},
                        {"Maintenance", 25000.00},
                        {"Implementation", 15000.00},
                        {"Other", 30000.00} }
                },

                    new Project() { ProjectID=3, ClientID=2, ProjectStatusID=1, TeamID=2, Name="System premiowy",
                    StartTime=new DateTime(2011, 08, 02), EndTime=new DateTime(2012, 07, 21),
                    Cost=40000.00, Resources=new Dictionary<string, double> {
                        {"Software", 3000.00},
                        {"Prototype" , 3000.00},
                        {"Testing", 6000.00},
                        {"Maintenance", 6000.00},
                        {"Implementation", 4000.00},
                        {"Other", 3000.00} }
                },
                };

            project.ForEach(p => context.Project.AddOrUpdate(p));
            context.SaveChanges();


            var mainTask = new List<MainTask>
                {
                //PROJECT 1
                    new MainTask() { MainTaskID=1, ProjectID=1, Name="Prototype", Priority=1,
                        StartTime=new DateTime(2011, 04, 10), EndTime=new DateTime(2011, 06, 01), Status=true,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=2, ProjectID=1, Name="Database", Priority=1,
                        StartTime=new DateTime(2011, 06, 01), EndTime=new DateTime(2011, 08, 01), Status=true,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=3, ProjectID=1, Name="Implementation", Priority=2,
                        StartTime=new DateTime(2011, 08, 01), EndTime=new DateTime(2011, 10, 01), Status=true,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=4, ProjectID=1, Name="Testing", Priority=3,
                        StartTime=new DateTime(2011, 10, 01), EndTime=new DateTime(2012, 01, 01), Status=true,
                        PrecedingMainTasks=null
                    },

                    //PROJECT 2
                    new MainTask() { MainTaskID=5, ProjectID=2, Name="Prototype", Priority=1,
                        StartTime=new DateTime(2012, 01, 11), EndTime=new DateTime(2012, 01, 20), Status=true,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=6, ProjectID=2, Name="Database", Priority=1,
                        StartTime=new DateTime(2012, 01, 21), EndTime=new DateTime(2012, 02, 10), Status=true,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=7, ProjectID=2, Name="Database 2", Priority=2,
                        StartTime=new DateTime(2012, 01, 21), EndTime=new DateTime(2012, 04, 15), Status=false,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=8, ProjectID=2, Name="Implementation", Priority=3,
                        StartTime=new DateTime(2012, 02, 12), EndTime=new DateTime(2012, 05, 22), Status=false,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=9, ProjectID=2, Name="Testing", Priority=3,
                        StartTime=new DateTime(2012, 05, 25), EndTime=new DateTime(2012, 07, 20), Status=false,
                        PrecedingMainTasks=null
                    },

                    //PROJECT 3
                    new MainTask() { MainTaskID=10, ProjectID=3, Name="Prototype", Priority=1,
                        StartTime=new DateTime(2011, 08, 04), EndTime=new DateTime(2011, 12, 21), Status=true,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=11, ProjectID=3, Name="Implementation", Priority=1,
                        StartTime=new DateTime(2011, 12, 25), EndTime=new DateTime(2012, 03, 21), Status=false,
                        PrecedingMainTasks=null
                    },
                    new MainTask() { MainTaskID=12, ProjectID=3, Name="Testing", Priority=3,
                        StartTime=new DateTime(2012, 03, 22), EndTime=new DateTime(2012, 07, 21), Status=false,
                        PrecedingMainTasks=null
                    }
                };

            //Project 1
            mainTask[1].PrecedingMainTasks = new List<MainTask> { mainTask[0] };
            mainTask[2].PrecedingMainTasks = new List<MainTask> { mainTask[1] };
            mainTask[3].PrecedingMainTasks = new List<MainTask> { mainTask[1], mainTask[2] };

            //Project 2
            mainTask[5].PrecedingMainTasks = new List<MainTask> { mainTask[4] };
            mainTask[6].PrecedingMainTasks = new List<MainTask> { mainTask[4] };
            mainTask[7].PrecedingMainTasks = new List<MainTask> { mainTask[5], mainTask[6]};
            mainTask[8].PrecedingMainTasks = new List<MainTask> { mainTask[7]};

            //Project 3
            mainTask[10].PrecedingMainTasks = new List<MainTask> { mainTask[9] };
            mainTask[11].PrecedingMainTasks = new List<MainTask> { mainTask[10] };

            mainTask.ForEach(mt => context.MainTask.AddOrUpdate(mt));
            context.SaveChanges();


            var subtask = new List<Subtask>
                {
                //PROJEKT 1, 4 main taski, zespol 1 (id user 2,4,5)
                    new Subtask() { SubtaskID=1, MainTaskID=1, SubtaskStatusID=2,UserID=4, Description="subtask 1 do wykonania", 
                    Name="subtask 1", StartTime=new DateTime(2020, 03, 20), EndTime=new DateTime(2020, 03, 21), Priority = "High",
                    WhoCreated=2},
                    new Subtask() { SubtaskID=2, MainTaskID=2, SubtaskStatusID=3,UserID=5, Description="subtask 2 do wykonania",
                    Name="subtask 2", StartTime=new DateTime(2011, 06, 01), EndTime=new DateTime(2011, 08, 01), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=3, MainTaskID=3, SubtaskStatusID=3,UserID=4, Description="subtask 3 do wykonania",
                    Name="subtask 3", StartTime=new DateTime(2020, 03, 22), EndTime=new DateTime(2020, 03, 25), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=4, MainTaskID=3, SubtaskStatusID=3,UserID=5, Description="subtask 4 do wykonania",
                    Name="subtask 4", StartTime=new DateTime(2011, 08, 01), EndTime=new DateTime(2011, 10, 01), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=5, MainTaskID=4, SubtaskStatusID=2,UserID=4, Description="subtask 5 do wykonania",
                    Name="subtask 5", StartTime=new DateTime(2020, 03, 24), EndTime=new DateTime(2020, 04, 10), Priority = "High",
                    WhoCreated=2
                    },

                    //PROJEKT 2, 5 main taski, zespol 1 (id user 2,4,5)
                    new Subtask() { SubtaskID=6, MainTaskID=5, SubtaskStatusID=3,UserID=4, Description="subtask 6 do wykonania",
                    Name="subtask 6",  StartTime=new DateTime(2012, 01, 11), EndTime=new DateTime(2012, 01, 20), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=7, MainTaskID=6, SubtaskStatusID=3,UserID=5, Description="subtask 7 do wykonania",
                    Name="subtask 7", StartTime=new DateTime(2012, 01, 21), EndTime=new DateTime(2012, 01, 30), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=8, MainTaskID=6, SubtaskStatusID=3,UserID=4, Description="subtask 8 do wykonania",
                    Name="subtask 8", StartTime=new DateTime(2012, 01, 31), EndTime=new DateTime(2012, 02, 10), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=9, MainTaskID=7, SubtaskStatusID=2,UserID=5, Description="subtask 9 do wykonania",
                    Name="subtask 9", StartTime=new DateTime(2012, 01, 21), EndTime=new DateTime(2012, 04, 15), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=10, MainTaskID=8, SubtaskStatusID=2,UserID=4, Description="subtask 10 do wykonania",
                    Name="subtask 10", StartTime=new DateTime(2012, 02, 12), EndTime=new DateTime(2012, 03, 01), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=11, MainTaskID=8, SubtaskStatusID=2,UserID=4, Description="subtask 11 do wykonania",
                    Name="subtask 11", StartTime=new DateTime(2012, 03, 01), EndTime=new DateTime(2012, 04, 20), Priority = "High",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=12, MainTaskID=8, SubtaskStatusID=2,UserID=5, Description="subtask 12 do wykonania",
                    Name="subtask 12", StartTime=new DateTime(2012, 04, 20), EndTime=new DateTime(2012, 05, 22), Priority = "Very low",
                    WhoCreated=2
                    },
                    new Subtask() { SubtaskID=13, MainTaskID=9, SubtaskStatusID=2,UserID=4, Description="subtask 13 do wykonania",
                    Name="subtask 13", StartTime=new DateTime(2012, 05, 25), EndTime=new DateTime(2012, 07, 20), Priority = "Very high",
                    WhoCreated=2
                    },

                    //PROJEKT 3, 3 main taski, zespol 2 (id user 3,6,7,8,9)
                    new Subtask() { SubtaskID=14, MainTaskID=10, SubtaskStatusID=2,UserID=6, Description="subtask 14 do wykonania",
                    Name="subtask 14", StartTime=new DateTime(2011, 08, 04), EndTime=new DateTime(2011, 12, 21), Priority = "High",
                    WhoCreated=3
                    },
                    new Subtask() { SubtaskID=15, MainTaskID=11, SubtaskStatusID=2,UserID=7, Description="subtask 15 do wykonania",
                    Name="subtask 15", StartTime=new DateTime(2011, 12, 25), EndTime=new DateTime(2012, 03, 21), Priority = "Medium",
                    WhoCreated=3
                    },
                    new Subtask() { SubtaskID=16, MainTaskID=12, SubtaskStatusID=2,UserID=8, Description="subtask 16 do wykonania",
                    Name="subtask 16", StartTime=new DateTime(2012, 03, 22), EndTime=new DateTime(2012, 07, 21), Priority = "Low",
                    WhoCreated=3
                    },

                    //zlecone przez pracownikow
                    new Subtask() { SubtaskID=17, MainTaskID=null, SubtaskStatusID=1,UserID=4, Description="subtask 17 do wykonania",
                    Name="subtask 17", StartTime=new DateTime(2012, 03, 22), EndTime=new DateTime(2012, 07, 21), Priority = "Low",
                    WhoCreated=4
                    },
                    new Subtask() { SubtaskID=18, MainTaskID=null, SubtaskStatusID=3,UserID=4, Description="subtask 18 do wykonania",
                    Name="subtask 18", StartTime=new DateTime(2012, 03, 22), EndTime=new DateTime(2012, 07, 21), Priority = "Low",
                    WhoCreated=4
                    },
                    new Subtask() { SubtaskID=19, MainTaskID=null, SubtaskStatusID=2,UserID=4, Description="subtask 19 do wykonania",
                    Name="subtask 19", StartTime=new DateTime(2012, 03, 22), EndTime=new DateTime(2012, 07, 21), Priority = "Low",
                    WhoCreated=4
                    },

                };

            subtask.ForEach(st => context.Subtask.AddOrUpdate(st));
            context.SaveChanges();



            var article = new List<Article>
                {
                    new Article() { ArticleID=1, Description="Entity Framework 6", Url="https://www.plukasiewicz.net/Artykuly/EntityFramework_Code_First", 
                        DateAdded=new DateTime(2012, 07, 21), UserID = 4},
                    new Article() { ArticleID=2, Description="SQL" , Url="https://www.w3schools.com/sql/",
                        DateAdded=new DateTime(2013, 07, 21), UserID = 7},
                    new Article() { ArticleID=3, Description="C# and WPF", Url="https://docs.microsoft.com/pl-pl/dotnet/csharp/",
                        DateAdded=new DateTime(2014, 07, 21), UserID = 8}
                };

            article.ForEach(a => context.Article.AddOrUpdate(a));
            context.SaveChanges();

            var recentActivities = new List<RecentActivity>
            {
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 1", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 2", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 3", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 4", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 5", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 6", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 7", TeamID=1},
                new RecentActivity() {RecentActivityID=1, DateAdded = DateTime.Now, Description = "Activity 8", TeamID=1},
            };

            recentActivities.ForEach(a => context.RecentActivity.AddOrUpdate(a));
            context.SaveChanges();
        }
    }
}
