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
        key: "checkFlag",
        label: "checkFlag"
    },
    {
        key: "paramCode",
        label: "paramCode"
    },
    {
        key: "paramDesc",
        label: "paramDesc"
    },
    {
        key: "paramName",
        label: "paramName"
    },
    {
        key: "paramNameAlias",
        label: "paramNameAlias"
    },
    {
        key: "paramNameEn",
        label: "paramNameEn"
    },
    {
        key: "paramSort",
        label: "paramSort"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


