<script setup>
const emit = defineEmits();

const onClick = (e) => emit('click', e);

</script>

<template>
    <button class="bg-blue-600 hover:bg-blue-700 text-white rounded py-1 px-2" @click.stop="onClick"> <slot></slot> </button>
</template>