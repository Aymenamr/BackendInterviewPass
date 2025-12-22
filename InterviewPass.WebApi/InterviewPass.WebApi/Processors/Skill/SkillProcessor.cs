using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Processors.Skill;


    public class SkillProcessor : ISkillProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SkillProcessor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public SkillModel ProcessSkill(SkillModel skill)
        {
            // Map model to entity
            Skill skillEntity = _mapper.Map<Skill>(skill);

            // Add Skill
            string id = _unitOfWork.SkillRepo.Add(skillEntity).Id;

            // Assign generated Id back to model
            skill.Id = id;

            // Save to database
            _unitOfWork.Save();

            return skill;
        }
    }
