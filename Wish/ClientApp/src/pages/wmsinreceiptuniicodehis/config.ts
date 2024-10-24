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
        label: "批次"
    },
    {
        key: "curPalletBarcode",
        label: "当前托盘号"
    },
    {
        key: "curPositionNo",
        label: "当前位置编号"
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
        key: "dataCode",
        label: "DC"
    },
    {
        key: "erpWhouseNo",
        label: "ERP仓库"
    },
    {
        key: "expDate",
        label: "失效日期"
    },
    {
        key: "extend1",
        label: "extend1"
    },
    {
        key: "extend10",
        label: "extend10"
    },
    {
        key: "extend11",
        label: "extend11"
    },
    {
        key: "extend12",
        label: "extend12"
    },
    {
        key: "extend13",
        label: "extend13"
    },
    {
        key: "extend14",
        label: "extend14"
    },
    {
        key: "extend15",
        label: "extend15"
    },
    {
        key: "extend2",
        label: "extend2"
    },
    {
        key: "extend3",
        label: "extend3"
    },
    {
        key: "extend4",
        label: "extend4"
    },
    {
        key: "extend5",
        label: "extend5"
    },
    {
        key: "extend6",
        label: "extend6"
    },
    {
        key: "extend7",
        label: "extend7"
    },
    {
        key: "extend8",
        label: "extend8"
    },
    {
        key: "extend9",
        label: "extend9"
    },
    {
        key: "externalInDtlId",
        label: "外部入库单行号"
    },
    {
        key: "externalInNo",
        label: "外部入库单号"
    },
    {
        key: "inDtlId",
        label: "inDtlId"
    },
    {
        key: "inNo",
        label: "WMS入库单号"
    },
    {
        key: "iqcResultNo",
        label: "WMS检验单号"
    },
    {
        key: "materialName",
        label: "物料名称"
    },
    {
        key: "materialCode",
        label: "物料编码"
    },
    {
        key: "materialSpec",
        label: "规格"
    },
    {
        key: "mslGradeCode",
        label: "MSL等级"
    },
    {
        key: "productDate",
        label: "生产日期"
    },
    {
        key: "projectNo",
        label: "项目号"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "receiptDtlId",
        label: "receiptDtlId"
    },
    {
        key: "receiptNo",
        label: "WMS收货单号"
    },
    {
        key: "qty",
        label: "数量"
    },
    {
        key: "recordQty",
        label: "实际上架数量"
    },
    {
        key: "receiptRecordId",
        label: "receiptRecordId"
    },
    {
        key: "supplierExposeTimeDuration",
        label: "供应商暴露时长"
    },
    {
        key: "supplierName",
        label: "供应商名称"
    },
    {
        key: "supplierNameAlias",
        label: "supplierNameAlias"
    },
    {
        key: "supplierNameEn",
        label: "supplierNameEn"
    },
    {
        key: "supplierCode",
        label: "供应商编码"
    },
    {
        key: "skuCode",
        label: "SKU"
    },
    {
        key: "uniicode",
        label: "包装条码/SN号"
    },
    {
        key: "unitCode",
        label: "单位"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "runiiStatus",
        label: "入库唯一码状态"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


