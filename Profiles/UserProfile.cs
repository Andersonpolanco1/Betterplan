using AutoMapper;
using BetterplanAPI.DTOs;
using BetterplanAPI.Models;

namespace BetterplanAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>()
                .ForMember(destination => destination.UserFullName,
                options => options.MapFrom(source => source.Firstname + " " + source.Surname)) 

                .ForMember(destination => destination.AdvisorFullName,
                options => options.MapFrom(source => source.Advisor.Firstname + " " + source.Advisor.Surname)
                );

            CreateMap<Goal, UserGoalDto>()
                .ForMember(destination => destination.Financialentity,
                options => options.MapFrom(source => source.Financialentity.Title));

            CreateMap<Goal, UserGoalDetailDto>().
                ForMember(destination => destination.Financialentity,
                options => options.MapFrom(source => source.Financialentity.Title))

            .ForMember(destination => destination.GoalcategoryName,
                options => options.MapFrom(source => source.Goalcategory.Title));
        }
    }

}
