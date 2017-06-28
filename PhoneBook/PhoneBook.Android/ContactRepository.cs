using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.Threading.Tasks;

namespace PhoneBook.Droid
{
    class ContactRepository
    {
        private readonly SQLiteAsyncConnection sqlConnection;

        public ContactRepository()
        {
            sqlConnection = new SQLiteAsyncConnection(FileAccessHelper.GetLocalFilePath("people.db3"));
            sqlConnection.CreateTableAsync<Contact>();
        }

        public async Task<List<Contact>> GetContacts()
        {
            return await sqlConnection.Table<Contact>().ToListAsync(); 
        }
        
        public async Task<Contact> GetContact(int id)
        {
            return await sqlConnection.Table<Contact>().Where(tdi => tdi.id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteContact(Contact contact)
        {
            await sqlConnection.DeleteAsync(contact);
        }

        public async Task AddContact(Contact contact)
        {
            await sqlConnection.InsertAsync(contact);
        }
    }
}