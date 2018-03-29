using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoCRM.Models
{
	public class Link
	{
		public string contact_id { get; set; }
		public string lead_id { get; set; }
		public int last_modified { get; set; }
	}

	public class LeadsAndContactsResponse
	{
		public List<Link> links { get; set; }
		public int server_time { get; set; }
	}

	public class LeadsAndContactsRoot
	{
		public LeadsAndContactsResponse response { get; set; }
	}
}
