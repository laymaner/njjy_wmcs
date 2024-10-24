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
        key: "adjustDesc",
        label: i18n.t("wmsstockadjust.adjustDesc")
    },
    {
        key: "adjustOperate",
        label: i18n.t("wmsstockadjust.adjustOperate")
    },
    {
        key: "adjustType",
        label: i18n.t("wmsstockadjust.adjustType")
    },
    {
        key: "packageBarcode",
        label: i18n.t("wmsstockadjust.packageBarcode")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsstockadjust.palletBarcode")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsstockadjust.proprietorCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstockadjust.stockCode")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsstockadjust.whouseNo")
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


