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
        key: "Block_Code",
        label: i18n.t("dbconfig.Block_Code")
    },
    {
        key: "Block_Name",
        label: i18n.t("dbconfig.Block_Name")
    },
    {
        key: "Block_Offset",
        label: i18n.t("dbconfig.Block_Offset")
    },
    {
        key: "Block_Length",
        label: i18n.t("dbconfig.Block_Length")
    },
    {
        key: "Plc_Name_view",
        label: i18n.t("dbconfig.Plc_Name_view")
    },
    // {
    //     key: "Describe",
    //     label: i18n.t("dbconfig.Describe")
    // },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


