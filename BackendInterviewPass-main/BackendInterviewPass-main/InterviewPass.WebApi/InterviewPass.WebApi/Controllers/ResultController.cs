using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResultController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _unitOfWork.ResultRepository.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.ResultRepository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResultModel model)
        {
            var entity = _mapper.Map<Result>(model);
            await _unitOfWork.ResultRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ResultModel model)
        {
            var existing = await _unitOfWork.ResultRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(model, existing);
            _unitOfWork.ResultRepository.Update(existing);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.ResultRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _unitOfWork.ResultRepository.Delete(existing);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }

}
