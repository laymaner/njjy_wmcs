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
        key: "batchNo",
        label: "batchNo"
    },
    {
        key: "confirmQty",
        label: "confirmQty"
    },
    {
        key: "erpWhouseNo",
        label: "erpWhouseNo"
    },
    {
        key: "inspectionResult",
        label: "inspectionResult"
    },
    {
        key: "itnQcDtlStatus",
        label: "itnQcDtlStatus"
    },
    {
        key: "itnQcNo",
        label: "itnQcNo"
    },
    {
        key: "itnQcQty",
        label: "itnQcQty"
    },
    {
        key: "materialName",
        label: "materialName"
    },
    {
        key: "materialCode",
        label: "materialCode"
    },
    {
        key: "materialSpec",
        label: "materialSpec"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "unitCode",
        label: "unitCode"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


