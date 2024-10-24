﻿import i18n from "@/lang";

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
        label: i18n.t("devicestatuslog.Device_Code"),
        width: 100
    },
    {
        key: "Attribute",
        label: i18n.t("devicestatuslog.Attribute"),
        width: 100
    },
    {
        key: "Content",
        label: i18n.t("devicestatuslog.Content"),
        width: 150
    },
    {
        key: "Message",
        label: i18n.t("devicestatuslog.Message")
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


