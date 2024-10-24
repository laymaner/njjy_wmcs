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
        key: "areaNo",
        label: i18n.t("wmsstock.areaNo")
    },
    {
        key: "binNo",
        label: i18n.t("wmsstock.binNo")
    },
    {
        key: "errFlag",
        label: i18n.t("wmsstock.errFlag")
    },
    {
        key: "errMsg",
        label: i18n.t("wmsstock.errMsg")
    },
    {
        key: "height",
        label: i18n.t("wmsstock.height")
    },
    {
        key: "invoiceNo",
        label: i18n.t("wmsstock.invoiceNo")
    },
    {
        key: "loadedType",
        label: i18n.t("wmsstock.loadedType")
    },
    {
        key: "locAllotGroup",
        label: i18n.t("wmsstock.locAllotGroup")
    },
    {
        key: "locNo",
        label: i18n.t("wmsstock.locNo")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsstock.palletBarcode")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsstock.proprietorCode")
    },
    {
        key: "regionNo",
        label: i18n.t("wmsstock.regionNo")
    },
    {
        key: "roadwayNo",
        label: i18n.t("wmsstock.roadwayNo")
    },
    {
        key: "specialFlag",
        label: i18n.t("wmsstock.specialFlag")
    },
    {
        key: "specialMsg",
        label: i18n.t("wmsstock.specialMsg")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstock.stockCode")
    },
    {
        key: "stockStatus",
        label: i18n.t("wmsstock.stockStatus")
    },
    {
        key: "weight",
        label: i18n.t("wmsstock.weight")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsstock.whouseNo")
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


