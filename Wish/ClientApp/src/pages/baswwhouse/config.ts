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
        key: "contacts",
        label: i18n.t("baswwhouse.contacts")
    },
    {
        key: "maxTaskQty",
        label: i18n.t("baswwhouse.maxTaskQty")
    },
    {
        key: "telephone",
        label: i18n.t("baswwhouse.telephone")
    },
    {
        key: "usedFlag",
        label: i18n.t("baswwhouse.usedFlag")
    },
    {
        key: "whouseAddress",
        label: i18n.t("baswwhouse.whouseAddress")
    },
    {
        key: "whouseName",
        label: i18n.t("baswwhouse.whouseName")
    },
    // {
    //     key: "whouseNameAlias",
    //     label: i18n.t("baswwhouse.whouseNameAlias")
    // },
    // {
    //     key: "whouseNameEn",
    //     label: i18n.t("baswwhouse.whouseNameEn")
    // },
    {
        key: "whouseNo",
        label: i18n.t("baswwhouse.whouseNo")
    },
    {
        key: "whouseType",
        label: i18n.t("baswwhouse.whouseType")
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


