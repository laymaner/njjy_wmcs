<template>
    <card>
        <wtm-search-box :ref="searchRefName" :events="searchEvent" :formOptions="SEARCH_DATA" :needCollapse="true" :isActive.sync="isActive" />
        <!-- 操作按钮 -->
        <wtm-but-box :assembly="assembly" :action-list="actionList" :selected-data="selectData" :events="actionEvent" />
        <!-- 列表 -->
        <wtm-table-box :attrs="{...searchAttrs, actionList}" :events="{...searchEvent, ...actionEvent}">

        </wtm-table-box>
        <!-- 弹出框 -->
        <dialog-form :is-show.sync="dialogIsShow" :dialog-data="dialogData" :status="dialogStatus" @onSearch="onHoldSearch" />
        <!-- 导入 -->
        <upload-box :is-show.sync="uploadIsShow" @onImport="onImport" @onDownload="onDownload" />
    </card>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import searchMixin from "@/vue-custom/mixin/search";
import actionMixin from "@/vue-custom/mixin/action-mixin";
import DialogForm from "./views/dialog-form.vue";
import store from "./store/index";
// 查询参数, table列 ★★★★★
import { ASSEMBLIES, TABLE_HEADER,  } from "./config";
import i18n from "@/lang";

@Component({
    name: "wmsoutinvoicerecordhishis",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;

    @Action
    getDicByStatus;
    // @State
    getDicByStatusData : any=[];

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
              //   "allocatResult":{
              //       label: this.$t("wmsoutinvoicerecordhis.allocatResult"),
              //       rules: [],
              //       type: "input"
              // },
              //   "allotQty":{
              //       label: this.$t("wmsoutinvoicerecordhis.allotQty"),
              //       rules: [],
              //       type: "input"
              // },
              //   "allotType":{
              //       label: this.$t("wmsoutinvoicerecordhis.allotType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "areaNo":{
                    label: this.$t("wmsoutinvoicerecordhis.areaNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "assemblyIdx":{
              //       label: this.$t("wmsoutinvoicerecordhis.assemblyIdx"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "batchNo":{
                    label: this.$t("wmsoutinvoicerecordhis.batchNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "binNo":{
                    label: this.$t("wmsoutinvoicerecordhis.binNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
                "deliveryLocNo":{
                    label: this.$t("wmsoutinvoicerecordhis.deliveryLocNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              "outRecordStatus":{
                    label: this.$t("wmsoutinvoicerecordhis.outRecordStatus"),
                    rules: [],
                    type: "select",
                    children: this.getDicByStatusData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
              //   "erpWhouseNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.erpWhouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "externalOutNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.externalOutNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "fpNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.fpNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "invoiceNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.invoiceNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "materialCode":{
                    label: this.$t("wmsoutinvoicerecordhis.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "materialSpec":{
              //       label: this.$t("wmsoutinvoicerecordhis.materialSpec"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "palletBarcode":{
                    label: this.$t("wmsoutinvoicerecordhis.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "pickTaskNo":{
                    label: this.$t("wmsoutinvoicerecordhis.pickTaskNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "productDeptCode":{
              //       label: this.$t("wmsoutinvoicerecordhis.productDeptCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "proprietorCode":{
              //       label: this.$t("wmsoutinvoicerecordhis.proprietorCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "regionNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.regionNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "stockCode":{
                    label: this.$t("wmsoutinvoicerecordhis.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "supplierCode":{
              //       label: this.$t("wmsoutinvoicerecordhis.supplierCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ticketNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.ticketNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "unitCode":{
              //       label: this.$t("wmsoutinvoicerecordhis.unitCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "whouseNo":{
              //       label: this.$t("wmsoutinvoicerecordhis.whouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "urgentFlag":{
              //       label: this.$t("wmsoutinvoicerecordhis.urgentFlag"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },

            }
        };
    }

     mounted() {
      this.getDicByStatus().then((data) => {
          if(data.status==="0"){
            this.getDicByStatusData = data.data.items; // 将获取的数据存储到数组中
          }else{
            this.getDicByStatusData =[];
          }
      });
    }
}
</script>
