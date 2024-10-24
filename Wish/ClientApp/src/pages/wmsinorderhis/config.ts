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
        label: "区域编码"
    },
    {
        key: "cvCode",
        label: "cvCode"
    },
    {
        key: "cvName",
        label: "cvName"
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
        label: "客供类型"
    },
    {
        key: "deliverMode",
        label: "deliverMode"
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
        key: "externalInId",
        label: "externalInId"
    },
    {
        key: "externalInNo",
        label: "外部入库单号"
    },
    {
        key: "inNo",
        label: "入库单号"
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
        key: "inStatus",
        label: "入库单状态"
    },
    {
        key: "operationReason",
        label: "操作原因"
    },
    {
        key: "orderDesc",
        label: "备注说明"
    },
    {
        key: "planArrivalDate",
        label: "planArrivalDate"
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
        label: "ticketNo"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


