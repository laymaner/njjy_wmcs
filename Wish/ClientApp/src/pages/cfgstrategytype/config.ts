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
        key: "strategyTypeCategory",
        label: "strategyTypeCategory"
    },
    {
        key: "strategyTypeCode",
        label: "strategyTypeCode"
    },
    {
        key: "strategyTypeDesription",
        label: "strategyTypeDesription"
    },
    {
        key: "strategyTypeName",
        label: "strategyTypeName"
    },
    {
        key: "strategyTypeNameAlias",
        label: "strategyTypeNameAlias"
    },
    {
        key: "strategyTypeNameEn",
        label: "strategyTypeNameEn"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


