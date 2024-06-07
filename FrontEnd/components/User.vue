<script setup>
const users = await useFetch('https://webapplication120240607152445.azurewebsites.net//usuario')
const user = users.data
console.log(user.value);
</script>

<template>
  {{ user[0].Nome }}
</template>