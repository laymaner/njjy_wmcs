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
        key: "Device_Code",
        label: i18n.t("devicetasklog.Device_Code"),
        width: 100
    },
    {
        key: "Direct",
        label: i18n.t("devicetasklog.Direct"),
        width: 100
    },
    {
        key: "Task_No",
        label: i18n.t("devicetasklog.Task_No"),
        width: 150
    },
    {
        key: "Message",
        label: i18n.t("devicetasklog.Message")
        // ellipsis: true
    },
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
export const DirectTypes: Array<any> = [
    { Text: "Plc2Wcs", Value: "Plc2Wcs" },
    { Text: "Wcs2Plc", Value: "Wcs2Plc" }
];


