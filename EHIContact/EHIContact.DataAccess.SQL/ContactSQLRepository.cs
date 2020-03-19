using EHIContact.Core.Contracts;
using EHIContact.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHIContact.DataAccess.SQL
{
    public class ContactSQLRepository : IContactDataAccessRepository
    {
        private DataContext context;
        public ContactSQLRepository()
        {
            context = new DataContext();
        }
        public IQueryable<Contact> Colleciton()
        {
            return context.Contacts.AsQueryable(); 
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public bool Delete(int Id)
        {
            Contact contactToDelete = context.Contacts.Find(Id);
            if (contactToDelete != null)
            {
                context.Contacts.Remove(contactToDelete);
                return true;
            }
            else
                return false;
        }

        public Contact Find(int Id)
        {
            return context.Contacts.Find(Id);
        }

        public bool InActivate(int Id)
        {
            Contact contactToInactivate = context.Contacts.Find(Id);
            if (contactToInactivate != null)
            {
                contactToInactivate.ActiveStatus = false;
                return true;
            }
            else
                return false;
        }

        public void Insert(Contact contact)
        {
            context.Contacts.Add(contact);
        }

        public bool Update(Contact contact)
        {
            Contact contactToUpdate = context.Contacts.Find(contact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.FirstName = contact.FirstName;
                contactToUpdate.LastName = contact.LastName;
                contactToUpdate.PhoneNumber = contact.PhoneNumber;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.ActiveStatus = contact.ActiveStatus;
                return true;
            }
            else
                return false;
        }
    }
}
