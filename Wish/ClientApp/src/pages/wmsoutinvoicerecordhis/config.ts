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
        label: i18n.t("wmsoutinvoicerecordhis.pickTaskNo"),
        width: 150
    },
    {
        key: "projectNo",
        label: i18n.t("wmsoutinvoicerecordhis.projectNo"),
        width: 150
    },
    {
        key: "outBarCode",
        label: i18n.t("wmsoutinvoicerecordhis.outBarCode"),
        width: 120
    },
    {
        key: "allocatResult",
        label: i18n.t("wmsoutinvoicerecordhis.allocatResult"),
        width: 200
    },
    {
        key: "allotQty",
        label: i18n.t("wmsoutinvoicerecordhis.allotQty")
    },
    // {
    //     key: "allotType",
    //     label: i18n.t("wmsoutinvoicerecordhis.allotType")
    // },
    // {
    //     key: "outRecordStatus",
    //     label: i18n.t("wmsoutinvoicerecordhis.outRecordStatus")
    // },
    {
        key: "outRecordStatusDesc",
        label: i18n.t("wmsoutinvoicerecordhis.outRecordStatus")
    },
    {
        key: "areaNo",
        label: i18n.t("wmsoutinvoicerecordhis.areaNo"),
        width: 80
    },
    // {
    //     key: "assemblyIdx",
    //     label: i18n.t("wmsoutinvoicerecordhis.assemblyIdx")
    // },
    {
        key: "batchNo",
        label: i18n.t("wmsoutinvoicerecordhis.batchNo"),
        width: 150
    },
    // {
    //     key: "belongDepartment",
    //     label: i18n.t("wmsoutinvoicerecordhis.belongDepartment")
    // },
    {
        key: "binNo",
        label: i18n.t("wmsoutinvoicerecordhis.binNo"),
        width: 150
    },
    {
        key: "deliveryLocNo",
        label: i18n.t("wmsoutinvoicerecordhis.deliveryLocNo")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsoutinvoicerecordhis.stockCode"),
        width: 150
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsoutinvoicerecordhis.stockDtlId"),
        width: 120
    },
    {
        key: "docTypeCode",
        label: i18n.t("wmsoutinvoicerecordhis.docTypeCode")
    },
    // {
    //     key: "erpWhouseNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.erpWhouseNo")
    // },
    // {
    //     key: "externalOutDtlId",
    //     label: i18n.t("wmsoutinvoicerecordhis.externalOutDtlId")
    // },
    // {
    //     key: "externalOutNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.externalOutNo")
    // },
    // {
    //     key: "fpName",
    //     label: i18n.t("wmsoutinvoicerecordhis.fpName")
    // },
    // {
    //     key: "fpNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.fpNo")
    // },
    // {
    //     key: "fpQty",
    //     label: i18n.t("wmsoutinvoicerecordhis.fpQty")
    // },
    // {
    //     key: "inOutName",
    //     label: i18n.t("wmsoutinvoicerecordhis.inOutName")
    // },
    {
        key: "inOutTypeNo",
        label: i18n.t("wmsoutinvoicerecordhis.inOutTypeNo")
    },
    // {
    //     key: "inspectionResult",
    //     label: i18n.t("wmsoutinvoicerecordhis.inspectionResult")
    // },
    // {
    //     key: "invoiceDtlId",
    //     label: i18n.t("wmsoutinvoicerecordhis.invoiceDtlId")
    // },
    // {
    //     key: "invoiceNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.invoiceNo")
    // },
    // {
    //     key: "isBatch",
    //     label: i18n.t("wmsoutinvoicerecordhis.isBatch")
    // },
    // {
    //     key: "issuedResult",
    //     label: i18n.t("wmsoutinvoicerecordhis.issuedResult")
    // },
    {
        key: "materialName",
        label: i18n.t("wmsoutinvoicerecordhis.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsoutinvoicerecordhis.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsoutinvoicerecordhis.materialSpec")
    },
    // {
    //     key: "orderDtlId",
    //     label: i18n.t("wmsoutinvoicerecordhis.orderDtlId")
    // },
    // {
    //     key: "orderNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.orderNo")
    // },
   
    // {
    //     key: "palletBarcode",
    //     label: i18n.t("wmsoutinvoicerecordhis.palletBarcode")
    // },
    // {
    //     key: "palletPickType",
    //     label: i18n.t("wmsoutinvoicerecordhis.palletPickType")
    // },
    // {
    //     key: "pickLocNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.pickLocNo")
    // },
    {
        key: "pickQty",
        label: i18n.t("wmsoutinvoicerecordhis.pickQty")
    },
    
    // {
    //     key: "pickType",
    //     label: i18n.t("wmsoutinvoicerecordhis.pickType")
    // },
    // {
    //     key: "preStockDtlId",
    //     label: i18n.t("wmsoutinvoicerecordhis.preStockDtlId")
    // },
    // {
    //     key: "productDeptCode",
    //     label: i18n.t("wmsoutinvoicerecordhis.productDeptCode")
    // },
    // {
    //     key: "productDeptName",
    //     label: i18n.t("wmsoutinvoicerecordhis.productDeptName")
    // },
    // {
    //     key: "productLocation",
    //     label: i18n.t("wmsoutinvoicerecordhis.productLocation")
    // },
    
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmsoutinvoicerecordhis.proprietorCode")
    // },
    // {
    //     key: "regionNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.regionNo")
    // },
    // {
    //     key: "reversePickFlag",
    //     label: i18n.t("wmsoutinvoicerecordhis.reversePickFlag")
    // },
    // {
    //     key: "skuCode",
    //     label: i18n.t("wmsoutinvoicerecordhis.skuCode")
    // },
    // {
    //     key: "sourceBy",
    //     label: i18n.t("wmsoutinvoicerecordhis.sourceBy")
    // },
    
    // {
    //     key: "supplierCode",
    //     label: i18n.t("wmsoutinvoicerecordhis.supplierCode")
    // },
    // {
    //     key: "supplierName",
    //     label: i18n.t("wmsoutinvoicerecordhis.supplierName")
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsoutinvoicerecordhis.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsoutinvoicerecordhis.supplierNameEn")
    // },
    // {
    //     key: "supplyType",
    //     label: i18n.t("wmsoutinvoicerecordhis.supplyType")
    // },
    // {
    //     key: "ticketNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.ticketNo")
    // },
    // {
    //     key: "ticketPlanBeginTime",
    //     label: i18n.t("wmsoutinvoicerecordhis.ticketPlanBeginTime")
    // },
    // {
    //     key: "ticketType",
    //     label: i18n.t("wmsoutinvoicerecordhis.ticketType")
    // },
    {
        key: "unitCode",
        label: i18n.t("wmsoutinvoicerecordhis.unitCode")
    },
    // {
    //     key: "waveNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.waveNo")
    // },
    // {
    //     key: "whouseNo",
    //     label: i18n.t("wmsoutinvoicerecordhis.whouseNo")
    // },
    // {
    //     key: "operationMode",
    //     label: i18n.t("wmsoutinvoicerecordhis.operationMode")
    // },
    
    // {
    //     key: "urgentFlag",
    //     label: i18n.t("wmsoutinvoicerecordhis.urgentFlag")
    // },
    // {
    //     key: "loadedTtype",
    //     label: i18n.t("wmsoutinvoicerecordhis.loadedTtype")
    // },
    // {
    //     key: "productSn",
    //     label: i18n.t("wmsoutinvoicerecordhis.productSn")
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


