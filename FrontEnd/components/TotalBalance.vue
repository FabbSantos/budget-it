
<script setup>
        const transactionsData = inject('transactionsData')
        const saldo = computed(() => {
                return transactionsData.value.reduce((acc, transaction) => {
                        if (transaction.Tipo === 'D') {
                                return acc + transaction.Valor
                        } 
                        return acc - transaction.Valor
                }, 0)
        })
</script>

<template>
        <v-lazy
        transition="slide-y-transition"
        >
                <v-card class="d-flex align-center px-4 py-3">
                        <div class="bg-secondaryPink rounded-circle h-auto pa-4">
                        <v-icon
                        class=""
                        size="40"
                        color="primaryPink"
                        >
                        mdi-wallet-bifold
                        </v-icon>
                        </div>
                        <v-card-item class="mt-3">
                                <v-card-title class="text-body-1 text-label">Total Balance</v-card-title>
                                <v-card-text class="d-flex ga-4 ps-0">
                                <p class="font-weight-bold text-h6">R${{saldo}}</p>
                                <v-chip x-small color="success">+1.29%</v-chip>
                                </v-card-text>
                        </v-card-item>
                </v-card>
        </v-lazy>
</template>