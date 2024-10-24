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
        key: "areaNo",
        label: "areaNo"
    },
    {
        key: "erpWhouseName",
        label: "erpWhouseName"
    },
    {
        key: "erpWhouseNameAlias",
        label: "erpWhouseNameAlias"
    },
    {
        key: "erpWhouseNameEn",
        label: "erpWhouseNameEn"
    },
    {
        key: "erpWhouseNo",
        label: "erpWhouseNo"
    },
    {
        key: "erpWhouseType",
        label: "erpWhouseType"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


