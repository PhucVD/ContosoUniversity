using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork: IDisposable
    {
        private DataContext context;
        private bool isDisposed = false;

        public UnitOfWork()
        {
            this.isDisposed = false;
            this.context = new DataContext();
        }

        public DataContext GetContext()
        {
            if (this.isDisposed || this.context == null)
            {
                this.context = new DataContext();
                this.isDisposed = false;
            }

            return context;
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
