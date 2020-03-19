using EHIContact.Core.Models;
using System.Linq;

namespace EHIContact.Core.Contracts
{
    public interface IContactDataAccessRepository
    {
        IQueryable<Contact> Colleciton();
        void Commit();
        bool Delete(int Id);
        Contact Find(int Id);
        bool InActivate(int Id);
        void Insert(Contact contact);
        bool Update(Contact contact);
    }
}
