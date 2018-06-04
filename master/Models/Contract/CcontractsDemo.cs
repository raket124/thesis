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
            var createVehicles = new Cfunction("createVehicles", Cfunction.ACCESSIBILITY.Public);
            createVehicles.Add(new Berror());
            createVehicles.Add(new Bassign());
            createVehicles.Add(new Berror());
            output.AddFunction(createVehicles);
            output.AddFunction(new Cfunction("updateRegistrationCountry", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateEcmrListInVin", Cfunction.ACCESSIBILITY.Private));
            output.AddFunction(new Cfunction("retrieveAndUpdateVin", Cfunction.ACCESSIBILITY.Private));
            doc.Add(output);
        }
        private static void EcmrRules(Ccontracts doc)
        {
            var output = new Ccontract("ecmrRules") { };
            output.AddFunction(new Cfunction("createECMRs", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateEcmrStatusToLoaded", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateEcmrStatusToInTransit", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateEcmrStatusToConfirmedDelivered", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateECMRStatusToCancelled", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateExpectedPickupWindow", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateExpectedDeliveryWindow", Cfunction.ACCESSIBILITY.Public));
            doc.Add(output);
        }
        private static void TransportOrderRules(Ccontracts doc)
        {
            var output = new Ccontract("transportOrderRules") { };
            output.AddFunction(new Cfunction("createTransportOrder", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("createTransportOrders", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateTransportOrderToInProgress", Cfunction.ACCESSIBILITY.Private));
            output.AddFunction(new Cfunction("updateTransportOrderStatusToCompleted", Cfunction.ACCESSIBILITY.Private));
            output.AddFunction(new Cfunction("validateVinIds", Cfunction.ACCESSIBILITY.Private));
            output.AddFunction(new Cfunction("updateTransportOrderPickupWindow", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateTransportOrderDeliveryWindow", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("updateTransportOrderStatusToCancelled", Cfunction.ACCESSIBILITY.Public));
            doc.Add(output);
        }
        private static void OrgRules(Ccontracts doc)
        {
            var output = new Ccontract("orgRules") { };
            output.AddFunction(new Cfunction("createLegalOwnerOrg", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("createCompoundOrg", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("createCarrierOrg", Cfunction.ACCESSIBILITY.Public));
            output.AddFunction(new Cfunction("createRecipientOrg", Cfunction.ACCESSIBILITY.Public));
            doc.Add(output);
        }
    }
}
