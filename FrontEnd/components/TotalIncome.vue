<script setup>
        const transactionsData = inject('transactionsData')
        const totalIncome = computed(() => {
                return transactionsData.value.reduce((acc, transaction) => {
                        if (transaction.Tipo === 'D') {
                                return acc + transaction.Valor
                        } 
                        return acc
                }, 0)
        })

</script>

<template>
        <v-lazy
        transition="slide-y-transition"
        >
                <v-card class="px-4 py-3">
                        <div class="bg-bgIncomeGreen rounded-circle d-flex circle pa-2">
                                <v-icon
                                class=""
                                size="30"
                                color="incomeGreen"
                                >
                                mdi-currency-usd-off
                        </v-icon>
                        </div>
                        <v-card-title class="text-body-2 pl-0 pb-0 text-label">Total Income</v-card-title>
                        <v-card-text class="d-flex ga-4 pl-0 py-0">
                        <p class="font-weight-bold text-h6">R${{totalIncome}}</p>
                        <v-chip small color="incomeGreen">+129%</v-chip>
                        </v-card-text>
                </v-card>
        </v-lazy>
</template>