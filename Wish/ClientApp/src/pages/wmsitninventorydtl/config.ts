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
        label: "批次号"
    },
    {
        key: "confirmQty",
        label: "确认数量"
    },
    {
        key: "difFlag",
        label: "是否差异"
    },
    {
        key: "inspectionResult",
        label: "inspectionResult"
    },
    {
        key: "inventoryDtlStatus",
        label: "盘点明细状态"
    },
    {
        key: "inventoryNo",
        label: "盘点单号"
    },
    {
        key: "inventoryQty",
        label: "盘点数量"
    },
    {
        key: "materialCode",
        label: "物料编码"
    },
    {
        key: "materialName",
        label: "物料名称"
    },
    {
        key: "materialSpec",
        label: "物料规格"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "qty",
        label: "计划数量"
    },
    {
        key: "unitCode",
        label: "单位"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


