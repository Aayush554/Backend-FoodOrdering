using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PaymentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

                /*
         NAME

            GetPaymentByUserId - Retrieves a payment by user ID.

         DESCRIPTION

            This method retrieves a Payment object associated with the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns the Payment object representing the payment for the user.
         */
        public Payment GetPaymentByUserId(int userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return GetPayment((int)user.Id);
        }

                /*
         NAME

            CreatePayment - Creates a new payment.

         DESCRIPTION

            This method adds a new payment to the data context.

         PARAMETERS

            payment - The Payment object representing the payment to be created.

         RETURNS

            Returns true if the payment creation was successful; otherwise, false.
         */
        public bool CreatePayment(Payment payment)
        {
            _context.Add(payment);
            return Save();
        }

                /*
         NAME

            DeletePayment - Deletes a payment.

         DESCRIPTION

            This method removes a payment from the data context.

         PARAMETERS

            payment - The Payment object representing the payment to be deleted.

         RETURNS

            Returns true if the payment deletion was successful; otherwise, false.
         */
        public bool DeletePayment(Payment payment)
        {
            _context.Remove(payment);
            return Save();
        }

                /*
         NAME

            PaymentExistsByUserId - Checks if a payment exists for a user by user ID.

         DESCRIPTION

            This method checks if a payment exists for the specified user ID in the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns true if a payment exists for the user with the specified ID; otherwise, false.
         */
        public bool PaymentExistsByUserId(int userId)
        {
            return _context.Payment.Where(p => p.UserId == userId).FirstOrDefault() != null ? true: false;
        }

                /*
         NAME

            Save - Saves changes to the data context.

         DESCRIPTION

            This method saves changes made to the data context.

         RETURNS

            Returns true if the save operation was successful; otherwise, false.
         */
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
                /*
         NAME

            UpdatePayment - Updates a payment.

         DESCRIPTION

            This method updates a payment in the data context.

         PARAMETERS

            payment - The Payment object representing the payment to be updated.

         RETURNS

            Returns true if the update operation was successful; otherwise, false.
         */
        public bool UpdatePayment(Payment payment)
        {
            _context.Update(payment);
            return Save();
        }
                /*
         NAME

            GetPayment - Retrieves a payment by ID.

         DESCRIPTION

            This method retrieves a Payment object with the specified ID from the data context.

         PARAMETERS

            id - An integer representing the ID of the payment.

         RETURNS

            Returns the Payment object representing the payment.
         */
        public Payment GetPayment(int id)
        {
           return _context.Payment.Where(p => p.Id == id).FirstOrDefault();
        }
                /*
         NAME

            PaymentExists - Checks if a payment with a specific ID exists.

         DESCRIPTION

            This method checks if a payment with the specified ID exists in the data context.

         PARAMETERS

            id - An integer representing the ID of the payment.

         RETURNS

            Returns true if a payment with the specified ID exists; otherwise, false.
         */
        public bool PaymentExists(int id)
        {
            return _context.Payment.Any(p => p.Id == id);
        }
    }
}
