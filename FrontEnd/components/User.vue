<script setup>
const users = await useFetch('http://localhost:5231/usuario')
const user = users.data
console.log(user.value);
</script>

<template>
  {{ user[0].Nome }}
</template>