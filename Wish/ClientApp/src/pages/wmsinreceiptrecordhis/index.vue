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
    name: "wmsinreceiptrecordhishis",
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
              //       label: this.$t("wmsinreceiptrecordhis.areaNo"),
              //       rules: [],
              //       type: "input"
              // },
              //   "batchNo":{
              //       label: this.$t("wmsinreceiptrecordhis.batchNo"),
              //       rules: [],
              //       type: "input"
              // },
                "binNo":{
                    label: this.$t("wmsinreceiptrecordhis.binNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "departmentName":{
              //       label: this.$t("wmsinreceiptrecordhis.departmentName"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "docTypeCode":{
              //       label: this.$t("wmsinreceiptrecordhis.docTypeCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "erpWhouseNo":{
              //       label: this.$t("wmsinreceiptrecordhis.erpWhouseNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "externalInNo":{
              //       label: this.$t("wmsinreceiptrecordhis.externalInNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "inNo":{
                    label: this.$t("wmsinreceiptrecordhis.inNo"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
              //   "inOutTypeNo":{
              //       label: this.$t("wmsinreceiptrecordhis.inOutTypeNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "inspectionResult":{
              //       label: this.$t("wmsinreceiptrecordhis.inspectionResult"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "inspector":{
              //       label: this.$t("wmsinreceiptrecordhis.inspector"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "iqcResultNo":{
              //       label: this.$t("wmsinreceiptrecordhis.iqcResultNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "inRecordStatus":{
                    label: this.$t("wmsinreceiptrecordhis.inRecordStatus"),
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
              //       label: this.$t("wmsinreceiptrecordhis.loadedType"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "materialName":{
              //       label: this.$t("wmsinreceiptrecordhis.materialName"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "materialCode":{
                    label: this.$t("wmsinreceiptrecordhis.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "palletBarcode":{
                    label: this.$t("wmsinreceiptrecordhis.palletBarcode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "ptaStockCode":{
              //       label: this.$t("wmsinreceiptrecordhis.ptaStockCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ptaStockDtlId":{
              //       label: this.$t("wmsinreceiptrecordhis.ptaStockDtlId"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "proprietorCode":{
              //       label: this.$t("wmsinreceiptrecordhis.proprietorCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ptaBinNo":{
              //       label: this.$t("wmsinreceiptrecordhis.ptaBinNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ptaPalletBarcode":{
              //       label: this.$t("wmsinreceiptrecordhis.ptaPalletBarcode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "receiptNo":{
              //       label: this.$t("wmsinreceiptrecordhis.receiptNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
                "recordQty":{
                    label: this.$t("wmsinreceiptrecordhis.recordQty"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              //   "regionNo":{
              //       label: this.$t("wmsinreceiptrecordhis.regionNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "returnResult":{
              //       label: this.$t("wmsinreceiptrecordhis.returnResult"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "skuCode":{
              //       label: this.$t("wmsinreceiptrecordhis.skuCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "stockCode":{
              //       label: this.$t("wmsinreceiptrecordhis.stockCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "supplierCode":{
              //       label: this.$t("wmsinreceiptrecordhis.supplierCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "supplierName":{
              //       label: this.$t("wmsinreceiptrecordhis.supplierName"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "ticketNo":{
              //       label: this.$t("wmsinreceiptrecordhis.ticketNo"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "urgentFlag":{
              //       label: this.$t("wmsinreceiptrecordhis.urgentFlag"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "unitCode":{
              //       label: this.$t("wmsinreceiptrecordhis.unitCode"),
              //       rules: [],
              //       type: "input"
              //       ,isHidden: !this.isActive
              // },
              //   "whouseNo":{
              //       label: this.$t("wmsinreceiptrecordhis.whouseNo"),
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
