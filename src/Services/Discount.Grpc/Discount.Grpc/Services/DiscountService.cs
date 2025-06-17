using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext discountContext): DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var couponFound = await discountContext.Coupons.FirstOrDefaultAsync(coupon =>coupon.ProductName == request.ProductName);
        return new CouponModel
        {
            ProductName = couponFound.ProductName,
            Description = couponFound.Description,
            Amount = couponFound.Amount.ToString()
        };
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var newCoupon = new Coupon
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = decimal.Parse(request.Coupon.Amount)
        };
        
        await discountContext.AddAsync(newCoupon);
        await discountContext.SaveChangesAsync();
        return new CouponModel
        {
            ProductName = newCoupon.ProductName,
            Description = newCoupon.Description,
            Amount = newCoupon.Amount.ToString()
        };


    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var couponToUpdate = new Coupon
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = decimal.Parse(request.Coupon.Amount)
        };
        discountContext.Update(couponToUpdate);
        
        discountContext.SaveChangesAsync();
        return Task.FromResult(new CouponModel
        {
            Id = request.Coupon.Id,
            ProductName = couponToUpdate.ProductName,
            Description = couponToUpdate.Description,
            Amount = couponToUpdate.Amount.ToString()
        });
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var res =discountContext.Remove(new Coupon { Id = request.Id });
        
        return Task.FromResult(new DeleteDiscountResponse
        {
            Deleted = res.State == EntityState.Deleted,
        });
        
        
    }
}