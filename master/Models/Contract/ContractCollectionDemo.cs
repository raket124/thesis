using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    class ContractCollectionDemo
    {
        public static ContractCollection KoopmanContract()
        {
            var output = new ContractCollection();
            ContractCollectionDemo.VehicleRules(output);
            ContractCollectionDemo.EcmrRules(output);
            ContractCollectionDemo.TransportOrderRules(output);
            ContractCollectionDemo.OrgRules(output);
            return output;
        }
        private static void VehicleRules(ContractCollection doc)
        {
            var output = new ContractModel("vehicleRules") { };
            var createVehicles = new Function("createVehicles", Function.ACCESSIBILITY.Public) { Docs = "Create RecipientOrg transaction processor function." };
            createVehicles.Blocks.Add(new MyInput());
            createVehicles.Blocks.Add(new MyAssign());
            createVehicles.Blocks.Add(new MyLog());
            output.Functions.Add(createVehicles);
            output.Functions.Add(new Function("updateRegistrationCountry", Function.ACCESSIBILITY.Public) { Docs = "Create UpdateRegistrationCountry transaction processor function." });
            output.Functions.Add(new Function("updateEcmrListInVin", Function.ACCESSIBILITY.Private));
            output.Functions.Add(new Function("retrieveAndUpdateVin", Function.ACCESSIBILITY.Private));
            doc.Contracts.Add(output);
        }
        private static void EcmrRules(ContractCollection doc)
        {
            var output = new ContractModel("ecmrRules") { };
            output.Functions.Add(new Function("createECMRs", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateEcmrStatusToLoaded", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateEcmrStatusToInTransit", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateEcmrStatusToConfirmedDelivered", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateECMRStatusToCancelled", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateExpectedPickupWindow", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateExpectedDeliveryWindow", Function.ACCESSIBILITY.Public));
            doc.Contracts.Add(output);
        }
        private static void TransportOrderRules(ContractCollection doc)
        {
            var output = new ContractModel("transportOrderRules") { };
            output.Functions.Add(new Function("createTransportOrder", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("createTransportOrders", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateTransportOrderToInProgress", Function.ACCESSIBILITY.Private));
            output.Functions.Add(new Function("updateTransportOrderStatusToCompleted", Function.ACCESSIBILITY.Private));
            output.Functions.Add(new Function("validateVinIds", Function.ACCESSIBILITY.Private));
            output.Functions.Add(new Function("updateTransportOrderPickupWindow", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateTransportOrderDeliveryWindow", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("updateTransportOrderStatusToCancelled", Function.ACCESSIBILITY.Public));
            doc.Contracts.Add(output);
        }
        private static void OrgRules(ContractCollection doc)
        {
            var output = new ContractModel("orgRules") { };
            output.Functions.Add(new Function("createLegalOwnerOrg", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("createCompoundOrg", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("createCarrierOrg", Function.ACCESSIBILITY.Public));
            output.Functions.Add(new Function("createRecipientOrg", Function.ACCESSIBILITY.Public));
            doc.Contracts.Add(output);
        }
    }
}
