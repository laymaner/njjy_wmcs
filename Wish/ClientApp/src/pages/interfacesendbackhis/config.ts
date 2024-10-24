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
        label: i18n.t("interfacesendbackhis.interfaceCode"),
        width: 120
    },
    {
        key: "interfaceName",
        label: i18n.t("interfacesendbackhis.interfaceName"),
        width: 120
    },
    {
        key: "interfaceSendInfo",
        label: i18n.t("interfacesendbackhis.interfaceSendInfo"),
        width: 390
    },
    {
        key: "interfaceResult",
        label: i18n.t("interfacesendbackhis.interfaceResult"),
        width: 550
    },
    // {
    //     key: "returnFlag",
    //     label: i18n.t("interfacesendbackhis.returnFlag")
    // },
    {
        key: "returnFlagDesc",
        label: i18n.t("interfacesendbackhis.returnFlag"),
        width: 120
    },
    {
        key: "returnTimes",
        label: i18n.t("interfacesendbackhis.returnTimes"),
        width: 120
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


