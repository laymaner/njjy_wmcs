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
        key: "regionTypeCode",
        label: "regionTypeCode"
    },
    {
        key: "regionTypeFlag",
        label: "regionTypeFlag"
    },
    {
        key: "regionTypeName",
        label: "regionTypeName"
    },
    {
        key: "regionTypeNameAlias",
        label: "regionTypeNameAlias"
    },
    {
        key: "regionTypeNameEn",
        label: "regionTypeNameEn"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


