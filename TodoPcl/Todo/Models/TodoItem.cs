using System;
using Realms;

namespace Todo
{
	public class TodoItem : RealmObject
	{
		[ObjectId]
		public string ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}

