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
        key: "differQty",
        label: "differQty"
    },
    {
        key: "erpStockNo",
        label: "erpStockNo"
    },
    {
        key: "erpStockQty",
        label: "erpStockQty"
    },
    {
        key: "erpWhouseNo",
        label: "erpWhouseNo"
    },
    {
        key: "materialCategoryCode",
        label: "materialCategoryCode"
    },
    {
        key: "materialCode",
        label: "materialCode"
    },
    {
        key: "materialTypeCode",
        label: "materialTypeCode"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "reconcileNo",
        label: "reconcileNo"
    },
    {
        key: "reconcileOperator",
        label: "reconcileOperator"
    },
    {
        key: "reconcileTime",
        label: "reconcileTime"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "wmsStockQty",
        label: "wmsStockQty"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


