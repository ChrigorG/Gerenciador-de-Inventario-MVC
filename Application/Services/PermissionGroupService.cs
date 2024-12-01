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

        public async Task<PermissionGroupDTO> GetPermissionGroup()
        {
            return new PermissionGroupDTO()
            {
                Title = "Grupo de Permissão",
                ListPermissionGroup = await GetList()
            };
        }

        public async Task<PermissionGroupDTO> FormPermissionGroup(int id)
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
            PermissionGroup? permissionGroup = await _permissionGroupRepository.Get(id);
            PermissionGroupDTO? permissionGroupDTO = _mapper.Map<PermissionGroupDTO?>(permissionGroup);

            if (permissionGroupDTO == null)
            {
                return NotFound();
            }

            permissionGroupDTO.Title = $"Atualizar os dados do grupo de permissão {permissionGroupDTO.Id} - {permissionGroupDTO.Name}";
            return permissionGroupDTO;
        }

        public async Task<PermissionGroupDTO> SavePermissionGroup(PermissionGroupDTO permissionGroupDTO)
        {
            PermissionGroup? permissionGroup  = _mapper.Map<PermissionGroup>(permissionGroupDTO);
            var validationResults = ValidationEntities.Validate(permissionGroup);

            if (validationResults.Count > 0)
            {
                foreach (var error in validationResults)
                {
                    permissionGroupDTO.Message += $"{error.ErrorMessage}\n";
                }

                permissionGroupDTO.StatusErroMessage = true;
                return permissionGroupDTO;
            }

            // Adicionar um novo Grupo de Permissão
            if (Util.IsNullOrZero(permissionGroup.Id))
            {
                permissionGroup = await _permissionGroupRepository.Add(permissionGroup);
                if (permissionGroup == null)
                {
                    return InternalServerError($"salvar os dados do grupo de permissão {permissionGroup!.Name}");
                }
            } else // Atualizar o Grupo de Permissão
            {
                permissionGroup = await _permissionGroupRepository.Update(permissionGroup);
                if (permissionGroup == null)
                {
                    return InternalServerError($"atualizar o grupo de permissão {permissionGroup!.Id} - {permissionGroup!.Name}");
                }
            }

            permissionGroupDTO = _mapper.Map<PermissionGroupDTO>(permissionGroup);
            permissionGroupDTO.ListPermissionGroup = await GetList();
            return permissionGroupDTO;
        }

        public async Task<PermissionGroupDTO> DeletePermissionGroup(int id)
        {
            PermissionGroup? permissionGroup = await _permissionGroupRepository.Get(id);

            if (permissionGroup == null)
            {
                return NotFound();
            }

            permissionGroup = await _permissionGroupRepository.Delete(permissionGroup);
            if (permissionGroup == null)
            {
                return InternalServerError($"deletar o grupo de permissão {permissionGroup!.Id} - {permissionGroup!.Name}");
            }

            PermissionGroupDTO permissionGroupDTO = _mapper.Map<PermissionGroupDTO>(permissionGroup);
            permissionGroupDTO.ListPermissionGroup = await GetList();
            return permissionGroupDTO;
        }

        private async Task<List<PermissionGroupDTO>> GetList()
        {
            List<PermissionGroup> permissionGroup = await _permissionGroupRepository.Get();
            return _mapper.Map<List<PermissionGroupDTO>>(permissionGroup);
        }

        private PermissionGroupDTO NotFound()
        {
            return new PermissionGroupDTO()
            {
                StatusErroMessage = true,
                Message = "Nenhum Grupo de Permissão encontrado!"
            };
        }

        private PermissionGroupDTO InternalServerError(string complementMessage)
        {
            return new PermissionGroupDTO()
            {
                StatusErroMessage = true,
                Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!"
            };
        }
    }
}
