using AutoMapper;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.DTOs.Response;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DbTestEntity, TestEntity>()
                .ReverseMap();
                cfg.CreateMap<TestEntity, TestEntityResponseDto>();
                cfg.CreateMap<TestEntityRequestDto, TestEntity>();
            });
        }
    }
}