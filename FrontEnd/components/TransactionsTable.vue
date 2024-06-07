<template>
        <v-lazy>
                <v-data-table :v-model="headers" :items="formattedItems" striped single-select
                        aria-label="Simple table">
                        <template #item="{ item }">
                                <tr
                                        :class="[{ 'income': item.Tipo === 'Income' }, { 'expense': item.Tipo === 'Expense' }]">
                                        <td v-for="(value) in item" :key="item.Id_transacao">
                                                <span>{{ value }}</span>
                                        </td>
                                        <!-- Coluna para ações de edição -->
                                        <td>
                                                <v-dialog theme="light">
                                                        <template #activator="{ props }">
                                                                <v-btn v-bind="props" icon size="small" small
                                                                        color="primary">
                                                                        <v-icon>mdi-pencil</v-icon>
                                                                </v-btn>
                                                        </template>
                                                        <v-card>
                                                                <v-form fast-fail:true @submit.prevent validate:true>
                                                                        <v-card prepend-icon="mdi-pencil"
                                                                                title="Edit Transaction">
                                                                                <v-card-text>
                                                                                        <v-row dense>
                                                                                                <v-col cols="12" md="4"
                                                                                                        sm="6">
                                                                                                        <v-text-field
                                                                                                                label="Valor*"
                                                                                                                required
                                                                                                                :rules="valorRules"
                                                                                                                v-model="valorE"></v-text-field>
                                                                                                </v-col>
                                                                                                <v-col cols="12" md="4"
                                                                                                        sm="6">
                                                                                                        <v-select
                                                                                                                label="Transaction Type"
                                                                                                                :items="['Income', 'Expense']"
                                                                                                                required
                                                                                                                v-model="transactionTypeE"></v-select>
                                                                                                </v-col>
                                                                                                <v-col cols="12" md="4"
                                                                                                        sm="6">
                                                                                                        <v-text-field
                                                                                                                label="Transaction Data"
                                                                                                                required
                                                                                                                :rules="dataRules"
                                                                                                                v-model="transactionDataE">
                                                                                                        </v-text-field>
                                                                                                </v-col>
                                                                                        </v-row>
                                                                                </v-card-text>
                                                                                <v-card-actions>
                                                                                        <v-btn color="primary"
                                                                                                variant="tonal"
                                                                                                @click="editTransaction(item)">
                                                                                                {{ editted == false ?
                                                                                                'Confirm Edit' : 'Done'
                                                                                                }}</v-btn>
                                                                                </v-card-actions>
                                                                        </v-card>
                                                                </v-form>
                                                        </v-card>
                                                </v-dialog>
                                        </td>
                                        <td>
                                                <v-dialog theme="light">
                                                        <template #activator="{ props }">
                                                                <v-btn v-bind="props" icon size="small" small
                                                                        color="error">
                                                                        <v-icon>mdi-delete</v-icon>
                                                                </v-btn>
                                                        </template>
                                                        <v-card class="d-flex justify-center align-center mx-auto py-8"
                                                                min-width="344">
                                                                <v-card-text class="text-center">
                                                                        You're about to <span
                                                                                class="text-error font-weight-bold">DELETE</span>
                                                                        a transaction. <br />You sure?
                                                                </v-card-text>
                                                                <v-card-actions>
                                                                        <v-btn variant="outlined" color="error"
                                                                                @click="deleteTransaction(item.Id_transacao)">
                                                                                {{ deleted === false ? 'Yes, I am' :
                                                                                'deleted'}}</v-btn>
                                                                </v-card-actions>
                                                        </v-card>
                                                </v-dialog>
                                        </td>
                                </tr>
                        </template>
                </v-data-table>
        </v-lazy>
</template>

<script setup>
import { inject, ref, computed } from 'vue';

// Definição dos headers
const headers = ref([
        { text: 'ID', value: 'Id_transacao' },
        { text: 'Valor', value: 'Valor' },
        { text: 'Tipo', value: 'Tipo' },
        { text: 'Data', value: 'Data' },
]);
const deleted =ref(false)
const editted =ref(false)
const transactionTypeE = ref('');
const valorE = ref();
const transactionDataE = ref();
// Injeção dos dados
const transactionsData = inject('transactionsData');

// Função para formatar os itens
function formatItem(item) {
        // Converte o Tipo para Income ou Expense
        const tipoConvertido = item.Tipo === 'D' ? 'Income' : 'Expense';
        // Adiciona o símbolo de moeda ao campo Valor
        const valorFormatado = `R$ ${item.Valor.toFixed(2).replace('.', ',')}`;
        // Formata o campo Data para dd/mm/yyyy
        const dataFormatada = new Date(item.Data).toLocaleDateString('pt-BR');
        // Retorna o item formatado
        return {
                ...item,
                Valor: valorFormatado,
                Tipo: tipoConvertido,
                Data: dataFormatada,
        };
}


const valorRules = ref([
        (value) => (!/[^0-9]/.test(value) ? false : 'Valor can only contain digits.'),
]);
const dataRules = ref([
        (value) => {
                const regex = /^\d{4}-\d{2}-\d{2}$/;
                if (regex.test(value)) {
                        return true;
                } else {
                        return 'Data must be in the format yyyy-mm-dd.';
                }
        }
]);


async function editTransaction(item) {
        deleted.value = false
        transactionTypeE.value = item.Tipo === 'Income' ? 'D' : 'R';
        try {
                const response = await $fetch(`https://webapplication120240607152445.azurewebsites.net/transacao?Id_transacao=${item.Id_transacao}&Valor=${valorE.value}&Tipo=${transactionTypeE.value}&Data=${transactionDataE.value}`, {
                        method: 'PUT',
                        headers: {
                                'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                                Valor: valorE.value,
                                Tipo: transactionTypeE.value === 'Income' ? 'D' : 'R',
                                Data: transactionDataE.value,
                        }),
                });

                if (!response.ok) {
                        throw new Error(`HTTP error status: ${response.status}`);
                }

                const data = await response.json();
                // console.log(data);
                // Atualize o estado ou faça algo com os dados retornados
        } catch (error) {
                console.error('Erro ao editar a transação:', error);
        } finally {
                editted.value = true
        }
}
async function deleteTransaction(id) {
        deleted.value = false

        try {
                await $fetch(`https://webapplication120240607152445.azurewebsites.net/transacao/${id}`, {
                        method: 'DELETE',
                        headers: {
                                'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                                Id_transacao: id,
                        }),
                });
        
        }catch (error){
                console.error('Erro ao deletar a transação:', error);
        }
        finally{
                deleted.value=true
        }
}


// Aplica a formatação aos dados
const formattedItems = computed(() => transactionsData.value.map(formatItem));
</script>

<style>
/* Estilos aqui */
.income {
        background-color: #d4edda;
}
.expense {
        background-color: #f8d7da;
}
.v-dialog__content {
        background-color: transparent !important;
}
.v-overlay__scrim {
        background: rgba(255, 255, 255, .1)!important;
        /* Ajuste a opacidade conforme necessário */
}
</style>