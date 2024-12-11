using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Helper;
using System.Security.Claims;

namespace Application.Services
{
    public class PermissionGroupService : IPermissionGroupService
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public PermissionGroupService(IPermissionGroupRepository permissionGroupRepository,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _permissionGroupRepository = permissionGroupRepository;
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public PermissionGroupDTO GetPermissionGroup()
        {
            PermissionGroupDTO? permissionGroupDTO = new PermissionGroupDTO();
            string permission = this.GetPermission();
            if (permission == Constants.PermissionDenied)
            {
                permissionGroupDTO.StatusErroMessage = true;
                permissionGroupDTO.Message = "Acesso negado!";
                return permissionGroupDTO;
            }

            permissionGroupDTO.Title = "Grupo de Permissão";
            permissionGroupDTO.ListPermissionGroup = GetList();
            return permissionGroupDTO;
        }

        public PermissionGroupDTO FormPermissionGroup(int id)
        {
            PermissionGroupDTO? permissionGroupDTO = new PermissionGroupDTO();
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                permissionGroupDTO.StatusErroMessage = true;
                permissionGroupDTO.Message = "Acesso negado!";
                return permissionGroupDTO;
            }

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
             permissionGroupDTO = _mapper.Map<PermissionGroupDTO?>(permissionGroup);

            if (permissionGroupDTO == null)
            {
                return NotFound(new PermissionGroupDTO());
            }

            permissionGroupDTO.Title = $"Atualizar os dados do grupo de permissão {permissionGroupDTO.Id} - {permissionGroupDTO.Name}";
            return permissionGroupDTO;
        }

        public PermissionGroupDTO SavePermissionGroup(PermissionGroupDTO permissionGroupDTO)
        {
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                permissionGroupDTO.StatusErroMessage = true;
                permissionGroupDTO.Message = "Acesso negado!";
                return permissionGroupDTO;
            }

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
            PermissionGroupDTO? permissionGroupDTO = new PermissionGroupDTO();
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                permissionGroupDTO.StatusErroMessage = true;
                permissionGroupDTO.Message = "Acesso negado!";
                return permissionGroupDTO;
            }

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

            permissionGroupDTO = _mapper.Map<PermissionGroupDTO>(permissionGroup);
            permissionGroupDTO.ListPermissionGroup = GetList();
            return permissionGroupDTO;
        }

        public List<PermissionGroupDTO> GetList()
        {
            List<PermissionGroup> permissionGroup = _permissionGroupRepository.Get();
            return _mapper.Map<List<PermissionGroupDTO>>(permissionGroup);
        }

        private string GetPermission()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = 0;
            if (userIdClaim != null && int.TryParse(userIdClaim!.Value, out userId))
            {
                Employee? employee = _employeeRepository.Get(userId);
                PermissionGroup? permissionGroup = _permissionGroupRepository.Get(employee?.IdPermissionGroup ?? 0);

                return permissionGroup?.ActionPermissionGroup ?? Constants.PermissionDenied;
            }
            return Constants.PermissionDenied;
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
