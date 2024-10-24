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
        key: "docTypeCode",
        label: "docTypeCode"
    },
    {
        key: "moveNo",
        label: "moveNo"
    },
    {
        key: "moveStatus",
        label: "moveStatus"
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
        key: "putdownLocNo",
        label: "putdownLocNo"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


