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
    name: "interfacesendback",
    mixins: [searchMixin(TABLE_HEADER), actionMixin(ASSEMBLIES)],
    store,
    components: {
        DialogForm
    }
})
export default class Index extends Vue {
    isActive: boolean = false;
    @Action
    getDicBySign;
    // @State
    getDicBySignData : any=[];

    get SEARCH_DATA() {
        return {
            formProps: {
                "label-width": "75px",
                inline: true
            },
            formItem: {
                "interfaceCode":{
                    label: this.$t("interfacesendback.interfaceCode"),
                    rules: [],
                    type: "input"
              },
                "interfaceName":{
                    label: this.$t("interfacesendback.interfaceName"),
                    rules: [],
                    type: "input"
              },
                "interfaceSendInfo":{
                    label: this.$t("interfacesendback.interfaceSendInfo"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "interfaceResult":{
                    label: this.$t("interfacesendback.interfaceResult"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },
                "returnFlag":{
                    label: this.$t("interfacesendback.returnFlag"),
                    rules: [],
                    type: "select",
                    children: this.getDicBySignData,
                    props: {
                        clearable: true,
                        placeholder: this.$t("form.all")
                    }
                    // ,isHidden: !this.isActive
              },
                "returnTimes":{
                    label: this.$t("interfacesendback.returnTimes"),
                    rules: [],
                    type: "input"
                    ,isHidden: !this.isActive
              },

            }
        };
    }

     mounted() {
        this.getDicBySign().then((data) => {
            
            if(data.status==="0"){
                this.getDicBySignData = data.data.items; // 将获取的数据存储到数组中
            }else{
                this.getDicBySignData =[];
            }
        });
    }
}
</script>
