namespace Presentation.DependencyRegistration
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();
            services.AddHttpContextAccessor();
            services.AddSerilogUI(builder.Configuration);
            services.AddMvcWithAuthPolicy();
            services.AddHealthChecks();

            services.AddCors(options => options.AddPolicy("CORS",
            policy =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            }));

            return services;
        }
    }
}
