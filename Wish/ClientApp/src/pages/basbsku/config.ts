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
        key: "pickRuleNo",
        label: "pickRuleNo"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "skuCode",
        label: "skuCode"
    },
    {
        key: "skuRulesNo",
        label: "skuRulesNo"
    },
    {
        key: "storageRuleNo",
        label: "storageRuleNo"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


