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
        key: "batchNo",
        label: "批次"
    },
    {
        key: "binNo",
        label: "库位号"
    },
    {
        key: "departmentName",
        label: "部门名称"
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
        key: "inspectionResult",
        label: "inspectionResult"
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
        key: "iqcResultNo",
        label: "WMS检验结果单号"
    },
    {
        key: "iqcResultStatus",
        label: "检验结果状态"
    },
    {
        key: "iqcType",
        label: "质检方式"
    },
    {
        key: "isReturnFlag",
        label: "isReturnFlag"
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
        key: "orderDesc",
        label: "orderDesc"
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
        key: "minPkgQty",
        label: "包装数量"
    },
    {
        key: "postBackQty",
        label: "已回传数量"
    },
    {
        key: "productSn",
        label: "成品序列号"
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
        key: "putawayQty",
        label: "上架数量"
    },
    {
        key: "qty",
        label: "数量"
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
        key: "recordQty",
        label: "已组盘数量"
    },
    {
        key: "regionNo",
        label: "库区编号"
    },
    {
        key: "returnQty",
        label: "直接退货数量"
    },
    {
        key: "skuCode",
        label: "skuCode"
    },
    {
        key: "sourceBy",
        label: "数据来源"
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
        key: "ticketNo",
        label: "ticketNo"
    },
    {
        key: "unitCode",
        label: "unitCode"
    },
    {
        key: "urgentFlag",
        label: "急料标记"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


