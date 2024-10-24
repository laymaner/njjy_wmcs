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
        label: i18n.t("wmsstockhis.areaNo")
    },
    {
        key: "binNo",
        label: i18n.t("wmsstockhis.binNo")
    },
    {
        key: "errFlag",
        label: i18n.t("wmsstockhis.errFlag")
    },
    {
        key: "errMsg",
        label: i18n.t("wmsstockhis.errMsg")
    },
    {
        key: "height",
        label: i18n.t("wmsstockhis.height")
    },
    {
        key: "invoiceNo",
        label: i18n.t("wmsstockhis.invoiceNo")
    },
    {
        key: "loadedType",
        label: i18n.t("wmsstockhis.loadedType")
    },
    {
        key: "locAllotGroup",
        label: i18n.t("wmsstockhis.locAllotGroup")
    },
    {
        key: "locNo",
        label: i18n.t("wmsstockhis.locNo")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsstockhis.palletBarcode")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsstockhis.proprietorCode")
    },
    {
        key: "regionNo",
        label: i18n.t("wmsstockhis.regionNo")
    },
    {
        key: "roadwayNo",
        label: i18n.t("wmsstockhis.roadwayNo")
    },
    {
        key: "specialFlag",
        label: i18n.t("wmsstockhis.specialFlag")
    },
    {
        key: "specialMsg",
        label: i18n.t("wmsstockhis.specialMsg")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstockhis.stockCode")
    },
    {
        key: "stockStatus",
        label: i18n.t("wmsstockhis.stockStatus")
    },
    {
        key: "weight",
        label: i18n.t("wmsstockhis.weight")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsstockhis.whouseNo")
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


