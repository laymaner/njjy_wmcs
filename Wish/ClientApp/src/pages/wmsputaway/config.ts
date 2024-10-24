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
        key: "loadedType",
        label: "loadedType"
    },
    {
        key: "manualFlag",
        label: "manualFlag"
    },
    {
        key: "onlineLocNo",
        label: "onlineLocNo"
    },
    {
        key: "onlineMethod",
        label: "onlineMethod"
    },
    {
        key: "palletBarcode",
        label: "palletBarcode"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "ptaRegionNo",
        label: "ptaRegionNo"
    },
    {
        key: "putawayNo",
        label: "putawayNo"
    },
    {
        key: "putawayStatus",
        label: "putawayStatus"
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
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


