using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Combinations;
using master.Models.Contract.Block.Conditioning;
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
        public static ContractCollection KoopmanContract()
        {
            var output = new ContractCollection();
            //ContractCollectionDemo.VehicleRules(output);
            ContractCollectionDemo.EcmrRules(output);
            //ContractCollectionDemo.TransportOrderRules(output);
            //ContractCollectionDemo.OrgRules(output);
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
                        Docs = "Create RecipientOrg transaction processor function.",
                        Blocks = new ObservableCollection<Base>()
                        {

                        }
                    },
                    new Function("updateRegistrationCountry", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "Create UpdateRegistrationCountry transaction processor function.",
                        Blocks = new ObservableCollection<Base>()
                        {

                        }
                    },
                    new Function("updateEcmrListInVin", Function.ACCESSIBILITY.Private)
                    {
                        Blocks = new ObservableCollection<Base>()
                        {

                        }
                    },
                    new Function("retrieveAndUpdateVin", Function.ACCESSIBILITY.Private)
                    {
                        Blocks = new ObservableCollection<Base>()
                        {

                        }
                    }
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
                    //new Function("createECMRs", Function.ACCESSIBILITY.Public)
                    //{
                    //    Docs = "Create ECMRs transaction processor function.",
                    //    //Blocks = new ObservableCollection<Block.Base>()
                    //    //{
                    //    //    new MyInput()
                    //    //    {
                    //    //        //Vars = new ObservableCollection<Block.MyVariable>()
                    //    //        //{
                    //    //        //    new Block.MyVariable(typeof(MyAsset))
                    //    //        //    {
                    //    //        //        Relation = Data.Variable.RELATION.reference,
                    //    //        //        ObjectName = "TransportOrder",
                    //    //        //        Alias = "transportOrder",
                    //    //        //    },
                    //    //        //    new Block.MyVariable(typeof(MyAsset))
                    //    //        //    {
                    //    //        //        Relation = Data.Variable.RELATION.variable,
                    //    //        //        ObjectName = "ECMR",
                    //    //        //        Alias = "ecmrs",
                    //    //        //        List = true
                    //    //        //    }
                    //    //        //}
                    //    //    },
                    //    //    new MyTotalEcmrs()
                    //    //    {
                    //    //        Input = "ecmrs",
                    //    //        Alias = "totalEcmrsGoods"
                    //    //    },
                    //    //    //new MySimpleIf()
                    //    //    //{
                    //    //    //    Condition = new Block.Conditioning.ConditionBase()
                    //    //    //    {
                    //    //    //        LHS = "transportOrder.goods.length",
                    //    //    //        Comparison = Block.Conditioning.ConditionBase.COMPARE.lesser,
                    //    //    //        RHS = "totalEcmrsGoods"
                    //    //    //    }
                    //    //    //},
                    //    //    new MyError()
                    //    //    {
                    //    //        Text = "The total amount of goods of the ECMRs exceeds the total listed in the TransportOrder"
                    //    //    },
                    //    //    new MyEnd(),
                    //    //    new MyForeach()
                    //    //    {
                    //    //        //Variable = "ecmrs",
                    //    //        //Alias = "ecmr"
                    //    //    },
                    //    //    new MyAssign()
                    //    //    {
                    //    //        //Variable = "ecmr.orderID",
                    //    //        //Value = "transportOrder.getIdentifier()"
                    //    //    },
                    //    //    new MyRegistry()
                    //    //    {
                    //    //        Action = MyRegistry.ACTION.Insert,
                    //    //        //Variable = "ecmrs"
                    //    //    },
                    //    //    new MyEnd()
                    //    //}
                    //},
                    new Function("updateEcmrStatusToLoaded", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateEcmrStatusToLoaded transaction processor function.",
                        Blocks = new ObservableCollection<Base>()
                        {
                            new MyInput()
                            {
                                Variables = new ObservableCollection<MyVariable>()
                                {
                                    new MyVariable(typeof(MyAsset))
                                    {
                                        Input = true,
                                        Relation = Data.Variable.RELATION.reference,
                                        ObjectName = "ECMR",
                                        Alias = "ecmr"
                                    },
                                    new MyVariable(typeof(String))
                                    {
                                        Input = true,
                                        Alias = "remark"
                                    }
                                }
                            },
                            new MyIfError()
                            {
                                If = new MyIf()
                                {
                                    Condition = new Condition()
                                    {
                                        Conditions = new ObservableCollection<ConditionBase>()
                                        {
                                            new ConditionBase()
                                            {
                                                LHS = new VariableLink(new MyVariable(typeof(MyAsset))
                                                {
                                                    Input = true,
                                                    ObjectName = "ECMR",
                                                    Alias = "ecmr"
                                                }, new VariableLink(new MyVariable(typeof(MyEnum))
                                                {
                                                    ObjectName = "EcmrStatus",
                                                    Alias = "status"
                                                })),
                                                Comparison = ConditionBase.COMPARE.not_equal,
                                                RHS = new VariableLink(new MyVariable(typeof(MyEnum))
                                                {
                                                    ObjectName = "EcmrStatus",
                                                    Alias = "Loaded"
                                                }),
                                            }
                                        }
                                    }
                                },
                                Error = new MyError()
                                {
                                    Text = "Invalid transaction. Trying to set status IN_TRANSIT to an ECMR with status: #alias:input.ecmr.status"
                                }
                            },
                            new MyCreation()
                            {
                                Object = new MyAssign()
                                {
                                    Value = new VariableLink(new MyVariable(typeof(MyConcept))
                                    {
                                        ObjectName = "Signature",
                                        Alias = "signature"
                                    })
                                },
                                Modifications = new MyModification()
                                {
                                    Assignments = new ObservableCollection<MyAssign>()
                                    {
                                        new MyAssign()
                                        {
                                            Variable = new VariableLink(new MyVariable(typeof(MyParticipant))
                                            {
                                                ObjectName = "User"
                                            }),
                                            Value = new VariableLink(new MyVariable(typeof(MyParticipant))
                                            {
                                                ObjectName = "User",
                                                Alias = "#currentUser"
                                            })
                                        },
                                        new MyAssign()
                                        {
                                            Variable = new VariableLink(new MyVariable(typeof(DateTime))
                                            {
                                                Alias = "timestamp"
                                            }),
                                            Value = new VariableLink(new MyVariable(typeof(DateTime))
                                            {
                                                Alias = "#currentDateTime"
                                            })
                                        },
                                        new MyAssign()
                                        {
                                            Variable = new VariableLink(new MyVariable(typeof(string))
                                            {
                                                Alias = "remark",
                                            }),
                                            Value = new VariableLink(new MyVariable(typeof(string))
                                            {
                                                Input = true,
                                                Alias = "remark",
                                            })
                                        }
                                    }
                                }
                            },
                            new MyAssign()
                            {
                                Variable = new VariableLink(new MyVariable(typeof(MyAsset))
                                {
                                    Input = true,
                                    ObjectName = "ECMR",
                                    Alias = "ecmr"
                                }, new VariableLink(new MyVariable(typeof(MyConcept))
                                {
                                    ObjectName = "Signature",
                                    Alias = "signature"
                                })),
                                Value = new VariableLink(new MyVariable(typeof(MyConcept))
                                {
                                    ObjectName = "Signature",
                                    Alias = "signature"
                                })
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Update,
                                Variable = new VariableLink(new MyVariable(typeof(MyAsset))
                                {
                                    Input = true,
                                    ObjectName = "ECMR",
                                    Alias = "ecmr"
                                })
                            }
                        }
                    },
                    //new Function("updateEcmrStatusToInTransit", Function.ACCESSIBILITY.Public)
                    //{
                    //    Docs = "UpdateEcmrStatusToInTransit transaction processor function."
                    //},
                    //new Function("updateEcmrStatusToConfirmedDelivered", Function.ACCESSIBILITY.Public)
                    //{
                    //    Docs = "UpdateEcmrStatusToDelivered transaction processor function."
                    //},
                    //new Function("updateECMRStatusToCancelled", Function.ACCESSIBILITY.Public)
                    //{
                    //    Docs = "UpdateECMRStatusToCancelled transaction processor function.",
                    //},
                    new Function("updateExpectedPickupWindow", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateExpectedPickupWindow transaction processor function.",
                        //Blocks = new ObservableCollection<Base>()
                        //{
                        //    new MyAssign(),
                        //    new MyElse(),
                        //    new MyEnd(),
                        //    new MyError() { Text = "The error message" },
                        //    new MyForeach()
                        //    {
                        //        IteratorAlias = new VariableLink(new MyVariable(typeof(int)) { Alias = "iter" }),
                        //        ObjectAlias = new VariableLink(new MyVariable(typeof(string)) { Alias = "name" }),
                        //        List = new VariableLink(new MyVariable(typeof(string)) { Alias = "names", List=true })
                        //    },
                        //    new MyIf()
                        //    {
                        //        Condition = new Condition()
                        //        {
                        //            Conditions = new ObservableCollection<ConditionBase>()
                        //            {
                        //                new ConditionBase()
                        //                {
                        //                    LHS = new VariableLink(new MyVariable(typeof(MyAsset))
                        //                    {
                        //                        Input = true,
                        //                        ObjectName = "ECMR",
                        //                        Alias = "ecmr"
                        //                    }),
                        //                    Comparison = ConditionBase.COMPARE.not_equal,
                        //                    RHS = new VariableLink(new MyVariable(typeof(MyEnum))
                        //                    {
                        //                        ObjectName = "EcmrStatus",
                        //                        Alias = "Loaded"
                        //                    }),
                        //                    Alias = "comp1"
                        //                },
                        //                new ConditionBase()
                        //                {
                        //                    LHS = new VariableLink(new MyVariable(typeof(MyAsset))
                        //                    {
                        //                        Input = true,
                        //                        ObjectName = "User",
                        //                        Alias = "new_user"
                        //                    }),
                        //                    Comparison = ConditionBase.COMPARE.equal,
                        //                    RHS = new VariableLink(new MyVariable(typeof(MyEnum))
                        //                    {
                        //                        ObjectName = "User",
                        //                        Alias = "old_user"
                        //                    }),
                        //                    Alias = "comp2"
                        //                }
                        //            },
                        //            Groups = new ObservableCollection<ConditionGroup>()
                        //            {
                        //                new ConditionGroup()
                        //                {
                        //                    Conditions = new ObservableCollection<string>()
                        //                    {
                        //                        "comp1",
                        //                        "comp2"
                        //                    },
                        //                    Connectors = new ObservableCollection<ConditionGroup.COMPARE>()
                        //                    {
                        //                        ConditionGroup.COMPARE.and
                        //                    },
                        //                    Alias = "combination1"
                        //                },
                        //                new ConditionGroup()
                        //                {
                        //                    Conditions = new ObservableCollection<string>()
                        //                    {
                        //                        "comp2",
                        //                        "comp1"
                        //                    },
                        //                    Connectors = new ObservableCollection<ConditionGroup.COMPARE>()
                        //                    {
                        //                        ConditionGroup.COMPARE.or
                        //                    },
                        //                    Alias = "combination2"
                        //                }
                        //            },
                        //            Value = new ConditionGroup()
                        //            {
                        //                Conditions = new ObservableCollection<string>()
                        //                {
                        //                    "combination1",
                        //                    "combination2"
                        //                },
                        //                Connectors = new ObservableCollection<ConditionGroup.COMPARE>()
                        //                {
                        //                    ConditionGroup.COMPARE.and
                        //                }
                        //            }
                        //        }
                        //    },
                        //    new MyLog() { Text = "The log message" },
                        //    new MyRegistry()
                        //    {
                        //        Action = MyRegistry.ACTION.Insert,
                        //        Variable = new VariableLink(new MyVariable(typeof(MyParticipant)) { Alias = "new_user" }),
                        //        Delay = false
                        //    },

                        //    new MyCreation(),
                        //    new MyIfError()
                        //    {
                        //        If = new MyIf()
                        //        {
                        //            Condition = new Condition()
                        //            {
                        //                Conditions = new ObservableCollection<ConditionBase>()
                        //                {
                        //                    new ConditionBase()
                        //                    {
                        //                        LHS = new VariableLink(new MyVariable(typeof(MyAsset))
                        //                        {
                        //                            Input = true,
                        //                            ObjectName = "ECMR",
                        //                            Alias = "ecmr"
                        //                        }),
                        //                        Comparison = ConditionBase.COMPARE.not_equal,
                        //                        RHS = new VariableLink(new MyVariable(typeof(MyEnum))
                        //                        {
                        //                            ObjectName = "EcmrStatus",
                        //                            Alias = "Loaded"
                        //                        }),
                        //                    }
                        //                },
                        //            }
                        //        },
                        //        Error = new MyError() { Text = "The error message" }
                        //    },
                        //    new MyInput(),
                        //    new MyModification(),
                        //    new MyValidation()
                        //}
                    },
                    new Function("updateExpectedDeliveryWindow", Function.ACCESSIBILITY.Public)
                    {
                        Docs = "UpdateExpectedDeliveryWindow transaction processor function.",
                        Blocks = new ObservableCollection<Base>()
                        {
                            new MyInput()
                            {
                                Variables = new ObservableCollection<MyVariable>()
                                {
                                    new MyVariable(typeof(MyAsset))
                                    {
                                        Input = true,
                                        Relation = Data.Variable.RELATION.reference,
                                        ObjectName = "ECMR",
                                        Alias = "ecmr"
                                    },
                                    new MyVariable(typeof(MyConcept))
                                    {
                                        Input = true,
                                        ObjectName = "DateWindow",
                                        Alias = "new_window"
                                    }
                                }
                            },
                            new MyIfError()
                            {
                                If = new MyIf()
                                {
                                    Condition = new Condition()
                                    {
                                        Conditions = new ObservableCollection<ConditionBase>()
                                        {
                                            new ConditionBase()
                                            {
                                                LHS = new VariableLink(new MyVariable(typeof(MyConcept))
                                                {
                                                    Input = true,
                                                    ObjectName = "DateWindow",
                                                    Alias = "new_window"
                                                },
                                                new VariableLink(new MyVariable(typeof(DateTime))
                                                {
                                                    Alias = "start"
                                                })),
                                                Comparison =  ConditionBase.COMPARE.lesser,
                                                RHS = new VariableLink(new MyVariable(typeof(MyConcept))
                                                {
                                                    Input = true,
                                                    ObjectName = "DateWindow",
                                                    Alias = "new_window"
                                                },
                                                new VariableLink(new MyVariable(typeof(DateTime))
                                                {
                                                    Alias = "end"
                                                }))
                                            }
                                        }
                                    }
                                },
                                Error = new MyError() { Text = "Invalid datewindow provided." }
                            },
                            new MyAssign()
                            {
                                Variable = new VariableLink(new MyVariable(typeof(MyAsset))
                                {
                                    Input = true,
                                    Relation = Data.Variable.RELATION.reference,
                                    ObjectName = "ECMR",
                                    Alias = "ecmr"
                                }, 
                                new VariableLink(new MyVariable(typeof(MyConcept))
                                {
                                    ObjectName = "Good",
                                    Alias = "good"
                                }, 
                                new VariableLink(new MyVariable(typeof(MyConcept))
                                {
                                    ObjectName = "DateWindow",
                                    Alias = "deliveryWindow"
                                }))),

                                Value = new VariableLink(new MyVariable(typeof(MyAsset))
                                {
                                    Input = true,
                                    ObjectName = "DateWindow",
                                    Alias = "new_window"
                                })
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Update,
                                Variable = new VariableLink(new MyVariable(typeof(MyAsset))
                                {
                                    Input = true,
                                    Relation = Data.Variable.RELATION.reference,
                                    ObjectName = "ECMR",
                                    Alias = "ecmr"
                                })
                            }
                        }
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
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        Relation = Data.Variable.RELATION.variable,
                                //        ObjectName = "TransportOrder",
                                //        Alias = "transportOrder"
                                //    }
                                //}
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Insert,
                                //Variable = "transportOrder"
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
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        Relation = Data.Variable.RELATION.variable,
                                //        ObjectName = "TransportOrder",
                                //        Alias = "transportOrders",
                                //        List = true
                                //    }
                                //}
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Insert,
                                //Variable = "transportOrders"
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
                        Docs = "UpdateTransportOrderStatusToCancelled transaction processor function.",
                        Blocks = new ObservableCollection<Block.Base>()
                        {
                            new MyInput()
                            {
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "TransportOrder",
                                //        Alias = "Alias_TransportOrder",
                                //    },
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "ECMR",
                                //        Alias = "Alias_ECMR",
                                //    },
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "LegalOwnerOrg",
                                //        Alias = "Alias_LegalOwnerOrg",
                                //    },
                                //    new Block.MyVariable(typeof(MyParticipant))
                                //    {
                                //        ObjectName = "LegalOwnerAdmin",
                                //        Alias = "Alias_LegalOwnerAdmin",
                                //    },
                                //    new Block.MyVariable(typeof(MyConcept))
                                //    {
                                //        ObjectName = "Address",
                                //        Alias = "Alias_Address",
                                //    },
                                //    new Block.MyVariable(typeof(string))
                                //    {
                                //        Alias = "Alias_Name",
                                //    },
                                //    new Block.MyVariable(typeof(int))
                                //    {
                                //        Alias = "Alias_Money",
                                //    }
                                //}
                            },
                            //new MySimpleIf()
                        }
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
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "LegalOwnerOrg",
                                //        Alias = "newLegalOwnerOrg",
                                //    }
                                //}
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Insert,
                                //Variable = "newLegalOwnerOrg"
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
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "CompoundOrg",
                                //        Alias = "newCompoundOrg",
                                //    }
                                //}
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Insert,
                                //Variable = "newCompoundOrg"
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
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "CarrierOrg",
                                //        Alias = "newCarrierOrg",
                                //    }
                                //}
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Insert,
                                //Variable = "newCarrierOrg"
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
                                //Vars = new ObservableCollection<Block.MyVariable>()
                                //{
                                //    new Block.MyVariable(typeof(MyAsset))
                                //    {
                                //        ObjectName = "RecipientOrg",
                                //        Alias = "newRecipientOrg",
                                //    }
                                //}
                            },
                            new MyRegistry()
                            {
                                Action = MyRegistry.ACTION.Insert,
                                //Variable = "newRecipientOrg"
                            }
                        }
                    }
                }
            });
        }
    }
}
