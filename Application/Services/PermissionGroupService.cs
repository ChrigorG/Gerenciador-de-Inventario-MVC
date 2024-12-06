using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;

namespace Application.Services
{
    public class PermissionGroupService : IPermissionGroupService
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IMapper _mapper;

        public PermissionGroupService(
            IPermissionGroupRepository permissionGroupRepository,
            IMapper mapper)
        {
            _permissionGroupRepository = permissionGroupRepository;
            _mapper = mapper;
        }

        public PermissionGroupDTO GetPermissionGroup()
        {
            return new PermissionGroupDTO()
            {
                Title = "Grupo de Permissão",
                ListPermissionGroup = GetList()
            };
        }

        public PermissionGroupDTO FormPermissionGroup(int id)
        {
            // Se o id for nullo ou zero será tratado como um novo Funcionario
            if (Util.IsNullOrZero(id))
            {
                return new PermissionGroupDTO()
                {
                    Title = "Adicionar um Grupo de Permissão"
                };
            }

            // A partir daqui seria somente para atualização do grupo de permissão
            PermissionGroup? permissionGroup = _permissionGroupRepository.Get(id);
            PermissionGroupDTO? permissionGroupDTO = _mapper.Map<PermissionGroupDTO?>(permissionGroup);

            if (permissionGroupDTO == null)
            {
                return NotFound(new PermissionGroupDTO());
            }

            permissionGroupDTO.Title = $"Atualizar os dados do grupo de permissão {permissionGroupDTO.Id} - {permissionGroupDTO.Name}";
            return permissionGroupDTO;
        }

        public PermissionGroupDTO SavePermissionGroup(PermissionGroupDTO permissionGroupDTO)
        {
            permissionGroupDTO.ValidatedDTO();
            if (permissionGroupDTO.StatusErroMessage)
            {
                return permissionGroupDTO;
            }

            PermissionGroup? permissionGroup;
            // Adicionar um novo Funcionário
            if (Util.IsNullOrZero(permissionGroupDTO.Id))
            {
                permissionGroup = _mapper.Map<PermissionGroup>(permissionGroupDTO);
                permissionGroup = _permissionGroupRepository.Add(permissionGroup);
                if (permissionGroup == null)
                {
                    return InternalServerError(permissionGroupDTO, $"salvar os dados do funcionário {permissionGroup!.Name}");
                }
            } else // Atualizar o Funcionário
            {
                permissionGroup = _permissionGroupRepository.Get(permissionGroupDTO.Id);
                if (permissionGroup == null)
                {
                    return NotFound(permissionGroupDTO);
                }

                permissionGroup = ConvertDtoToModel(permissionGroup, permissionGroupDTO);
                permissionGroup = _permissionGroupRepository.Update(permissionGroup);
                if (permissionGroup == null)
                {
                    return InternalServerError(permissionGroupDTO, $"atualizar o funcionário {permissionGroup!.Id} - {permissionGroup!.Name}");
                }
            }

            permissionGroupDTO = _mapper.Map<PermissionGroupDTO>(permissionGroup);
            permissionGroupDTO.ListPermissionGroup = GetList();
            return permissionGroupDTO;
        }

        public PermissionGroupDTO DeletePermissionGroup(int id)
        {
            PermissionGroup? permissionGroup = _permissionGroupRepository.Get(id);

            if (permissionGroup == null)
            {
                return NotFound(new PermissionGroupDTO());
            }

            permissionGroup = _permissionGroupRepository.Delete(permissionGroup);
            if (permissionGroup == null)
            {
                return InternalServerError(new PermissionGroupDTO(), $"deletar o grupo de permissão {permissionGroup!.Id} - {permissionGroup!.Name}");
            }

            PermissionGroupDTO permissionGroupDTO = _mapper.Map<PermissionGroupDTO>(permissionGroup);
            permissionGroupDTO.ListPermissionGroup = GetList();
            return permissionGroupDTO;
        }

        private List<PermissionGroupDTO> GetList()
        {
            List<PermissionGroup> permissionGroup = _permissionGroupRepository.Get();
            return _mapper.Map<List<PermissionGroupDTO>>(permissionGroup);
        }

        private PermissionGroup ConvertDtoToModel(PermissionGroup permissionGroup, PermissionGroupDTO permissionGroupDTO)
        {
            permissionGroup.ActionPermissionGroup = permissionGroupDTO.ActionPermissionGroup;
            permissionGroup.ActionStockMovements = permissionGroupDTO.ActionStockMovements;
            permissionGroup.ActionEmployee = permissionGroupDTO.ActionEmployee;
            permissionGroup.ActionProduct = permissionGroupDTO.ActionProduct;
            permissionGroup.Description = permissionGroupDTO.Description;
            permissionGroup.Status = permissionGroupDTO.Status;
            permissionGroup.Name = permissionGroupDTO.Name;

            return permissionGroup;
        }

        private PermissionGroupDTO NotFound(PermissionGroupDTO permissionGroupDTO)
        {
            permissionGroupDTO.StatusErroMessage = true;
            permissionGroupDTO.Message = "Nenhum Grupo de Permissão encontrado!";
            return permissionGroupDTO;
        }

        private PermissionGroupDTO InternalServerError(PermissionGroupDTO permissionGroupDTO, string complementMessage)
        {

            permissionGroupDTO.StatusErroMessage = true;
            permissionGroupDTO.Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!";
            return permissionGroupDTO;
        }
    }
}
