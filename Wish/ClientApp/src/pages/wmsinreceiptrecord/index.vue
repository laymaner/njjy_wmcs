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
    name: "wmsinreceiptrecord",
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
              //   "areaNo":{
              //       label: this.$t("wmsinreceiptrecord.areaNo"),
              //       rules: [],
              //       type: "input"
              // },
              //   "batchNo":{
              //       label: this.$t("wmsinreceiptrecord.batchNo"),
              //       rules: [],
              //       type: "input"
              // },
                "binNo":{
                    label: this.$t("wmsinreceiptrecord.binNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "departmentName":{
              //       label: this.$t("wmsinreceiptrecord.departmentName"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "docTypeCode":{
              //       label: this.$t("wmsinreceiptrecord.docTypeCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "erpWhouseNo":{
              //       label: this.$t("wmsinreceiptrecord.erpWhouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "externalInNo":{
              //       label: this.$t("wmsinreceiptrecord.externalInNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "inNo":{
                    label: this.$t("wmsinreceiptrecord.inNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "inOutTypeNo":{
              //       label: this.$t("wmsinreceiptrecord.inOutTypeNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "inspectionResult":{
              //       label: this.$t("wmsinreceiptrecord.inspectionResult"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "inspector":{
              //       label: this.$t("wmsinreceiptrecord.inspector"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "iqcResultNo":{
              //       label: this.$t("wmsinreceiptrecord.iqcResultNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "inRecordStatus":{
                    label: this.$t("wmsinreceiptrecord.inRecordStatus"),
                    rules: [],
                    type: "select",
                    children: this.getDicByStatusData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
              //   "loadedType":{
              //       label: this.$t("wmsinreceiptrecord.loadedType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "materialName":{
              //       label: this.$t("wmsinreceiptrecord.materialName"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "materialCode":{
                    label: this.$t("wmsinreceiptrecord.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsinreceiptrecord.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "ptaStockCode":{
              //       label: this.$t("wmsinreceiptrecord.ptaStockCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ptaStockDtlId":{
              //       label: this.$t("wmsinreceiptrecord.ptaStockDtlId"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "proprietorCode":{
              //       label: this.$t("wmsinreceiptrecord.proprietorCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ptaBinNo":{
              //       label: this.$t("wmsinreceiptrecord.ptaBinNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ptaPalletBarcode":{
              //       label: this.$t("wmsinreceiptrecord.ptaPalletBarcode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "receiptNo":{
              //       label: this.$t("wmsinreceiptrecord.receiptNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "recordQty":{
                    label: this.$t("wmsinreceiptrecord.recordQty"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "regionNo":{
              //       label: this.$t("wmsinreceiptrecord.regionNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "returnResult":{
              //       label: this.$t("wmsinreceiptrecord.returnResult"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "skuCode":{
              //       label: this.$t("wmsinreceiptrecord.skuCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "stockCode":{
              //       label: this.$t("wmsinreceiptrecord.stockCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "supplierCode":{
              //       label: this.$t("wmsinreceiptrecord.supplierCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "supplierName":{
              //       label: this.$t("wmsinreceiptrecord.supplierName"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ticketNo":{
              //       label: this.$t("wmsinreceiptrecord.ticketNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "urgentFlag":{
              //       label: this.$t("wmsinreceiptrecord.urgentFlag"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "unitCode":{
              //       label: this.$t("wmsinreceiptrecord.unitCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "whouseNo":{
              //       label: this.$t("wmsinreceiptrecord.whouseNo"),
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
