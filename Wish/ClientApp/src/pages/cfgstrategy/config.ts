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
        key: "strategyDesc",
        label: "strategyDesc"
    },
    {
        key: "strategyName",
        label: "strategyName"
    },
    {
        key: "strategyNameAlias",
        label: "strategyNameAlias"
    },
    {
        key: "strategyNameEn",
        label: "strategyNameEn"
    },
    {
        key: "strategyNo",
        label: "strategyNo"
    },
    {
        key: "strategyTypeCode",
        label: "strategyTypeCode"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


