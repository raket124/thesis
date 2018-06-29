using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    class ContractCollectionDemo
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
            onAnimalMovementDeparture.Blocks.Add(new MyInput()
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Relation = Data.Variable.RELATION.reference,
                        Type = Block.Variable.TYPES.Asset,
                        ObjectName = "Field",
                        Alias = "fromField",
                    }
                }
            });
            var onAnimalMovementArrival = new Function("onAnimalMovementArrival", Function.ACCESSIBILITY.Public);
            onAnimalMovementArrival.Blocks.Add(new MyInput()
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Relation = Data.Variable.RELATION.reference,
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
            var updateECMRStatusToCancelled = new Function("updateECMRStatusToCancelled", Function.ACCESSIBILITY.Public);
            updateECMRStatusToCancelled.Blocks.Add(new MyInput()
            {
                Vars = new ObservableCollection<Block.Variable>()
                {
                    new Block.Variable()
                    {
                        Relation = Data.Variable.RELATION.reference,
                        ObjectName = "ECMR",
                        Alias = "ecmr",
                        Type = Block.Variable.TYPES.Asset
                    },
                    new Block.Variable()
                    {
                        ObjectName = "Cancellation",
                        Alias = "cancellation",
                        Type = Block.Variable.TYPES.Concept,
                        
                    }
                }
            });
            updateECMRStatusToCancelled.Blocks.Add(new MyAssign()
            {

            });
            updateECMRStatusToCancelled.Blocks.Add(new MyAssign()
            {
                //      new Cancellation
            });
            //updateECMRStatusToCancelled.Blocks.Add(new MyAssignRelation(updateECMRStatusToCancelled)
            //{
            //    //      new Entity
            //});
            updateECMRStatusToCancelled.Blocks.Add(new MyAssign()
            {

            });
            updateECMRStatusToCancelled.Blocks.Add(new MyAssign()
            {

            });
            updateECMRStatusToCancelled.Blocks.Add(new MyUseRegistry()
            {
                Action = MyUseRegistry.ACTION.Update,
            });
            output.Functions.Add(updateECMRStatusToCancelled);
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
            var contract = new ContractModel("orgRules")
            {
                Docs = "Organization rules.",
                Functions = new ObservableCollection<Function>()
                {
                    new Function("createLegalOwnerOrg", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create new LegalOwner object on the blockchain.",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
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
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                            }
                        }
                    },
                    new Function("createCompoundOrg", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create new CompoundOrg object on the blockchain.",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
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
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                            }
                        }
                    },
                    new Function("createCarrierOrg", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create new CarrierOrg object on the blockchain",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
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
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                            }
                        }
                    },
                    new Function("createRecipientOrg", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create new RecipientOrg object on the blockchain",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
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
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                            }
                        }
                    }
                }
            };
            doc.Contracts.Add(contract);
        }
    }
}
