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
        key: "blindFlag",
        label: "blindFlag"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "erpWhouseNo",
        label: "erpWhouseNo"
    },
    {
        key: "inventoryLocNo",
        label: "inventoryLocNo"
    },
    {
        key: "inventoryNo",
        label: "inventoryNo"
    },
    {
        key: "inventoryStatus",
        label: "inventoryStatus"
    },
    {
        key: "issuedFlag",
        label: "issuedFlag"
    },
    {
        key: "issuedOperator",
        label: "issuedOperator"
    },
    {
        key: "issuedTime",
        label: "issuedTime"
    },
    {
        key: "operationReason",
        label: "operationReason"
    },
    {
        key: "orderDesc",
        label: "orderDesc"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


