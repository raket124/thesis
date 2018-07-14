﻿using master.Models.Contract.Block.Blocks;
using master.Models.Data.Component.Components;
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
                    new Block.Variable(typeof(MyAsset))
                    {
                        Relation = Data.Variable.RELATION.reference,
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
                    new Block.Variable(typeof(MyAsset))
                    {
                        Relation = Data.Variable.RELATION.reference,
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
            doc.Contracts.Add(new ContractModel("vehicleRules")
            {
                Docs = "Vehicle rules.",
                Functions = new ObservableCollection<Function>()
                {
                    new Function("createVehicles", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create RecipientOrg transaction processor function."
                    },
                    new Function("updateRegistrationCountry", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create UpdateRegistrationCountry transaction processor function."
                    },
                    new Function("updateEcmrListInVin", Function.ACCESSIBILITY.Private),
                    new Function("retrieveAndUpdateVin", Function.ACCESSIBILITY.Private)
                }
            });
        }
        private static void EcmrRules(ContractCollection doc)
        {
            doc.Contracts.Add(new ContractModel("ecmrRules")
            {
                Docs = "ECMR rules",
                Functions = new ObservableCollection<Function>()
                {
                    new Function("createECMRs", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create ECMRs transaction processor function."
                    },
                    new Function("updateEcmrStatusToLoaded", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateEcmrStatusToLoaded transaction processor function."
                    },
                    new Function("updateEcmrStatusToInTransit", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateEcmrStatusToInTransit transaction processor function."
                    },
                    new Function("updateEcmrStatusToConfirmedDelivered", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateEcmrStatusToDelivered transaction processor function."
                    },
                    new Function("updateECMRStatusToCancelled", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateECMRStatusToCancelled transaction processor function.",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
                            {
                                Vars = new ObservableCollection<Block.Variable>()
                                {
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        Relation = Data.Variable.RELATION.reference,
                                        ObjectName = "ECMR",
                                        Alias = "ecmr"
                                    },
                                    new Block.Variable(typeof(MyConcept))
                                    {
                                        ObjectName = "Cancellation",
                                        Alias = "cancellation"
                                    }
                                }
                            },
                            new MyAssign()
                            {
                                Variable = "ecmr.status",
                                Value = "EcmrStatus.Cancelled"
                            },
                            new MyAssign()
                            {
                                Variable = "ecmr.cancellation",
                                Value = "new Cancellation"
                            },
                            new MyAssign()
                            {
                                Variable = "ecmr.cancellation.cancelledBy",
                                Value = "CurrentParticipant"
                            },
                            new MyAssign()
                            {
                                Variable = "ecmr.cancellation.date",
                                Value = "cancellation.date"
                            },
                            new MyAssign()
                            {
                                Variable = "ecmr.cancellation.reason",
                                Value = "cancellation.reason"
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Update,
                                Alias = "ecmr"
                            }
                        }
                    },
                    new Function("updateExpectedPickupWindow", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateExpectedPickupWindow transaction processor function."
                    },
                    new Function("updateExpectedDeliveryWindow", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateExpectedDeliveryWindow transaction processor function."
                    }
                }
            });
        }

        private static void TransportOrderRules(ContractCollection doc)
        {
            doc.Contracts.Add(new ContractModel("transportOrderRules")
            {
                Docs = "Transport order rules",
                Functions = new ObservableCollection<Function>()
                {
                    new Function("createTransportOrder", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create transport order transaction processor function.",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
                            {
                                Vars = new ObservableCollection<Block.Variable>()
                                {
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        Relation = Data.Variable.RELATION.variable,
                                        ObjectName = "TransportOrder",
                                        Alias = "transportOrder"
                                    }
                                }
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                                Alias = "transportOrder"
                            }
                        }
                    },
                    new Function("createTransportOrders", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create transport orders transaction processor function.",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
                            {
                                Vars = new ObservableCollection<Block.Variable>()
                                {
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        Relation = Data.Variable.RELATION.variable,
                                        ObjectName = "TransportOrder",
                                        Alias = "transportOrders",
                                        List = true
                                    }
                                }
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                                Alias = "transportOrders"
                            }
                        }
                    },
                    new Function("updateTransportOrderToInProgress", Function.ACCESSIBILITY.Private)
                    {
                    },
                    new Function("updateTransportOrderStatusToCompleted", Function.ACCESSIBILITY.Private)
                    {
                    },
                    new Function("validateVinIds", Function.ACCESSIBILITY.Private)
                    {
                    },
                    new Function("updateTransportOrderPickupWindow", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Update Pickup transport order transaction processor function."
                    },
                    new Function("updateTransportOrderDeliveryWindow", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Update DeliveryWindow transport order transaction processor function."
                    },
                    new Function("updateTransportOrderStatusToCancelled", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateTransportOrderStatusToCancelled transaction processor function."
                    }
                }
            });
        }
        private static void OrgRules(ContractCollection doc)
        {
            doc.Contracts.Add(new ContractModel("orgRules")
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
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        ObjectName = "LegalOwnerOrg",
                                        Alias = "newLegalOwnerOrg",
                                    }
                                }
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                                Alias = "newLegalOwnerOrg"
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
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        ObjectName = "CompoundOrg",
                                        Alias = "newCompoundOrg",
                                    }
                                }
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                                Alias = "newCompoundOrg"
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
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        ObjectName = "CarrierOrg",
                                        Alias = "newCarrierOrg",
                                    }
                                }
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                                Alias = "newCarrierOrg"
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
                                    new Block.Variable(typeof(MyAsset))
                                    {
                                        ObjectName = "RecipientOrg",
                                        Alias = "newRecipientOrg",
                                    }
                                }
                            },
                            new MyUseRegistry()
                            {
                                Action = MyUseRegistry.ACTION.Insert,
                                Alias = "newRecipientOrg"
                            }
                        }
                    }
                }
            });
        }
    }
}
