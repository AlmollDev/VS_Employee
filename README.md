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
@startuml
actor Client as "کاربر / مرورگر / Postman"

rectangle "EmployeeAdminPortalLST API" {
    
    rectangle "Controllers" {
        rectangle "EmployeeController" {
            note right
                - دریافت درخواست HTTP (GET, POST, PUT, DELETE)
                - اعتبارسنجی اولیه ModelState
                - فراخوانی GenericService
            end note
        }
    }

    rectangle "Services" {
        rectangle "GenericService<T, TKey>" {
            note right
                - عملیات CRUD عمومی
                - Paging / Sorting / Filtering
                - DeleteRangeAsync
            end note
        }
    }

    rectangle "Data" {
        rectangle "ApplicationDbContext" {
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

'ارتباطات بین لایه‌ها
Client --> "EmployeeController" : ارسال HTTP Request
"EmployeeController" --> "GenericService<T, TKey>" : فراخوانی سرویس
"GenericService<T, TKey>" --> "ApplicationDbContext" : عملیات CRUD
"ApplicationDbContext" --> Database : ذخیره / خواندن داده‌ها
Database --> "ApplicationDbContext" : پاسخ داده‌ها
"ApplicationDbContext" --> "GenericService<T, TKey>" : داده‌های پردازش شده
"GenericService<T, TKey>" --> "EmployeeController" : پاسخ سرویس
"EmployeeController" --> Client : ارسال HTTP Response

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

