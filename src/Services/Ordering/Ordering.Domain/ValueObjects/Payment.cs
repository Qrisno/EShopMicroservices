using System.Text.RegularExpressions;

namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardNumber { get;  } = string.Empty;
    public string CardHolderName { get; } = string.Empty;
    public string ExpiryDate { get;  } = string.Empty;
    public string CVV { get;  } = string.Empty;
    public string PaymentMethod { get;  } = string.Empty;
    private Payment() { }

    private Payment(string cardNumber, string cardHolderName, string expiryDate, string cvv, string paymentMethod)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpiryDate = expiryDate;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardNumber, string cardHolderName, string expiryDate, string cvv,
        string paymentMethod)
    {
        // Supports both MM/YY and MM/YYYY
        var regex = new Regex(@"^(0[1-9]|1[0-2])\/(([0-9]{2})|([0-9]{4}))$");
        ArgumentException.ThrowIfNullOrEmpty(cardNumber);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cardNumber.Length, 16);
        ArgumentOutOfRangeException.ThrowIfLessThan(cardNumber.Length, 16);
        ArgumentException.ThrowIfNullOrEmpty(cardNumber);
        ArgumentException.ThrowIfNullOrEmpty(expiryDate);
        ArgumentException.ThrowIfNullOrEmpty(cvv);
        ArgumentOutOfRangeException.ThrowIfLessThan(cvv.Length, 3);
        if(!regex.IsMatch(expiryDate))
            throw new DomainException("Invalid expiry Date format, it must be MM/YY or MM/YYYY");
        CheckCardExpiration(expiryDate);
        
        ArgumentException.ThrowIfNullOrEmpty(paymentMethod);
        ArgumentException.ThrowIfNullOrEmpty(cardHolderName);

        return new Payment( cardNumber, cardHolderName, expiryDate, cvv, paymentMethod);
    }

    private static bool CheckCardExpiration(string expirydate)
    {
        var parts = expirydate.Split('/');
        int month = int.Parse(parts[0]);
        int year = int.Parse(parts[1]);
        // Create a date for the last day of the expiry month
        var expiryDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        if (expiryDate < DateTime.Today)
        {
            throw new DomainException("Card seems to be expired");
        }

        return false;

    }
}