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
        key: "materialTypeDesc",
        label: "materialTypeDesc"
    },
    {
        key: "materialTypeName",
        label: "materialTypeName"
    },
    {
        key: "materialTypeNameAlias",
        label: "materialTypeNameAlias"
    },
    {
        key: "materialTypeNameEn",
        label: "materialTypeNameEn"
    },
    {
        key: "materialTypeCode",
        label: "materialTypeCode"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "virtualFlag",
        label: "virtualFlag"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


