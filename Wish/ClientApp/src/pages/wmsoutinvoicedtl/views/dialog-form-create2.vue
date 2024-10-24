<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <div>
        <h2>发货单数据</h2>
        <el-form ref="mainForm" :model="mainFormData">
          <el-form-item label="单号">
            <el-input v-model="mainFormData.inNo" />
          </el-form-item>
          <el-form-item label="供应商名称">
            <el-input v-model="mainFormData.supplierName" />
          </el-form-item>
          <el-form-item label="地址">
            <el-input v-model="mainFormData.address" />
          </el-form-item>
          <el-form-item label="电话">
            <el-input v-model="mainFormData.phone" />
          </el-form-item>
        </el-form>
      </div>
      <!-- 发货单明细数据 -->
      <div>
        <h2>发货单明细数据</h2>
        <el-table :data="detailData" style="width: 100%">
          <el-table-column label="物料名称">
            <template slot-scope="scope">
              <el-input v-model="scope.row.materialName" @blur="checkDetailInput(scope.$index)" />
            </template>
          </el-table-column>
          <el-table-column label="物料编码">
            <template slot-scope="scope">
              <el-input v-model="scope.row.materialCode" @blur="checkDetailInput(scope.$index)" />
            </template>
          </el-table-column>
          <el-table-column label="批次号">
            <template slot-scope="scope">
              <el-input v-model="scope.row.batchNo" @blur="checkDetailInput(scope.$index)" />
            </template>
          </el-table-column>
          <el-table-column label="唯一码">
            <template slot-scope="scope">
              <el-input v-model="scope.row.uniqueCode" @blur="checkDetailInput(scope.$index)" />
            </template>
          </el-table-column>
        </el-table>
        <el-button @click="addDetailRow">添加明细</el-button>
      </div>

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
             "Entity.areaNo":{
                 label: this.$t("wmsoutinvoicedtl.areaNo"),
                 rules: [{ required: true, message: this.$t("wmsoutinvoicedtl.areaNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }

    
    data() {
    return {
      mainFormData: {
        inNo: "",
        supplierName: "",
        address: "",
        phone: ""
      },
      detailData: [
        {
          materialName: "",
          materialCode: "",
          batchNo: "",
          uniqueCode: ""
        }
      ]
    };
  }

    addDetailRow() {
        this.detailData.push({
            materialName: "",
            materialCode: "",
            batchNo: "",
            uniqueCode: ""
        });
    }

    checkDetailInput(index) {
        const detail = this.detailData[index];
        if (detail.materialName.trim()!== "" || detail.materialCode.trim()!== "" || detail.batchNo.trim()!== "" || detail.uniqueCode.trim()!== "") {
            if (index + 1 === this.detailData.length) {
                this.addDetailRow();
            }
        }
    }

    submitData() {
        // 在此处处理提交数据的逻辑
        console.log("主单据数据：", this.mainFormData);
        console.log("明细数据：", this.detailData);
    }

    beforeOpen() {

    }
}
</script>
