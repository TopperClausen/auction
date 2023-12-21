<script setup>
import { ref } from 'vue';


const emit = defineEmits();

const props = defineProps([
    'placeholder',
    'type',
    'label',
    'model',
    'errors'
])

const value = ref('');

const onChange = (e) => {
    emit('input', value.value || '');
} 

</script>

<template>
    <div>
        <label class="block text-gray-700 text-sm font-bold mb-2"> {{ props.label }} </label>
        <input class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" :type="props.type" :placeholder="props.placeholder" v-model="value" @input.stop="onChange">
        <span class="text-red-700 font-light text-sm" v-for="error in props.errors"> {{ error }} </span>
    </div>
</template>