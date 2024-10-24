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
        key: "erpWhouseNo",
        label: "erpWhouseNo"
    },
    {
        key: "priority",
        label: "priority"
    },
    {
        key: "erpWhouseName",
        label: "erpWhouseName"
    },
    {
        key: "erpWhouseNameEn",
        label: "erpWhouseNameEn"
    },
    {
        key: "erpWhouseNameAlias",
        label: "erpWhouseNameAlias"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


