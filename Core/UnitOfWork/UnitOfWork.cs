using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public string UserId
        {
            get
            {
                if (_httpContext == null) return string.Empty;

                return _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }

        public string UserName
        {
            get
            {
                if (_httpContext == null) return string.Empty;

                return _httpContext.User.FindFirstValue(ClaimTypes.Name);
            }
        }

        private Context _context { get; set; }

        private HttpContext _httpContext { get; set; }

        public UnitOfWork(Context context, HttpContext httpContext)
        {
            _context = context;

            _httpContext = httpContext;

            InitRepositories();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #region Repositoies
        private void InitRepositories()
        {
            
        }
        #endregion


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context = null;
            }
        }
    }
}
