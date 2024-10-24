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
        key: "strategyItemDesc",
        label: "strategyItemDesc"
    },
    {
        key: "strategyItemGroupIdx",
        label: "strategyItemGroupIdx"
    },
    {
        key: "strategyItemGroupNo",
        label: "strategyItemGroupNo"
    },
    {
        key: "strategyItemName",
        label: "strategyItemName"
    },
    {
        key: "strategyItemNameAlias",
        label: "strategyItemNameAlias"
    },
    {
        key: "strategyItemNameEn",
        label: "strategyItemNameEn"
    },
    {
        key: "strategyItemNo",
        label: "strategyItemNo"
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


