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
             "Entity.areaName":{
                 label: this.$t("baswareano.areaName"),
                 rules: [{ required: true, message: this.$t("baswareano.areaName")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.areaNameAlias":{
            //      label: this.$t("baswareano.areaNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.areaNameEn":{
            //      label: this.$t("baswareano.areaNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.areaNo":{
                 label: this.$t("baswareano.areaNo"),
                 rules: [{ required: true, message: this.$t("baswareano.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.areaType":{
                 label: this.$t("baswareano.areaType"),
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: this.$t("baswareano.usedFlag"),
                 rules: [{ required: true, message: this.$t("baswareano.usedFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: this.$t("baswareano.whouseNo"),
                 rules: [],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
