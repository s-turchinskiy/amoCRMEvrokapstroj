using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoCRM.Models
{
	public class UserResponse
	{
		public string id { get; set; }
		public string name { get; set; }
		public string last_name { get; set; }

	}

	public class AccountResponse
	{
		public string id { get; set; }
		public string name { get; set; }
		public List<UserResponse> users { get; set; }

	}

	public class AccountsResponse
	{
		public AccountResponse account { get; set; }
		public int server_time { get; set; }
	}

	public class AccountResponseRoot
	{
		public AccountsResponse response { get; set; }
	}
}
