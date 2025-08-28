EmployeeAdminPortalLST
یک پروژه ASP.NET Core Web API برای مدیریت کارمندان با معماری Clean / Layered و سرویس‌های Generic.

🌐 ساختار پروژه
EmployeeAdminPortalLST/
├── Controllers/                    # کنترلرهای API
│   ├── EmployeeController.cs       # مدیریت عملیات کارمندان
│   └── WeatherForecastController.cs # کنترلر تست
├── Data/
│   └── ApplicationDbContext.cs     # EF Core DbContext
├── Models/
│   ├── Entities/                   # موجودیت‌های دیتابیس
│   │   ├── Employee.cs
│   │   └── User.cs
│   ├── DTOs/                       # مدل‌های انتقال داده (DTO)
│   │   ├── AddEmployeeDto.cs
│   │   ├── UpdateEmployeeDto.cs
│   │   └── PageResult.cs
│   └── Filters/
│       └── EmployeeFilterRequestModel.cs # مدل فیلتر کارمندان
├── Services/                       # سرویس‌های کسب‌وکار
│   └── GenericService.cs           # Generic CRUD Service
├── Migrations/                     # EF Core Migrations
├── Program.cs                      # نقطه ورود برنامه
├── appsettings.json                # تنظیمات برنامه
└── EmployeeAdminPortalLST.csproj   # فایل پروژه


🔁 جریان داده‌ها (Flow)@startuml
actor Client as "کاربر / درخواست HTTP"

rectangle "EmployeeAdminPortalLST API" {
    
    rectangle "Controllers" {
        Controller "EmployeeController" {
            note right
                - دریافت درخواست HTTP (GET, POST, PUT, DELETE)
                - اعتبارسنجی اولیه ModelState
                - فراخوانی GenericService
            end note
        }
    }

    rectangle "Services" {
        GenericService "GenericService<T, TKey>" {
            note right
                - عملیات CRUD عمومی برای هر Entity
                - Paging / Sorting / Filtering
                - DeleteRangeAsync
            end note
        }
    }

    rectangle "Data" {
        DbContext "ApplicationDbContext" {
            note right
                - دسترسی به DbSet<T>
                - اجرای Query و SaveChangesAsync
            end note
        }
    }

    rectangle "Database" {
        note right
            - ذخیره و بازیابی داده‌ها
        end note
    }

}

' ارتباطات
Client --> Controller : ارسال HTTP Request
Controller --> GenericService : فراخوانی سرویس (IGenericService<Employee, TKey>)
GenericService --> DbContext : عملیات CRUD
DbContext --> Database : ذخیره / خواندن داده‌ها
Database --> DbContext : پاسخ داده‌ها
DbContext --> GenericService : داده‌های پردازش شده
GenericService --> Controller : پاسخ سرویس
Controller --> Client : ارسال HTTP Response

@enduml

🔹 ویژگی‌ها

Generic Service: قابلیت استفاده مجدد برای هر Entity (Employee، User، Product ...)
DTOها: جداسازی مدل‌های دیتابیس و مدل‌های انتقال داده
Paging / Sorting / Filtering: با استفاده از PageResult<T> و EmployeeFilterRequestModel
Bulk Delete: حذف دسته‌جمعی با BulkDeleteDto
Partial Update: به‌روزرسانی جزیی کارمندان با JsonPatchDocument


💡 آینده‌نگری

افزودن پروژه Console Client برای تست مستقیم سرویس‌ها
پیاده‌سازی واحد تست (Unit Test) با Mock کردن IGenericService
قابلیت گسترش سرویس‌ها برای Entityهای اختصاصی


📌 نحوه اجرا

کلون کردن پروژه:
git clone <repository-url>


تنظیم ConnectionString در فایل appsettings.json

اجرای Migration:
dotnet ef database update


اجرای پروژه:
dotnet run


تست API از طریق Swagger:https://localhost:{port}/swagger

