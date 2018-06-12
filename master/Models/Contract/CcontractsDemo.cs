using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class CcontractsDemo
    {
        public static Ccontracts KoopmanContract()
        {
            var output = new Ccontracts();
            CcontractsDemo.VehicleRules(output);
            CcontractsDemo.EcmrRules(output);
            CcontractsDemo.TransportOrderRules(output);
            CcontractsDemo.OrgRules(output);
            return output;
        }
        private static void VehicleRules(Ccontracts doc)
        {
            var output = new Ccontract("vehicleRules") { };
            var createVehicles = new Cfunction("createVehicles", Cfunction.ACCESSIBILITY.Public) { Docs = "Hello\nThis is a test\nLove T"};
            createVehicles.Add(new Berror());
            createVehicles.Add(new Bassign());
            createVehicles.Add(new Berror());
            output.Functions.Add(createVehicles);
            output.Functions.Add(new Cfunction("updateRegistrationCountry", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateEcmrListInVin", Cfunction.ACCESSIBILITY.Private));
            output.Functions.Add(new Cfunction("retrieveAndUpdateVin", Cfunction.ACCESSIBILITY.Private));
            doc.Contracts.Add(output);
        }
        private static void EcmrRules(Ccontracts doc)
        {
            var output = new Ccontract("ecmrRules") { };
            output.Functions.Add(new Cfunction("createECMRs", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateEcmrStatusToLoaded", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateEcmrStatusToInTransit", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateEcmrStatusToConfirmedDelivered", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateECMRStatusToCancelled", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateExpectedPickupWindow", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateExpectedDeliveryWindow", Cfunction.ACCESSIBILITY.Public));
            doc.Contracts.Add(output);
        }
        private static void TransportOrderRules(Ccontracts doc)
        {
            var output = new Ccontract("transportOrderRules") { };
            output.Functions.Add(new Cfunction("createTransportOrder", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("createTransportOrders", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateTransportOrderToInProgress", Cfunction.ACCESSIBILITY.Private));
            output.Functions.Add(new Cfunction("updateTransportOrderStatusToCompleted", Cfunction.ACCESSIBILITY.Private));
            output.Functions.Add(new Cfunction("validateVinIds", Cfunction.ACCESSIBILITY.Private));
            output.Functions.Add(new Cfunction("updateTransportOrderPickupWindow", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateTransportOrderDeliveryWindow", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("updateTransportOrderStatusToCancelled", Cfunction.ACCESSIBILITY.Public));
            doc.Contracts.Add(output);
        }
        private static void OrgRules(Ccontracts doc)
        {
            var output = new Ccontract("orgRules") { };
            output.Functions.Add(new Cfunction("createLegalOwnerOrg", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("createCompoundOrg", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("createCarrierOrg", Cfunction.ACCESSIBILITY.Public));
            output.Functions.Add(new Cfunction("createRecipientOrg", Cfunction.ACCESSIBILITY.Public));
            doc.Contracts.Add(output);
        }
    }
}
