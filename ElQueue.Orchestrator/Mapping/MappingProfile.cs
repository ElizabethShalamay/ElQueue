using AutoMapper;
using ElQueue.BLL.Models;
using ElQueue.DAL.Models;
using ElQueue.Orchestrator.Dtos;

namespace ElQueue.Orchestrator.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewQueueDto, QueueBm>()
                .ForMember(queue => queue.TimeSlots, opt => opt.Ignore())
                .ForMember(queue => queue.IsActive, opt => opt.Ignore())
                .ForMember(queue => queue.Account, opt => opt.Ignore())
                .ForMember(queue => queue.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(queueDto => queueDto.EndTime, opt => opt.Ignore())
                .ForMember(queueDto => queueDto.StartTime, opt => opt.Ignore())
                .ForMember(queueDto => queueDto.TimeSlotDuration, opt => opt.Ignore())
                .ForMember(queueDto => queueDto.TimeSlotNumber, opt => opt.Ignore());

            CreateMap<QueueToUpdateDto, QueueBm>()
                .ForMember(queue => queue.TimeSlots, opt => opt.Ignore())
                .ForMember(queue => queue.IsActive, opt => opt.Ignore())
                .ForMember(queue => queue.Account, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(queueDto => queueDto.EndTime, opt => opt.Ignore())
                .ForMember(queueDto => queueDto.StartTime, opt => opt.Ignore())
                .ForMember(queueDto => queueDto.TimeSlotDuration, opt => opt.Ignore())
                .ForMember(queueDto => queueDto.TimeSlotNumber, opt => opt.Ignore());

            CreateMap<NewQueueDto, QueueToUpdateDto>()
                .ForMember(queue => queue.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<QueueBm, RawQueue>()
                .ReverseMap()
                .ForMember(queue => queue.TimeSlots, opt => opt.Ignore());

            CreateMap<UserBm, RawUser>()
                .ReverseMap()
                .ForMember(user => user.TimeSlots, opt => opt.Ignore());

            CreateMap<User, UserBm>()
            .ForMember(userBm => userBm.Name, o => o.MapFrom(user=> user.UserName))
            .ForMember(userBm => userBm.Phone, opt => opt.MapFrom(user => user.PhoneNumber))
            .ForMember(userBm => userBm.Address, opt => opt.MapFrom(user => user.Address))
            .ForMember(userBm => userBm.Id, opt => opt.MapFrom(user => user.Id))
            .ForMember(userBm => userBm.Email, opt => opt.MapFrom(user => user.Email))
            .ForMember(userBm => userBm.TimeSlots, opt => opt.MapFrom(user => user.TimeSlots))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<RegisterModel, User>()
            .ForMember(user => user.UserName, o => o.MapFrom(model => model.Login))
            .ForMember(user => user.Email, opt => opt.MapFrom(model => model.Email))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
