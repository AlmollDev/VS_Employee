[Client / Browser / Postman]
          |
          | HTTP Request (GET, POST, PUT, DELETE)
          v
[EmployeeController]
  - دریافت درخواست
  - Validation اولیه (ModelState)
  - فراخوانی متد GenericService (GetAllAsync, AddAsync, ...)
          |
          v
[IGenericService<Employee, TKey> / GenericService<Employee, TKey>]
  - عملیات CRUD عمومی
  - فیلترها، Paging، Sorting
          |
          v
[ApplicationDbContext (EF Core)]
  - دسترسی مستقیم به DbSet<Employee>
  - اجرای Query
  - SaveChangesAsync
          |
          v
[Database SQL Server]
  - ذخیره/خواندن داده‌ها
          |
          ^ پاسخ به Controller
[Controller] -> Response -> Client
