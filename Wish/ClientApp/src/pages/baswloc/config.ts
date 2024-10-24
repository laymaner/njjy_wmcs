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
        key: "locGroupNo",
        label: i18n.t("baswloc.locGroupNo")
    },
    {
        key: "locName",
        label:  i18n.t("baswloc.locName")
    },
    // {
    //     key: "locNameAlias",
    //     label:  i18n.t("baswloc.locNameAlias")
    // },
    // {
    //     key: "locNameEn",
    //     label:  i18n.t("baswloc.locNameEn")
    // },
    {
        key: "locNo",
        label:  i18n.t("baswloc.locNo")
    },
    {
        key: "locTypeCode",
        label:  i18n.t("baswloc.locTypeCode")
    },
    {
        key: "usedFlag",
        label:  i18n.t("baswloc.usedFlag")
    },
    {
        key: "whouseNo",
        label: i18n.t("baswloc.whouseNo")
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


