using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;
using Nullam.Infra.Party;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    string connectionString = builder.Configuration.GetConnectionString("NullamContext");
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddDbContext<NullamDb>(options => options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddRazorPages();
    builder.Services.AddTransient<IEventsRepo, EventsRepo>();
    builder.Services.AddTransient<IPersonsRepo, PersonsRepo>();
    builder.Services.AddTransient<IOrganizationsRepo, OrganizationsRepo>();

    WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) {
        _ = app.UseMigrationsEndPoint();
    } else {
        _ = app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        _ = app.UseHsts();
    }

    using (IServiceScope scope = app.Services.CreateScope()) {
        GetRepo.SetService(app.Services);
        NullamDb? db = scope.ServiceProvider.GetService<NullamDb>();
        _ = (db?.Database?.EnsureCreated());
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();