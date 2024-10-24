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
        key: "batchNo",
        label: "batchNo"
    },
    {
        key: "confirmQty",
        label: "confirmQty"
    },
    {
        key: "difFlag",
        label: "difFlag"
    },
    {
        key: "inspectionResult",
        label: "inspectionResult"
    },
    {
        key: "inventoryDtlStatus",
        label: "inventoryDtlStatus"
    },
    {
        key: "inventoryNo",
        label: "inventoryNo"
    },
    {
        key: "inventoryQty",
        label: "inventoryQty"
    },
    {
        key: "materialCode",
        label: "materialCode"
    },
    {
        key: "materialName",
        label: "materialName"
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
        key: "qty",
        label: "qty"
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


