using AutoMapper;
using ChallengeTaller.Models;
using DesafioTaller;
using DesafioTaller.DTO;
using DesafioTaller.Helpers;
using DesafioTaller.Interfaces;
using DesafioTaller.Servicios;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

var cadenaConexionSql = new ConexionBaseDatos(config.GetConnectionString("SQL"));
builder.Services.AddSingleton(cadenaConexionSql);
builder.Services.AddScoped<IServicioVehiculo, ServicioVehiculo>();
builder.Services.AddScoped<IServicioPresupuesto, ServicioPresupuesto>();
builder.Services.AddScoped<IServicioRepuesto, ServicioRepuesto>();


var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfiles());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class VehiculoDTOToVehiculoConverter : ITypeConverter<VehiculoDTO, Vehiculo>
{
    public Vehiculo Convert(VehiculoDTO source, Vehiculo destination, ResolutionContext context)
    {
        if (source.Tipo == TipoVehiculo.Automovil)
        {
            var automovil = context.Mapper.Map<Automovil>(source);
            // No se necesita manipulación adicional para cargar los desperfectos en Automovil
            return automovil;
        }
        else if (source.Tipo == TipoVehiculo.Moto)
        {
            var moto = context.Mapper.Map<Moto>(source);
            // No se necesita manipulación adicional para cargar los desperfectos en Moto
            return moto;
        }
        else
        {
            // Manejar otros tipos de vehículos si es necesario
            return null;
        }
    }
}