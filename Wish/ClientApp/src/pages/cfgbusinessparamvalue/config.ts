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
        key: "businessCode",
        label: "businessCode"
    },
    {
        key: "businessModuleCode",
        label: "businessModuleCode"
    },
    {
        key: "defaultFlag",
        label: "defaultFlag"
    },
    {
        key: "paramCode",
        label: "paramCode"
    },
    {
        key: "paramValueCode",
        label: "paramValueCode"
    },
    {
        key: "paramValueDesc",
        label: "paramValueDesc"
    },
    {
        key: "paramValueName",
        label: "paramValueName"
    },
    {
        key: "paramValueNameAlias",
        label: "paramValueNameAlias"
    },
    {
        key: "paramValueNameEn",
        label: "paramValueNameEn"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


