import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "pickTaskNo",
        label: i18n.t("wmsoutinvoicerecord.pickTaskNo"),
        width: 150
    },
    {
        key: "projectNo",
        label: i18n.t("wmsoutinvoicerecord.projectNo"),
        width: 150
    },
    {
        key: "outBarCode",
        label: i18n.t("wmsoutinvoicerecord.outBarCode"),
        width: 120
    },
    {
        key: "allocatResult",
        label: i18n.t("wmsoutinvoicerecord.allocatResult"),
        width: 200
    },
    {
        key: "allotQty",
        label: i18n.t("wmsoutinvoicerecord.allotQty")
    },
    // {
    //     key: "allotType",
    //     label: i18n.t("wmsoutinvoicerecord.allotType")
    // },
    // {
    //     key: "outRecordStatus",
    //     label: i18n.t("wmsoutinvoicerecord.outRecordStatus")
    // },
    {
        key: "outRecordStatusDesc",
        label: i18n.t("wmsoutinvoicerecord.outRecordStatus")
    },
    {
        key: "areaNo",
        label: i18n.t("wmsoutinvoicerecord.areaNo"),
        width: 80
    },
    // {
    //     key: "assemblyIdx",
    //     label: i18n.t("wmsoutinvoicerecord.assemblyIdx")
    // },
    {
        key: "batchNo",
        label: i18n.t("wmsoutinvoicerecord.batchNo"),
        width: 150
    },
    // {
    //     key: "belongDepartment",
    //     label: i18n.t("wmsoutinvoicerecord.belongDepartment")
    // },
    {
        key: "binNo",
        label: i18n.t("wmsoutinvoicerecord.binNo"),
        width: 150
    },
    {
        key: "deliveryLocNo",
        label: i18n.t("wmsoutinvoicerecord.deliveryLocNo")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsoutinvoicerecord.stockCode"),
        width: 150
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsoutinvoicerecord.stockDtlId"),
        width: 120
    },
    {
        key: "docTypeCode",
        label: i18n.t("wmsoutinvoicerecord.docTypeCode")
    },
    // {
    //     key: "erpWhouseNo",
    //     label: i18n.t("wmsoutinvoicerecord.erpWhouseNo")
    // },
    // {
    //     key: "externalOutDtlId",
    //     label: i18n.t("wmsoutinvoicerecord.externalOutDtlId")
    // },
    // {
    //     key: "externalOutNo",
    //     label: i18n.t("wmsoutinvoicerecord.externalOutNo")
    // },
    // {
    //     key: "fpName",
    //     label: i18n.t("wmsoutinvoicerecord.fpName")
    // },
    // {
    //     key: "fpNo",
    //     label: i18n.t("wmsoutinvoicerecord.fpNo")
    // },
    // {
    //     key: "fpQty",
    //     label: i18n.t("wmsoutinvoicerecord.fpQty")
    // },
    // {
    //     key: "inOutName",
    //     label: i18n.t("wmsoutinvoicerecord.inOutName")
    // },
    {
        key: "inOutTypeNo",
        label: i18n.t("wmsoutinvoicerecord.inOutTypeNo")
    },
    // {
    //     key: "inspectionResult",
    //     label: i18n.t("wmsoutinvoicerecord.inspectionResult")
    // },
    // {
    //     key: "invoiceDtlId",
    //     label: i18n.t("wmsoutinvoicerecord.invoiceDtlId")
    // },
    // {
    //     key: "invoiceNo",
    //     label: i18n.t("wmsoutinvoicerecord.invoiceNo")
    // },
    // {
    //     key: "isBatch",
    //     label: i18n.t("wmsoutinvoicerecord.isBatch")
    // },
    // {
    //     key: "issuedResult",
    //     label: i18n.t("wmsoutinvoicerecord.issuedResult")
    // },
    {
        key: "materialName",
        label: i18n.t("wmsoutinvoicerecord.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsoutinvoicerecord.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsoutinvoicerecord.materialSpec")
    },
    // {
    //     key: "orderDtlId",
    //     label: i18n.t("wmsoutinvoicerecord.orderDtlId")
    // },
    // {
    //     key: "orderNo",
    //     label: i18n.t("wmsoutinvoicerecord.orderNo")
    // },
   
    // {
    //     key: "palletBarcode",
    //     label: i18n.t("wmsoutinvoicerecord.palletBarcode")
    // },
    // {
    //     key: "palletPickType",
    //     label: i18n.t("wmsoutinvoicerecord.palletPickType")
    // },
    // {
    //     key: "pickLocNo",
    //     label: i18n.t("wmsoutinvoicerecord.pickLocNo")
    // },
    {
        key: "pickQty",
        label: i18n.t("wmsoutinvoicerecord.pickQty")
    },
    
    // {
    //     key: "pickType",
    //     label: i18n.t("wmsoutinvoicerecord.pickType")
    // },
    // {
    //     key: "preStockDtlId",
    //     label: i18n.t("wmsoutinvoicerecord.preStockDtlId")
    // },
    // {
    //     key: "productDeptCode",
    //     label: i18n.t("wmsoutinvoicerecord.productDeptCode")
    // },
    // {
    //     key: "productDeptName",
    //     label: i18n.t("wmsoutinvoicerecord.productDeptName")
    // },
    // {
    //     key: "productLocation",
    //     label: i18n.t("wmsoutinvoicerecord.productLocation")
    // },
    
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmsoutinvoicerecord.proprietorCode")
    // },
    // {
    //     key: "regionNo",
    //     label: i18n.t("wmsoutinvoicerecord.regionNo")
    // },
    // {
    //     key: "reversePickFlag",
    //     label: i18n.t("wmsoutinvoicerecord.reversePickFlag")
    // },
    // {
    //     key: "skuCode",
    //     label: i18n.t("wmsoutinvoicerecord.skuCode")
    // },
    // {
    //     key: "sourceBy",
    //     label: i18n.t("wmsoutinvoicerecord.sourceBy")
    // },
    
    // {
    //     key: "supplierCode",
    //     label: i18n.t("wmsoutinvoicerecord.supplierCode")
    // },
    // {
    //     key: "supplierName",
    //     label: i18n.t("wmsoutinvoicerecord.supplierName")
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsoutinvoicerecord.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsoutinvoicerecord.supplierNameEn")
    // },
    // {
    //     key: "supplyType",
    //     label: i18n.t("wmsoutinvoicerecord.supplyType")
    // },
    // {
    //     key: "ticketNo",
    //     label: i18n.t("wmsoutinvoicerecord.ticketNo")
    // },
    // {
    //     key: "ticketPlanBeginTime",
    //     label: i18n.t("wmsoutinvoicerecord.ticketPlanBeginTime")
    // },
    // {
    //     key: "ticketType",
    //     label: i18n.t("wmsoutinvoicerecord.ticketType")
    // },
    {
        key: "unitCode",
        label: i18n.t("wmsoutinvoicerecord.unitCode")
    },
    // {
    //     key: "waveNo",
    //     label: i18n.t("wmsoutinvoicerecord.waveNo")
    // },
    // {
    //     key: "whouseNo",
    //     label: i18n.t("wmsoutinvoicerecord.whouseNo")
    // },
    // {
    //     key: "operationMode",
    //     label: i18n.t("wmsoutinvoicerecord.operationMode")
    // },
    
    // {
    //     key: "urgentFlag",
    //     label: i18n.t("wmsoutinvoicerecord.urgentFlag")
    // },
    // {
    //     key: "loadedTtype",
    //     label: i18n.t("wmsoutinvoicerecord.loadedTtype")
    // },
    // {
    //     key: "productSn",
    //     label: i18n.t("wmsoutinvoicerecord.productSn")
    // },
    {
        key: "CreateBy",
        label: i18n.t("CommonString.CreateBy"),
        width: 100
    },
    {
        key: "CreateTime",
        label: i18n.t("CommonString.CreateTime"),
        width: 150
    },{
        key: "UpdateBy",
        label: i18n.t("CommonString.UpdateBy"),
        width: 100
    },
    {
        key: "UpdateTime",
        label: i18n.t("CommonString.UpdateTime"),
        width: 150
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


