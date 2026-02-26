using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Commands
{
    public record AddToCartCommand(
     int ProductId,
     string ProductName,
     decimal Price,
     int Quantity
 );
}
