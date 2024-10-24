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
        key: "dictionaryCode",
        label: "dictionaryCode"
    },
    {
        key: "dictionaryItemCode",
        label: "dictionaryItemCode"
    },
    {
        key: "dictionaryItemName",
        label: "dictionaryItemName"
    },
    {
        key: "dictionaryItemNameAlias",
        label: "dictionaryItemNameAlias"
    },
    {
        key: "dictionaryItemNameEn",
        label: "dictionaryItemNameEn"
    },
    {
        key: "dictionaryName",
        label: "dictionaryName"
    },
    {
        key: "dictionaryNameAlias",
        label: "dictionaryNameAlias"
    },
    {
        key: "dictionaryNameEn",
        label: "dictionaryNameEn"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


