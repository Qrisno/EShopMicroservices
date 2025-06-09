using BuildingBlocks.Exceptions;

namespace Basket.API.Exceptions;

public class BasketNotFoundException(string userName): NotFoundException(userName)
{
    
}