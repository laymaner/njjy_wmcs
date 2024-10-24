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
        label: "分配操作人"
    },
    {
        key: "allotTime",
        label: "分配开始时间"
    },
    {
        key: "deliveryLocNo",
        label: "deliveryLocNo"
    },
    {
        key: "docTypeCode",
        label: "单据类型"
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
        label: "波次号"
    },
    {
        key: "waveStatus",
        label: "波次单状态"
    },
    {
        key: "waveType",
        label: "波次类型"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


