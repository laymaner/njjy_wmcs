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
        key: "businessModuleCode",
        label: "businessModuleCode"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "paramCode",
        label: "paramCode"
    },
    {
        key: "paramValueCode",
        label: "paramValueCode"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


