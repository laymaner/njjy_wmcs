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
        key: "mapConfig",
        label: "mapConfig"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


