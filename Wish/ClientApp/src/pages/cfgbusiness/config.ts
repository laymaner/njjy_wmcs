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
        key: "businessDesc",
        label: "businessDesc"
    },
    {
        key: "businessName",
        label: "businessName"
    },
    {
        key: "businessNameAlias",
        label: "businessNameAlias"
    },
    {
        key: "businessNameEn",
        label: "businessNameEn"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


