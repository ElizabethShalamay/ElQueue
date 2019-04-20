using AutoMapper;
using ElQueue.BLL.Models;
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
        }
    }
}
