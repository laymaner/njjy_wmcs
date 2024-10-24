import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported"
];

export const TABLE_HEADER: Array<object> = [


    // {
    //     key: "areaNo",
    //     label: i18n.t("wmsinreceiptrecord.areaNo")
    // },
    // {
    //     key: "batchNo",
    //     label: i18n.t("wmsinreceiptrecord.batchNo")
    // },
    {
        key: "inNo",
        label: i18n.t("wmsinreceiptrecord.inNo"),
        width: 150
    },
    {
        key: "binNo",
        label: i18n.t("wmsinreceiptrecord.binNo"),
        width: 150
    },
    // {
    //     key: "departmentName",
    //     label: i18n.t("wmsinreceiptrecord.departmentName")
    // },
    {
        key: "docTypeCode",
        label: i18n.t("wmsinreceiptrecord.docTypeCode")
    },
    // {
    //     key: "erpWhouseNo",
    //     label: i18n.t("wmsinreceiptrecord.erpWhouseNo")
    // },
    // {
    //     key: "externalInDtlId",
    //     label: i18n.t("wmsinreceiptrecord.externalInDtlId")
    // },
    // {
    //     key: "externalInNo",
    //     label: i18n.t("wmsinreceiptrecord.externalInNo")
    // },
    // {
    //     key: "inDtlId",
    //     label: i18n.t("wmsinreceiptrecord.inDtlId")
    // },
    // {
    //     key: "inNo",
    //     label: i18n.t("wmsinreceiptrecord.inNo"),
    //     width: 150
    // },
    // {
    //     key: "inOutName",
    //     label: i18n.t("wmsinreceiptrecord.inOutName")
    // },
    {
        key: "inOutTypeNo",
        label: i18n.t("wmsinreceiptrecord.inOutTypeNo")
    },
    // {
    //     key: "inspectionResult",
    //     label: i18n.t("wmsinreceiptrecord.inspectionResult")
    // },
    // {
    //     key: "inspector",
    //     label: i18n.t("wmsinreceiptrecord.inspector")
    // },
    // {
    //     key: "iqcResultNo",
    //     label: i18n.t("wmsinreceiptrecord.iqcResultNo")
    // },
    // {
    //     key: "inRecordStatus",
    //     label: i18n.t("wmsinreceiptrecord.inRecordStatus")
    // },
    {
        key: "inRecordStatusDesc",
        label: i18n.t("wmsinreceiptrecord.inRecordStatus")
    },
    // {
    //     key: "loadedType",
    //     label: i18n.t("wmsinreceiptrecord.loadedType")
    // },
    {
        key: "materialName",
        label: i18n.t("wmsinreceiptrecord.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsinreceiptrecord.materialCode"),
        width: 150
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsinreceiptrecord.materialSpec")
    },
    // {
    //     key: "orderDtlId",
    //     label: i18n.t("wmsinreceiptrecord.orderDtlId")
    // },
    // {
    //     key: "orderNo",
    //     label: i18n.t("wmsinreceiptrecord.orderNo")
    // },
    {
        key: "palletBarcode",
        label: i18n.t("wmsinreceiptrecord.palletBarcode")
    },
    {
        key: "ptaRegionNo",
        label: i18n.t("wmsinreceiptrecord.ptaRegionNo")
    },
    {
        key: "ptaStockCode",
        label: i18n.t("wmsinreceiptrecord.ptaStockCode"),
        width: 150
    },
    {
        key: "ptaStockDtlId",
        label: i18n.t("wmsinreceiptrecord.ptaStockDtlId"),
        width: 130
    },
    {
        key: "minPkgQty",
        label: i18n.t("wmsinreceiptrecord.minPkgQty"),
        width: 100
    },
    // {
    //     key: "projectNo",
    //     label: i18n.t("wmsinreceiptrecord.projectNo")
    // },
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmsinreceiptrecord.proprietorCode")
    // },
    {
        key: "ptaBinNo",
        label: i18n.t("wmsinreceiptrecord.ptaBinNo"),
        width: 150
    },
    {
        key: "ptaPalletBarcode",
        label: i18n.t("wmsinreceiptrecord.ptaPalletBarcode"),
        width: 150
    },
    // {
    //     key: "receiptDtlId",
    //     label: i18n.t("wmsinreceiptrecord.receiptDtlId")
    // },
    // {
    //     key: "receiptNo",
    //     label: i18n.t("wmsinreceiptrecord.receiptNo")
    // },
    {
        key: "recordQty",
        label: i18n.t("wmsinreceiptrecord.recordQty")
    },
    {
        key: "regionNo",
        label: i18n.t("wmsinreceiptrecord.regionNo")
    },
    // {
    //     key: "replenishFlag",
    //     label: i18n.t("wmsinreceiptrecord.replenishFlag")
    // },
    // {
    //     key: "returnFlag",
    //     label: i18n.t("wmsinreceiptrecord.returnFlag")
    // },
    // {
    //     key: "returnResult",
    //     label: i18n.t("wmsinreceiptrecord.returnResult")
    // },
    // {
    //     key: "returnTime",
    //     label: i18n.t("wmsinreceiptrecord.returnTime")
    // },
    // {
    //     key: "skuCode",
    //     label: i18n.t("wmsinreceiptrecord.skuCode")
    // },
    // {
    //     key: "sourceBy",
    //     label: i18n.t("wmsinreceiptrecord.sourceBy")
    // },
    // {
    //     key: "stockCode",
    //     label: i18n.t("wmsinreceiptrecord.stockCode")
    // },
    // {
    //     key: "stockDtlId",
    //     label: i18n.t("wmsinreceiptrecord.stockDtlId")
    // },
    // {
    //     key: "supplierCode",
    //     label: i18n.t("wmsinreceiptrecord.supplierCode")
    // },
    // {
    //     key: "supplierName",
    //     label: i18n.t("wmsinreceiptrecord.supplierName")
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsinreceiptrecord.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsinreceiptrecord.supplierNameEn")
    // },
    // {
    //     key: "ticketNo",
    //     label: i18n.t("wmsinreceiptrecord.ticketNo")
    // },
    // {
    //     key: "urgentFlag",
    //     label: i18n.t("wmsinreceiptrecord.urgentFlag")
    // },
    {
        key: "unitCode",
        label: i18n.t("wmsinreceiptrecord.unitCode")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsinreceiptrecord.whouseNo")
    },{
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


