
namespace AmoCRM.Models
{

	public class DataFor1C
	{
		public string codeLead { get; set; } = "";
		public string codeContact { get; set; } = "";
		public string name { get; set; } = "";
		public string closeDate { get; set; } = "";
		public string createDate { get; set; } = "";
		public string nameContact1 { get; set; } = "";
		public string phonesContact1 { get; set; } = "";
		public string nameContact2 { get; set; } = "";
		public string phonesContact2 { get; set; } = "";
		public string email { get; set; } = "";
		public string districtName { get; set; } = "";
		public string address { get; set; } = "";
		public string floor { get; set; } = "";
		public string prepayment { get; set; } = "0";
		public string basicPayment { get; set; } = "0";
		public string scopeOfWork { get; set; } = "0";
		public string thickness { get; set; } = "0";
		public string budget { get; set; } = "0";
		public string house { get; set; } = "";
		public string doorNumber { get; set; } = "";
		public string numberOfRooms { get; set; } = "0";
		public string houseBlock { get; set; } = "";
		public string typeOfHouse { get; set; } = "";
		public string costOfWork { get; set; } = "0";
		public string costOfMaterials { get; set; } = "0";
		public string housingCooperative { get; set; } = "";
		public string apartment { get; set; } = "0";
		public string managerId { get; set; } = "";
		public string managerName { get; set; } = "";
		public string gauger1 { get; set; } = "";
		public string foreman { get; set; } = "";
		public string team { get; set; } = "";
		public string passportIssueDate { get; set; } = "";
		public string passportIssuedBy { get; set; } = "";
		public string passportNumer { get; set; } = "";
		public string passportRegistrationAddress { get; set; } = "";

        public string pump { get; set; } = ""; //Насос

        public string personOrderedMaterial { get; set; } = ""; //Заказал материал
        public string damperBeltPlan { get; set; } = "0"; //план Демпферная лента м/п
        public string sandPlan { get; set; } = "0"; //план Песок м³
        public string cementPlan { get; set; } = "0"; //план Цемент кг
        public string membranePlan { get; set; } = "0"; //план Пленка м²
        public string fiberglassPlan { get; set; } = "0"; //план Фибрв кг.

        public string personAcceptedMaterial { get; set; } = ""; //Принял материал
        public string damperBeltFact { get; set; } = "0"; //факт Демпф лента м/п
        public string sandFact { get; set; } = "0"; //факт Песок м³
        public string cementFact { get; set; } = "0"; //факт Цемент кг
        public string membraneFact { get; set; } = "0"; //факт Пленка м²
        public string fiberglassFact { get; set; } = "0"; //факт Фибров кг.


    }
}
