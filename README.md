حتما! در ادامه یک **README کامل** برای پروژه `EmployeeAdminPortalLST` آماده کردم. در محل هر دیاگرام، نام فایل یا توضیح جایگزین گذاشتم تا بعداً بتوانی تصویر آن را اضافه کنی.

```markdown
# EmployeeAdminPortalLST

یک پروژه **ASP.NET Core Web API** برای مدیریت کارمندان با معماری **Clean / Layered** و استفاده از **سرویس‌های Generic**.  

---

## 🌐 ساختار پروژه

```

EmployeeAdminPortalLST/
├── Controllers/                    # کنترلرهای API
│   ├── EmployeeController.cs       # مدیریت عملیات کارمندان
│   └── WeatherForecastController.cs # کنترلر تست
├── Data/
│   └── ApplicationDbContext.cs     # کلاس DbContext برای EF Core
├── Models/
│   ├── Entities/                   # موجودیت‌های دیتابیس
│   │   ├── Employee.cs
│   │   └── User.cs
│   ├── DTOs/                       # مدل‌های انتقال داده (DTO)
│   │   ├── AddEmployeeDto.cs
│   │   ├── UpdateEmployeeDto.cs
│   │   ├── PageResult.cs
│   │   └── BulkDeleteDto.cs
│   └── Filters/
│       └── EmployeeFilterRequestModel.cs # مدل فیلتر کارمندان
├── Services/                       # سرویس‌های کسب‌وکار
│   └── GenericService.cs           # Generic CRUD Service
├── Migrations/                     # EF Core Migrations
├── Program.cs                       # نقطه ورود برنامه
├── appsettings.json                  # تنظیمات برنامه
└── EmployeeAdminPortalLST.csproj    # فایل پروژه

````

---

## 🔁 جریان داده‌ها (Flow)

**Client / Browser / Postman**  

ارسال درخواست HTTP (`GET`, `POST`, `PUT`, `DELETE`)  

**EmployeeController**  
- دریافت درخواست  
- Validation اولیه (ModelState)  
- فراخوانی Generic Service (`IGenericService<Employee, TKey>`)

**GenericService<T, TKey>**  
- عملیات CRUD عمومی برای هر Entity  
- Paging / Sorting / Filtering  
- Patch / Bulk Delete  

**ApplicationDbContext (EF Core)**  
- دسترسی مستقیم به DbSet  
- اجرای Query و `SaveChangesAsync`

**Database (SQL Server)**  
- ذخیره یا خواندن داده‌ها  

**Response به Client**  
- Controller پاسخ را باز می‌گرداند

---

## 🔹 ویژگی‌ها

- **Generic Service**: قابلیت استفاده مجدد برای هر Entity (`Employee`, `User`, `Product` ...)  
- **DTOها**: جداسازی مدل‌های دیتابیس و مدل‌های انتقال داده  
- **Paging / Sorting / Filtering**: با استفاده از `PageResult<T>` و `EmployeeFilterRequestModel`  
- **Bulk Delete**: حذف دسته‌جمعی با `BulkDeleteDto`  
- **Partial Update**: به‌روزرسانی جزیی کارمندان با `JsonPatchDocument`  

---

## 📊 دیاگرام‌ها

### 1️⃣ دیاگرام معماری فولدرها و لایه‌ها


<img width="1206" height="478" alt="dLHjQzim4Fukq7yusYyRDFJBEjPIGcFPxWQ1PNMwVfQTcoEYIAwiT0iZ_tsINvBOSsWf1c7TFNVtt9lkIikDtQmb9ULAg88RleQKPwlSw8rQC2srWVDVL_uxXXZCB5UvHFXB2K3GjRYzPLfPewL4Cnx3UH24KHmC0XCqBo93Zs9Vei_iUX0LfjuX_h4U5yVnDViqZEEMfxp46iPO" src="https://github.com/user-attachments/assets/b7b5e764-47f9-4d3b-a96c-c7fd38d5143e" />










### 2️⃣ دیاگرام جریان درخواست بین کنسول و Web API








<img width="1719" height="367" alt="diagram" src="https://github.com/user-attachments/assets/c404ed7c-3be9-49a5-9bcf-3e9f55094843" />













### 3️⃣ دیاگرام داخلی Console App





<img width="631" height="594" alt="bPF1JiCm38RlaV8ELhd0OIV40McQjfLEup1f0Y7ENcfAj2mfIJen4EzEagsYtPWGfpRsx-RpRtViF5hVR1HdRYrrZHOtO4bur9KY-EGCO8fYNLdJw38tojZXC5UIj1-Du68sA_BfzQ-YHx9RAIYgyfjyKjmcbR4bsQI0bGebd7rnnXaA7o9FZcmy_ToVvKOx4rucTQsaG2-DplPK" src="https://github.com/user-attachments/assets/a4b42b98-9f3d-4420-a20e-f7eb047f0b79" />










full diagram::







<img width="3713" height="865" alt="full diagram" src="https://github.com/user-attachments/assets/8f9bfc2e-e816-41d3-95f2-7db3e2802dff" />








## 💡 آینده‌نگری

- افزودن پروژه **Console Client** برای تست مستقیم سرویس‌ها  
- پیاده‌سازی واحد تست (Unit Test) با Mock کردن `IGenericService`  
- قابلیت گسترش سرویس‌ها برای Entityهای اختصاصی  

---

## 📌 نحوه اجرا

1. کلون کردن پروژه:
```bash
git clone <repository-url>
````

2. تنظیم `ConnectionString` در فایل `appsettings.json`

3. اجرای Migration:

```bash
dotnet ef database update
```

4. اجرای پروژه:

```bash
dotnet run
```

5. تست API از طریق Swagger:

```
https://localhost:{port}/swagger
```

---

## 📦 اضافه کردن Console App

* پروژه کنسول را داخل همان Solution اضافه کنید:

```bash
dotnet new console -n EmployeeAdminPortalLST.ConsoleClient
```

* در Console App از HttpClient برای ارتباط با Web API استفاده کنید
* برای تست سرویس‌ها بدون نیاز به Postman یا Browser

---

```

اگر بخواهی، می‌توانم نسخه‌ی **Visual/Enhanced README** هم آماده کنم که شامل **رنگ، آیکون، و فونت‌های خوانا** برای ارائه یا GitHub Pages باشد.  

میخوای اون نسخه رو هم بسازم؟
```
