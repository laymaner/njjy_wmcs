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
import { ASSEMBLIES, TABLE_HEADER,IsOrderTypes,  } from "./config";
import i18n from "@/lang";

@Component({
    name: "wmsstockuniicode",
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
                "uniicode":{
                    label: this.$t("wmsstockuniicode.uniicode"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
                "areaNo":{
                    label: this.$t("wmsstockuniicode.areaNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              "projectNo":{
                    label: this.$t("wmsstockuniicode.projectNo"),
                    rules: [],
                    type: "input"
                    
              },
                "batchNo":{
                    label: this.$t("wmsstockuniicode.batchNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
            //     "delayFrozenFlag":{
            //         label: this.$t("wmsstockuniicode.delayFrozenFlag"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
            //     "erpWhouseNo":{
            //         label: this.$t("wmsstockuniicode.erpWhouseNo"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
            //     "exposeFrozenFlag":{
            //         label: this.$t("wmsstockuniicode.exposeFrozenFlag"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
                "materialCode":{
                    label: this.$t("wmsstockuniicode.materialCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
            //     "materialSpec":{
            //         label: this.$t("wmsstockuniicode.materialSpec"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
                "palletBarcode":{
                    label: this.$t("wmsstockuniicode.palletBarcode"),
                    rules: [],
                    type: "input"
                    // ,isHidden: !this.isActive
              },
                
            //     "skuCode":{
            //         label: this.$t("wmsstockuniicode.skuCode"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
            "binNo":{
                    label: this.$t("wmsstockuniicode.binNo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "stockCode":{
                    label: this.$t("wmsstockuniicode.stockCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
              "stockStatus":{
                    label: this.$t("wmsstockuniicode.stockStatus"),
                    rules: [],
                    type: "select",
                    children: this.getDicByStatusData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
              "unpackStatus":{
                    label: this.$t("wmsstockuniicode.unpackStatus"),
                    rules: [],
                    type: "select",
                    children: IsOrderTypes,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    ,isHidden: !this.isActive
              },
                "supplierCode":{
                    label: this.$t("wmsstockuniicode.supplierCode"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                
            //     "unitCode":{
            //         label: this.$t("wmsstockuniicode.unitCode"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },
            //     "whouseNo":{
            //         label: this.$t("wmsstockuniicode.whouseNo"),
            //         rules: [],
            //         type: "input"
            //         ,isHidden: !this.isActive
            //   },

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
