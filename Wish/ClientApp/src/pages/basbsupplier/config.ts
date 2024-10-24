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
        key: "address",
        label: "address"
    },
    {
        key: "contacts",
        label: "contacts"
    },
    {
        key: "description",
        label: "description"
    },
    {
        key: "fax",
        label: "fax"
    },
    {
        key: "mail",
        label: "mail"
    },
    {
        key: "mobile",
        label: "mobile"
    },
    {
        key: "phone",
        label: "phone"
    },
    {
        key: "proprietorCode",
        label: "proprietorCode"
    },
    {
        key: "suppilerFullname",
        label: "suppilerFullname"
    },
    {
        key: "suppilerFullnameAlias",
        label: "suppilerFullnameAlias"
    },
    {
        key: "suppilerFullnameEn",
        label: "suppilerFullnameEn"
    },
    {
        key: "supplierName",
        label: "supplierName"
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
        label: "supplierCode"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
    {
        key: "zip",
        label: "zip"
    },
    {
        key: "companyCode",
        label: "companyCode"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


