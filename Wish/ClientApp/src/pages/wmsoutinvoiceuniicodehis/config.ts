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
        key: "allotQty",
        label: "分配数量"
    },
    {
        key: "areaNo",
        label: "楼号"
    },
    {
        key: "batchNo",
        label: "批次号"
    },
    {
        key: "dataCode",
        label: "DC"
    },
    {
        key: "delayReason",
        label: "延期原因"
    },
    {
        key: "delayTimes",
        label: "延期次数"
    },
    {
        key: "delayToEndDate",
        label: "delayToEndDate"
    },
    {
        key: "driedScrapFlag",
        label: "是否烘干报废"
    },
    {
        key: "driedTimes",
        label: "已烘干次数"
    },
    {
        key: "erpBinNo",
        label: "erpBinNo"
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
        key: "inspectionResult",
        label: "质检结果"
    },
    {
        key: "invoiceDtlId",
        label: "invoiceDtlId"
    },
    {
        key: "invoiceNo",
        label: "发货单号"
    },
    {
        key: "invoiceRecordId",
        label: "invoiceRecordId"
    },
    {
        key: "leftMslTimes",
        label: "剩余湿敏时长"
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
        key: "packageTime",
        label: "封包时间"
    },
    {
        key: "palletBarcode",
        label: "载体条码"
    },
    {
        key: "pickQty",
        label: "拣选数量"
    },
    {
        key: "pickTaskNo",
        label: "拣选任务编号"
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
        key: "realExposeTimes",
        label: "实际暴露时长"
    },
    {
        key: "skuCode",
        label: "skuCode"
    },
    {
        key: "stockCode",
        label: "stockCode"
    },
    {
        key: "stockDtlId",
        label: "stockDtlId"
    },
    {
        key: "supplierCode",
        label: "供应商编码"
    },
    {
        key: "supplierExposeTimes",
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
        key: "uniicode",
        label: "包装条码/SN码"
    },
    {
        key: "unpackTime",
        label: "开封时间"
    },
    {
        key: "waveNo",
        label: "波次号"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "outBarcode",
        label: "出库条码"
    },
    {
        key: "ouniiStatus",
        label: "ouniiStatus"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


