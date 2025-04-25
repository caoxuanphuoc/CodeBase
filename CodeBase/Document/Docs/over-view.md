# Tài liệu hướng dẫn cho nhà phát triển mới

## 1: Cấu trúc dự án

Dự án được tổ chức theo mô hình kiến trúc nhiều tầng, với các phần chính như sau:

```
CodeBase/
├── Codebase.Web.Host/              # Tầng presentation (API controllers)
│   ├── Controllers/
│   │   ├── V1/                     # API phiên bản 1
│   │   │   ├── ExampleController.cs # Controller xử lý các request liên quan đến Example
│   │   ├── Models/                 # Các model dùng cho request API
├── CodeBase.Service/               # Tầng business logic
│   ├── Handlers/                   # Các service xử lý logic nghiệp vụ
│   │   ├── V1/
│   │   │   ├── Example/            # Xử lý nghiệp vụ liên quan đến Example
│   │   │   │   ├── IExampleAppService.cs     # Interface định nghĩa các phương thức
│   │   │   │   ├── ExampleAppService.cs      # Triển khai logic nghiệp vụ
│   │   │   │   ├── Dto/            # Data Transfer Objects
│   │   │   │   │   ├── CreateExampleDto.cs   # DTO cho tạo mới
│   │   │   │   │   ├── ExampleDto.cs         # DTO cho dữ liệu trả về
│   │   │   │   │   ├── UpdateExampleDto.cs   # DTO cho cập nhật
│   │   │   │   ├── Validators/     # Các validator cho DTO
├── CodeBase.Core/                  # Tầng core (thực thể, interface cơ bản)
│   ├── Entities/                   # Định nghĩa các thực thể
│   │   ├── Example.cs              # Thực thể Example
│   │   ├── EntityBase.cs           # Lớp cơ sở cho tất cả các thực thể
├── CodeBase.EntityFrameworkCore/   # Tầng truy cập dữ liệu
│   ├── Repositories/               # Triển khai các repository
│   │   ├── BaseRepository.cs       # Repository cơ sở
│   │   ├── UnitOfWork/            # Unit of Work pattern
│   │   │   ├── IUnitOfWork.cs     # Interface Unit of Work
│   │   │   ├── UnitOfWork.cs      # Triển khai Unit of Work
```

## 2: Quy trình xử lý từ API đến cơ sở dữ liệu

Dưới đây là luồng xử lý chính dựa trên API ExampleController:

### 2.1 mermaid

```
sequenceDiagram
    participant Client
    participant Controller
    participant AppService
    participant Repository
    participant UnitOfWork
    participant Database

    Client->>Controller: HTTP Request
    Controller->>AppService: DTO
    AppService->>Repository: Entity
    Repository->>Database: Query
    Database-->>Repository: Result
    Repository-->>AppService: Entity
    AppService->>UnitOfWork: SaveChanges
    UnitOfWork->>Database: Commit
    Database-->>UnitOfWork: Success
    UnitOfWork-->>AppService: Success
    AppService-->>Controller: DTO
    Controller-->>Client: HTTP Response
```

## 3: Chi tiết luồng xử lý

### 3.1. API Layer (Controllers)

• API nhận HTTP request từ client
• Kiểm tra tính hợp lệ của dữ liệu đầu vào
• Chuyển đổi Model thành DTO
• Tạo mapping profile giữa DTO và entity
• Gọi đến service tương ứng

Ví dụ từ ExampleController.cs:

```csharp
[HttpPost(Name = "CreateExample")]
public async Task<ExampleDto> CreateExample(CreateExampleModels exampleModel)
{
    CreateExampleDto data = new CreateExampleDto
    {
        Description = exampleModel.Description,
        Name = exampleModel.Name
    };
    return await _exampleAppService.CreateExample(data);
}
```

### 3.2: Service Layer (Handlers)

• Chứa logic nghiệp vụ
• Xác thực dữ liệu đầu vào
• Xử lý các quy tắc nghiệp vụ
• Sử dụng Repository và UnitOfWork để thao tác với dữ liệu
• khai báo DI tại program
Interface IExampleAppService.cs định nghĩa các phương thức:

```csharp
public interface IExampleAppService
{
    Task<ExampleDto> CreateExample(CreateExampleDto exampleDto);
    Task<ExampleDto> GetById(int id);
    Task<List<ExampleDto>> GetAll();
    Task<ExampleDto> Update(UpdateExampleDto input);
}
```

### 3.3: Repository Layer

• Thực hiện các thao tác CRUD với cơ sở dữ liệu
• Sử dụng Entity Framework Core để tương tác với database
• Tuân thủ Repository Pattern
• Sử dụng Unit of Work để quản lý transaction

### 3.4: Unit of Work Pattern

• Quản lý transaction
• Đảm bảo tính nhất quán của dữ liệu
• Cho phép rollback khi có lỗi

## 4: Ví dụ chi tiết dựa trên ExampleController

### 4.1. Create Example (POST API)

1. Client gửi HTTP POST request đến /Example với payload:

```json
{
  "name": "Example Name",
  "description": "Example Description"
}
```

2. ExampleController.CreateExample() nhận request và chuyển đổi thành CreateExampleDto
3. ExampleAppService.CreateExample() nhận DTO, thực hiện xác thực và xử lý nghiệp vụ
4. Repository.Add() tạo entity Example và thêm vào database
5. UnitOfWork.SaveChanges() lưu thay đổi vào database
6. Repository trả về Entity cho Service
7. Service chuyển đổi Entity thành DTO và trả về cho Controller
8. Controller trả về response API với status code 200 OK

## 5: Các chú ý khi phát triển

### 5.1. DTO vs Model vs Entity:

• Model: Sử dụng ở tầng API để nhận request từ client
• DTO: Chuyển dữ liệu giữa các tầng (Controller <-> Service)
• Entity: Đại diện cho cấu trúc dữ liệu trong database

### 5.2. Dependency Injection:

• Sử dụng DI để tiêm các dependency vào constructor
• Khai báo các interface trong IoC container ở Startup.cs
• Ví dụ:

```csharp
services.AddScoped<IExampleAppService, ExampleAppService>();
services.AddScoped<IBaseRepository<ExampleEntity, int>, BaseRepository<ExampleEntity, int>>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
```

### 5.3. Xử lý lỗi:

• Sử dụng try-catch và trả về mã lỗi HTTP phù hợp
• Ghi log lỗi để dễ dàng debug
• Sử dụng Unit of Work để rollback khi có lỗi

### 5.4. Validation:

• Validation dữ liệu đầu vào ở cả tầng Controller và Service
• Sử dụng FluentValidation cho các DTO
• Ví dụ:

```csharp
public class CreateExampleDtoValidator : AbstractValidator<CreateExampleDto>
{
    public CreateExampleDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
```

### 5.5. Repository Pattern:

• Sử dụng IBaseRepository<TEntity, TKey> cho các thao tác CRUD cơ bản
• Override các phương thức khi cần xử lý đặc biệt
• Ví dụ:

```csharp
public class ExampleRepository : BaseRepository<ExampleEntity, int>
{
    public ExampleRepository(DbContext context) : base(context)
    {
    }
}
```

### 5.6. Unit of Work Pattern:

• Sử dụng IUnitOfWork để quản lý transaction
• Đảm bảo tính nhất quán của dữ liệu
• Ví dụ:

```csharp
public class ExampleAppService
{
    private readonly IUnitOfWork _unitOfWork;

    public async Task<ExampleDto> CreateExample(CreateExampleDto input)
    {
        var entity = _mapper.Map<ExampleEntity>(input);
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ExampleDto>(entity);
    }
}
```

## 6: Hướng dẫn tạo và quản lý Migration

### 6.1. Tạo Migration mới

Để tạo migration mới, thực hiện các bước sau:

1. Đảm bảo đã cài đặt các package cần thiết:

   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.EntityFrameworkCore.Tools
   - Npgsql.EntityFrameworkCore.PostgreSQL (cho PostgreSQL)

2. Chạy lệnh tạo migration từ thư mục gốc của project:

```bash
cd Codebase.Web.Host
dotnet ef migrations add InitialCreate --project ../CodeBase.EntityFrameworkCore
```

Trong đó:

- `InitialCreate` là tên của migration
- `--project` chỉ định project chứa DbContext

### 6.2. Cập nhật Database

Sau khi tạo migration, để áp dụng các thay đổi vào database:

```bash
dotnet ef database update
```

### 6.3. Xóa Migration

Nếu cần xóa migration gần nhất:

```bash
dotnet ef migrations remove
```

### 6.4. Lưu ý quan trọng

- Luôn kiểm tra kỹ các thay đổi trong file migration trước khi áp dụng
- Backup database trước khi thực hiện các thay đổi lớn
- Đảm bảo connection string được cấu hình đúng trong `appsettings.json`
- Nếu có lỗi, có thể xóa thư mục Migrations và tạo lại từ đầu
