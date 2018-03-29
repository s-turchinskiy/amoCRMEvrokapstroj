using System;
using System.Collections.Generic;
using System.Linq;
using AmoCRM.Classes;
using AmoCRM.Models;
using Newtonsoft.Json;
using System.Net;

namespace AmoCRM
{
    public class Work
    {
        public void GetFromAmoCRMSendTo1C(String hostAmoCRM, String ClientId, String ClientSecret, String host1CGet, String host1CPost)
        {
            var getDataFromAmoCRM = new GetDataFromAmoCRM(hostAmoCRM, ClientId, ClientSecret);

            var leads = getDataFromAmoCRM.GetLeads();
            leads = leads.Where(item => item.pipeline_id == 498874).ToList();
            Log.WriteInfo("leads load, count: " + leads.Count());
            var users = getDataFromAmoCRM.GetUsers();
            Log.WriteInfo("users load, count: " + users.Count());

            var dataFor1CList = new List<DataFor1C>();
            foreach (var item in leads)
            {
                if (leads.IndexOf(item) != 0 && leads.IndexOf(item) % 50 == 0)
                {
                    Log.WriteInfo("processed " + leads.IndexOf(item) + " leads");
                }

                var dataFor1C = new DataFor1C();

                dataFor1C.codeLead = item.id;

                FillDataFromContacts(getDataFromAmoCRM, dataFor1C, item);
                dataFor1C.name = item.name;
                dataFor1C.createDate = item.date_create.ToString();
                dataFor1C.closeDate = item.date_close.ToString();

                var user = users.Find(p => p.id == item.responsible_user_id);
                if (user == null)
                {
                    continue;
                }

                dataFor1C.managerId = user.id;
                dataFor1C.managerName = user.name;

                SetFieldFromCustomField(dataFor1C, item);

                dataFor1CList.Add(dataFor1C);
            }
            Log.WriteInfo("processed all leads, " + leads.Count());
            Log.WriteInfo("dataFor1CList, count: " + dataFor1CList.Count());

            SendDataFor1C(dataFor1CList, host1CPost);
        }

        private void SendDataFor1C(List<DataFor1C> data, String host1CPost)
        {
            var dataToSend = new List<DataFor1C>();
            string json;
            foreach (var item in data)
            {
                if (data.IndexOf(item) != 0 && data.IndexOf(item) % 10 == 0)
                {
                    json = JsonConvert.SerializeObject(dataToSend);
                    Provider.SendPOSTResponse(host1CPost + "UpdateLeads", json);
                    dataToSend.Clear();

                    Log.WriteInfo("data " + data.IndexOf(item).ToString() + " of " + data.Count());
                }

                dataToSend.Add(item);
            }

            json = JsonConvert.SerializeObject(dataToSend);
            Provider.SendPOSTResponse(host1CPost + "UpdateLeads", json);
            Log.WriteInfo("data " + data.Count().ToString() + " of " + data.Count()
                   + " in " + DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss"));
        }

        private void FillDataFromContacts(GetDataFromAmoCRM getDataFromAmoCRM, DataFor1C dataFor1C, LeadResponse item)
        {

            var contacts = getDataFromAmoCRM.GetContacts(item.main_contact_id, item.linked_company_id);

            if (contacts != null && contacts.Count() > 0)
            {
                dataFor1C.nameContact1 = contacts[0].name;
                dataFor1C.codeContact = contacts[0].id.ToString();
                if (contacts.Count() >= 1)
                {
                    foreach (var custom_field in contacts[0].custom_fields)
                    {
                        switch (custom_field.name)
                        {
                            case "Телефон":
                                foreach (var phone in custom_field.values)
                                {
                                    if (dataFor1C.phonesContact1 == "")
                                    {
                                        dataFor1C.phonesContact1 += phone.value;
                                    }
                                    else
                                    {
                                        dataFor1C.phonesContact1 += "|" + phone.value;
                                    }
                                }
                                break;
                            case "Email":
                                dataFor1C.email = custom_field.values[0].value; break;
                        }


                    }
                }
            }
            else
            {
                Log.WriteInfo("not found contact for " + item.name);
            }

            var leadsAndContacts = getDataFromAmoCRM.GetLeadsAndContacts(item.id);
            foreach (var itemLinks in leadsAndContacts)
            {
                if (itemLinks.contact_id == item.main_contact_id.ToString())
                {
                    continue;
                }

                var contacts2 = getDataFromAmoCRM.GetContacts(itemLinks.contact_id, "");

                dataFor1C.nameContact2 = contacts2[0].name;
                foreach (var custom_field in contacts2[0].custom_fields)
                {
                    switch (custom_field.name)
                    {
                        case "Телефон":
                            foreach (var phone in custom_field.values)
                            {
                                if (phone.value == "")
                                {
                                    continue;
                                }

                                if (dataFor1C.phonesContact2 == "")
                                {
                                    dataFor1C.phonesContact2 += phone.value;
                                }
                                else
                                {
                                    dataFor1C.phonesContact2 += "|" + phone.value;
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void SetFieldFromCustomField(DataFor1C dataFor1C, LeadResponse item)
        {

            foreach (var custom_field in item.custom_fields)
            {
                var vals = custom_field.values;
                string value = vals != null && vals.Any() ? vals[0]?.value : null;

                switch (custom_field.name)
                {
                    case "Адрес объекта": dataFor1C.address = value; break;
                    case "Объем работ (м2)": dataFor1C.scopeOfWork = value; break;
                    case "Толщина (мм)": dataFor1C.thickness = value; break;
                    case "Этаж клиента": dataFor1C.floor = value; break;
                    case "Доплата": dataFor1C.basicPayment = value; break;
                    case "Предоплата": dataFor1C.prepayment = value; break;
                    case "Площадка": dataFor1C.districtName = value; break;
                    case "Бюджет": dataFor1C.budget = value; break;
                    case "Дом": dataFor1C.house = value; break;
                    case "Подъезд": dataFor1C.doorNumber = value; break;
                    case "Количество комнат": dataFor1C.numberOfRooms = value; break;
                    case "Корпус": dataFor1C.houseBlock = value; break;
                    case "Тип дома": dataFor1C.typeOfHouse = value; break;
                    case "Стоимость работ": dataFor1C.costOfWork = value; break;
                    case "Стоимость материала": dataFor1C.costOfMaterials = value; break;
                    case "ЖК": dataFor1C.housingCooperative = value; break;
                    case "Номер квартиры": dataFor1C.apartment = value; break;
                    case "Замерщики": dataFor1C.gauger1 = value; break;
                    case "Прораб": dataFor1C.foreman = value; break;
                    case "Дата начала работ": dataFor1C.createDate = value; break;
                    case "Дата выдачи": dataFor1C.passportIssueDate = value; break;
                    case "Кем выдан": dataFor1C.passportIssuedBy = value; break;
                    case "Номер": dataFor1C.passportNumer = value; break;
                    case "Адрес регистрации": dataFor1C.passportRegistrationAddress = value; break;
                    case "Исполнители":
                        {
                            var count = custom_field.values.Count();
                            foreach (var itemTeam in custom_field.values)
                            {
                                if (dataFor1C.team == "")
                                {
                                    dataFor1C.team += itemTeam.value;
                                }
                                else
                                {
                                    dataFor1C.team += "|" + itemTeam.value;
                                }
                            }
                            break;
                        }

                    case "Насос": dataFor1C.pump = value; break;
                    case "Заказал материал": dataFor1C.personOrderedMaterial = value; break;
                    case "план Демпферная лента м/п": dataFor1C.damperBeltPlan = value; break;
                    case "план Песок м³": dataFor1C.sandPlan = value; break;
                    case "план Цемент кг": dataFor1C.cementPlan = value; break;
                    case "план Пленка м²": dataFor1C.membranePlan = value; break;
                    case "план Фибрв кг.": dataFor1C.fiberglassPlan = value; break;
                    case "Принял материал": dataFor1C.personAcceptedMaterial = value; break;
                    case "факт Демпф лента м/п": dataFor1C.damperBeltFact = value; break;
                    case "факт Песок м³": dataFor1C.sandFact = value; break;
                    case "факт Цемент кг": dataFor1C.cementFact = value; break;
                    case "факт Пленка м²": dataFor1C.membraneFact = value; break;
                    case "факт Фибров кг.": dataFor1C.fiberglassFact = value; break;
                }
            }
        }
    }
}
