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
        key: "areaName",
        label: i18n.t("baswareano.areaName")
    },
    // {
    //     key: "areaNameAlias",
    //     label: i18n.t("baswareano.areaNameAlias")
    // },
    // {
    //     key: "areaNameEn",
    //     label: i18n.t("baswareano.areaNameEn")
    // },
    {
        key: "areaNo",
        label: i18n.t("baswareano.areaNo")
    },
    {
        key: "areaType",
        label: i18n.t("baswareano.areaType")
    },
    {
        key: "usedFlag",
        label: i18n.t("baswareano.usedFlag")
    },
    {
        key: "whouseNo",
        label: i18n.t("baswareano.whouseNo")
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


