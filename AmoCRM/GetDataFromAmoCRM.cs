using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AmoCRM.Models;
using AmoCRM.Classes;
using Newtonsoft.Json;

namespace AmoCRM
{
	public class GetDataFromAmoCRM
	{
		private readonly CookieContainer CookieContainerToAmoCRM;
		private readonly String HostAmoCRM;

		public GetDataFromAmoCRM(String hostAmoCRM, String ClientId, String ClientSecret)
		{
			var url = hostAmoCRM + "/private/api/auth.php?type=json&USER_LOGIN=" + ClientId + "&USER_HASH=" + ClientSecret;
			CookieContainerToAmoCRM = Provider.GetCookieContainer(url);
			HostAmoCRM = hostAmoCRM;
		}

		public List<UserResponse> GetUsers()
		{
			var accountsResponseJson = Provider.SendGetResponse(HostAmoCRM + "/private/api/v2/json/accounts/current", CookieContainerToAmoCRM);
			var accountsResponse = JsonConvert.DeserializeObject<AccountResponseRoot>(accountsResponseJson);
			return accountsResponse.response.account.users;
		}

		public List<Link> GetLeadsAndContacts(string leadNumber)
		{
			var leadsAndContactsResponseJson = Provider.SendGetResponse(HostAmoCRM + "/private/api/v2/json/contacts/links?deals_link=" + leadNumber, CookieContainerToAmoCRM);
			var leadsAndContactsResponse = JsonConvert.DeserializeObject<LeadsAndContactsRoot>(leadsAndContactsResponseJson);
			if (leadsAndContactsResponse==null)
			{
				return new List<Link>();
			}

			return leadsAndContactsResponse.response.links;
		}

		public List<LeadResponse> GetLeads()
		{
			var leadRequest = new LeadsRequestRoot();
			leadRequest.SetRequest();


			string leadRequestJson = JsonConvert.SerializeObject(leadRequest);
			var leadResponseJson = Provider.SendPOSTResponse(HostAmoCRM + "/private/api/v2/json/leads/list?status=142", leadRequestJson, CookieContainerToAmoCRM);
			var leadResponse = JsonConvert.DeserializeObject<LeadResponseRoot>(leadResponseJson);
			return leadResponse.response.leads;
		}

		public List<ContactResponse> GetContacts(object contact_id, string linked_company_id)
		{
			var contactIsComplete = true;
			try
			{
				contactIsComplete = (bool) contact_id;
			}
			catch
			{

			}

			var companyIsComplete = !(linked_company_id =="" || linked_company_id=="0");


			if (companyIsComplete && contactIsComplete)
			{
				//var mm = 5;
			}

			var contactsResponseJson = "";
			if (contactIsComplete)
			{
				contactsResponseJson = Provider.SendGetResponse(HostAmoCRM + "/private/api/v2/json/contacts/list?id=" + contact_id, CookieContainerToAmoCRM);
			}
			else
			{
				contactsResponseJson = Provider.SendGetResponse(HostAmoCRM + "/private/api/v2/json/company/list?id=" + linked_company_id, CookieContainerToAmoCRM);
			}


			var contactsResponse = JsonConvert.DeserializeObject<ContactResponseRoot>(contactsResponseJson);

			if (contactsResponse==null)
			{
				return null;
			}

			return contactsResponse.response.contacts;
		}

	}
}
