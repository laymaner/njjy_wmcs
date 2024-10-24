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
        label: "cvType"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
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
        label: "externalInNo"
    },
    {
        key: "inNo",
        label: "inNo"
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
        key: "operationReason",
        label: "operationReason"
    },
    {
        key: "orderDesc",
        label: "orderDesc"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "receipter",
        label: "receipter"
    },
    {
        key: "receiptNo",
        label: "receiptNo"
    },
    {
        key: "receiptStatus",
        label: "receiptStatus"
    },
    {
        key: "receiptTime",
        label: "receiptTime"
    },
    {
        key: "regionNo",
        label: "regionNo"
    },
    {
        key: "sourceBy",
        label: "sourceBy"
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


