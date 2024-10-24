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
        label: i18n.t("baswregion.areaNo")
    },
    {
        key: "manualFlag",
        label: i18n.t("baswregion.manualFlag")
    },
    {
        key: "palletMgt",
        label: i18n.t("baswregion.palletMgt")
    },
    {
        key: "pickupMethod",
        label: i18n.t("baswregion.pickupMethod")
    },
    {
        key: "regionName",
        label: i18n.t("baswregion.regionName")
    },
    // {
    //     key: "regionNameAlias",
    //     label: i18n.t("baswregion.regionNameAlias")
    // },
    // {
    //     key: "regionNameEn",
    //     label: i18n.t("baswregion.regionNameEn")
    // },
    {
        key: "regionNo",
        label: i18n.t("baswregion.regionNo")
    },
    {
        key: "regionTypeCode",
        label: i18n.t("baswregion.regionTypeCode")
    },
    {
        key: "sdType",
        label: i18n.t("baswregion.sdType")
    },
    {
        key: "usedFlag",
        label: i18n.t("baswregion.usedFlag")
    },
    {
        key: "virtualFlag",
        label: i18n.t("baswregion.virtualFlag")
    },
    {
        key: "whouseNo",
        label: i18n.t("baswregion.whouseNo")
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


