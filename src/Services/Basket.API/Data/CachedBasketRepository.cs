using System.Text.Json;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache): IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(username);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }
        var basket = await repo.GetBasket(username, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    { 
        await repo.StoreBasket(basket, cancellationToken);
        
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        
        return basket;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    { 
        await repo.DeleteBasket(username, cancellationToken);
        await cache.RemoveAsync(username, cancellationToken);
        return true;
    }
}