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
        key: "allotFlag",
        label: "allotFlag"
    },
    {
        key: "allotOperator",
        label: "allotOperator"
    },
    {
        key: "allotTime",
        label: "allotTime"
    },
    {
        key: "deliveryLocNo",
        label: "deliveryLocNo"
    },
    {
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "issuedFlag",
        label: "issuedFlag"
    },
    {
        key: "issuedOperator",
        label: "issuedOperator"
    },
    {
        key: "issuedResult",
        label: "issuedResult"
    },
    {
        key: "issuedTime",
        label: "issuedTime"
    },
    {
        key: "operationReason",
        label: "operationReason"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "waveNo",
        label: "waveNo"
    },
    {
        key: "waveStatus",
        label: "waveStatus"
    },
    {
        key: "waveType",
        label: "waveType"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


