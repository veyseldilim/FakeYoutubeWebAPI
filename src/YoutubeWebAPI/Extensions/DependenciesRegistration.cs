using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Repositories;
using YoutubeWeb.Domain.Request.Comment.Validator;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Request.User.Validator;
using YoutubeWeb.Domain.Services;

namespace YoutubeWebAPI.Extensions
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services
            .AddSingleton<IMapper, Mapper>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services

            .AddScoped<ICommentService, CommentService>()
            .AddScoped<IPostService, PostService>()
            .AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services
            .AddScoped<ICommentRepository, CommentRepository>()
            .AddScoped<IPostRepository, PostRepository>()
            .AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IMvcBuilder AddValidation(this IMvcBuilder builder)
        {

            builder.AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssemblyContaining<AddCommentRequestValidator>();


            });

              
            return builder;

        }


    }
}
