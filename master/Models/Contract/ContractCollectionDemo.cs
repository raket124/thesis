using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    public class ContractCollectionDemo
    {
        public static ContractCollection TutorialContract()
        {
            var output = new ContractCollection();
            ContractCollectionDemo.Animaltracking(output);
            return output;
        }

        private static void Animaltracking(ContractCollection doc)
        {
            var output = new ContractModel("AnimalTrackingRules") { };

            var onAnimalMovementDeparture = new Function("onAnimalMovementDeparture", Function.ACCESSIBILITY.Public);
            onAnimalMovementDeparture.Blocks.Add(new MyInput(onAnimalMovementDeparture)
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Ref = true,
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "Field",
                        Alias = "fromField",
                    }
                }
            });
            var onAnimalMovementArrival = new Function("onAnimalMovementArrival", Function.ACCESSIBILITY.Public);
            onAnimalMovementArrival.Blocks.Add(new MyInput(onAnimalMovementArrival)
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Ref = true,
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "Field",
                        Alias = "arrivalField",
                    }
                }
            });



            doc.Contracts.Add(output);
        }



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
            createVehicles.Blocks.Add(new MyInput(createVehicles));
            createVehicles.Blocks.Add(new MyAssign(createVehicles));
            createVehicles.Blocks.Add(new MyLog(createVehicles));
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
            var contract = new ContractModel("orgRules") { Docs = "Organization rules." };
            doc.Contracts.Add(contract);

            var createLegalOwnerOrg = new Function("createLegalOwnerOrg", Function.ACCESSIBILITY.Public) { Docs = "Create new LegalOwner object on the blockchain." };
            createLegalOwnerOrg.Blocks.Add(new MyInput(createLegalOwnerOrg)
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "LegalOwnerOrg",
                        Alias = "newLegalOwnerOrg",
                    }
                }
            });
            contract.Functions.Add(createLegalOwnerOrg);

            var createCompoundOrg = new Function("createCompoundOrg", Function.ACCESSIBILITY.Public) { Docs = "Create new CompoundOrg object on the blockchain." };
            createCompoundOrg.Blocks.Add(new MyInput(createCompoundOrg)
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "CompoundOrg",
                        Alias = "newCompoundOrg",
                    }
                }
            });
            contract.Functions.Add(createCompoundOrg);

            var createCarrierOrg = new Function("createCarrierOrg", Function.ACCESSIBILITY.Public) { Docs = "Create new CarrierOrg object on the blockchain" };
            createCarrierOrg.Blocks.Add(new MyInput(createCarrierOrg)
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "CarrierOrg",
                        Alias = "newCarrierOrg",
                    }
                }
            });
            contract.Functions.Add(createCarrierOrg);

            var createRecipientOrg = new Function("createRecipientOrg", Function.ACCESSIBILITY.Public) { Docs = "Create new RecipientOrg object on the blockchain" };
            createRecipientOrg.Blocks.Add(new MyInput(createRecipientOrg)
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "RecipientOrg",
                        Alias = "newRecipientOrg",
                    }
                }
            });
            createRecipientOrg.Blocks.Add(new MyAssign(createRecipientOrg)
            {

            });
            contract.Functions.Add(createRecipientOrg);
        }
    }
}
