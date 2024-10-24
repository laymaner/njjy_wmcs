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
        label: "楼号"
    },
    {
        key: "badDescription",
        label: "不良说明"
    },
    {
        key: "badOptions",
        label: "不良选项"
    },
    {
        key: "badSolveType",
        label: "不良处理方式"
    },
    {
        key: "batchNo",
        label: "批次"
    },
    {
        key: "binNo",
        label: "库位编码"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "erpWhouseNo",
        label: "ERP仓库"
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
        key: "inOutName",
        label: "inOutName"
    },
    {
        key: "inOutTypeNo",
        label: "inOutTypeNo"
    },
    {
        key: "inspector",
        label: "质检员"
    },
    {
        key: "iqcRecordNo",
        label: "WMS检验记录单号"
    },
    {
        key: "iqcRecordStatus",
        label: "检验结果状态"
    },
    {
        key: "iqcType",
        label: "质检方式"
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
        key: "minPkgQty",
        label: "包装数量"
    },
    {
        key: "orderDtlId",
        label: "orderDtlId"
    },
    {
        key: "orderNo",
        label: "关联单号"
    },
    {
        key: "orderDesc",
        label: "备注说明"
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
        key: "qualifiedQty",
        label: "检验合格数量"
    },
    {
        key: "erpQualifiedSpecialQty",
        label: "ERP特采数量"
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
        key: "receiptQty",
        label: "收货完成数量"
    },
    {
        key: "regionNo",
        label: "库区编码"
    },
    {
        key: "sourceBy",
        label: "sourceBy"
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
        key: "wmsUnqualifiedQty",
        label: "检验不合格数量"
    },
    {
        key: "erpUnqualifiedQty",
        label: "ERP不合格数量"
    },
    {
        key: "urgentFlag",
        label: "急料标记"
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


