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
        key: "businessCode",
        label: "businessCode"
    },
    {
        key: "cvType",
        label: "cvType"
    },
    {
        key: "developFlag",
        label: "developFlag"
    },
    {
        key: "docPriority",
        label: "docPriority"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "docTypeDesc",
        label: "docTypeDesc"
    },
    {
        key: "docTypeName",
        label: "docTypeName"
    },
    {
        key: "docTypeNameAlias",
        label: "docTypeNameAlias"
    },
    {
        key: "docTypeNameEn",
        label: "docTypeNameEn"
    },
    {
        key: "externalDocTypeCode",
        label: "externalDocTypeCode"
    },
    {
        key: "taskMaxQty",
        label: "taskMaxQty"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


