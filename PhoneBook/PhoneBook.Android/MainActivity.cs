using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhoneBook.Droid
{
	[Activity (Label = "Contacts", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        Button add, showall;
        EditText name;
        ContactRepository repository;
        ListView contactListNames;
        List<string> contactList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

            name = FindViewById<EditText>(Resource.Id.contactName);
            add = FindViewById<Button>(Resource.Id.addContact);
            showall = FindViewById<Button>(Resource.Id.showAllContact);
            contactListNames = FindViewById<ListView>(Resource.Id.contactList);
            repository = new ContactRepository();

            add.Click += async (sender, e) =>
            {
                if (name.Text.Length!=0)
                {
                    var newContact = new Contact { contactName = name.Text.ToString() };
                    name.Text = "";
                    await repository.AddContact(newContact);

                    contactList = new List<string>();
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, contactList);
                    contactListNames.Adapter = adapter;
                    
                    Toast.MakeText(this, "Contact Added Successfully!!", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Enter Name Please!!", ToastLength.Short).Show();
                }
            };

            showall.Click += Showall_Click;
        }

        private async void Showall_Click(object sender, EventArgs e)
        {
            var contacts = await repository.GetContacts();
            contactList = new List<string>();

            foreach (var c in contacts)
            {
                contactList.Add(c.contactName);
            }       
                
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1,contactList);
            contactListNames.Adapter = adapter;
        }
    }
}


