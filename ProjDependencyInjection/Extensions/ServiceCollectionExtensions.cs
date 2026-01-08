using System.IdentityModel.Tokens.Jwt;
using Application.General;
using Application.Models.Entity;
using Application.Models.Entity;
using DataAccess;
using Domain.Auth.Commands.AccountLoginHandler;
using Domain.Auth.Commands.CreateAccountHandler;
using Domain.Auth.Commands.SendConfirmationEmail;
using Domain.Auth.Profile.Query.GetUserByIdHandler;
using Domain.Companies.Commands.CreateCompanyHandler;
using Domain.Companies.Commands.DeleteCompanyHandler;
using Domain.Companies.Commands.UpadateCompanyHandler;
using Domain.Companies.Query.GetCompanyHandler;
using Domain.Tasks.Commands.CreateTaskHandler;
using Domain.Tasks.Commands.DeleteTaskHandler;
using Domain.Tasks.Commands.UpdateTaskHandler;
using Domain.Tasks.Queries.GetTaskByIdHandler;
using Domain.Tasks.Queries.GetTasksByCompanyIdHandler;
using Domain.Tasks.Queries.GetTasksByTeamIdHandler;
using Domain.Tasks.Queries.GetTasksByUserIdHandler;
using Domain.Teams.Commands.CreateTeamHandler;
using Domain.Teams.Commands.DeleteTeamByIdHandler;
using Domain.Teams.Commands.InviteUserToTeamHandler;
using Domain.Teams.Commands.UpdateTeamHandler;
using Domain.Teams.Queries.GetTeamByIdHandler;
using Domain.Teams.Queries.GetTeamsByCompanyIdHandler;
using ProjDependencyInjection.ConfigureOptions;
using TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjDependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCommonServices(this IServiceCollection service, IConfiguration configuration,
        string projectName)
    {
        service.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                //options.Tokens.EmailConfirmationTokenProvider = "EmailDataProtectorTokenProvider";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        service.AddRouting();

        service.AddCustomAuthorization();

        service.ConfigureOptions<ConfigureJwtBearear>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();
        //Auth services
        service.AddTransient<ICreateAccountHandler, CreateAccountHandler>();
        service.AddTransient<IAccountLoginHandler, AccountLoginHandler>();
        service.AddTransient<ISendConfirmationEmailRequestHandler, SendConfirmationEmailRequestHandler>();
        service.AddTransient<IGetUserByIdHandler, GetUserByIdHandler>();
        
        //Company services
        service.AddTransient<ICreateCompanyHandler, CreateCompanyHandler>();
        service.AddTransient<IGetCompanyHandler, GetCompanyHandler>();
        service.AddTransient<IDeleteCompanyHandler, DeleteCompanyHandler>();
        service.AddTransient<IUpdateCompanyHandler, UpdateCompanyHandler>();
        
        //Task services
        service.AddTransient<ICreateTaskHandler, CreateTaskHandler>();
        service.AddTransient<IDeleteTaskHandler, DeleteTaskHandler>();
        service.AddTransient<IUpdateTaskHandler, UpdateTaskHandler>();
        service.AddTransient<IGetTaskByIdHandler, GetTaskByIdHandler>();
        service.AddTransient<IGetTasksByCompanyIdHandler, GetTasksByCompanyIdHandler>();
        service.AddTransient<IGetTasksByTeamIdHandler, GetTasksByTeamIdHandler>();
        service.AddTransient<IGetTasksByUserIdHandler, GetTasksByUserIdHandler>();

        //Team services
        service.AddTransient<ICreateTeamHandler, CreateTeamHandler>();
        service.AddTransient<IDeleteTeamHandler, DeleteTeamHandler>();
        service.AddTransient<IUpdateTeamHandler, UpdateTeamHandler>();
        service.AddTransient<IGetTeamByIdHandler, GetTeamByIdHandler>();
        service.AddTransient<IGetTeamsByCompanyIdHandler, GetTeamsByCompanyIdHandler>();
        service.AddTransient<IInviteUserToTeamHandler, InviteUserToTeamHandler>();

        service.AddSettingsConfig(configuration);

        service.AddTransient<IJwtTokenService, JwtTokenService>();

        service.AddHttpContextAccessor();
        service.AddCors();
        service.AddMapster();
        service.AddControllers();
        service.AddEndpointsApiExplorer();
        service.AddSwagger(projectName);
        service.AddDataAccess(configuration);
    }
}