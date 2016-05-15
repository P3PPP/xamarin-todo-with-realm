using System;
using System.Collections.Generic;
using System.Linq;
using Realms;

namespace Todo
{
	public class TodoItemDatabase
	{
		Realm realm;

		public TodoItemDatabase()
		{
			realm = Realm.GetInstance();
		}

		public IEnumerable<TodoItem> GetItems()
		{
			return realm.All<TodoItem>().ToList();
		}

		public IEnumerable<TodoItem> GetItemsNotDone()
		{
			return realm.All<TodoItem>().Where(x => x.Done == false).ToList();
		}

		public TodoItem GetItem(string id)
		{
			return realm.All<TodoItem>()
				        .ToList()
				        .FirstOrDefault(x => x.ID == id);
		}

		public string SaveItem(TodoItem item)
		{
			var newItem = realm.All<TodoItem>()
							 .ToList()
							 .FirstOrDefault(x => x.ID == item.ID);

			using (var trans = realm.BeginWrite()) {
				if (newItem == null) {
					newItem = realm.CreateObject<TodoItem>();
					newItem.ID = Guid.NewGuid().ToString();
				}

				newItem.Name = item.Name;
				newItem.Notes = item.Notes;
				newItem.Done = item.Done;

				trans.Commit();
			}

			return newItem?.ID;
		}

		public string DeleteItem(string id)
		{
			var item = realm.All<TodoItem>()
							 .ToList()
							 .FirstOrDefault(x => x.ID == id);

			if (item == null)
				return null;

			using (var trans = realm.BeginWrite()) {
				realm.Remove(item);
				trans.Commit();
			}

			return id;
		}
	}
}

