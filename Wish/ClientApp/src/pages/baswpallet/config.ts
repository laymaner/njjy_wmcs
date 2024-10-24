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
        key: "palletBarcode",
        label: "palletBarcode"
    },
    {
        key: "palletDesc",
        label: "palletDesc"
    },
    {
        key: "palletDescAlias",
        label: "palletDescAlias"
    },
    {
        key: "palletDescEn",
        label: "palletDescEn"
    },
    {
        key: "palletTypeCode",
        label: "palletTypeCode"
    },
    {
        key: "printsQty",
        label: "printsQty"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


