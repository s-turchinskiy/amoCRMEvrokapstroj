using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoCRM.Models
{
	public class Tag
	{
		public int id { get; set; }
		public string name { get; set; }
		public int element_type { get; set; }
	}

	public class Custom_field
	{
		public int id { get; set; }
		public string name { get; set; }
		public List<Values> values { get; set; }
	}

	public class Values
	{
		public string value { get; set; }
	}

	public class LeadResponse
	{
		public string id { get; set; }
		public string name { get; set; }
		public int date_create { get; set; }
		public string created_user_id { get; set; }
		public int last_modified { get; set; }
		public string account_id { get; set; }
		public string price { get; set; }
		public string responsible_user_id { get; set; }
		public string linked_company_id { get; set; }
		public int group_id { get; set; }
		public int pipeline_id { get; set; }
		public int date_close { get; set; }
		public int closest_task { get; set; }
		public int deleted { get; set; }
		public List<Tag> tags { get; set; }
		public string status_id { get; set; }
		public List<Custom_field> custom_fields { get; set; }
		public object main_contact_id { get; set; }
	}

	public class LeadsResponse
	{
		public List<LeadResponse> leads { get; set; }
		public int server_time { get; set; }
	}

	public class LeadResponseRoot
	{
		public LeadsResponse response { get; set; }
	}
}
