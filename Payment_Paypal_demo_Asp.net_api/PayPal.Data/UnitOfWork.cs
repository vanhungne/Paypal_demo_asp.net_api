using Microsoft.EntityFrameworkCore.Storage;
using PayPal.Data.Data;
using PayPal.Data.Interfaces;
using PayPal.Data.Repository;
using System;
using System.Threading.Tasks;

namespace PayPal.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataDbContext _context;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IPaymentRepository _paymentRepository;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(DataDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_context);
                return _userRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                _orderRepository ??= new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_context);
                return _productRepository;
            }
        }

        public IPaymentRepository PaymentRepository
        {
            get
            {
                _paymentRepository ??= new PaymentRepository(_context);
                return _paymentRepository;
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return null;
            }

            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync();
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }

                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}