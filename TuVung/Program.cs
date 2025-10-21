using Microsoft.EntityFrameworkCore;
using TuVung.Data;
using TuVung.Helpers;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 1️⃣ Add services to the container
// =======================================

builder.Services.AddControllers();

// 🔹 Cấu hình CORS (cho phép full truy cập)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()    // ✅ Cho phép tất cả domain
              .AllowAnyMethod()    // ✅ Cho phép tất cả HTTP method (GET, POST, PUT, DELETE, PATCH, v.v.)
              .AllowAnyHeader());  // ✅ Cho phép tất cả header (Authorization, Content-Type, v.v.)
});

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Kết nối PostgreSQL
builder.Services.AddDbContext<VocabularyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(QuestionMappingProfile));
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Kích hoạt CORS ở cấp middleware
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
