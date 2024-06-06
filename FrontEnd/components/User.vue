<script setup>
  const users = await useFetch('http://localhost:5231/usuario')

  const user = users.data.value[0];
</script>

<template>
    {{ user.Nome }}
</template>