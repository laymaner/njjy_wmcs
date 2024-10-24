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
        key: "developFlag",
        label: "developFlag"
    },
    {
        key: "parCode",
        label: "parCode"
    },
    {
        key: "parDesc",
        label: "parDesc"
    },
    {
        key: "parDescAlias",
        label: "parDescAlias"
    },
    {
        key: "parDescEn",
        label: "parDescEn"
    },
    {
        key: "parValue",
        label: "parValue"
    },
    {
        key: "parValueType",
        label: "parValueType"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


