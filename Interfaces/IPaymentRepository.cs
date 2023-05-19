using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface IPaymentRepository
    {
        Payment GetPayment(int id);
        bool PaymentExists(int id);
        Payment GetPaymentByUserId(int userId);
        bool PaymentExistsByUserId(int userId);
        bool CreatePayment(Payment payment);
        bool UpdatePayment(Payment payment);
        bool DeletePayment(Payment payment);
        bool Save();
    }
}
