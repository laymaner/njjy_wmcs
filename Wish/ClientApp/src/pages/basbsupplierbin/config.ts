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
        key: "areaNo",
        label: "areaNo"
    },
    {
        key: "binNo",
        label: "binNo"
    },
    {
        key: "regionNo",
        label: "regionNo"
    },
    {
        key: "supplierCode",
        label: "supplierCode"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "erpWhouseNo",
        label: "erpWhouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


