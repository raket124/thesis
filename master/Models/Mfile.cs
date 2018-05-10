using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Mfile : Mbase
    {
        protected enum TYPES { Asset, Participant, Transaction, Event, Concept, Enum };
        protected Dictionary<Type, TYPES> sorter;

        [DataMember]
        protected List<Masset> assetComponents;
        [DataMember]
        protected List<Mconcept> conceptsComponents;
        [DataMember]
        protected List<Menum> enumComponents;
        [DataMember]
        protected List<Mevent> eventComponents;
        [DataMember]
        protected List<Mparticipant> participantComponents;
        [DataMember]
        protected List<Mtransaction> transactionComponents;
        [DataMember]
        protected string fileNamespace;

        public string Namespace
        {
            get { return this.fileNamespace; }
            set { this.fileNamespace = value; }
        }

        public Mfile(string name) : base(name)
        {
            this.sorter = new Dictionary<Type, TYPES>
            {
                { typeof(Masset), TYPES.Asset },
                { typeof(Mconcept), TYPES.Concept },
                { typeof(Menum), TYPES.Enum },
                { typeof(Mevent), TYPES.Event },
                { typeof(Mparticipant), TYPES.Participant },
                { typeof(Mtransaction), TYPES.Transaction }
            };
            this.assetComponents = new List<Masset>();
            this.conceptsComponents = new List<Mconcept>();
            this.enumComponents = new List<Menum>();
            this.eventComponents = new List<Mevent>();
            this.participantComponents = new List<Mparticipant>();
            this.transactionComponents = new List<Mtransaction>();
        }

        public void AddComponent(Mbase component)
        {
            switch (this.sorter[component.GetType()])
            {
                case TYPES.Asset:
                    this.assetComponents.Add(component as Masset);
                    break;
                case TYPES.Concept:
                    this.conceptsComponents.Add(component as Mconcept);
                    break;
                case TYPES.Enum:
                    this.enumComponents.Add(component as Menum);
                    break;
                case TYPES.Event:
                    this.eventComponents.Add(component as Mevent);
                    break;
                case TYPES.Participant:
                    this.participantComponents.Add(component as Mparticipant);
                    break;
                case TYPES.Transaction:
                    this.transactionComponents.Add(component as Mtransaction);
                    break;
                default:
                    throw new Exception("Invalid class is provided");
            }
        }

        public List<Masset> GetAssets()
        {
            return this.assetComponents;
        }


        public static Mfile KoopmanCTO()
        {
            var doc = new Mfile("businessModel")
            {
                Namespace = "org.digitalcmr",
                Docs = "Digital CMR business network definition."
            };

            // assets
            Mfile.TransportOrder(doc);
            Mfile.ECMR(doc);
            Mfile.Entity(doc);
            doc.AddComponent(new Masset("LegalOwnerOrg") { Parent = "Entity" });
            doc.AddComponent(new Masset("CompoundOrg") { Parent = "Entity" });
            doc.AddComponent(new Masset("CarrierOrg") { Parent = "Entity" });
            doc.AddComponent(new Masset("RecipientOrg") { Parent = "Entity" });
            Mfile.Vehicle(doc);

            //participants
            Mfile.User(doc);
            doc.AddComponent(new Mparticipant("LegalOwnerAdmin") { Parent = "User" });
            doc.AddComponent(new Mparticipant("CompoundAdmin") { Parent = "User" });
            doc.AddComponent(new Mparticipant("CarrierAdmin") { Parent = "User" });
            doc.AddComponent(new Mparticipant("CarrierMember") { Parent = "User" });
            doc.AddComponent(new Mparticipant("RecipientAdmin") { Parent = "User" });
            doc.AddComponent(new Mparticipant("RecipientMember") { Parent = "User" });

            // concepts
            Mfile.Address(doc);
            Mfile.Delivery(doc);
            Mfile.Loading(doc);
            Mfile.Creation(doc);
            Mfile.Cancellation(doc);
            Mfile.Good(doc);
            Mfile.Remark(doc);
            Mfile.Signature(doc);
            Mfile.DateWindow(doc);

            // enums
            Mfile.EcmrStatus(doc);
            Mfile.OrderStatus(doc);

            return doc;
        }

        private static void TransportOrder(Mfile doc)
        {
            var output = new Masset("TransportOrder") { Identifier = "orderID" };
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
        private static void ECMR(Mfile doc)
        {
            var output = new Masset("ECMR") { Identifier = "ecmrID" };
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
        private static void Entity(Mfile doc)
        {
            var output = new Masset("Entity") { Identifier = "entityID", Abstract = true };
            output.AddComponent(new Mvariable("String", "entityID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "name", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Vehicle(Mfile doc)
        {
            var output = new Masset("Vehicle") { Identifier = "vin" };
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

        private static void User(Mfile doc)
        {
            var output = new Masset("User") { Identifier = "userID", Abstract = true };
            output.AddComponent(new Mvariable("String", "userID", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "userName", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "firstName", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "lastName", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }

        private static void Address(Mfile doc)
        {
            var output = new Mconcept("Address");
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
        private static void Delivery(Mfile doc)
        {
            var output = new Mconcept("Delivery");
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateWindow", "expectedWindow", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("DateTime", "actualDate", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Loading(Mfile doc)
        {
            var output = new Mconcept("Loading");
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateWindow", "expectedWindow", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("DateTime", "actualDate", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Creation(Mfile doc)
        {
            var output = new Mconcept("Creation");
            output.AddComponent(new Mvariable("Address", "address", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateTime", "date", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Cancellation(Mfile doc)
        {
            var output = new Mconcept("Cancellation");
            output.AddComponent(new Mvariable("Entity", "cancelledBy", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("DateTime", "date", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("String", "reason", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }
        private static void Good(Mfile doc)
        {
            var output = new Mconcept("Good");
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
        private static void Remark(Mfile doc)
        {
            var output = new Mconcept("Remark");
            output.AddComponent(new Mvariable("String", "comments", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Boolean", "isDamaged", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void Signature(Mfile doc)
        {
            var output = new Mconcept("Signature");
            output.AddComponent(new Mvariable("User", "certificate", Mvariable.RELATION.reference));
            output.AddComponent(new Mvariable("Double", "latitude", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("Double", "longitude", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("String", "ip", Mvariable.RELATION.variable) { Optional = true });
            output.AddComponent(new Mvariable("DateTime", "timestamp", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("Remark", "generalRemark", Mvariable.RELATION.variable) { Optional = true });
            doc.AddComponent(output);
        }
        private static void DateWindow(Mfile doc)
        {
            var output = new Mconcept("DateWindow");
            output.AddComponent(new Mvariable("DateTime", "startDate", Mvariable.RELATION.variable));
            output.AddComponent(new Mvariable("DateTime", "endDate", Mvariable.RELATION.variable));
            doc.AddComponent(output);
        }

        private static void EcmrStatus(Mfile doc)
        {
            var output = new Menum("EcmrStatus");
            output.AddItem("CREATED");
            output.AddItem("LOADED");
            output.AddItem("IN_TRANSIT");
            output.AddItem("DELIVERED");
            output.AddItem("CONFIRMED_DELIVERED");
            output.AddItem("CANCELLED");
            doc.AddComponent(output);
        }
        private static void OrderStatus(Mfile doc)
        {
            var output = new Menum("OrderStatus");
            output.AddItem("OPEN");
            output.AddItem("IN_PROGRESS");
            output.AddItem("COMPLETED");
            output.AddItem("CANCELLED");
            doc.AddComponent(output);
        }
    }
}
