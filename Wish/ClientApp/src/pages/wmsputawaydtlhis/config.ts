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
        key: "erpWhouseNo",
        label: "erpWhouseNo"
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
        label: "inspectionResult"
    },
    {
        key: "materialCode",
        label: "物料编码"
    },
    {
        key: "materialName",
        label: "物料编码"
    },
    {
        key: "materialSpec",
        label: "物料规格"
    },
    {
        key: "orderDtlId",
        label: "orderDtlId"
    },
    {
        key: "orderNo",
        label: "关联单据编号"
    },
    {
        key: "palletBarcode",
        label: "载体条码"
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
        key: "ptaBinNo",
        label: "上架库位"
    },
    {
        key: "putawayDtlStatus",
        label: "状态"
    },
    {
        key: "putawayNo",
        label: "putawayNo"
    },
    {
        key: "recordId",
        label: "recordId"
    },
    {
        key: "recordQty",
        label: "数量"
    },
    {
        key: "regionNo",
        label: "库区编号"
    },
    {
        key: "roadwayNo",
        label: "巷道"
    },
    {
        key: "skuCode",
        label: "SKU编码"
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
        label: "supplierCode"
    },
    {
        key: "supplierName",
        label: "供方名称"
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
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "unitCode",
        label: "unitCode"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


