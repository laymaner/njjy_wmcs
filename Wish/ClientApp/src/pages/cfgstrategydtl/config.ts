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
        key: "strategyItemIdx",
        label: "strategyItemIdx"
    },
    {
        key: "strategyItemNo",
        label: "strategyItemNo"
    },
    {
        key: "strategyItemValue1",
        label: "strategyItemValue1"
    },
    {
        key: "strategyItemValue2",
        label: "strategyItemValue2"
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


