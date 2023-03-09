using AutoMapper;
using Entity = ScripturesApi.Domain.Entities;
using Model = ScripturesApi.ViewModels;

namespace ScripturesApi.Profiles;

public class VerseProfile : Profile
{
    public VerseProfile()
    {
        CreateMap<Entity.Verse, Model.Verse>();
    }
}
