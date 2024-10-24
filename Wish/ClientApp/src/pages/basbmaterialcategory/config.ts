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
        key: "delayDays",
        label: "delayDays"
    },
    {
        key: "materialFlag",
        label: "materialFlag"
    },
    {
        key: "isAutoDelay",
        label: "isAutoDelay"
    },
    {
        key: "materialCategoryDesc",
        label: "materialCategoryDesc"
    },
    {
        key: "materialCategoryName",
        label: "materialCategoryName"
    },
    {
        key: "materialCategoryNameAlias",
        label: "materialCategoryNameAlias"
    },
    {
        key: "materialCategoryNameEn",
        label: "materialCategoryNameEn"
    },
    {
        key: "materialCategoryCode",
        label: "materialCategoryCode"
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


