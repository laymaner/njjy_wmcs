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
        key: "cvCode",
        label: "cvCode"
    },
    {
        key: "cvName",
        label: "客供名称"
    },
    {
        key: "cvNameAlias",
        label: "cvNameAlias"
    },
    {
        key: "cvNameEn",
        label: "cvNameEn"
    },
    {
        key: "cvType",
        label: "cvType"
    },
    {
        key: "docTypeCode",
        label: "单据类型"
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
        key: "externalOutDtlId",
        label: "externalOutDtlId"
    },
    {
        key: "externalOutNo",
        label: "外部出库单号"
    },
    {
        key: "fpName",
        label: "成品名称"
    },
    {
        key: "fpNo",
        label: "成品编码"
    },
    {
        key: "fpQty",
        label: "成品数量"
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
        key: "invoiceNo",
        label: "发货单号"
    },
    {
        key: "invoiceStatus",
        label: "发货单状态"
    },
    {
        key: "operationReason",
        label: "operationReason"
    },
    {
        key: "orderDesc",
        label: "orderDesc"
    },
    {
        key: "orderPriority",
        label: "单据优先级"
    },
    {
        key: "productLocation",
        label: "生产地点"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "sourceBy",
        label: "数据来源"
    },
    {
        key: "ticketNo",
        label: "工单号"
    },
    {
        key: "ticketType",
        label: "ticketType"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "projectNo",
        label: "项目号"
    },
    {
        key: "waveNo",
        label: "waveNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


