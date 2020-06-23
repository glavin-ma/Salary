using Microsoft.EntityFrameworkCore;
using Models.Employment;
using System;
using System.Threading.Tasks;

namespace DAL.Context
{
    public interface IDataContext : IDisposable
    {
        DbSet<Employee> Employees { get; }
        Task Commit();
    }
}
