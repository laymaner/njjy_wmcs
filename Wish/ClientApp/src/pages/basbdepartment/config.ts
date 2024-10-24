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
        key: "DepartmentCode",
        label: "DepartmentCode"
    },
    {
        key: "DepartmentName",
        label: "DepartmentName"
    },
    {
        key: "DepartmentNameAlias",
        label: "DepartmentNameAlias"
    },
    {
        key: "DepartmentNameEn",
        label: "DepartmentNameEn"
    },
    {
        key: "UsedFlag",
        label: "UsedFlag"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


