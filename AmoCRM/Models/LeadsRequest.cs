using Newtonsoft.Json;

namespace AmoCRM.Models
{
	public class LeadsRequestRoot
	{
		public LeadsRequest request { get; set; }

		public void SetRequest()
		{
			this.request = new LeadsRequest();
			this.request.leads = new Leads();
			this.request.leads.query = "";
			this.request.leads.limit_rows = 1;
		}

	}

	public class LeadsRequest
	{
		public Leads leads { get; set; }
	}

	public class Leads
	{
		public string query { get; set; }
		public int limit_rows { get; set; }

	}
}
