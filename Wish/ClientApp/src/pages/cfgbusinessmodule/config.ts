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
        key: "businessModuleDesc",
        label: "businessModuleDesc"
    },
    {
        key: "businessModuleName",
        label: "businessModuleName"
    },
    {
        key: "businessModuleNameAlias",
        label: "businessModuleNameAlias"
    },
    {
        key: "businessModuleNameEn",
        label: "businessModuleNameEn"
    },
    {
        key: "businessModuleSort",
        label: "businessModuleSort"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


