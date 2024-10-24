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
        key: "barcodeFlag",
        label: "码级管理"
    },
    {
        key: "batchNo",
        label: "批次"
    },
    {
        key: "confirmQty",
        label: "CONFIRM_QTY"
    },
    {
        key: "curLocationNo",
        label: "curLocationNo"
    },
    {
        key: "curLocationType",
        label: "curLocationType"
    },
    {
        key: "curPalletbarCode",
        label: "curPalletbarCode"
    },
    {
        key: "curStockCode",
        label: "当前库存编码"
    },
    {
        key: "curStockDtlId",
        label: "当前库存明细ID"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "frLocationNo",
        label: "来源站台/库位号"
    },
    {
        key: "frLocationType",
        label: "来源位置类型"
    },
    {
        key: "frPalletBarcode",
        label: "来源托盘条码"
    },
    {
        key: "frRegionNo",
        label: "frRegionNo"
    },
    {
        key: "frStockCode",
        label: "frStockCode"
    },
    {
        key: "frStockDtlId",
        label: "frStockDtlId"
    },
    {
        key: "inspectionResult",
        label: "inspectionResult"
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
        key: "moveNo",
        label: "moveNo"
    },
    {
        key: "moveDtlId",
        label: "moveDtlId"
    },
    {
        key: "moveQty",
        label: "moveQty"
    },
    {
        key: "moveRecordStatus",
        label: "moveRecordStatus"
    },
    {
        key: "pickMethod",
        label: "pickMethod"
    },
    {
        key: "pickType",
        label: "pickType"
    },
    {
        key: "productDate",
        label: "productDate"
    },
    {
        key: "expDate",
        label: "expDate"
    },
    {
        key: "putDownLocNo",
        label: "putDownLocNo"
    },
    {
        key: "skuCode",
        label: "skuCode"
    },
    {
        key: "stockQty",
        label: "stockQty"
    },
    {
        key: "supplierBatchNo",
        label: "supplierBatchNo"
    },
    {
        key: "supplierCode",
        label: "supplierCode"
    },
    {
        key: "supplierName",
        label: "供应商名称"
    },
    {
        key: "supplierNameEn",
        label: "供应商名称-EN"
    },
    {
        key: "supplierNameAlias",
        label: "supplierNameAlias"
    },
    {
        key: "supplierType",
        label: "供货方类型：供应商、产线"
    },
    {
        key: "toLocationNo",
        label: "目标库位/站台号"
    },
    {
        key: "toLocationType",
        label: "toLocationType"
    },
    {
        key: "toPalletBarcode",
        label: "toPalletBarcode"
    },
    {
        key: "toRegionNo",
        label: "目标库区"
    },
    {
        key: "toStockCode",
        label: "目标库存编码"
    },
    {
        key: "toStockDtlId",
        label: "目标库存明细ID"
    },
    {
        key: "unitCode",
        label: "unitCode"
    },
    {
        key: "proprietorCode",
        label: "货主"
    },
    {
        key: "whouseNo",
        label: "仓库号"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


