using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Commands
{
    public record recordDeleteProductCommand(
        int Id,
    string Name,
    string Type,
    decimal Price,
    string PhotoUrl

        );
    
    
}
