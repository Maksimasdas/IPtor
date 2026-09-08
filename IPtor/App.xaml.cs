using IPtor.Buisness.Interfaces;
using IPtor.Buisness.Services;
using IPtor.DAL;
using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using IPtor.ViewModels;
using IPtor.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace IPtor
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            //var connectionString = "Server=(localdb)\\mssqllocaldb;Database=IPtorDb;Trusted_Connection=True;";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //подробные заметки по контейнеру, потому что тема сложная, надо оставить в памяти, что бы вспомнить если что
            //AddScoped - это правило, если пишут ICarService значит нужен CarService и т.д.
            //Scoped означает, что один на всех, все кто требуют ICarService получают его одну и ту же реализацию то есть один и тот же CarService
            //почему так? ICarService это контракт, а CarService это его реализация, по сути мы просто говорим что нам надо и контейнер подчиняясь правилам дает нам реализацию того, что нам необходимо
            //AddTransient - это регистрация моделей без интерфейсов - по самому классу
            //Transient означает каждый раз новый, каждый когда запрашивают CarsViewModel и т.д. — создаётся новый экземпляр.
            // ===== БАЗА ДАННЫХ =====
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // ===== DAL =====
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ===== BUSINESS =====
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IActService, ActService>();
            services.AddScoped<IReportService, ReportService>();

            // ===== VIEWMODELS =====
            services.AddTransient<MainViewModel>();
            services.AddTransient<CarsViewModel>();
            //=== вкладка сотрудников
            services.AddTransient<PersonsViewModel>();
            // ===== ГЛАВНОЕ ОКНО =====
            services.AddTransient<MainWindow>();

            // ===== ОКНО ДОБАВЛЕНИЯ МАШИНЫ =====
            services.AddTransient<AddCarViewModel>();

            //регистрация что бы делать акты приемки
            services.AddTransient<TransferActViewModel>();

            //регистрация что бы делать акты ТО-2
            services.AddTransient<TOActViewModel>();

            //предварительная запись на ТО
            services.AddScoped<IRepairRequestService, RepairRequestService>();
            services.AddTransient<RepairRequestsViewModel>();

            //диаграммы регистрация
            services.AddTransient<StatisticsViewModel>();

            _serviceProvider = services.BuildServiceProvider(); //контейнер заморожен, правила зафиксированы
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Автоматически применить миграции и создать БД если её нет
                context.Database.Migrate();

                SeedData(context);
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void SeedData(AppDbContext context)
        {
            // Проверяем — если данные уже есть, не добавляем повторно
            if (context.Cars.Any()) return;

            // ===== СОТРУДНИКИ =====
            var person1 = new Person
            {
                Name = "Иванов И.И.",
                Service = ServiceType.ОМТОиС,
                Position = "Начальник службы",
                Rank = "Майор",
                UndPosition = "Нач. службы",
                IsActive = true
            };

            var person2 = new Person
            {
                Name = "Петров П.П.",
                Service = ServiceType.ЦВСС,
                Position = "Инженер",
                Rank = "Капитан",
                UndPosition = "Инженер УТО", 
                IsActive= true
            };

            var person3 = new Person
            {
                Name = "Сидоров С.С.",
                Service = ServiceType.ЦВТС,
                Position = "Старший механик",
                Rank = "Прапорщик",
                UndPosition = "Ст. механик", 
                IsActive = true
            };

            context.Persons.AddRange(person1, person2, person3);

            // ===== МАШИНА 1 =====
            var car1 = new Car
            {
                Name = "УРАЛ-4320",
                Model = "4320",
                Number = "А123ВС",
                ClassName = "АШ",
                Service = ServiceType.ЦМО,
                Vin = "XTA432000A0000001",
                Certificate = "СТС №123456",
                EngineNumber = "ЯМЗ-236-001",
                Formular = 1001,
                FormularDate = new DateTime(2020, 5, 10),
                Year = 2020,
                InventoryNumber = "ИНВ-001",
                Batteries = new List<Battery>
        {
            new Battery
            {
                Specifications = "6СТ-90",
                StartingCurrent = "550A",
                Polarity = "Прямая",
                BodyType = "Европейский",
                InstallationDate = new DateTime(2023, 1, 15)
            },
            new Battery
            {
                Specifications = "6СТ-90",
                StartingCurrent = "550A",
                Polarity = "Прямая",
                BodyType = "Европейский",
                InstallationDate = new DateTime(2023, 1, 15)
            }
        },
                Tires = new List<Tire>
        {
            new Tire { Width = 12.00f, Profile = null, Diameter = 20, Brand = "Кама", Model = "И-250", InstallationDate = new DateTime(2023, 1, 15) },
            new Tire { Width = 12.00f, Profile = null, Diameter = 20, Brand = "Кама", Model = "И-250", InstallationDate = new DateTime(2023, 1, 15) },
            new Tire { Width = 12.00f, Profile = null, Diameter = 20, Brand = "Кама", Model = "И-250", InstallationDate = new DateTime(2023, 1, 15) },
            new Tire { Width = 12.00f, Profile = null, Diameter = 20, Brand = "Кама", Model = "И-250", InstallationDate = new DateTime(2023, 1, 15) },
            new Tire { Width = 12.00f, Profile = null, Diameter = 20, Brand = "Кама", Model = "И-250", InstallationDate = new DateTime(2023, 1, 15) },
            new Tire { Width = 12.00f, Profile = null, Diameter = 20, Brand = "Кама", Model = "И-250", InstallationDate = new DateTime(2023, 1, 15) }
        }
            };

            // ===== МАШИНА 2 =====
            var car2 = new Car
            {
                Name = "КАМАЗ-5350",
                Model = "5350",
                Number = "В456КМ",
                ClassName = "АСА",
                Service = ServiceType.ЦХРЗ,
                Vin = "XTC535000B0000002",
                Certificate = "СТС №654321",
                EngineNumber = "КАМАЗ-740-002",
                Formular = 2002,
                FormularDate = new DateTime(2021, 3, 20),
                Year = 2021,
                InventoryNumber = "ИНВ-002",
                Batteries = new List<Battery>
        {
            new Battery
            {
                Specifications = "6СТ-110",
                StartingCurrent = "650A",
                Polarity = "Обратная",
                BodyType = "Азиатский",
                InstallationDate = new DateTime(2023, 6, 10)
            }
        },
                Tires = new List<Tire>
        {
            new Tire { Width = 425.0f, Profile = 85.0f, Diameter = 21, Brand = "Michelin", Model = "XZL", InstallationDate = new DateTime(2023, 6, 10) },
            new Tire { Width = 425.0f, Profile = 85.0f, Diameter = 21, Brand = "Michelin", Model = "XZL", InstallationDate = new DateTime(2023, 6, 10) },
            new Tire { Width = 425.0f, Profile = 85.0f, Diameter = 21, Brand = "Michelin", Model = "XZL", InstallationDate = new DateTime(2023, 6, 10) },
            new Tire { Width = 425.0f, Profile = 85.0f, Diameter = 21, Brand = "Michelin", Model = "XZL", InstallationDate = new DateTime(2023, 6, 10) }
        }
            };

            context.Cars.AddRange(car1, car2);
            context.SaveChanges();
        }
    }
}