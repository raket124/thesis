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
