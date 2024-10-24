/**
 * 页面集合
 */
export default {
  actionlog: {
    name: "actionlog",
    path: "/actionlog",
    controller: "WalkingTec.Mvvm.Admin.Api,ActionLog",
    icon: "el-icon-receiving"
  },
   frameworkuserbase: {
    name: "frameworkuser",
    path: "/frameworkuser",
    controller: "WalkingTec.Mvvm.Admin.Api,FrameworkUser",
    icon: "el-icon-user"
  },
  frameworkrole: {
    name: "frameworkrole",
    path: "/frameworkrole",
    controller: "WalkingTec.Mvvm.Admin.Api,FrameworkRole",
    icon: "el-icon-s-custom"
  },
 frameworkgroup: {
    name: "frameworkgroup",
    path: "/frameworkgroup",
    controller: "WalkingTec.Mvvm.Admin.Api,FrameworkGroup",
    icon: "el-icon-office-building"
  },
  frameworkmenu: {
    name: "frameworkmenu",
    path: "/frameworkmenu",
    controller: "WalkingTec.Mvvm.Admin.Api,FrameworkMenu",
    icon: "el-icon-menu"
  },
  dataprivilege: {
    name: "dataprivilege",
    path: "/dataprivilege",
    controller: "WalkingTec.Mvvm.Admin.Api,DataPrivilege",
    icon: "el-icon-odometer"
  }

, student: {
    name: '学生管理',
    path: '/student',
    controller: 'WalkingTec.Mvvm.VueDemo.Controllers,Student'
    }

, school: {
    name: '学校管理',
    path: '/school',
    controller: 'WalkingTec.Mvvm.VueDemo.Controllers,School'
    }

, major: {
    name: '专业管理',
    path: '/major',
    controller: 'WalkingTec.Mvvm.VueDemo.Controllers,Major'
    }

, frameworktenant: {
    name: '租户管理',
    path: '/frameworktenant',
    controller: 'WalkingTec.Mvvm.Admin.Api,FrameworkTenant'
    }

, basbcustomer: {
    name: 'basbcustomer',
    path: '/basbcustomer',
    controller: 'Wish.Controllers,BasBCustomer'
    }

, basbdepartment: {
    name: 'basbdepartment',
    path: '/basbdepartment',
    controller: 'Wish.Controllers,BasBDepartment'
    }

, basbmaterial: {
    name: 'basbmaterial',
    path: '/basbmaterial',
    controller: 'Wish.Controllers,BasBMaterial'
    }

, basbmaterialcategory: {
    name: 'basbmaterialcategory',
    path: '/basbmaterialcategory',
    controller: 'Wish.Controllers,BasBMaterialCategory'
    }

, basbmaterialtype: {
    name: 'basbmaterialtype',
    path: '/basbmaterialtype',
    controller: 'Wish.Controllers,BasBMaterialType'
    }

, basbproprietor: {
    name: 'basbproprietor',
    path: '/basbproprietor',
    controller: 'Wish.Controllers,BasBProprietor'
    }

, basbsku: {
    name: 'basbsku',
    path: '/basbsku',
    controller: 'Wish.Controllers,BasBSku'
    }

, basbsupplier: {
    name: 'basbsupplier',
    path: '/basbsupplier',
    controller: 'Wish.Controllers,BasBSupplier'
    }

, basbsupplierbin: {
    name: 'basbsupplierbin',
    path: '/basbsupplierbin',
    controller: 'Wish.Controllers,BasBSupplierBin'
    }

, basbunit: {
    name: 'basbunit',
    path: '/basbunit',
    controller: 'Wish.Controllers,BasBUnit'
    }

, dbconfig: {
    name: 'dbconfig',
    path: '/dbconfig',
    controller: 'Wish.Controllers,DBConfig'
    }

, plcconfig: {
    name: 'plcconfig',
    path: '/plcconfig',
    controller: 'Wish.Controllers,PlcConfig'
    }

, standarddevice: {
    name: 'standarddevice',
    path: '/standarddevice',
    controller: 'Wish.Controllers,StandardDevice'
    }

, deviceconfig: {
    name: 'deviceconfig',
    path: '/deviceconfig',
    controller: 'Wish.Controllers,DeviceConfig'
    }

, devicetasklog: {
    name: 'devicetasklog',
    path: '/devicetasklog',
    controller: 'Wish.Controllers,DeviceTaskLog'
    }

, devicestatuslog: {
    name: 'devicestatuslog',
    path: '/devicestatuslog',
    controller: 'Wish.Controllers,DeviceStatusLog'
    }

, devicealarmlog: {
    name: 'devicealarmlog',
    path: '/devicealarmlog',
    controller: 'Wish.Controllers,DeviceAlarmLog'
    }

, srmcmd: {
    name: 'srmcmd',
    path: '/srmcmd',
    controller: 'Wish.Controllers,SrmCmd'
    }

, srmcmdhis: {
    name: 'srmcmdhis',
    path: '/srmcmdhis',
    controller: 'Wish.Controllers,SrmCmdHis'
    }

, baswarea: {
    name: 'baswarea',
    path: '/baswarea',
    controller: 'Wish.Controllers,BasWArea'
    }

, baswbin: {
    name: 'baswbin',
    path: '/baswbin',
    controller: 'Wish.Controllers,BasWBin'
    }

, baswerpwhouse: {
    name: 'baswerpwhouse',
    path: '/baswerpwhouse',
    controller: 'Wish.Controllers,BasWErpWhouse'
    }

, baswerpwhousebin: {
    name: 'baswerpwhousebin',
    path: '/baswerpwhousebin',
    controller: 'Wish.Controllers,BasWErpWhouseBin'
    }

, baswloc: {
    name: 'baswloc',
    path: '/baswloc',
    controller: 'Wish.Controllers,BasWLoc'
    }

, baswlocgroup: {
    name: 'baswlocgroup',
    path: '/baswlocgroup',
    controller: 'Wish.Controllers,BasWLocGroup'
    }

, baswpallet: {
    name: 'baswpallet',
    path: '/baswpallet',
    controller: 'Wish.Controllers,BasWPallet'
    }

, baswpallettype: {
    name: 'baswpallettype',
    path: '/baswpallettype',
    controller: 'Wish.Controllers,BasWPalletType'
    }

, baswrack: {
    name: 'baswrack',
    path: '/baswrack',
    controller: 'Wish.Controllers,BasWRack'
    }

, baswregion: {
    name: 'baswregion',
    path: '/baswregion',
    controller: 'Wish.Controllers,BasWRegion'
    }

, baswregiontype: {
    name: 'baswregiontype',
    path: '/baswregiontype',
    controller: 'Wish.Controllers,BasWRegionType'
    }

, baswroadway: {
    name: 'baswroadway',
    path: '/baswroadway',
    controller: 'Wish.Controllers,BasWRoadway'
    }

, baswwhouse: {
    name: 'baswwhouse',
    path: '/baswwhouse',
    controller: 'Wish.Controllers,BasWWhouse'
    }

, wmsitninventory: {
    name: 'wmsitninventory',
    path: '/wmsitninventory',
    controller: 'Wish.Controllers,WmsItnInventory'
    }

, wmsitninventorydtl: {
    name: 'wmsitninventorydtl',
    path: '/wmsitninventorydtl',
    controller: 'Wish.Controllers,WmsItnInventoryDtl'
    }

, wmsitninventorydtlhis: {
    name: 'wmsitninventorydtlhis',
    path: '/wmsitninventorydtlhis',
    controller: 'Wish.Controllers,WmsItnInventoryDtlHis'
    }

, wmsitninventoryhis: {
    name: 'wmsitninventoryhis',
    path: '/wmsitninventoryhis',
    controller: 'Wish.Controllers,WmsItnInventoryHis'
    }

, wmsitninventoryrecord: {
    name: 'wmsitninventoryrecord',
    path: '/wmsitninventoryrecord',
    controller: 'Wish.Controllers,WmsItnInventoryRecord'
    }

, wmsitninventoryrecorddif: {
    name: 'wmsitninventoryrecorddif',
    path: '/wmsitninventoryrecorddif',
    controller: 'Wish.Controllers,WmsItnInventoryRecordDif'
    }

, wmsitninventoryrecorddifhis: {
    name: 'wmsitninventoryrecorddifhis',
    path: '/wmsitninventoryrecorddifhis',
    controller: 'Wish.Controllers,WmsItnInventoryRecordDifHis'
    }

, wmsitninventoryrecordhis: {
    name: 'wmsitninventoryrecordhis',
    path: '/wmsitninventoryrecordhis',
    controller: 'Wish.Controllers,WmsItnInventoryRecordHis'
    }

, wmsinorder: {
    name: 'wmsinorder',
    path: '/wmsinorder',
    controller: 'Wish.Controllers,WmsInOrder'
    }

, wmsinorderdtl: {
    name: 'wmsinorderdtl',
    path: '/wmsinorderdtl',
    controller: 'Wish.Controllers,WmsInOrderDtl'
    }

, wmsinorderdtlhis: {
    name: 'wmsinorderdtlhis',
    path: '/wmsinorderdtlhis',
    controller: 'Wish.Controllers,WmsInOrderDtlHis'
    }

, wmsinorderhis: {
    name: 'wmsinorderhis',
    path: '/wmsinorderhis',
    controller: 'Wish.Controllers,WmsInOrderHis'
    }

, wmsinreceipt: {
    name: 'wmsinreceipt',
    path: '/wmsinreceipt',
    controller: 'Wish.Controllers,WmsInReceipt'
    }

, wmsinreceiptdtl: {
    name: 'wmsinreceiptdtl',
    path: '/wmsinreceiptdtl',
    controller: 'Wish.Controllers,WmsInReceiptDtl'
    }

, wmsinreceiptdtlhis: {
    name: 'wmsinreceiptdtlhis',
    path: '/wmsinreceiptdtlhis',
    controller: 'Wish.Controllers,WmsInReceiptDtlHis'
    }

, wmsinreceipthis: {
    name: 'wmsinreceipthis',
    path: '/wmsinreceipthis',
    controller: 'Wish.Controllers,WmsInReceiptHis'
    }

, wmsinreceiptiqcrecord: {
    name: 'wmsinreceiptiqcrecord',
    path: '/wmsinreceiptiqcrecord',
    controller: 'Wish.Controllers,WmsInReceiptIqcRecord'
    }

, wmsinreceiptiqcrecordhis: {
    name: 'wmsinreceiptiqcrecordhis',
    path: '/wmsinreceiptiqcrecordhis',
    controller: 'Wish.Controllers,WmsInReceiptIqcRecordHis'
    }

, wmsinreceiptiqcresult: {
    name: 'wmsinreceiptiqcresult',
    path: '/wmsinreceiptiqcresult',
    controller: 'Wish.Controllers,WmsInReceiptIqcResult'
    }

, wmsinreceiptiqcresulthis: {
    name: 'wmsinreceiptiqcresulthis',
    path: '/wmsinreceiptiqcresulthis',
    controller: 'Wish.Controllers,WmsInReceiptIqcResultHis'
    }

, wmsinreceiptrecord: {
    name: 'wmsinreceiptrecord',
    path: '/wmsinreceiptrecord',
    controller: 'Wish.Controllers,WmsInReceiptRecord'
    }

, wmsinreceiptrecordhis: {
    name: 'wmsinreceiptrecordhis',
    path: '/wmsinreceiptrecordhis',
    controller: 'Wish.Controllers,WmsInReceiptRecordHis'
    }

, wmsinreceiptuniicode: {
    name: 'wmsinreceiptuniicode',
    path: '/wmsinreceiptuniicode',
    controller: 'Wish.Controllers,WmsInReceiptUniicode'
    }

, wmsinreceiptuniicodehis: {
    name: 'wmsinreceiptuniicodehis',
    path: '/wmsinreceiptuniicodehis',
    controller: 'Wish.Controllers,WmsInReceiptUniicodeHis'
    }

, wmsitnqc: {
    name: 'wmsitnqc',
    path: '/wmsitnqc',
    controller: 'Wish.Controllers,WmsItnQc'
    }

, wmsitnqcdtl: {
    name: 'wmsitnqcdtl',
    path: '/wmsitnqcdtl',
    controller: 'Wish.Controllers,WmsItnQcDtl'
    }

, wmsitnqcdtlhis: {
    name: 'wmsitnqcdtlhis',
    path: '/wmsitnqcdtlhis',
    controller: 'Wish.Controllers,WmsItnQcDtlHis'
    }

, wmsitnqchis: {
    name: 'wmsitnqchis',
    path: '/wmsitnqchis',
    controller: 'Wish.Controllers,WmsItnQcHis'
    }

, wmsitnqcrecord: {
    name: 'wmsitnqcrecord',
    path: '/wmsitnqcrecord',
    controller: 'Wish.Controllers,WmsItnQcRecord'
    }

, wmsitnqcrecordhis: {
    name: 'wmsitnqcrecordhis',
    path: '/wmsitnqcrecordhis',
    controller: 'Wish.Controllers,WmsItnQcRecordHis'
    }

, wmsitnmove: {
    name: 'wmsitnmove',
    path: '/wmsitnmove',
    controller: 'Wish.Controllers,WmsItnMove'
    }

, wmsitnmovedtl: {
    name: 'wmsitnmovedtl',
    path: '/wmsitnmovedtl',
    controller: 'Wish.Controllers,WmsItnMoveDtl'
    }

, wmsitnmovedtlhis: {
    name: 'wmsitnmovedtlhis',
    path: '/wmsitnmovedtlhis',
    controller: 'Wish.Controllers,WmsItnMoveDtlHis'
    }

, wmsitnmovehis: {
    name: 'wmsitnmovehis',
    path: '/wmsitnmovehis',
    controller: 'Wish.Controllers,WmsItnMoveHis'
    }

, wmsitnmoverecord: {
    name: 'wmsitnmoverecord',
    path: '/wmsitnmoverecord',
    controller: 'Wish.Controllers,WmsItnMoveRecord'
    }

, wmsitnmoverecordhis: {
    name: 'wmsitnmoverecordhis',
    path: '/wmsitnmoverecordhis',
    controller: 'Wish.Controllers,WmsItnMoveRecordHis'
    }

, wmsoutinvoice: {
    name: 'wmsoutinvoice',
    path: '/wmsoutinvoice',
    controller: 'Wish.Controllers,WmsOutInvoice'
    }

, wmsoutinvoicedtl: {
    name: 'wmsoutinvoicedtl',
    path: '/wmsoutinvoicedtl',
    controller: 'Wish.Controllers,WmsOutInvoiceDtl'
    }

, wmsoutinvoicedtlhis: {
    name: 'wmsoutinvoicedtlhis',
    path: '/wmsoutinvoicedtlhis',
    controller: 'Wish.Controllers,WmsOutInvoiceDtlHis'
    }

, wmsoutinvoicehis: {
    name: 'wmsoutinvoicehis',
    path: '/wmsoutinvoicehis',
    controller: 'Wish.Controllers,WmsOutInvoiceHis'
    }

, wmsoutinvoicerecord: {
    name: 'wmsoutinvoicerecord',
    path: '/wmsoutinvoicerecord',
    controller: 'Wish.Controllers,WmsOutInvoiceRecord'
    }

, wmsoutinvoicerecordhis: {
    name: 'wmsoutinvoicerecordhis',
    path: '/wmsoutinvoicerecordhis',
    controller: 'Wish.Controllers,WmsOutInvoiceRecordHis'
    }

, wmsoutinvoiceuniicode: {
    name: 'wmsoutinvoiceuniicode',
    path: '/wmsoutinvoiceuniicode',
    controller: 'Wish.Controllers,WmsOutInvoiceUniicode'
    }

, wmsoutinvoiceuniicodehis: {
    name: 'wmsoutinvoiceuniicodehis',
    path: '/wmsoutinvoiceuniicodehis',
    controller: 'Wish.Controllers,WmsOutInvoiceUniicodeHis'
    }

, wmsoutwave: {
    name: 'wmsoutwave',
    path: '/wmsoutwave',
    controller: 'Wish.Controllers,WmsOutWave'
    }

, wmsoutwavehis: {
    name: 'wmsoutwavehis',
    path: '/wmsoutwavehis',
    controller: 'Wish.Controllers,WmsOutWaveHis'
    }

, wmsputaway: {
    name: 'wmsputaway',
    path: '/wmsputaway',
    controller: 'Wish.Controllers,WmsPutaway'
    }

, wmsputawayhis: {
    name: 'wmsputawayhis',
    path: '/wmsputawayhis',
    controller: 'Wish.Controllers,WmsPutawayHis'
    }

, wmsputawaydtl: {
    name: 'wmsputawaydtl',
    path: '/wmsputawaydtl',
    controller: 'Wish.Controllers,WmsPutawayDtl'
    }

, wmsputawaydtlhis: {
    name: 'wmsputdowndtlhis',
    path: '/wmsputawaydtlhis',
    controller: 'Wish.Controllers,WmsPutawayDtlHis'
    }

, wmsputdown: {
    name: 'wmsputdown',
    path: '/wmsputdown',
    controller: 'Wish.Controllers,WmsPutdown'
    }

, wmsputdownhis: {
    name: 'wmsputdownhis',
    path: '/wmsputdownhis',
    controller: 'Wish.Controllers,WmsPutdownHis'
    }

, wmsputdowndtl: {
    name: 'wmsputdowndtl',
    path: '/wmsputdowndtl',
    controller: 'Wish.Controllers,WmsPutdownDtl'
    }

, wmsputdowndtlhis: {
    name: 'wmsputdowndtlhis',
    path: '/wmsputdowndtlhis',
    controller: 'Wish.Controllers,WmsPutdownDtlHis'
    }

, wmsstock: {
    name: 'wmsstock',
    path: '/wmsstock',
    controller: 'Wish.Controllers,WmsStock'
    }

, wmsstockhis: {
    name: 'wmsstockhis',
    path: '/wmsstockhis',
    controller: 'Wish.Controllers,WmsStockHis'
    }

, wmsstockadjust: {
    name: 'wmsstockadjust',
    path: '/wmsstockadjust',
    controller: 'Wish.Controllers,WmsStockAdjust'
    }

, wmsstockdtl: {
    name: 'wmsstockdtl',
    path: '/wmsstockdtl',
    controller: 'Wish.Controllers,WmsStockDtl'
    }

, wmsstockdtlhis: {
    name: 'wmsstockdtlhis',
    path: '/wmsstockdtlhis',
    controller: 'Wish.Controllers,WmsStockDtlHis'
    }

, wmsstockreconcileresult: {
    name: 'wmsstockreconcileresult',
    path: '/wmsstockreconcileresult',
    controller: 'Wish.Controllers,WmsStockReconcileResult'
    }

, wmsstockreconcileresulthis: {
    name: 'wmsstockreconcileresulthis',
    path: '/wmsstockreconcileresulthis',
    controller: 'Wish.Controllers,WmsStockReconcileResultHis'
    }

, wmsstockuniicode: {
    name: 'wmsstockuniicode',
    path: '/wmsstockuniicode',
    controller: 'Wish.Controllers,WmsStockUniicode'
    }

, wmsstockuniicodehis: {
    name: 'wmsstockuniicodehis',
    path: '/wmsstockuniicodehis',
    controller: 'Wish.Controllers,WmsStockUniicodeHis'
    }

, wmstask: {
    name: 'wmstask',
    path: '/wmstask',
    controller: 'Wish.Controllers,WmsTask'
    }

, wmstaskhis: {
    name: 'wmstaskhis',
    path: '/wmstaskhis',
    controller: 'Wish.Controllers,WmsTaskHis'
    }

, cfgbusiness: {
    name: 'cfgbusiness',
    path: '/cfgbusiness',
    controller: 'Wish.Controllers,CfgBusiness'
    }

, cfgbusinessmodule: {
    name: 'cfgbusinessmodule',
    path: '/cfgbusinessmodule',
    controller: 'Wish.Controllers,CfgBusinessModule'
    }

, cfgbusinessparam: {
    name: 'cfgbusinessparam',
    path: '/cfgbusinessparam',
    controller: 'Wish.Controllers,CfgBusinessParam'
    }

, cfgbusinessparamvalue: {
    name: 'cfgbusinessparamvalue',
    path: '/cfgbusinessparamvalue',
    controller: 'Wish.Controllers,CfgBusinessParamValue'
    }

, cfgdocloc: {
    name: 'cfgdocloc',
    path: '/cfgdocloc',
    controller: 'Wish.Controllers,CfgDocLoc'
    }

, cfgdoctype: {
    name: 'cfgdoctype',
    path: '/cfgdoctype',
    controller: 'Wish.Controllers,CfgDocType'
    }

, cfgdoctypedtl: {
    name: 'cfgdoctypedtl',
    path: '/cfgdoctypedtl',
    controller: 'Wish.Controllers,CfgDocTypeDtl'
    }

, cfgrelationship: {
    name: 'cfgrelationship',
    path: '/cfgrelationship',
    controller: 'Wish.Controllers,CfgRelationship'
    }

, cfgrelationshiptype: {
    name: 'cfgrelationshiptype',
    path: '/cfgrelationshiptype',
    controller: 'Wish.Controllers,CfgRelationshipType'
    }

, cfgstrategy: {
    name: 'cfgstrategy',
    path: '/cfgstrategy',
    controller: 'Wish.Controllers,CfgStrategy'
    }

, cfgstrategydtl: {
    name: 'cfgstrategydtl',
    path: '/cfgstrategydtl',
    controller: 'Wish.Controllers,CfgStrategyDtl'
    }

, cfgstrategyitem: {
    name: 'cfgstrategyitem',
    path: '/cfgstrategyitem',
    controller: 'Wish.Controllers,CfgStrategyItem'
    }

, cfgstrategytype: {
    name: 'cfgstrategytype',
    path: '/cfgstrategytype',
    controller: 'Wish.Controllers,CfgStrategyType'
    }

, cfgerpwhouse: {
    name: 'cfgerpwhouse',
    path: '/cfgerpwhouse',
    controller: 'Wish.Controllers,CfgErpWhouse'
    }

, syssequence: {
    name: 'syssequence',
    path: '/syssequence',
    controller: 'Wish.Controllers,SysSequence'
    }

, sysdictionary: {
    name: 'sysdictionary',
    path: '/sysdictionary',
    controller: 'Wish.Controllers,SysDictionary'
    }

, sysparameter: {
    name: 'sysparameter',
    path: '/sysparameter',
    controller: 'Wish.Controllers,SysParameter'
    }

, sysparametervalue: {
    name: 'sysparametervalue',
    path: '/sysparametervalue',
    controller: 'Wish.Controllers,SysParameterValue'
    }

, interfaceconfig: {
    name: 'interfaceconfig',
    path: '/interfaceconfig',
    controller: 'Wish.Controllers,InterfaceConfig'
    }

, interfacesendback: {
    name: 'interfacesendback',
    path: '/interfacesendback',
    controller: 'Wish.Controllers,InterfaceSendBack'
    }

, interfacesendbackhis: {
    name: 'interfacesendbackhis',
    path: '/interfacesendbackhis',
    controller: 'Wish.Controllers,InterfaceSendBackHis'
    }

, sysemail: {
    name: '电子邮件',
    path: '/sysemail',
    controller: 'Wish.Controllers,SysEmail'
    }

, sysmapmonitor: {
    name: '整线监控',
    path: '/sysmapmonitor',
    controller: 'Wish.Controllers,SysMapMonitor'
    }

, motdevparts: {
    name: '设备状态',
    path: '/motdevparts',
    controller: 'Wish.Controllers,MotDevParts'
    }

, motdevpartshis: {
    name: '设备状态历史',
    path: '/motdevpartshis',
    controller: 'Wish.Controllers,MotDevPartsHis'
    }
/**WTM**/
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

};
