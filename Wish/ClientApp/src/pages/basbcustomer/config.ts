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
        key: "Address",
        label: "Address"
    },
    {
        key: "Contacts",
        label: "Contacts"
    },
    {
        key: "CustomerFullname",
        label: "CustomerFullname"
    },
    {
        key: "CustomerFullnameAlias",
        label: "CustomerFullnameAlias"
    },
    {
        key: "CustomerFullnameEn",
        label: "CustomerFullnameEn"
    },
    {
        key: "CustomerName",
        label: "CustomerName"
    },
    {
        key: "CustomerNameAlias",
        label: "CustomerNameAlias"
    },
    {
        key: "CustomerNameEn",
        label: "CustomerNameEn"
    },
    {
        key: "CustomerCode",
        label: "CustomerCode"
    },
    {
        key: "Description",
        label: "Description"
    },
    {
        key: "Fax",
        label: "Fax"
    },
    {
        key: "Mail",
        label: "Mail"
    },
    {
        key: "Mobile",
        label: "Mobile"
    },
    {
        key: "Phone",
        label: "Phone"
    },
    {
        key: "ProprietorCode",
        label: "ProprietorCode"
    },
    {
        key: "UsedFlag",
        label: "UsedFlag"
    },
    {
        key: "WhouseNo",
        label: "WhouseNo"
    },
    {
        key: "Zip",
        label: "Zip"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


