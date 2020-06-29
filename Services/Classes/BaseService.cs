using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;

namespace Services.Classes
{
    public class BaseService
    {
        private IDataContext _dataContext;

        internal IDataContext DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    throw new Exception("Data Context is not initialized");
                }
                return _dataContext;
            }
            set { _dataContext = value; }
        }
    }
}
