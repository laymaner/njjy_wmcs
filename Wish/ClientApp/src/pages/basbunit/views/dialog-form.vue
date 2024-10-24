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
             "Entity.unitCode":{
                 label: "unitCode",
                 rules: [{ required: true, message: "unitCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unitName":{
                 label: "unitName",
                 rules: [{ required: true, message: "unitName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.unitNameAlias":{
                 label: "unitNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.unitNameEn":{
                 label: "unitNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.unitType":{
                 label: "unitType",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
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
