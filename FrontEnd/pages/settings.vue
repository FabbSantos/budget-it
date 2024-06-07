<template>

        <v-container class="pa-10">
                <h2>Edit your information:</h2>
                <v-form ref="form" :model-value="valid" @submit.prevent="editName">
                        <v-text-field class="py-8" color="expenseRed" v-model="nameForChange" label="your name" :rules="nameRules"
                                required></v-text-field>
                        <v-btn :disabled="!valid" color="success" type="submit">
                                {{ editted === false ? 'Edit name' : 'Name changed!'}}
                        </v-btn>
                </v-form>
        </v-container>
</template>

<script setup>
import { ref } from 'vue';

const editted = ref(false);

const valid = ref(true);
const nameRules = [
        v => !!v || 'O nome é obrigatório.',
        v => (v && v.length <= 20) || 'O nome deve ter menos de 10 caracteres'
];
const nameForChange = ref();

async function editName() {
        try {
                const response = await fetch(`http://localhost:5231/usuario?Nome=${nameForChange.value}`, {
                        method: 'PUT',
                        headers: {
                                'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                                name: nameForChange.value
                        })
                });

                if (!response.ok) {
                        throw new Error('Erro ao atualizar o nome do usuário');
                }
                editted.value = true;

        } catch (error) {
                console.error(error);
        }
}
</script>

<style scoped>

        label, .v-label {
                position: absolute;
                margin-top: 10px;
        }
        form {
                display: flex;
                flex-direction: column;
                align-items: center;
                max-width: 70vw;
                margin: 0 auto;
        }
        input, .v-input {
                position: relative;
                width: 100%;
                padding: 30px
        }
</style>