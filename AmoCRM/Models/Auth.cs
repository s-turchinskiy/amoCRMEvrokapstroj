using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoCRM.Models
{
	public class Settings
	{
		public string Host { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string Host1c { get; set; }

		public Settings(string host, string clientId, string clientSecret, string host1c)
		{
			//Host = host, ClientId
		}
	}
}
