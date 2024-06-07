<script setup>
const transactionType = ref('');
const valor = ref()
const dialog = ref(false);

const valorRules = ref([
        (value) => (!/[^0-9]/.test(value) ? false : 'Valor can only contain digits.'),
]);

async function addTransaction() {
        if (valor.value && transactionType.value) {
                const transaction = {
                        valor: valor.value,
                        tipo: transactionType.value === 'Income' ? 'D' : 'R',
                };
                console.log(transaction);
                await $fetch(`https://webapplication120240607152445.azurewebsites.net//transacao?Valor=${transaction.valor}&Tipo=${transaction.tipo}`, {
                        method: 'POST',
                        body: JSON.stringify({
                                Valor: transaction.valor,
                                Tipo: transaction.tipo
                        }),
                        headers: {
                                'Content-Type': 'application/json'
                        }
                });
                dialog.value = false;
        }
}
</script>

<template>
        <v-dialog v-model="dialog">
                <template #activator="{ props }">

                        <v-btn v-bind:="props" prepend-icon="mdi-bank-transfer" size="small">
                                Add Transaction
                        </v-btn>
                </template>
                <v-form fast-fail:true @submit.prevent validate:true>
                        <v-card prepend-icon="mdi-bank-transfer" title="Add Transaction">
                                <v-card-text>
                                        <v-row dense>
                                                <v-col cols="12" md="4" sm="6">
                                                        <v-text-field label="Valor*" required :rules="valorRules"
                                                                v-model="valor"></v-text-field>
                                                </v-col>
                                                <v-col cols="12" md="4" sm="6">
                                                        <v-select label="Transaction Type"
                                                                :items="['Income', 'Expense']" required
                                                                v-model="transactionType"></v-select>
                                                </v-col>
                                        </v-row>
                                </v-card-text>

                                <v-card-actions>
                                        <v-btn text="Close" variant="plain" @click="dialog = false"></v-btn>
                                        <v-btn color="primary" text="Add" variant="tonal"
                                                @click="addTransaction"></v-btn>
                                </v-card-actions>
                        </v-card>
                </v-form>

        </v-dialog>
</template>

<style scoped>
        .v-form {
                min-width: 60%;
                margin-inline: auto;
        }
        .v-row--dense {
                justify-content: center;
        }
</style>