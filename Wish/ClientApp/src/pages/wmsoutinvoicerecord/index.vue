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
    name: "wmsoutinvoicerecord",
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
              //       label: this.$t("wmsoutinvoicerecord.allocatResult"),
              //       rules: [],
              //       type: "input"
              // },
              //   "allotQty":{
              //       label: this.$t("wmsoutinvoicerecord.allotQty"),
              //       rules: [],
              //       type: "input"
              // },
              //   "allotType":{
              //       label: this.$t("wmsoutinvoicerecord.allotType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "areaNo":{
                    label: this.$t("wmsoutinvoicerecord.areaNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "assemblyIdx":{
              //       label: this.$t("wmsoutinvoicerecord.assemblyIdx"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "batchNo":{
                    label: this.$t("wmsoutinvoicerecord.batchNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
                "binNo":{
                    label: this.$t("wmsoutinvoicerecord.binNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "deliveryLocNo":{
                    label: this.$t("wmsoutinvoicerecord.deliveryLocNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              "outRecordStatus":{
                    label: this.$t("wmsoutinvoicerecord.outRecordStatus"),
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
              //       label: this.$t("wmsoutinvoicerecord.erpWhouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "externalOutNo":{
              //       label: this.$t("wmsoutinvoicerecord.externalOutNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "fpNo":{
              //       label: this.$t("wmsoutinvoicerecord.fpNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "invoiceNo":{
              //       label: this.$t("wmsoutinvoicerecord.invoiceNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "materialCode":{
                    label: this.$t("wmsoutinvoicerecord.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "materialSpec":{
              //       label: this.$t("wmsoutinvoicerecord.materialSpec"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "palletBarcode":{
                    label: this.$t("wmsoutinvoicerecord.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "pickTaskNo":{
                    label: this.$t("wmsoutinvoicerecord.pickTaskNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "productDeptCode":{
              //       label: this.$t("wmsoutinvoicerecord.productDeptCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "proprietorCode":{
              //       label: this.$t("wmsoutinvoicerecord.proprietorCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "regionNo":{
              //       label: this.$t("wmsoutinvoicerecord.regionNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "stockCode":{
                    label: this.$t("wmsoutinvoicerecord.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "supplierCode":{
              //       label: this.$t("wmsoutinvoicerecord.supplierCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ticketNo":{
              //       label: this.$t("wmsoutinvoicerecord.ticketNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "unitCode":{
              //       label: this.$t("wmsoutinvoicerecord.unitCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "whouseNo":{
              //       label: this.$t("wmsoutinvoicerecord.whouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "urgentFlag":{
              //       label: this.$t("wmsoutinvoicerecord.urgentFlag"),
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
