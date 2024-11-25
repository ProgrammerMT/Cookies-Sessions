var builder = WebApplication.CreateBuilder(args);

// �������� �� �����
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);  // ������� ������ ���� 30 ������
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
