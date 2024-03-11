
using Membership_Management.DAL.Interfaces;
using Membership_Managment.AutoDatabaseUpdateService;
using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.DAL.Repositories;
using Membership_Managment.MembershipExpService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace Membership_Managment
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));


            builder.Services.AddHostedService<MembershipExpirationService>();
            builder.Services.AddHostedService<DuePaymentUpdateService>();



            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenereicRepository<>));
            builder.Services.AddScoped(typeof(IMemberRepository), typeof(MemberRepository));
            builder.Services.AddScoped(typeof(IDocumentRepository), typeof(DocumentReposiory));
            builder.Services.AddScoped(typeof(IDocumentRepository), typeof(DocumentReposiory));
            builder.Services.AddScoped(typeof(IPackageRepository), typeof(PackageRepository));
            builder.Services.AddScoped(typeof(IMemberPackageRepository), typeof(MemberPackageRepository));
            builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
            builder.Services.AddScoped(typeof(IDuePaymentRepository), typeof(DuePaymentRepository));






            builder.Services.AddSwaggerGen();




            var app = builder.Build();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
