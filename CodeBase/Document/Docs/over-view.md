# Tài liệu hướng dẫn cho nhà phát triển mới

## 1: Cấu trúc dự án
Dự án được tổ chức theo mô hình kiến trúc nhiều tầng, với các phần chính như sau:


```

CodeBase/
├── Codebase.Web.Host/              # Tầng presentation (API controllers)
│   ├── Controllers/
│   │   ├── V1/                     # API phiên bản 1
│   │   │   ├── ExampleControler.cs # Controller xử lý các request liên quan đến Example
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
├── CodeBase.Core/                  # Tầng core (thực thể, interface cơ bản)
│   ├── Entities/                   # Định nghĩa các thực thể
│   │   ├── Example.cs              # Thực thể Example
│   │   ├── EntityBase.cs           # Lớp cơ sở cho tất cả các thực thể
├── CodeBase.Data/                  # Tầng truy cập dữ liệu
│   ├── Context/                    # DbContext cho Entity Framework
│   ├── Repositories/               # Triển khai các repository
│   │   ├── ExampleRepository.cs    # Xử lý thao tác CRUD cho Example

```


## 2: Quy trình xử lý từ API đến cơ sở dữ liệu
Dưới đây là luồng xử lý chính dựa trên API ExampleController:

```
flowchart TB
    A[Client] -->|HTTP Request| B[ExampleController]
    B -->|Chuyển đổi Model -> DTO| C[IExampleAppService]
    C -->|Xử lý nghiệp vụ| D[ExampleAppService]
    D -->|Gọi Repository| E[IExampleRepository]
    E -->|Thao tác CRUD| F[ExampleRepository]
    F -->|Entity Framework| G[DbContext]
    G -->|SQL Query| H[(Database)]
    H -->|Dữ liệu| G
    G -->|Entity| F
    F -->|Entity| E
    E -->|Entity| D
    D -->|Chuyển đổi Entity -> DTO| C
    C -->|DTO| B
    B -->|HTTP Response| A

```

## 3: Chi tiết luồng xử lý
### 3.1. API Layer (Controllers)
•	API nhận HTTP request từ client

•	Kiểm tra tính hợp lệ của dữ liệu đầu vào

•	Chuyển đổi Model thành DTO

•	Gọi đến service tương ứng

Ví dụ từ ExampleController.cs:
```
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
•	Chứa logic nghiệp vụ
•	Xác thực dữ liệu đầu vào
•	Xử lý các quy tắc nghiệp vụ
•	Gọi Repository để thao tác với dữ liệu
Interface IExampleAppService.cs định nghĩa các phương thức:

```
public interface IExampleAppService
{
    Task<ExampleDto> CreateExample(CreateExampleDto exampleDto);
    Task<ExampleDto> GetById(int id);
    Task<List<ExampleDto>> GetAll();
    Task<ExampleDto> Update(UpdateExampleDto input);
}
```

### 3.3: Data Access Layer (Repositories)
•	Thực hiện các thao tác CRUD với cơ sở dữ liệu
•	Chuyển đổi giữa Entity và DTO
•	Sử dụng Entity Framework Core để tương tác với database
### 3.4: Database
•	Lưu trữ dữ liệu vào các bảng tương ứng
•	Thực hiện các ràng buộc, mối quan hệ giữa các bảng

## 4: Ví dụ chi tiết dựa trên ExampleController
### 4.1. Create Example (POST API)
1.	Client gửi HTTP POST request đến /Example với payload:

```
{
    "name": "Example Name",
    "description": "Example Description"
}
```

2.	ExampleController.CreateExample() nhận request và chuyển đổi thành CreateExampleDto
3.	ExampleAppService.CreateExample() nhận DTO, thực hiện xác thực và xử lý nghiệp vụ
4.	ExampleRepository.Add() tạo entity Example và thêm vào database
5.	Dữ liệu được lưu vào bảng Examples trong database
6.	Repository trả về Entity cho Service
7.	Service chuyển đổi Entity thành DTO và trả về cho Controller
8.	Controller trả về response API với status code 200 OK
### 4.2. Get Example by ID (GET API)
Tương tự, quá trình lấy thông tin Example theo ID sẽ đi qua các bước:
•	API nhận request /Example/{id}
•	Controller gọi service GetById(id)
•	Service gọi repository để lấy entity từ database
•	Chuyển đổi entity thành DTO và trả về
## 5: Các chú ý khi phát triển
### 5.1.	DTO vs Model vs Entity:
•	Model: Sử dụng ở tầng API để nhận request từ client
•	DTO: Chuyển dữ liệu giữa các tầng (Controller <-> Service)
•	Entity: Đại diện cho cấu trúc dữ liệu trong database
### 5.2.	Dependency Injection:
•	Sử dụng DI để tiêm các dependency vào constructor
•	Khai báo các interface trong IoC container ở Startup.cs
### 5.3.	Xử lý lỗi:
•	Sử dụng try-catch và trả về mã lỗi HTTP phù hợp
•	Ghi log lỗi để dễ dàng debug
### 5.4.	Validation:
•	Validation dữ liệu đầu vào ở cả tầng Controller và Service
•	Sử dụng FluentValidation hoặc Data Annotations
### 5.5.	Routing:
•	Tên route nên rõ ràng và tuân theo chuẩn RESTful
•	Sử dụng đúng HTTP methods (GET, POST, PUT, DELETE)
