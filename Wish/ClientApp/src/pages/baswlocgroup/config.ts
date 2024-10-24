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
        key: "locGroupName",
        label: "locGroupName"
    },
    {
        key: "locGroupNameAlias",
        label: "locGroupNameAlias"
    },
    {
        key: "locGroupNameEn",
        label: "locGroupNameEn"
    },
    {
        key: "locGroupNo",
        label: "locGroupNo"
    },
    {
        key: "locGroupType",
        label: "locGroupType"
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


