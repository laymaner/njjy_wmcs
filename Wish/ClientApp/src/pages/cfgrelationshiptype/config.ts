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
        key: "leftAttributes",
        label: "leftAttributes"
    },
    {
        key: "leftTable",
        label: "leftTable"
    },
    {
        key: "leftTableCode",
        label: "leftTableCode"
    },
    {
        key: "leftTableCodeLabel",
        label: "leftTableCodeLabel"
    },
    {
        key: "leftTableCodeLabelAlias",
        label: "leftTableCodeLabelAlias"
    },
    {
        key: "leftTableCodeLabelEn",
        label: "leftTableCodeLabelEn"
    },
    {
        key: "leftTableName",
        label: "leftTableName"
    },
    {
        key: "leftTableNameAlias",
        label: "leftTableNameAlias"
    },
    {
        key: "leftTableNameEn",
        label: "leftTableNameEn"
    },
    {
        key: "leftTableNameLabel",
        label: "leftTableNameLabel"
    },
    {
        key: "leftTableNameLabelAlias",
        label: "leftTableNameLabelAlias"
    },
    {
        key: "leftTableNameLabelEn",
        label: "leftTableNameLabelEn"
    },
    {
        key: "leftWhouse",
        label: "leftWhouse"
    },
    {
        key: "relationshipTypeCode",
        label: "relationshipTypeCode"
    },
    {
        key: "relationshipTypeDesc",
        label: "relationshipTypeDesc"
    },
    {
        key: "relationshipTypeName",
        label: "relationshipTypeName"
    },
    {
        key: "relationshipTypeNameAlias",
        label: "relationshipTypeNameAlias"
    },
    {
        key: "relationshipTypeNameEn",
        label: "relationshipTypeNameEn"
    },
    {
        key: "rightAttributes",
        label: "rightAttributes"
    },
    {
        key: "rightTable",
        label: "rightTable"
    },
    {
        key: "rightTableCode",
        label: "rightTableCode"
    },
    {
        key: "rightTableCodeLabel",
        label: "rightTableCodeLabel"
    },
    {
        key: "rightTableCodeLabelAlias",
        label: "rightTableCodeLabelAlias"
    },
    {
        key: "rightTableCodeLabelEn",
        label: "rightTableCodeLabelEn"
    },
    {
        key: "rightTableName",
        label: "rightTableName"
    },
    {
        key: "rightTableNameAlias",
        label: "rightTableNameAlias"
    },
    {
        key: "rightTableNameEn",
        label: "rightTableNameEn"
    },
    {
        key: "rightTableNameLabel",
        label: "rightTableNameLabel"
    },
    {
        key: "rightTableNameLabelAlias",
        label: "rightTableNameLabelAlias"
    },
    {
        key: "rightTableNameLabelEn",
        label: "rightTableNameLabelEn"
    },
    {
        key: "rightTablePriority",
        label: "rightTablePriority"
    },
    {
        key: "rightWhouse",
        label: "rightWhouse"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


