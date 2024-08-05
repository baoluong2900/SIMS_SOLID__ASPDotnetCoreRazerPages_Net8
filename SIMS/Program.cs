using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using SIMS.Abstractions;
using SIMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Usage - Notyf
builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddScoped<CSVReader>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<GradeService>();
builder.Services.AddScoped<StudentCourseService>();

builder.Services.AddSingleton<ICSVReader, CSVReader>();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<IGradeService, GradeService>();
builder.Services.AddSingleton<IStudentCourseService,StudentCourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.UseNotyf();
app.Run();
