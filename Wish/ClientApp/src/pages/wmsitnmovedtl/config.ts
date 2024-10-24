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
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "areaNo",
        label: "areaNo"
    },
    {
        key: "moveNo",
        label: "moveNo"
    },
    {
        key: "regionNo",
        label: "regionNo"
    },
    {
        key: "roadwayNo",
        label: "roadwayNo"
    },
    {
        key: "binNo",
        label: "binNo"
    },
    {
        key: "stockCode",
        label: "stockCode"
    },
    {
        key: "palletBarcode",
        label: "palletBarcode"
    },
    {
        key: "moveDtlStatus",
        label: "moveDtlStatus"
    },
    {
        key: "loadedType",
        label: "loadedType"
    },
    {
        key: "moveQty",
        label: "moveQty"
    },
    {
        key: "createBy",
        label: "createBy"
    },
    {
        key: "createTime",
        label: "createTime"
    },
    {
        key: "updateBy",
        label: "updateBy"
    },
    {
        key: "updateTime",
        label: "updateTime"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


