<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <wtm-create-form :ref="refName" :status="status" :options="formOptions" ></wtm-create-form>
    </wtm-dialog-box>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import formMixin from "@/vue-custom/mixin/form-mixin";
import UploadImg from "@/components/page/UploadImg.vue";


@Component({
    mixins: [formMixin()]
})
export default class Index extends Vue {

    // 表单结构
    get formOptions() {
        const filterMethod = (query, item) => {
            return item.label.indexOf(query) > -1;
        };
        return {
            formProps: {
                "label-width": "100px"
            },
            formItem: {
                "Entity.ID": {
                    isHidden: true
                },
             "Entity.strategyItemDesc":{
                 label: "strategyItemDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemGroupIdx":{
                 label: "strategyItemGroupIdx",
                 rules: [{ required: true, message: "strategyItemGroupIdx"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.strategyItemGroupNo":{
                 label: "strategyItemGroupNo",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemName":{
                 label: "strategyItemName",
                 rules: [{ required: true, message: "strategyItemName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.strategyItemNameAlias":{
                 label: "strategyItemNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemNameEn":{
                 label: "strategyItemNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.strategyItemNo":{
                 label: "strategyItemNo",
                 rules: [{ required: true, message: "strategyItemNo"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.strategyTypeCode":{
                 label: "strategyTypeCode",
                 rules: [{ required: true, message: "strategyTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
