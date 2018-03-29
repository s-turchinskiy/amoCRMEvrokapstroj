using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoCRM.Models
{
	public class Value
	{
		public string value { get; set; }
		public string @enum { get; set; }
	}

	public class CustomField
	{
		public string id { get; set; }
		public string name { get; set; }
		public string code { get; set; }
		public List<Value> values { get; set; }
	}

	public class ContactResponse
	{
		public int id { get; set; }
		public string name { get; set; }
		public int last_modified { get; set; }
		public int account_id { get; set; }
		public int date_create { get; set; }
		public int created_user_id { get; set; }
		public int modified_user_id { get; set; }
		public int responsible_user_id { get; set; }
		public int group_id { get; set; }
		public int closest_task { get; set; }
		public string linked_company_id { get; set; }
		public string company_name { get; set; }
		public List<object> tags { get; set; }
		public string type { get; set; }
		public List<CustomField> custom_fields { get; set; }
		public List<string> linked_leads_id { get; set; }
	}

	public class ContactsResponse
	{
		public List<ContactResponse> contacts { get; set; }
		public int server_time { get; set; }
	}

	public class ContactResponseRoot
	{
		public ContactsResponse response { get; set; }
	}
}
