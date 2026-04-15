using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=medicamentos.db"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/api/auth/login";
});
var app = builder.Build();

   using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>(); 
        string[] roles = { "Doctor", "Enfermero" };
        foreach (var role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }
        }
    var doctorEmail = "doctor@med.com";
    if (userManager.FindByEmailAsync(doctorEmail).GetAwaiter().GetResult() == null)
    {
        var doctor = new ApplicationUser { 
            UserName = doctorEmail, 
            Email = doctorEmail,
            EmailConfirmed = true 
        };
        var result = userManager.CreateAsync(doctor, "Admin123!").GetAwaiter().GetResult();
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(doctor, "Doctor").GetAwaiter().GetResult();
        }
    }
    var enfermeroEmail = "enfermero@med.com";
    if (userManager.FindByEmailAsync(enfermeroEmail).GetAwaiter().GetResult() == null)
    {
        var enfermero = new ApplicationUser { 
            UserName = enfermeroEmail, 
            Email = enfermeroEmail,
            EmailConfirmed = true 
        };
        var result = userManager.CreateAsync(enfermero, "User123!").GetAwaiter().GetResult();
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(enfermero, "Enfermero").GetAwaiter().GetResult();
        }
    }
}
app.UseAuthentication();
app.UseAuthorization();    
app.MapControllers();

app.Run();