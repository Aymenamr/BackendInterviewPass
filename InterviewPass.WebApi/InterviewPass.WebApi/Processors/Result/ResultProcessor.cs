using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models;
using ResultEntity = InterviewPass.DataAccess.Entities.Result;

namespace InterviewPass.WebApi.Processors.Result
{
    public class ResultProcessor : IResultProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResultProcessor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<ResultModel> GetAll()
        {
            return _mapper.Map<List<ResultModel>>(_unitOfWork.ResultRepo.GetAll());
        }

        public ResultModel? GetById(string id)
        {
            var result = _unitOfWork.ResultRepo.GetByProperty(r => r.Id == id);
            return result == null ? null : _mapper.Map<ResultModel>(result);
        }

        public ResultModel Create(ResultModel model)
        {
            var resultEntity = _mapper.Map<ResultEntity>(model);
            var createdResult = _unitOfWork.ResultRepo.Add(resultEntity);
            _unitOfWork.Save();

            return _mapper.Map<ResultModel>(createdResult);
        }

        public ResultModel? Update(string id, ResultModel model)
        {
            var existingResult = _unitOfWork.ResultRepo.GetByProperty(r => r.Id == id);
            if (existingResult == null)
            {
                return null;
            }

            _mapper.Map(model, existingResult);
            _unitOfWork.ResultRepo.Update(existingResult);
            _unitOfWork.Save();

            return _mapper.Map<ResultModel>(existingResult);
        }

        public bool Delete(string id)
        {
            var result = _unitOfWork.ResultRepo.GetByProperty(r => r.Id == id);
            if (result == null)
            {
                return false;
            }

            _unitOfWork.ResultRepo.Delete(result);
            _unitOfWork.Save();
            return true;
        }
    }
}
