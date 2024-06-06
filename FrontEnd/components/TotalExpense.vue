<script setup>
        const transactionsData = inject('transactionsData')
        const totalExpense = computed(() => {
                return transactionsData.value.reduce((acc, transaction) => {
                        if (transaction.Tipo === 'R') {
                                return acc - transaction.Valor
                        } 
                        return acc*-1
                }, 0)
        })
</script>

<template>
        <v-lazy
        transition="slide-y-transition"
        >
                <v-card class="px-4 py-3">
                        <div class="bg-bgExpenseRed rounded-circle d-flex circle pa-2">
                                <v-icon
                                class=""
                                size="30"
                                color="expenseRed"
                                >
                                mdi-currency-usd
                        </v-icon>
                        </div>
                        <v-card-title class="text-body-2 pl-0 pb-0 text-label">Total Expense</v-card-title>
                        <v-card-text class="d-flex ga-4 pl-0 py-0">
                        <p class="font-weight-bold text-h6">- R${{totalExpense}}</p>
                        <v-chip small color="expenseRed">-29%</v-chip>
                        </v-card-text>
                </v-card>
        </v-lazy>
</template>