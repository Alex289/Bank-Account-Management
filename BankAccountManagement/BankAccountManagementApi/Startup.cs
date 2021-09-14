using BankAccountManagementApi.Application.Interfaces;
using BankAccountManagementApi.Application.Queries.Account;
using BankAccountManagementApi.Application.Queries.Bank;
using BankAccountManagementApi.Application.QueryHandler;
using BankAccountManagementApi.Application.Services;
using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.CommandHandlers;
using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.EventHandler;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Interfaces;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Domain.Notifications;
using BankAccountManagementApi.Infrastructure;
using BankAccountManagementApi.Infrastructure.Data;
using BankAccountManagementApi.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace BankAccountManagementApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BankContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors();
            services.AddControllersWithViews();

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Domain events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Bank commands and events
            services.AddScoped<IRequestHandler<CreateNewBankCommand, Guid>, BankCommandHandler>();
            services.AddScoped<INotificationHandler<BankCreatedEvent>, BankEventHandler>();

            services.AddScoped<IRequestHandler<GetAllBanksQuery, List<BankListViewModel>>, BankQueryHandler>();

            services.AddScoped<IRequestHandler<ChargeInterestsCommand, int>, BankCommandHandler>();
            services.AddScoped<INotificationHandler<ChargedInterestsEvent>, BankEventHandler>();

            // Account commands and events
            services.AddScoped<IRequestHandler<CreateNewAccountCommand, Guid>, AccountCommandHandler>();
            services.AddScoped<INotificationHandler<CreatedAccountEvent>, AccountEventHandler>();

            services.AddScoped<IRequestHandler<GetAllAccountsQuery, List<AccountListViewModel>>, AccountQueryHandler>();
            services.AddScoped<IRequestHandler<GetAccountByBankIdQueryAsync, List<AccountListViewModel>>, AccountQueryHandler>();

            services.AddScoped<IRequestHandler<GetAccountsByIdQuery, AccountListViewModel>, AccountQueryHandler>();

            services.AddScoped<IRequestHandler<DepositCommand, decimal>, AccountCommandHandler>();
            services.AddScoped<INotificationHandler<DepositedEvent>, AccountEventHandler>();

            services.AddScoped<IRequestHandler<WithdrawCommand, decimal>, AccountCommandHandler>();
            services.AddScoped<INotificationHandler<WithdrewEvent>, AccountEventHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankAccountManagementApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankAccountManagementApi v1"));
            }

            app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
