using Microsoft.Azure.Cosmos;
using Yarp.ReverseProxy.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using Oasis.Communication.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Oasis.Communication.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IQueueClient, QueueClient>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<SampleAuthorizationFilter>();
builder.Services.AddScoped<SampleResourceFilter>();
builder.Services.AddScoped<SampleActionFilter>();
builder.Services.AddLogging();
builder.Services.AddSingleton((s) =>
{
    return new CosmosClient(builder.Configuration.GetValue<string>("cosmosDBEndpoint"), builder.Configuration.GetValue<string>("cosmosDBKey"));
});
builder.Services.AddMvc(options =>
{
    options.Filters.Add<SampleAuthorizationFilter>();
    options.Filters.Add<SampleResourceFilter>();
    options.Filters.Add<SampleActionFilter>();

});
//Add Reverse Proxy
builder.Services.AddReverseProxy()
                    .LoadFromMemory(new RouteConfig[]
                    {
                        new RouteConfig
                        {
                            RouteId = "route1",
                            ClusterId = "cluster1",
                            Match = new RouteMatch
                            {
                                Path = "/api"
                            }
                        }
                    }
                    , new ClusterConfig[]
                    {
                        new ClusterConfig
                        {
                            ClusterId="cluster1",
                            Destinations=new Dictionary<string, DestinationConfig>(new Dictionary<string, DestinationConfig>()
                            {

                            }),
                            LoadBalancingPolicy = "RoundRobin"
                        }
                    });

//Add API Gateway
builder.Services.AddOcelot();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Get<string>(),
            ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Get<string>(),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:SecretKey").Get<string>()))
        };
    });

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(ep =>
{
    ep.MapReverseProxy();
});
//app.UseOcelot().GetAwaiter();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Oasis Communication");
});
}
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
try
{
    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
}
