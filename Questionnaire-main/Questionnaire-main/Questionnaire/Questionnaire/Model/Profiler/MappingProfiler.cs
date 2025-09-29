using AutoMapper;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;

namespace Questionnaire.Model.Profiler
{
    public class MappingProfiler: Profile
    {
        public MappingProfiler()
        {
            CreateMap<CandidateAnswerDTO, CandidateAnswers>()
                .ForMember(dest => dest.CandidateAnswer, opt => opt.MapFrom(src => src.Answer))
                 //.ForMember(dest => dest.CandidateId, opt => opt.Ignore())
                 .ReverseMap();
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<Logs, LogsDTO>().ReverseMap();
            CreateMap<OptionsAndAnswer, OptionsAndAnswerDTO>().ReverseMap();                
            CreateMap<QuestionDTO, Question>()
                .ForMember(dest => dest.Answertext, opt => opt.MapFrom(src => src.AnswerText)).ReverseMap();
            CreateMap<AnswerEvaluationDTO, AnswerEvaluation>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<Question, QuestionAndOptionDTO>()
            .ForMember(dest => dest.QuestionId,opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.QuestionText));
            CreateMap<OptionsAndAnswer, QuestionAndOptionDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.OptionText));
        }
    }
}