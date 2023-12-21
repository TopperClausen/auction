<script setup>
import { ref } from 'vue';
import { useCarStore } from '../stores/car';

const props = defineProps([
    'required'
]);

let value = ref(-1);

const carStore = useCarStore();
const emit = defineEmits();

if(carStore.brands.length === 0) {
    carStore.populate();
}

const onChange = (e) => {
   e.preventDefault();
   emit('change', value)
}
</script>


<template>
    <div>
        <label class="block text-gray-700 text-sm font-bold mb-2"> Brand </label> 
        <select 
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            v-model="value"
            @change.stop="onChange"
        >
            <option v-for="brand in carStore.brands" :value="brand.id">{{ brand.name }}</option>
        </select>
        <span class="text-red-700 font-light text-sm" v-if="props.required && value === -1"> Brand skal vælges </span>
    </div>
</template>