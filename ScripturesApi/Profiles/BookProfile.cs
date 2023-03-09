using AutoMapper;
using Entity = ScripturesApi.Domain.Entities;
using Model = ScripturesApi.ViewModels;

namespace ScripturesApi.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Entity.Book, Model.Book>();
    }
}
