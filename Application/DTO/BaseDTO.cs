using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public Boolean StatusMessage { get; set; }
        public String Message { get; set; } = string.Empty;
    }
}
