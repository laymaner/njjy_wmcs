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
        key: "batchNo",
        label: "批次"
    },
    {
        key: "departmentName",
        label: "部门"
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
        key: "receiptDtlStatus",
        label: "明细状态"
    },
    {
        key: "inNo",
        label: "WMS入库单号"
    },
    {
        key: "inspector",
        label: "质检员"
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
        key: "qualifiedQty",
        label: "检验合格数量"
    },
    {
        key: "qualifiedSpecialQty",
        label: "检验特采数量"
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
        key: "recordQty",
        label: "已组盘数量"
    },
    {
        key: "returnQty",
        label: "直接退货数量"
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
        key: "unqualifiedQty",
        label: "检验不合格数量"
    },
    {
        key: "urgentFlag",
        label: "急料标记"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "unitCode",
        label: "unitCode"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


