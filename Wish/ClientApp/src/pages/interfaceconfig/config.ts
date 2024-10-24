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
        key: "interfaceCode",
        label: i18n.t("interfaceconfig.interfaceCode"),
        width: 150
    },
    {
        key: "interfaceName",
        label: i18n.t("interfaceconfig.interfaceName"),
        width: 150
    },
    {
        key: "interfaceUrl",
        label: i18n.t("interfaceconfig.interfaceUrl"),
        width: 500
    },
    {
        key: "retryMaxTimes",
        label: i18n.t("interfaceconfig.retryMaxTimes"),
        width: 150
    },
    {
        key: "retryInterval",
        label: i18n.t("interfaceconfig.retryInterval"),
        width: 150
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


