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
        key: "proprietorFullname",
        label: "proprietorFullname"
    },
    {
        key: "proprietorFullnameAlias",
        label: "proprietorFullnameAlias"
    },
    {
        key: "proprietorFullnameEn",
        label: "proprietorFullnameEn"
    },
    {
        key: "proprietorName",
        label: "proprietorName"
    },
    {
        key: "proprietorNameAlias",
        label: "proprietorNameAlias"
    },
    {
        key: "proprietorNameEn",
        label: "proprietorNameEn"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "zip",
        label: "zip"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


