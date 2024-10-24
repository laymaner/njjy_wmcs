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
        key: "barcodeFlag",
        label: "barcodeFlag"
    },
    {
        key: "checkFormula",
        label: "checkFormula"
    },
    {
        key: "checkPalletFlag",
        label: "checkPalletFlag"
    },
    {
        key: "chekDesc",
        label: "chekDesc"
    },
    {
        key: "developFlag",
        label: "developFlag"
    },
    {
        key: "emptyMaxQty",
        label: "emptyMaxQty"
    },
    {
        key: "maxWeight",
        label: "maxWeight"
    },
    {
        key: "palletHeight",
        label: "palletHeight"
    },
    {
        key: "palletLength",
        label: "palletLength"
    },
    {
        key: "palletTypeCode",
        label: "palletTypeCode"
    },
    {
        key: "palletTypeFlag",
        label: "palletTypeFlag"
    },
    {
        key: "palletTypeName",
        label: "palletTypeName"
    },
    {
        key: "palletTypeNameAlias",
        label: "palletTypeNameAlias"
    },
    {
        key: "palletTypeNameEn",
        label: "palletTypeNameEn"
    },
    {
        key: "palletWeight",
        label: "palletWeight"
    },
    {
        key: "palletWidth",
        label: "palletWidth"
    },
    {
        key: "positionCol",
        label: "positionCol"
    },
    {
        key: "positionDirect",
        label: "positionDirect"
    },
    {
        key: "positionFlag",
        label: "positionFlag"
    },
    {
        key: "positionRow",
        label: "positionRow"
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


