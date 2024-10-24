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
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "loadedType",
        label: "loadedType"
    },
    {
        key: "orderNo",
        label: "orderNo"
    },
    {
        key: "palletBarcode",
        label: "palletBarcode"
    },
    {
        key: "pickupMethod",
        label: "pickupMethod"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "putdownNo",
        label: "putdownNo"
    },
    {
        key: "putdownStatus",
        label: "putdownStatus"
    },
    {
        key: "regionNo",
        label: "regionNo"
    },
    {
        key: "stockCode",
        label: "stockCode"
    },
    {
        key: "targetLocNo",
        label: "targetLocNo"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


