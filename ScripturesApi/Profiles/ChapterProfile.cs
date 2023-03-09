using AutoMapper;
using Entity = ScripturesApi.Domain.Entities;
using Model = ScripturesApi.ViewModels;

namespace ScripturesApi.Profiles;

public class ChapterProfile : Profile
{
    public ChapterProfile()
    {
        CreateMap<Entity.Chapter, Model.Chapter>();
    }
}
