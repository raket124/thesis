using master.Models.Data.Component.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data
{
    class DataModelDemo
    {
        public static DataModel Example1()
        {
            var doc = new DataModel();
            doc.AddComponent(new MyParticipant("Farmer")
            {
                Identifier = "Id",
                Components = new List<Variable>()
                {
                    new Variable("String", "Id", Variable.RELATION.variable),
                    new Variable("String", "Name", Variable.RELATION.variable)
                }
            });
            doc.AddComponent(new MyAsset("Cow")
            {
                Identifier = "Id",
                Components = new List<Variable>()
                {
                    new Variable("String", "Id", Variable.RELATION.variable),
                    new Variable("Farmer", "owner", Variable.RELATION.reference),
                    new Variable("Cow", "father", Variable.RELATION.reference),
                    new Variable("Cow", "mother", Variable.RELATION.reference),
                    new Variable("DateTime", "date_of_birth", Variable.RELATION.variable),
                    new Variable("Boolean", "dead", Variable.RELATION.variable),
                    new Variable("DateTime", "date_of_death", Variable.RELATION.variable),
                    new Variable("Sex", "sex", Variable.RELATION.variable),
                }
            });
            doc.AddComponent(new MyEnum("Sex")
            {
                Options = new List<string>()
                {
                    "Male",
                    "Female"
                }
            });
            return doc;
        }

        public static DataModel Example2()
        {
            var doc = new DataModel();
            doc.AddComponent(new MyParticipant("User")
            {
                Identifier = "Id",
                Abstract = true,
                Components = new List<Variable>()
                {
                    new Variable("String", "Id", Variable.RELATION.variable),
                    new Variable("String", "Name", Variable.RELATION.variable)
                }
            });
            doc.AddComponent(new MyParticipant("Student")
            {
                Parent = "User"
            });
            doc.AddComponent(new MyParticipant("Teacher")
            {
                Parent = "User"
            });
            doc.AddComponent(new MyAsset("Thesis")
            {
                Identifier = "Id",
                Components = new List<Variable>()
                {
                    new Variable("String", "Id", Variable.RELATION.variable),
                    new Variable("String", "title", Variable.RELATION.variable),
                    new Variable("String", "content", Variable.RELATION.variable),
                    new Variable("Student", "student", Variable.RELATION.reference),
                    new Variable("Teacher", "supervisor", Variable.RELATION.reference),
                    new Variable("Grader", "graders", Variable.RELATION.variable) { List = true, Optional = true },
                    new Variable("Integer", "grade", Variable.RELATION.variable) { Optional = true },
                }
            });
            doc.AddComponent(new MyConcept("Grader")
            {
                Components = new List<Variable>()
                {
                    new Variable("Teacher", "teacher", Variable.RELATION.reference),
                    new Variable("Integer", "grade", Variable.RELATION.variable) { Optional = true }
                }
            });
            return doc;
        }

        public static DataModel Example3()
        {
            var doc = new DataModel();
            doc.AddComponent(new MyParticipant("User")
            {
                Identifier = "Id",
                Abstract = true,
                Components = new List<Variable>()
                {
                    new Variable("String", "Id", Variable.RELATION.variable),
                    new Variable("String", "firstName", Variable.RELATION.variable),
                    new Variable("String", "lastName", Variable.RELATION.variable),
                }
            });
            doc.AddComponent(new MyParticipant("CompoundMember")
            {
                Parent = "User",
                Components = new List<Variable>()
                {
                    new Variable("CompoundOrganisation", "organisation", Variable.RELATION.reference)
                }
            });
            doc.AddComponent(new MyParticipant("CarrierMember")
            {
                Parent = "User",
                Components = new List<Variable>()
                {
                    new Variable("CarrierOrganisation", "organisation", Variable.RELATION.reference)
                }
            });
            doc.AddComponent(new MyParticipant("RecipientMember")
            {
                Parent = "User",
                Components = new List<Variable>()
                {
                    new Variable("RecipientOrganisation", "organisation", Variable.RELATION.reference)
                }
            });

            doc.AddComponent(new MyAsset("Organisation")
            {
                Identifier = "Id",
                Abstract = true,
                Components = new List<Variable>()
                {
                    new Variable("String", "Id", Variable.RELATION.variable),
                    new Variable("String", "name", Variable.RELATION.variable)
                }
            });
            doc.AddComponent(new MyAsset("LegalOwnerOrganisation")
            {
                Parent = "Organisation"
            });
            doc.AddComponent(new MyAsset("CarrierOrganisation")
            {
                Parent = "Organisation"
            });
            doc.AddComponent(new MyAsset("RecipientOrganisation")
            {
                Parent = "Organisation"
            });
            doc.AddComponent(new MyAsset("CompoundOrganisation")
            {
                Parent = "Organisation"
            });

            DataModelDemo.EcmrStatus(doc);
            DataModelDemo.OrderStatus(doc);

            doc.AddComponent(new MyConcept("Address")
            {
                Components = new List<Variable>()
                {
                    new Variable("String", "street", Variable.RELATION.variable),
                    new Variable("String", "houseNumber", Variable.RELATION.variable),
                    new Variable("String", "city", Variable.RELATION.variable),
                    new Variable("String", "zipCode", Variable.RELATION.variable),
                    new Variable("String", "country", Variable.RELATION.variable)
                }
            });
            doc.AddComponent(new MyConcept("DateWindow")
            {
                Components = new List<Variable>()
                {
                    new Variable("DateTime", "start", Variable.RELATION.variable),
                    new Variable("DateTime", "end", Variable.RELATION.variable)
                }
            });

            doc.AddComponent(new MyAsset("Vehicle")
            {
                Identifier = "vin",
                Components = new List<Variable>()
                {
                    new Variable("String", "vin", Variable.RELATION.variable),
                    new Variable("String", "plateNumber", Variable.RELATION.variable)
                }
            });
            doc.AddComponent(new MyConcept("Signature")
            {
                Components = new List<Variable>()
                {
                    new Variable("User", "user", Variable.RELATION.reference),
                    new Variable("DateTime", "timestamp", Variable.RELATION.variable),
                    new Variable("String", "remark", Variable.RELATION.variable) { Optional=true }
                }
            });
            doc.AddComponent(new MyConcept("Good")
            {
                Components = new List<Variable>()
                {
                    new Variable("Vehicle", "vehicle", Variable.RELATION.variable),
                    new Variable("Address", "loadingAddress", Variable.RELATION.variable),
                    new Variable("Address", "deliveryAddress", Variable.RELATION.variable),
                    new Variable("DateWindow", "pickupWindow", Variable.RELATION.variable),
                    new Variable("DateWindow", "deliveryWindow", Variable.RELATION.variable),
                }
            });

            doc.AddComponent(new MyAsset("TransportOrder")
            {
                Identifier = "íd",
                Components = new List<Variable>()
                {
                    new Variable("String", "id", Variable.RELATION.variable),
                    new Variable("LegalOwnerOrganisation", "owner", Variable.RELATION.reference),
                    new Variable("CarrierOrganisation", "carrier", Variable.RELATION.reference),
                    new Variable("ECMR", "ecmrs", Variable.RELATION.reference) { List=true },
                    new Variable("OrderStatus", "status", Variable.RELATION.variable),
                }
            });
            doc.AddComponent(new MyAsset("ECMR")
            {
                Identifier = "id",
                Components = new List<Variable>()
                {
                    new Variable("String", "id", Variable.RELATION.variable),
                    new Variable("TransportOrder", "transportOrder", Variable.RELATION.reference),
                    new Variable("CompoundOrganisation", "source", Variable.RELATION.reference),
                    new Variable("RecipientOrganisation", "recipient", Variable.RELATION.reference),
                    new Variable("Signature", "compoundSignature", Variable.RELATION.variable) { Optional=true },
                    new Variable("Signature", "carrierLoadingSignature", Variable.RELATION.variable) { Optional=true },
                    new Variable("Signature", "carrierDeliverySignature", Variable.RELATION.variable) { Optional=true },
                    new Variable("Signature", "recipientSignature", Variable.RELATION.variable) { Optional=true },
                    new Variable("Good", "good", Variable.RELATION.variable),
                    new Variable("EcmrStatus", "status", Variable.RELATION.variable),
                }
            });

            return doc;
        }


        public static DataModel KoopmanCTO()
        {
            var doc = new DataModel() { Namespace = "org.digitalcmr" };

            DataModelDemo.TransportOrder(doc);
            DataModelDemo.ECMR(doc);
            DataModelDemo.Entity(doc);
            doc.AddComponent(new MyAsset("LegalOwnerOrg") { Parent = "Entity" });
            doc.AddComponent(new MyAsset("CompoundOrg") { Parent = "Entity" });
            doc.AddComponent(new MyAsset("CarrierOrg") { Parent = "Entity" });
            doc.AddComponent(new MyAsset("RecipientOrg") { Parent = "Entity" });
            DataModelDemo.Vehicle(doc);

            DataModelDemo.User(doc);
            doc.AddComponent(new MyParticipant("LegalOwnerAdmin") { Parent = "User" });
            doc.AddComponent(new MyParticipant("CompoundAdmin") { Parent = "User" });
            doc.AddComponent(new MyParticipant("CarrierAdmin") { Parent = "User" });
            doc.AddComponent(new MyParticipant("CarrierMember") { Parent = "User" });
            doc.AddComponent(new MyParticipant("RecipientAdmin") { Parent = "User" });
            doc.AddComponent(new MyParticipant("RecipientMember") { Parent = "User" });

            DataModelDemo.Address(doc);
            DataModelDemo.Delivery(doc);
            DataModelDemo.Loading(doc);
            DataModelDemo.Creation(doc);
            DataModelDemo.Cancellation(doc);
            DataModelDemo.Good(doc);
            DataModelDemo.Remark(doc);
            DataModelDemo.Signature(doc);
            DataModelDemo.DateWindow(doc);

            DataModelDemo.EcmrStatus(doc);
            DataModelDemo.OrderStatus(doc);

            return doc;
        }

        private static void TransportOrder(DataModel doc)
        {
            var output = new MyAsset("TransportOrder") { Identifier = "orderID" };
            output.Components.Add(new Variable("String", "orderID", Variable.RELATION.variable));
            output.Components.Add(new Variable("LegalOwnerOrg", "owner", Variable.RELATION.reference));
            output.Components.Add(new Variable("CarrierOrg", "carrier", Variable.RELATION.reference));
            output.Components.Add(new Variable("Good", "goods", Variable.RELATION.variable) { List = true });
            output.Components.Add(new Variable("OrderStatus", "status", Variable.RELATION.variable));
            output.Components.Add(new Variable("Integer", "issueDate", Variable.RELATION.variable));
            output.Components.Add(new Variable("ECMR", "ecmrs", Variable.RELATION.reference) { List = true });
            output.Components.Add(new Variable("String", "orderRef", Variable.RELATION.variable));
            output.Components.Add(new Variable("Cancellation", "cancellation", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void ECMR(DataModel doc)
        {
            var output = new MyAsset("ECMR") { Identifier = "ecmrID" };
            output.Components.Add(new Variable("String", "ecmrID", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "agreementTerms", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "agreementTermsSec", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "legalOwnerRef", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "carrierRef", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "recipientRef", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateTime", "issueDate", Variable.RELATION.variable));
            output.Components.Add(new Variable("Entity", "issuedBy", Variable.RELATION.reference));
            output.Components.Add(new Variable("LegalOwnerOrg", "owner", Variable.RELATION.reference));
            output.Components.Add(new Variable("CompoundOrg", "source", Variable.RELATION.reference));
            output.Components.Add(new Variable("CarrierOrg", "carrier", Variable.RELATION.reference));
            output.Components.Add(new Variable("RecipientOrg", "recipient", Variable.RELATION.reference));
            output.Components.Add(new Variable("RecipientMember", "recipientMember", Variable.RELATION.reference) { Optional = true });
            output.Components.Add(new Variable("CarrierMember", "transporter", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("String", "carrierComments", Variable.RELATION.variable));
            output.Components.Add(new Variable("Loading", "loading", Variable.RELATION.variable));
            output.Components.Add(new Variable("Delivery", "delivery", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "documents", Variable.RELATION.variable) { List = true, Optional = true });
            output.Components.Add(new Variable("Good", "goods", Variable.RELATION.variable) { List = true });
            output.Components.Add(new Variable("String", "legalOwnerInstructions", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "paymentInstructions", Variable.RELATION.variable));
            output.Components.Add(new Variable("Creation", "creation", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "payOnDelivery", Variable.RELATION.variable));
            output.Components.Add(new Variable("Signature", "compoundSignature", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Signature", "carrierLoadingSignature", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Signature", "carrierDeliverySignature", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Signature", "recipientSignature", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("EcmrStatus", "status", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "orderID", Variable.RELATION.variable));
            output.Components.Add(new Variable("Cancellation", "cancellation", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Entity(DataModel doc)
        {
            var output = new MyAsset("Entity") { Identifier = "entityID", Abstract = true };
            output.Components.Add(new Variable("String", "entityID", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "name", Variable.RELATION.variable));
            output.Components.Add(new Variable("Address", "address", Variable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Vehicle(DataModel doc)
        {
            var output = new MyAsset("Vehicle") { Identifier = "vin" };
            output.Components.Add(new Variable("String", "vin", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "manufacturer", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "model", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "type", Variable.RELATION.variable));
            output.Components.Add(new Variable("ECMR", "ecmrs", Variable.RELATION.reference) { List = true });
            output.Components.Add(new Variable("Integer", "odoMeterReading", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "plateNumber", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "registrationCountry", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }

        private static void User(DataModel doc)
        {
            var output = new MyParticipant("User") { Identifier = "userID", Abstract = true };
            output.Components.Add(new Variable("String", "userID", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "userName", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "firstName", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "lastName", Variable.RELATION.variable));
            output.Components.Add(new Variable("Address", "address", Variable.RELATION.variable));
            doc.AddComponent(output);
        }

        private static void Address(DataModel doc)
        {
            var output = new MyConcept("Address");
            output.Components.Add(new Variable("String", "name", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "street", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "houseNumber", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "city", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "zipCode", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "country", Variable.RELATION.variable));
            output.Components.Add(new Variable("Double", "latitude", Variable.RELATION.variable));
            output.Components.Add(new Variable("Double", "longitude", Variable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Delivery(DataModel doc)
        {
            var output = new MyConcept("Delivery");
            output.Components.Add(new Variable("Address", "address", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateWindow", "expectedWindow", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("DateTime", "actualDate", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Loading(DataModel doc)
        {
            var output = new MyConcept("Loading");
            output.Components.Add(new Variable("Address", "address", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateWindow", "expectedWindow", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("DateTime", "actualDate", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Creation(DataModel doc)
        {
            var output = new MyConcept("Creation");
            output.Components.Add(new Variable("Address", "address", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateTime", "date", Variable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Cancellation(DataModel doc)
        {
            var output = new MyConcept("Cancellation");
            output.Components.Add(new Variable("Entity", "cancelledBy", Variable.RELATION.reference));
            output.Components.Add(new Variable("DateTime", "date", Variable.RELATION.variable));
            output.Components.Add(new Variable("String", "reason", Variable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Good(DataModel doc)
        {
            var output = new MyConcept("Good");
            output.Components.Add(new Variable("Vehicle", "vehicle", Variable.RELATION.variable));
            output.Components.Add(new Variable("Remark", "carrierLoadingRemark", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Remark", "compoundRemark", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Remark", "recipientRemark", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Remark", "carrierDeliveryRemark", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("String", "description", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Double", "weight", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Address", "loadingAddress", Variable.RELATION.variable));
            output.Components.Add(new Variable("Address", "deliveryAddress", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateWindow", "pickupWindow", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateWindow", "deliveryWindow", Variable.RELATION.variable));
            output.Components.Add(new Variable("Cancellation", "cancellation", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("CompoundOrg", "source", Variable.RELATION.reference));
            output.Components.Add(new Variable("RecipientOrg", "recipient", Variable.RELATION.reference));
            doc.AddComponent(output);
        }
        private static void Remark(DataModel doc)
        {
            var output = new MyConcept("Remark");
            output.Components.Add(new Variable("String", "comments", Variable.RELATION.variable));
            output.Components.Add(new Variable("Boolean", "isDamaged", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Signature(DataModel doc)
        {
            var output = new MyConcept("Signature");
            output.Components.Add(new Variable("User", "certificate", Variable.RELATION.reference));
            output.Components.Add(new Variable("Double", "latitude", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("Double", "longitude", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("String", "ip", Variable.RELATION.variable) { Optional = true });
            output.Components.Add(new Variable("DateTime", "timestamp", Variable.RELATION.variable));
            output.Components.Add(new Variable("Remark", "generalRemark", Variable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void DateWindow(DataModel doc)
        {
            var output = new MyConcept("DateWindow");
            output.Components.Add(new Variable("DateTime", "startDate", Variable.RELATION.variable));
            output.Components.Add(new Variable("DateTime", "endDate", Variable.RELATION.variable));
            doc.AddComponent(output);
        }

        private static void EcmrStatus(DataModel doc)
        {
            var output = new MyEnum("EcmrStatus");
            output.Options.Add("CREATED");
            output.Options.Add("LOADED");
            output.Options.Add("IN_TRANSIT");
            output.Options.Add("DELIVERED");
            output.Options.Add("CONFIRMED_DELIVERED");
            output.Options.Add("CANCELLED");
            doc.AddComponent(output);
        }
        private static void OrderStatus(DataModel doc)
        {
            var output = new MyEnum("OrderStatus");
            output.Options.Add("OPEN");
            output.Options.Add("IN_PROGRESS");
            output.Options.Add("COMPLETED");
            output.Options.Add("CANCELLED");
            doc.AddComponent(output);
        }
    }
}
