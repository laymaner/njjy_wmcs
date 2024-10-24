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
    //     label: i18n.t("wmsinreceiptrecordhis.areaNo")
    // },
    // {
    //     key: "batchNo",
    //     label: i18n.t("wmsinreceiptrecordhis.batchNo")
    // },
    {
        key: "inNo",
        label: i18n.t("wmsinreceiptrecordhis.inNo"),
        width: 150
    },
    {
        key: "binNo",
        label: i18n.t("wmsinreceiptrecordhis.binNo"),
        width: 150
    },
    // {
    //     key: "departmentName",
    //     label: i18n.t("wmsinreceiptrecordhis.departmentName")
    // },
    {
        key: "docTypeCode",
        label: i18n.t("wmsinreceiptrecordhis.docTypeCode")
    },
    // {
    //     key: "erpWhouseNo",
    //     label: i18n.t("wmsinreceiptrecordhis.erpWhouseNo")
    // },
    // {
    //     key: "externalInDtlId",
    //     label: i18n.t("wmsinreceiptrecordhis.externalInDtlId")
    // },
    // {
    //     key: "externalInNo",
    //     label: i18n.t("wmsinreceiptrecordhis.externalInNo")
    // },
    // {
    //     key: "inDtlId",
    //     label: i18n.t("wmsinreceiptrecordhis.inDtlId")
    // },
    // {
    //     key: "inNo",
    //     label: i18n.t("wmsinreceiptrecordhis.inNo"),
    //     width: 150
    // },
    // {
    //     key: "inOutName",
    //     label: i18n.t("wmsinreceiptrecordhis.inOutName")
    // },
    {
        key: "inOutTypeNo",
        label: i18n.t("wmsinreceiptrecordhis.inOutTypeNo")
    },
    // {
    //     key: "inspectionResult",
    //     label: i18n.t("wmsinreceiptrecordhis.inspectionResult")
    // },
    // {
    //     key: "inspector",
    //     label: i18n.t("wmsinreceiptrecordhis.inspector")
    // },
    // {
    //     key: "iqcResultNo",
    //     label: i18n.t("wmsinreceiptrecordhis.iqcResultNo")
    // },
    // {
    //     key: "inRecordStatus",
    //     label: i18n.t("wmsinreceiptrecordhis.inRecordStatus")
    // },
    {
        key: "inRecordStatusDesc",
        label: i18n.t("wmsinreceiptrecordhis.inRecordStatus")
    },
    // {
    //     key: "loadedType",
    //     label: i18n.t("wmsinreceiptrecordhis.loadedType")
    // },
    {
        key: "materialName",
        label: i18n.t("wmsinreceiptrecordhis.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsinreceiptrecordhis.materialCode"),
        width: 150
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsinreceiptrecordhis.materialSpec")
    },
    // {
    //     key: "orderDtlId",
    //     label: i18n.t("wmsinreceiptrecordhis.orderDtlId")
    // },
    // {
    //     key: "orderNo",
    //     label: i18n.t("wmsinreceiptrecordhis.orderNo")
    // },
    {
        key: "palletBarcode",
        label: i18n.t("wmsinreceiptrecordhis.palletBarcode")
    },
    {
        key: "ptaRegionNo",
        label: i18n.t("wmsinreceiptrecordhis.ptaRegionNo")
    },
    {
        key: "ptaStockCode",
        label: i18n.t("wmsinreceiptrecordhis.ptaStockCode"),
        width: 150
    },
    {
        key: "ptaStockDtlId",
        label: i18n.t("wmsinreceiptrecordhis.ptaStockDtlId"),
        width: 130
    },
    {
        key: "minPkgQty",
        label: i18n.t("wmsinreceiptrecordhis.minPkgQty"),
        width: 100
    },
    // {
    //     key: "projectNo",
    //     label: i18n.t("wmsinreceiptrecordhis.projectNo")
    // },
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmsinreceiptrecordhis.proprietorCode")
    // },
    {
        key: "ptaBinNo",
        label: i18n.t("wmsinreceiptrecordhis.ptaBinNo"),
        width: 150
    },
    {
        key: "ptaPalletBarcode",
        label: i18n.t("wmsinreceiptrecordhis.ptaPalletBarcode"),
        width: 150
    },
    // {
    //     key: "receiptDtlId",
    //     label: i18n.t("wmsinreceiptrecordhis.receiptDtlId")
    // },
    // {
    //     key: "receiptNo",
    //     label: i18n.t("wmsinreceiptrecordhis.receiptNo")
    // },
    {
        key: "recordQty",
        label: i18n.t("wmsinreceiptrecordhis.recordQty")
    },
    {
        key: "regionNo",
        label: i18n.t("wmsinreceiptrecordhis.regionNo")
    },
    // {
    //     key: "replenishFlag",
    //     label: i18n.t("wmsinreceiptrecordhis.replenishFlag")
    // },
    // {
    //     key: "returnFlag",
    //     label: i18n.t("wmsinreceiptrecordhis.returnFlag")
    // },
    // {
    //     key: "returnResult",
    //     label: i18n.t("wmsinreceiptrecordhis.returnResult")
    // },
    // {
    //     key: "returnTime",
    //     label: i18n.t("wmsinreceiptrecordhis.returnTime")
    // },
    // {
    //     key: "skuCode",
    //     label: i18n.t("wmsinreceiptrecordhis.skuCode")
    // },
    // {
    //     key: "sourceBy",
    //     label: i18n.t("wmsinreceiptrecordhis.sourceBy")
    // },
    // {
    //     key: "stockCode",
    //     label: i18n.t("wmsinreceiptrecordhis.stockCode")
    // },
    // {
    //     key: "stockDtlId",
    //     label: i18n.t("wmsinreceiptrecordhis.stockDtlId")
    // },
    // {
    //     key: "supplierCode",
    //     label: i18n.t("wmsinreceiptrecordhis.supplierCode")
    // },
    // {
    //     key: "supplierName",
    //     label: i18n.t("wmsinreceiptrecordhis.supplierName")
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsinreceiptrecordhis.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsinreceiptrecordhis.supplierNameEn")
    // },
    // {
    //     key: "ticketNo",
    //     label: i18n.t("wmsinreceiptrecordhis.ticketNo")
    // },
    // {
    //     key: "urgentFlag",
    //     label: i18n.t("wmsinreceiptrecordhis.urgentFlag")
    // },
    {
        key: "unitCode",
        label: i18n.t("wmsinreceiptrecordhis.unitCode")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsinreceiptrecordhis.whouseNo")
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


