using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class DmodelDemo
    {
        public static Dmodel KoopmanCTO()
        {
            var doc = new Dmodel() { Namespace = "org.digitalcmr" };

            // assets
            DmodelDemo.TransportOrder(doc);
            DmodelDemo.ECMR(doc);
            DmodelDemo.Entity(doc);
            doc.AddComponent(new Dasset("LegalOwnerOrg") { Parent = "Entity" });
            doc.AddComponent(new Dasset("CompoundOrg") { Parent = "Entity" });
            doc.AddComponent(new Dasset("CarrierOrg") { Parent = "Entity" });
            doc.AddComponent(new Dasset("RecipientOrg") { Parent = "Entity" });
            DmodelDemo.Vehicle(doc);

            //participants
            DmodelDemo.User(doc);
            doc.AddComponent(new Dparticipant("LegalOwnerAdmin") { Parent = "User" });
            doc.AddComponent(new Dparticipant("CompoundAdmin") { Parent = "User" });
            doc.AddComponent(new Dparticipant("CarrierAdmin") { Parent = "User" });
            doc.AddComponent(new Dparticipant("CarrierMember") { Parent = "User" });
            doc.AddComponent(new Dparticipant("RecipientAdmin") { Parent = "User" });
            doc.AddComponent(new Dparticipant("RecipientMember") { Parent = "User" });

            // concepts
            DmodelDemo.Address(doc);
            DmodelDemo.Delivery(doc);
            DmodelDemo.Loading(doc);
            DmodelDemo.Creation(doc);
            DmodelDemo.Cancellation(doc);
            DmodelDemo.Good(doc);
            DmodelDemo.Remark(doc);
            DmodelDemo.Signature(doc);
            DmodelDemo.DateWindow(doc);

            // enums
            DmodelDemo.EcmrStatus(doc);
            DmodelDemo.OrderStatus(doc);

            return doc;
        }

        private static void TransportOrder(Dmodel doc)
        {
            var output = new Dasset("TransportOrder") { Identifier = "orderID" };
            output.AddComponent(new Mvariable("String", "orderID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("LegalOwnerOrg", "owner", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("CarrierOrg", "carrier", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("Good", "goods", Mvariable.RELATION.variable) { List = true });
            output.AddComponent(new Mvariable("OrderStatus", "status", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Integer", "issueDate", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("ECMR", "ecmrs", Mvariable.RELATION.reference) { List = true });
            output.AddComponent(new Mvariable("String", "orderRef", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Cancellation", "cancellation", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void ECMR(Dmodel doc)
        {
            var output = new Dasset("ECMR") { Identifier = "ecmrID" };
            output.AddComponent(new Mvariable("String", "ecmrID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "agreementTerms", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "agreementTermsSec", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "legalOwnerRef", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "carrierRef", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "recipientRef", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateTime", "issueDate", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Entity", "issuedBy", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("LegalOwnerOrg", "owner", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("CompoundOrg", "source", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("CarrierOrg", "carrier", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("RecipientOrg", "recipient", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("RecipientMember", "recipientMember", Mvariable.RELATION.reference) { Optional = true });
            output.AddComponent(new Mvariable("CarrierMember", "transporter", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("String", "carrierComments", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Loading", "loading", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Delivery", "delivery", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "documents", Mvariable.RELATION.variable) { List = true, Optional = true });
            output.AddComponent(new Mvariable("Good", "goods", Mvariable.RELATION.variable) { List = true });
            output.AddComponent(new Mvariable("String", "legalOwnerInstructions", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "paymentInstructions", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Creation", "creation", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "payOnDelivery", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Signature", "compoundSignature", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Signature", "carrierLoadingSignature", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Signature", "carrierDeliverySignature", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Signature", "recipientSignature", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("EcmrStatus", "status", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "orderID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Cancellation", "cancellation", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Entity(Dmodel doc)
        {
            var output = new Dasset("Entity") { Identifier = "entityID", Abstract = true };
            output.AddComponent(new Mvariable("String", "entityID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "name", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Vehicle(Dmodel doc)
        {
            var output = new Dasset("Vehicle") { Identifier = "vin" };
            output.AddComponent(new Mvariable("String", "vin", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "manufacturer", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "model", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "type", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("ECMR", "ecmrs", Mvariable.RELATION.reference) { List = true });
            output.AddComponent(new Mvariable("Integer", "odoMeterReading", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "plateNumber", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "registrationCountry", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }

        private static void User(Dmodel doc)
        {
            var output = new Dparticipant("User") { Identifier = "userID", Abstract = true };
            output.AddComponent(new Mvariable("String", "userID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "userName", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "firstName", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "lastName", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }

        private static void Address(Dmodel doc)
        {
            var output = new Dconcept("Address");
            output.AddComponent(new Mvariable("String", "name", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "street", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "houseNumber", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "city", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "zipCode", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "country", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Double", "latitude", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Double", "longitude", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Delivery(Dmodel doc)
        {
            var output = new Dconcept("Delivery");
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateWindow", "expectedWindow", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("DateTime", "actualDate", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Loading(Dmodel doc)
        {
            var output = new Dconcept("Loading");
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateWindow", "expectedWindow", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("DateTime", "actualDate", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Creation(Dmodel doc)
        {
            var output = new Dconcept("Creation");
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateTime", "date", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Cancellation(Dmodel doc)
        {
            var output = new Dconcept("Cancellation");
            output.AddComponent(new Mvariable("Entity", "cancelledBy", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("DateTime", "date", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "reason", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Good(Dmodel doc)
        {
            var output = new Dconcept("Good");
            output.AddComponent(new Mvariable("Vehicle", "vehicle", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Remark", "carrierLoadingRemark", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Remark", "compoundRemark", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Remark", "recipientRemark", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Remark", "carrierDeliveryRemark", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("String", "description", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Double", "weight", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Address", "loadingAddress", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Address", "deliveryAddress", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateWindow", "pickupWindow", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateWindow", "deliveryWindow", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Cancellation", "cancellation", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("CompoundOrg", "source", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("RecipientOrg", "recipient", Mvariable.RELATION.reference));
            doc.AddComponent(output);
        }
        private static void Remark(Dmodel doc)
        {
            var output = new Dconcept("Remark");
            output.AddComponent(new Mvariable("String", "comments", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Boolean", "isDamaged", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Signature(Dmodel doc)
        {
            var output = new Dconcept("Signature");
            output.AddComponent(new Mvariable("User", "certificate", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("Double", "latitude", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Double", "longitude", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("String", "ip", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("DateTime", "timestamp", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Remark", "generalRemark", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void DateWindow(Dmodel doc)
        {
            var output = new Dconcept("DateWindow");
            output.AddComponent(new Mvariable("DateTime", "startDate", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateTime", "endDate", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }

        private static void EcmrStatus(Dmodel doc)
        {
            var output = new Denum("EcmrStatus");
            output.AddItem("CREATED");
            output.AddItem("LOADED");
            output.AddItem("IN_TRANSIT");
            output.AddItem("DELIVERED");
            output.AddItem("CONFIRMED_DELIVERED");
            output.AddItem("CANCELLED");
            doc.AddComponent(output);
        }
        private static void OrderStatus(Dmodel doc)
        {
            var output = new Denum("OrderStatus");
            output.AddItem("OPEN");
            output.AddItem("IN_PROGRESS");
            output.AddItem("COMPLETED");
            output.AddItem("CANCELLED");
            doc.AddComponent(output);
        }
    }
}
