using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class StockMovementsService : IStockMovementsService
    {
        private readonly IStockMovementsRepository _stockMovementsRepository;
        private readonly IMapper _mapper;

        public StockMovementsService(IStockMovementsRepository stockMovementsRepository,
            IMapper mapper)
        {
            _stockMovementsRepository = stockMovementsRepository;
            _mapper = mapper;
        }

        public async Task<StockMovementsDTO> GetStockMovements()
        {
            return new StockMovementsDTO()
            {
                Title = "Movimentação de Estoque",
                ListStockMovements = await GetList()
            };
        }

        private async Task<List<StockMovementsDTO>> GetList()
        {
            List<StockMovements> stockMovements = await _stockMovementsRepository.Get();
            return _mapper.Map<List<StockMovementsDTO>>(stockMovements);
        }
    }
}
