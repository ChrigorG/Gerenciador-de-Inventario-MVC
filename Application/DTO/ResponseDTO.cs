﻿
namespace Application.DTO
{
    public sealed class ResponseDTO
    {
        public string View { get; set; } = string.Empty;
        public bool StatusErro { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
